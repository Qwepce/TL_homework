using Fighters.ConsoleReader;
using Moq;

namespace Fighters.Tests.ConsoleReader;
public class ConsoleInputReaderTests
{
    private readonly Mock<ConsoleInputReader> _mockReader;

    public ConsoleInputReaderTests()
    {
        _mockReader = new Mock<ConsoleInputReader>();
    }

    [Fact]
    public void GetValidPositiveIntegerInput_InputIsValid_ShouldReturnParsedIntegerInput()
    {
        // Arrange
        _mockReader
            .Setup( r => r.ReadLine() )
            .Returns( "10" );

        var reader = _mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput();

        // Assert
        Assert.Equal( expected: 10, actual: result );
    }

    [Fact]
    public void GetValidPositiveIntegerInput_InputIsInvalidFormat_ShouldRetriesUntilValid()
    {
        // Arrange
        _mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "abc" )
            .Returns( "10" );

        var reader = _mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput();

        // Assert
        Assert.Equal( expected: 10, actual: result );
    }

    [Fact]
    public void GetValidPositiveIntegerInput_IntegerIsLowerThanDefaultLowerLimit_ShoudRetriesUntilValid()
    {
        // Arrange
        _mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "-2" )
            .Returns( "50" );

        var reader = _mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput();

        // Assert
        Assert.Equal( expected: 50, actual: result );
    }

    [Fact]
    public void GetValidPositiveIntegerInput_IntegerIsLowerThanCustomLowerLimit_ShoudRetriesUntilValid()
    {
        // Arrange
        _mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "5" )
            .Returns( "50" );

        var reader = _mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput( lowerLimit: 10 );

        // Assert
        Assert.Equal( expected: 50, actual: result );
    }

    [Fact]
    public void GetValidUserStringInput_InputIsValid_ShouldReturnValidString()
    {
        // Arrange
        _mockReader
            .Setup( r => r.ReadLine() )
            .Returns( "valid input" );

        var reader = _mockReader.Object;

        // Act
        string result = reader.GetValidUserStringInput();

        // Assert
        Assert.Equal( expected: "valid input", result );
    }

    [Fact]
    public void GetValidUserStringInput_InputIsEmptyOrWhiteSpaces_ShouldRetriesUntilValid()
    {
        // Arrange
        _mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "" )
            .Returns( "      " )
            .Returns( "valid input" );

        var reader = _mockReader.Object;

        // Act
        string result = reader.GetValidUserStringInput();

        // Assert
        Assert.Equal( expected: "valid input", result );
    }
}
