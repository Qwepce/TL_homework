using Fighters.ConsoleReader;
using Moq;

namespace Fighters.Tests.ConsoleReader;
public class ConsoleInputReaderTests
{
    [Fact]
    public void GetValidPositiveIntegerInput_InputIsValid_ShouldReturnParsedIntegerInput()
    {
        // Arrange
        var mockReader = new Mock<ConsoleInputReader>();
        mockReader
            .Setup( r => r.ReadLine() )
            .Returns( "10" );

        var reader = mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput();

        // Assert
        Assert.Equal( expected: 10, actual: result );
    }

    [Fact]
    public void GetValidPositiveIntegerInput_InputIsInvalidFormat_ShouldRetriesUntilValid()
    {
        // Arrange
        var mockReader = new Mock<ConsoleInputReader>();
        mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "abc" )
            .Returns( "10" );

        var reader = mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput();

        // Assert
        Assert.Equal( expected: 10, actual: result );
    }

    [Fact]
    public void GetValidPositiveIntegerInput_IntegerIsLowerThanDefaultLowerLimit_ShoudRetriesUntilValid()
    {
        // Arrange
        var mockReader = new Mock<ConsoleInputReader>();
        mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "-2" )
            .Returns( "50" );

        var reader = mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput();

        // Assert
        Assert.Equal( expected: 50, actual: result );
    }

    [Fact]
    public void GetValidPositiveIntegerInput_IntegerIsLowerThanCustomLowerLimit_ShoudRetriesUntilValid()
    {
        // Arrange
        var mockReader = new Mock<ConsoleInputReader>();
        mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "5" )
            .Returns( "50" );

        var reader = mockReader.Object;

        // Act
        int result = reader.GetValidPositiveIntegerInput( lowerLimit: 10 );

        // Assert
        Assert.Equal( expected: 50, actual: result );
    }

    [Fact]
    public void GetValidUserStringInput_InputIsValid_ShouldReturnValidString()
    {
        // Arrange
        var mockReader = new Mock<ConsoleInputReader>();
        mockReader
            .Setup( r => r.ReadLine() )
            .Returns( "valid input" );

        var reader = mockReader.Object;

        // Act
        string result = reader.GetValidUserStringInput();

        // Assert
        Assert.Equal( expected: "valid input", result );
    }

    [Fact]
    public void GetValidUserStringInput_InputIsEmptyOrWhiteSpaces_ShouldRetriesUntilValid()
    {
        // Arrange
        var mockReader = new Mock<ConsoleInputReader>();
        mockReader
            .SetupSequence( r => r.ReadLine() )
            .Returns( "" )
            .Returns( "      " )
            .Returns( "valid input" );

        var reader = mockReader.Object;

        // Act
        string result = reader.GetValidUserStringInput();

        // Assert
        Assert.Equal( expected: "valid input", result );
    }
}
