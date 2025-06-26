using System;
using System.Linq;

namespace Cube.Model;

/// <summary>
/// Represents the cube state as a feature vector for a Deep Neural Network.
/// </summary>
public unsafe struct CubeFeatures : IEquatable<CubeFeatures>
{
    /// <summary>
    /// The feature vector of size 364.
    /// </summary>
    public fixed float Features[364];

    public CubeFeatures()
    {
    }

    public bool Equals(CubeFeatures other)
    {
        for (int i = 0; i < 364; i++)
        {
            if (Features[i] != other.Features[i]) return false;
        }
        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is CubeFeatures other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        fixed (float* f = Features)
        {
            for (int i = 0; i < 364; i++)
            {
                hash.Add(f[i]);
            }
        }
        return hash.ToHashCode();
    }
}
