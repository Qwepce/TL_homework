using Fighters.ConsoleReader;
using Fighters.Extensions;
using Fighters.Factory;
using Fighters.Models.Fighters;
using Moq;

namespace Fighters.Tests;

public class GameManagerTests
{
    private readonly Mock<IFighterFactory> _fighterFactory;
    private readonly Mock<IConsoleInputReader> _consoleReader;

    private readonly GameManager _gameManager;

    public GameManagerTests()
    {
        _consoleReader = new Mock<IConsoleInputReader>();
        _fighterFactory = new Mock<IFighterFactory>();

        _gameManager = new GameManager( _fighterFactory.Object, _consoleReader.Object );
    }

    [Fact]
    public void PlayGame_WithTwoFighters_ShouldReturnsWinner()
    {
        // Arrange
        var firstFigher = new Mock<IFighter>();
        var secondFighter = new Mock<IFighter>();

        int lowerLimit = 2;

        _consoleReader
            .Setup( r => r.GetValidPositiveIntegerInput( lowerLimit ) )
            .Returns( 2 );

        _fighterFactory
            .SetupSequence( f => f.CreateFighter() )
            .Returns( firstFigher.Object )
            .Returns( secondFighter.Object );

        firstFigher
            .Setup( f => f.CalculateDamage() )
            .Returns( 100 );

        secondFighter
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 0 );

        firstFigher
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 100 );

        // Act
        IFighter winner = _gameManager.PlayGame();

        // Assert
        Assert.False( secondFighter.Object.IsAlive() );
        Assert.True( firstFigher.Object.IsAlive() );
        secondFighter.Verify( f => f.TakeDamage( It.IsAny<int>() ), Times.Once );
        _consoleReader.Verify( r => r.GetValidPositiveIntegerInput( lowerLimit ), Times.Once );
        _fighterFactory.Verify( f => f.CreateFighter(), Times.Exactly( 2 ) );
    }
}
