using Cube.Model;

namespace Cube.Model.Tests;

public class ConversionTests
{
    [Fact]
    public void CubeIndex_To_Cube_Conversion_For_Solved_State_Is_Correct()
    {
        // Arrange
        var solvedIndex = new CubeIndex();
        var expectedCube = new Cube();

        // Act
        var convertedCube = new Cube(solvedIndex);

        // Assert
        Assert.Equal(expectedCube, convertedCube);
    }
}
