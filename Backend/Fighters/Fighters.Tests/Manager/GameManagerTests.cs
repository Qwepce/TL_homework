using Fighters.ConsoleReader;
using Fighters.Extensions;
using Fighters.Factory;
using Fighters.Models.Fighters;
using Moq;

namespace Fighters.Tests.Manager;

public class GameManagerTests
{
    private readonly GameManager _gameManager;
    private readonly Mock<IFighterFactory> _fighterFactory;
    private readonly Mock<IConsoleInputReader> _consoleReader;

    private const int LowerLimit = 2;

    public GameManagerTests()
    {
        _consoleReader = new Mock<IConsoleInputReader>();
        _fighterFactory = new Mock<IFighterFactory>();

        _gameManager = new GameManager( _fighterFactory.Object, _consoleReader.Object );
    }

    [Fact]
    public void PlayGame_WithTwoFighters_ShouldReturnWinner()
    {
        // Arrange
        List<Mock<IFighter>> fighters = CreateMockFightersList( fightersCount: 2 );
        SetupConsoleAndFactoryMocks( fightersCount: 2, fighters );

        fighters[ 0 ]
            .Setup( f => f.CalculateDamage() )
            .Returns( 100 );

        fighters[ 1 ]
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 0 );

        fighters[ 0 ]
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 100 );

        // Act
        IFighter winner = _gameManager.PlayGame();

        // Assert
        Assert.False( fighters[ 1 ].Object.IsAlive() );
        Assert.True( fighters[ 0 ].Object.IsAlive() );
        Assert.Equal( expected: fighters[ 0 ].Object, actual: winner );

        fighters[ 1 ].Verify( f => f.TakeDamage( 100 ), Times.Once );
        _consoleReader.Verify( r => r.GetValidPositiveIntegerInput( LowerLimit ), Times.Once );
        _fighterFactory.Verify( f => f.CreateFighter(), Times.Exactly( 2 ) );
    }

    [Fact]
    public void PlayGame_WithMultipleFighters_ShouldReturnOnlyOneWinner()
    {
        // Arrange
        List<Mock<IFighter>> fighters = CreateMockFightersList( fightersCount: 3 );
        SetupConsoleAndFactoryMocks( fightersCount: 3, fighters );

        fighters[ 0 ].Setup( f => f.GetCurrentHealth() ).Returns( 100 );
        fighters[ 1 ].Setup( f => f.GetCurrentHealth() ).Returns( 100 );
        fighters[ 2 ].Setup( f => f.GetCurrentHealth() ).Returns( 100 );

        fighters[ 1 ].Setup( f => f.CalculateDamage() ).Returns( 100 );

        fighters[ 0 ].Setup( f => f.TakeDamage( 100 ) )
            .Callback( () => fighters[ 0 ].Setup( f => f.GetCurrentHealth() ).Returns( 0 ) );

        fighters[ 2 ].Setup( f => f.TakeDamage( 100 ) )
            .Callback( () => fighters[ 2 ].Setup( f => f.GetCurrentHealth() ).Returns( 0 ) );

        // Act
        IFighter winner = _gameManager.PlayGame();

        // Assert
        Assert.NotNull( winner );
        Assert.Equal( fighters[ 1 ].Object, winner );

        fighters[ 0 ].Verify( f => f.TakeDamage( 100 ), Times.Once );
        fighters[ 2 ].Verify( f => f.TakeDamage( 100 ), Times.Once );
        _fighterFactory.Verify( f => f.CreateFighter(), Times.Exactly( 3 ) );
    }

    [Fact]
    public void PlayGame_ShouldInitializeSpecifiedNumberOfFighters()
    {
        // Arrange
        List<Mock<IFighter>> fighters = CreateMockFightersList( 4 );
        SetupConsoleAndFactoryMocks( 4, fighters );

        // Act
        IFighter winner = _gameManager.PlayGame();

        // Assert
        _consoleReader.Verify( r => r.GetValidPositiveIntegerInput( LowerLimit ), Times.Once );
        _fighterFactory.Verify( ff => ff.CreateFighter(), Times.Exactly( 4 ) );
    }

    private static List<Mock<IFighter>> CreateMockFightersList( int fightersCount )
    {
        return Enumerable.Range( 0, fightersCount )
            .Select( f => new Mock<IFighter>() )
            .ToList();
    }

    private void SetupConsoleAndFactoryMocks( int fightersCount, List<Mock<IFighter>> fighters )
    {
        _consoleReader
            .Setup( r => r.GetValidPositiveIntegerInput( LowerLimit ) )
            .Returns( fightersCount );

        var sequence = _fighterFactory.SetupSequence( ff => ff.CreateFighter() );
        foreach ( var fighter in fighters )
        {
            sequence.Returns( fighter.Object );
        }
    }
}
