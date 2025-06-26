using System.Text;

namespace Cube.Model;

public unsafe struct Cube : IEquatable<Cube>
{
    public fixed byte Facelets[54];

    public Cube()
    {
        // Initialize with a solved state
        for (int i = 0; i < 54; i++)
        {
            Facelets[i] = (byte)(i / 9);
        }
    }

    public unsafe Cube(CubeIndex index)
    {
        this[Facelet.U] = Color.White;
        this[Facelet.R] = Color.Blue;
        this[Facelet.F] = Color.Red;
        this[Facelet.D] = Color.Yellow;
        this[Facelet.L] = Color.Green;
        this[Facelet.B] = Color.Orange;

        for (int p = 0; p < 8; p++)
        {
            CornerCubelet cornerPosition = (CornerCubelet)p;
            var (ud, bf, lr) = cornerPosition.ToFacelet();

            CornerCubelet corner = (CornerCubelet)index.CornerPermutation[p];
            var (faceA, faceB, faceC) = corner.ToFace();

            byte orientation = index.CornerOrientation[p];
            if (orientation == 0)
            {
                this[ud] = (Color)(byte)faceA;
                this[bf] = (Color)(byte)faceB;
                this[lr] = (Color)(byte)faceC;
            }
            else if (orientation == 1)
            {
                this[ud] = (Color)(byte)faceB;
                this[bf] = (Color)(byte)faceC;
                this[lr] = (Color)(byte)faceA;
            }
            else // orientation == 2
            {
                this[ud] = (Color)(byte)faceC;
                this[bf] = (Color)(byte)faceA;
                this[lr] = (Color)(byte)faceB;
            }
        }

        for (int p = 0; p < 12; p++)
        {
            EdgeCubelet edgePosition = (EdgeCubelet)p;
            var (faceletA, faceletB) = edgePosition.ToFacelet();

            EdgeCubelet edge = (EdgeCubelet)index.EdgePermutation[p];
            var (faceA, faceB) = edge.ToFace();

            byte orientation = index.EdgeOrientation[p];
            if (orientation == 0)
            {
                this[faceletA] = (Color)(byte)faceA;
                this[faceletB] = (Color)(byte)faceB;
            }
            else
            {
                this[faceletA] = (Color)(byte)faceB;
                this[faceletB] = (Color)(byte)faceA;
            }
        }
    }

    public Color this[Facelet facelet]
    {
        get => (Color)Facelets[(int)facelet];
        set => Facelets[(int)facelet] = (byte)value;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int r = 0; r < 3; r++)
        {
            for (int f = 0; f < 6; f++)
            {
                for(int c = 0; c < 3; c++)
                {
                    Facelet facelet = (Facelet)(f * 9 + r * 3 + c);
                    sb.Append(this[facelet].ToString()[0]);
                }
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public string ToFaceletsString()
    {
        var sb = new StringBuilder();
        for (int r = 0; r < 3; r++)
        {
            for (int f = 0; f < 6; f++)
            {
                for(int c = 0; c < 3; c++)
                {
                    Facelet facelet = (Facelet)(f * 9 + r * 3 + c);
                    sb.Append(facelet.ToString().PadRight(4));
                }
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public bool Equals(Cube other)
    {
        fixed (byte* a = Facelets)
        {
            return new ReadOnlySpan<byte>(a, 54).SequenceEqual(new ReadOnlySpan<byte>(other.Facelets, 54));
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is Cube other && Equals(other);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        fixed (byte* fl = Facelets)
        {
            hash.AddBytes(new ReadOnlySpan<byte>(fl, 54));
        }
        return hash.ToHashCode();
    }
}
