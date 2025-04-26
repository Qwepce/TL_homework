using Fighters.ConsoleReader;
using Fighters.Extensions;
using Fighters.Factory;
using Fighters.Models.Fighters;
using Fighters.Utils;

namespace Fighters;

public class GameManager : IGameManager
{
    private readonly IFighterFactory _fighterFactory;
    private readonly IConsoleInputReader _validator;

    private List<IFighter> _fighters = [];
    private int _roundNumber = 1;
    private const int FightersCountInBattleRound = 2;

    public GameManager( IFighterFactory fighterFactory, IConsoleInputReader validator )
    {
        _fighterFactory = fighterFactory;
        _validator = validator;
    }

    public IFighter PlayGame()
    {
        InitFighters();

        while ( _fighters.Count > 1 )
        {
            PlayRound();
        }

        return _fighters[ 0 ];
    }

    private void InitFighters()
    {
        Console.Write( Messages.AskUserInputNumberOfFighters );
        int numberOfFighters = _validator.GetValidPositiveIntegerInput( lowerLimit: 2 );

        for ( int i = 1; i <= numberOfFighters; i++ )
        {
            IFighter newFighter = _fighterFactory.CreateFighter();

            _fighters.Add( newFighter );

            Messages.PrintSuccessfullFighterCreation( i, newFighter );
        }
    }

    private void PlayRound()
    {
        List<int> randomFightersIndexes = Enumerable.Range( 0, _fighters.Count )
            .OrderBy( x => Random.Shared.Next() )
            .Take( FightersCountInBattleRound )
            .ToList();

        int firstFighterIndex = randomFightersIndexes[ 0 ];
        int secondFighterIndex = randomFightersIndexes[ 1 ];

        IFighter firstFighter = _fighters[ firstFighterIndex ];
        IFighter secondFighter = _fighters[ secondFighterIndex ];

        ProcessBattleBetweenFighters( firstFighter, secondFighter );

        _roundNumber++;
    }

    private void ProcessBattleBetweenFighters( IFighter firstFighter, IFighter secondFighter )
    {
        Messages.PrintFightersNameAndRoundNumber( _roundNumber, firstFighter, secondFighter );

        ProcessAttack( attacker: firstFighter, defender: secondFighter );

        if ( !secondFighter.IsAlive() )
        {
            Messages.PrintMessageAboutFightersDeath( secondFighter );
            _fighters.Remove( secondFighter );
            return;
        }

        ProcessAttack( attacker: secondFighter, defender: firstFighter );

        if ( !firstFighter.IsAlive() )
        {
            Messages.PrintMessageAboutFightersDeath( firstFighter );
            _fighters.Remove( firstFighter );
        }
    }

    private static void ProcessAttack( IFighter attacker, IFighter defender )
    {
        int damage = attacker.CalculateDamage();
        defender.TakeDamage( damage );

        Messages.PrintInfoAboutFighterAfterRound( defender, damage );
    }
}
