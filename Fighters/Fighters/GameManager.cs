using Fighters.Extensions;
using Fighters.Models.Fighters;
using Fighters.Utils;
using Fighters.Validator;

namespace Fighters;

public static class GameManager
{
    public static IFighter PlayGame( ICustomValidator validator )
    {
        List<IFighter> fighters = InitFighters( validator );

        int roundCounter = 1;

        while ( fighters.Count > 1 )
        {
            PlayRound( fighters, roundCounter++ );
        }

        return fighters[ 0 ];
    }

    private static List<IFighter> InitFighters( ICustomValidator validator )
    {
        List<IFighter> fighters = AskUserCreateFighters( validator );

        return fighters;
    }

    private static List<IFighter> AskUserCreateFighters( ICustomValidator validator )
    {
        Console.Write( Messages.AskUserInputNumberOfFighters );
        int numberOfFighters;

        while ( ( numberOfFighters = validator.GetPositiveIntegerInput( Messages.InvalidNumberOfFighters ) ) < 2 )
        {
            Console.Write( Messages.InvalidNumberOfFighters );
        }

        List<IFighter> fighters = [];

        for ( int i = 1; i <= numberOfFighters; i++ )
        {
            IFighter newFighter = FighterFactory.CreateFighter( validator );

            fighters.Add( newFighter );

            Messages.PrintSuccessfullFighterCreation( i, newFighter );
        }

        return fighters;
    }

    private static void PlayRound( List<IFighter> fighters, int roundNumber )
    {
        Random random = new Random();

        int firstFighterIndex = random.Next( fighters.Count );
        int secondFighterIndex;

        do
        {
            secondFighterIndex = random.Next( fighters.Count );
        } while ( secondFighterIndex == firstFighterIndex );

        IFighter firstFighter = fighters[ firstFighterIndex ];
        IFighter secondFighter = fighters[ secondFighterIndex ];

        ProcessBattleBetweenFighters(
            fighters,
            firstFighter,
            secondFighter,
            roundNumber );

    }

    private static void ProcessBattleBetweenFighters(
        List<IFighter> fighters,
        IFighter firstFighter,
        IFighter secondFighter,
        int roundNumber )
    {
        Thread.Sleep( 1000 );
        Messages.PrintFightersNameAndRoundNumber( roundNumber, firstFighter, secondFighter );

        bool secondFighterDied = AttackAndCheckDefenderDeath( fighters, firstFighter, secondFighter );

        if ( secondFighterDied )
        {
            Messages.PrintMessageAboutFightersDeath( secondFighter );
            fighters.Remove( secondFighter );
            return;
        }

        bool firstFighterDied = AttackAndCheckDefenderDeath( fighters, secondFighter, firstFighter );

        if ( firstFighterDied )
        {
            Messages.PrintMessageAboutFightersDeath( firstFighter );
            fighters.Remove( firstFighter );
        }
    }

    private static bool AttackAndCheckDefenderDeath(
        List<IFighter> fighters,
        IFighter attacker,
        IFighter defender )
    {
        int damage = attacker.CalculateDamage();
        defender.TakeDamage( damage );

        Thread.Sleep( 1000 );
        Messages.PrintInfoAboutFighterAfterRound( defender, damage );

        if ( !defender.IsAlive() )
        {
            return true;
        }

        return false;
    }
}
