
using System.Runtime.CompilerServices;

namespace Cube.Model;

// https://webhome.cs.uvic.ca/~ruskey/Publications/RankPerm/MyrvoldRuskey.pdf

public class PermutationRank
{
    public static readonly int[] factorialLookup = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600]; // 0! to 12!

    public static unsafe int RankFromPermutation(byte* permutation, int len)
    {
        var index = stackalloc byte[len];
        var copy = stackalloc byte[len];
        Unsafe.CopyBlock(copy, permutation, (uint)(len * sizeof(byte)));
        for (byte i = 0; i < len; i++)
        {
            index[copy[i]] = i;
        }

        int rank = 0;
        int potential = 1;

        for (int i = len - 1; i > 0; i--)
        {
            byte choice = copy[i];
            Swap(copy, i, index[i]);
            Swap(index, choice, i);
            rank += choice * potential;
            potential *= i + 1;
        }

        return rank;
    }

    public static unsafe void PermutationFromRank(byte* permutation, int len, int rank)
    {
        for (int i = 0; i < len; i++)
        {
            permutation[i] = (byte)i;
        }

        for (int i = len; i > 0; i--)
        {
            Swap(permutation, i - 1, rank % i);
            rank /= i;
        }
    }

    private static unsafe void Swap(byte* arr, int i, int j)
    {
        if (i == j) return;
        var temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}