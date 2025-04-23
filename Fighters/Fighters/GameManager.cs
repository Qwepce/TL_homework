using Fighters.Extensions;
using Fighters.Factory;
using Fighters.Models.Fighters;
using Fighters.Utils;
using Fighters.Validator;

namespace Fighters;

public class GameManager
{
    private readonly IFighterFactory _fighterFactory;
    private readonly ICustomValidator _validator;

    private List<IFighter> _fighters = [];
    private int _roundNumber = 1;

    public GameManager( IFighterFactory fighterFactory, ICustomValidator validator )
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
        int numberOfFighters = _validator.GetPositiveIntegerInput( 2 );

        for ( int i = 1; i <= numberOfFighters; i++ )
        {
            IFighter newFighter = _fighterFactory.CreateFighter();

            _fighters.Add( newFighter );

            Messages.PrintSuccessfullFighterCreation( i, newFighter );
        }
    }

    private void PlayRound()
    {
        List<int> randomFightersIndices = Enumerable.Range( 0, _fighters.Count )
            .OrderBy( x => Random.Shared.Next() )
            .Take( 2 )
            .ToList();

        int firstFighterIndex = randomFightersIndices[ 0 ];
        int secondFighterIndex = randomFightersIndices[ 1 ];

        IFighter firstFighter = _fighters[ firstFighterIndex ];
        IFighter secondFighter = _fighters[ secondFighterIndex ];

        ProcessBattleBetweenFighters( firstFighter, secondFighter );

        _roundNumber++;
    }

    private void ProcessBattleBetweenFighters(
        IFighter firstFighter,
        IFighter secondFighter )
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

    private void ProcessAttack( IFighter attacker, IFighter defender )
    {
        int damage = attacker.CalculateDamage();
        defender.TakeDamage( damage );

        Messages.PrintInfoAboutFighterAfterRound( defender, damage );
    }
}
