using Fighters.ConsoleReader;
using Fighters.Factory;
using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Fighters.Tests.Factory;

public class FighterFactoryTests
{
    private readonly Mock<IConsoleInputReader> _consoleReader;
    private readonly FighterFactory _factory;

    private readonly Dictionary<int, IRace> _races = new()
    {
        { 1, new Human() },
        { 2, new Goblin() }
    };

    private readonly Dictionary<int, IWeapon> _weapons = new()
    {
        { 1, new NoWeapon() },
        { 2, new Dagger() },
        { 3, new Sword() }
    };

    private readonly Dictionary<int, IArmor> _armors = new()
    {
        { 1, new NoArmor() },
        { 2, new ChainMail() }
    };

    private readonly Dictionary<int, IFighterClass> _fighterClasses = new()
    {
        { 1, new Knight() },
        { 2, new Assassin() }
    };

    public FighterFactoryTests()
    {
        _consoleReader = new Mock<IConsoleInputReader>();
        _factory = new FighterFactory(
            _consoleReader.Object,
            _races,
            _weapons,
            _armors,
            _fighterClasses );
    }

    [Fact]
    public void CreateFighter_InputsAreValid_ShouldCreateNewFighter()
    {
        // Arrange
        const int lowerLimit = 1;

        _consoleReader
            .SetupSequence( c => c.GetValidUserStringInput() )
            .Returns( "Боец 1" );

        _consoleReader
            .SetupSequence( c => c.GetValidPositiveIntegerInput( lowerLimit ) )
            .Returns( 1 )
            .Returns( 1 )
            .Returns( 1 )
            .Returns( 1 );

        // Act
        IFighter createdFighter = _factory.CreateFighter();

        // Assert
        Assert.NotNull( createdFighter );
        Assert.Equal( expected: "Боец 1", actual: createdFighter.GetName() );

        Assert.IsType<Human>( createdFighter.GetRace() );
        Assert.IsType<NoWeapon>( createdFighter.GetWeapon() );
        Assert.IsType<NoArmor>( createdFighter.GetArmor() );
        Assert.IsType<Knight>( createdFighter.GetFighterClass() );

        _consoleReader.Verify( r => r.GetValidPositiveIntegerInput( lowerLimit ), Times.Exactly( 4 ) );
    }

    [Fact]
    public void CreateFighter_SelectInvalidOptions_ShouldRetriesUntilAllOptionAreValidAndCreateFighter()
    {
        // Arrange
        const int lowerLimit = 1;

        _consoleReader
            .Setup( c => c.GetValidUserStringInput() )
            .Returns( "Тестовый боец" );

        _consoleReader
            .SetupSequence( c => c.GetValidPositiveIntegerInput( lowerLimit ) )
            .Returns( 666 )
            .Returns( 1 )
            .Returns( 1 )
            .Returns( 1 )
            .Returns( 1 );

        // Act
        IFighter createdFighter = _factory.CreateFighter();

        // Assert
        Assert.NotNull( createdFighter );
        Assert.Equal( expected: "Тестовый боец", actual: createdFighter.GetName() );

        Assert.IsType<Human>( createdFighter.GetRace() );
        Assert.IsType<NoWeapon>( createdFighter.GetWeapon() );
        Assert.IsType<NoArmor>( createdFighter.GetArmor() );
        Assert.IsType<Knight>( createdFighter.GetFighterClass() );

        _consoleReader.Verify( r => r.GetValidPositiveIntegerInput( lowerLimit ), Times.Exactly( 5 ) );
    }
}
