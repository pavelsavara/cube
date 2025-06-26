using System.Dynamic;
using Cube.Model;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Cube.Model.Tests;

public class PermutationRankTests
{
    ITestOutputHelper output;
    public PermutationRankTests(ITestOutputHelper output)
    {
        this.output = output;
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    //[InlineData(8)]
    //[InlineData(12)]
    public unsafe void RankFromPermutation_IsInverseOfPermutationFromRank(int length)
    {
        // Arrange
        byte[] permutation1 = new byte[length];
        byte[] permutation2 = new byte[length];
        int factorial = PermutationRank.factorialLookup[length];

        HashSet<byte[]> uniquePermutations = new HashSet<byte[]>();

        fixed (byte* p1 = permutation1, p2 = permutation2)
        {

            // Act & Assert - Test round-trip conversion for all possible ranks
            for (int originalRank = 0; originalRank < factorial; originalRank++)
            {
                // Convert rank to permutation
                PermutationRank.PermutationFromRank(p1, length, originalRank);

                // Convert permutation back to rank
                int calculatedRank = PermutationRank.RankFromPermutation(p1, length);

                output.WriteLine($"Rank {originalRank}, Calculated Rank: {calculatedRank}, Permutation: {string.Join(", ", permutation1)}");

                // Verify the rank matches
                Assert.Equal(originalRank, calculatedRank);

                // make sure that the permutation is unique
                Assert.True(uniquePermutations.Add((byte[])permutation1.Clone()));
            }
        }
    }
}
