namespace Cube.Model;

/// <summary>
/// Represents the state of the cube using permutations and orientations of its pieces.
/// This is the canonical, indexable representation.
/// </summary>
public unsafe struct CubeIndex : IEquatable<CubeIndex>
{
    /// <summary>
    /// Permutation of the corners. Index is the corner position, value is the corner piece.
    /// </summary>
    public fixed byte CornerPermutation[8];

    /// <summary>
    /// Orientation of the corners. Index is the corner position, value is the orientation (0, 1, or 2).
    /// </summary>
    public fixed byte CornerOrientation[8];

    /// <summary>
    /// Permutation of the edges. Index is the edge position, value is the edge piece.
    /// </summary>
    public fixed byte EdgePermutation[12];

    /// <summary>
    /// Orientation of the edges. Index is the edge position, value is the orientation (0 or 1).
    /// </summary>
    public fixed byte EdgeOrientation[12];

    /// <summary>
    /// Initializes a new instance of the <see cref="CubeIndex"/> struct to the solved state.
    /// </summary>
    public CubeIndex()
    {
        for (byte i = 0; i < 8; i++)
        {
            CornerPermutation[i] = i;
            CornerOrientation[i] = 0;
        }

        for (byte i = 0; i < 12; i++)
        {
            EdgePermutation[i] = i;
            EdgeOrientation[i] = 0;
        }
    }

    public CubeIndex(Cube cube)
    {
    }

    public int ToIndex()
    {
        fixed (byte* cp = CornerPermutation, co = CornerOrientation, ep = EdgePermutation, eo = EdgeOrientation)
        {
            var cornerRank = PermutationRank.RankFromPermutation(cp, 8);
            var cornerRankPotential = PermutationRank.factorialLookup[8];
            var edgeRank = PermutationRank.RankFromPermutation(ep, 12);

            // todo orientation - multiset
            return cornerRankPotential * edgeRank + cornerRank;
        }
    }

    public bool Equals(CubeIndex other)
    {
        fixed (byte* cp = CornerPermutation, co = CornerOrientation, ep = EdgePermutation, eo = EdgeOrientation)
        {
            return
                new ReadOnlySpan<byte>(cp, 8).SequenceEqual(new ReadOnlySpan<byte>(other.CornerPermutation, 8)) &&
                new ReadOnlySpan<byte>(co, 8).SequenceEqual(new ReadOnlySpan<byte>(other.CornerOrientation, 8)) &&
                new ReadOnlySpan<byte>(ep, 12).SequenceEqual(new ReadOnlySpan<byte>(other.EdgePermutation, 12)) &&
                new ReadOnlySpan<byte>(eo, 12).SequenceEqual(new ReadOnlySpan<byte>(other.EdgeOrientation, 12));
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is CubeIndex other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        fixed (byte* cp = CornerPermutation, co = CornerOrientation, ep = EdgePermutation, eo = EdgeOrientation)
        {
            hash.AddBytes(new ReadOnlySpan<byte>(cp, 8));
            hash.AddBytes(new ReadOnlySpan<byte>(co, 8));
            hash.AddBytes(new ReadOnlySpan<byte>(ep, 12));
            hash.AddBytes(new ReadOnlySpan<byte>(eo, 12));
        }
        return hash.ToHashCode();
    }
}
