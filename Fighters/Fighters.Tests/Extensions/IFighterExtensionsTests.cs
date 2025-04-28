using Fighters.Extensions;
using Fighters.Models.Fighters;
using Moq;

namespace Fighters.Tests.Extensions;

public class IFighterExtensionsTests
{
    [Fact]
    public void IsAlive_FighterIsDead_ShouldReturnsFalse()
    {
        // Arrange
        var testFighter = new Mock<IFighter>();
        testFighter
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 0 );

        // Act
        bool result = testFighter.Object.IsAlive();

        // Assert
        Assert.False( result );
    }

    [Fact]
    public void IsAlive_FighterIsAlive_ShouldReturnsTrue()
    {
        // Arrange
        var testFighter = new Mock<IFighter>();
        testFighter
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 100 );

        // Act
        bool result = testFighter.Object.IsAlive();

        // Assert
        Assert.True( result );
    }
}
