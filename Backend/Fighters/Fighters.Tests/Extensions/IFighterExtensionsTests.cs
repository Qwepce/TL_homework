using Fighters.Extensions;
using Fighters.Models.Fighters;
using Moq;

namespace Fighters.Tests.Extensions;

public class IFighterExtensionsTests
{
    private readonly Mock<IFighter> _fighter;

    public IFighterExtensionsTests()
    {
        _fighter = new Mock<IFighter>();
    }

    [Fact]
    public void IsAlive_FighterIsDead_ShouldReturnsFalse()
    {
        // Arrange
        _fighter
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 0 );

        // Act
        bool result = _fighter.Object.IsAlive();

        // Assert
        Assert.False( result );
    }

    [Fact]
    public void IsAlive_FighterIsAlive_ShouldReturnsTrue()
    {
        // Arrange
        _fighter
            .Setup( f => f.GetCurrentHealth() )
            .Returns( 100 );

        // Act
        bool result = _fighter.Object.IsAlive();

        // Assert
        Assert.True( result );
    }
}
