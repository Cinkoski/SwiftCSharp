using System.Collections.Generic;

namespace SwiftCSharp
{
    class BinUtils
    {
        public static long BaseLeft(long bin)
        {
            return bin & (bin + 1);
        }

        public static long BaseRight(long bin)
        {
            return (bin | (bin + 1)) - 1;
        }

        public static long LayerOffset(long bin)
        {
            return bin >> (Layer(bin) + 1);
        }

        public static int Layer(long bin)
        {
            int r = 0;

            long tail = bin + 1;
            tail = tail & (-tail);

            if (tail > 0x80000000U)
            {
                r = 32;
                tail >>= 16;    // FIXME: hide warning
                tail >>= 16;
            }

            byte[] DeBRUIJN = new byte[32] { 0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21, 19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 };

            return r + DeBRUIJN[0x1f & ((tail * 0x077CB531U) >> 27)];
        }

        public static long OffsetToBin(int layer, long offset)
        {
            return ((2 * offset + 1) << layer) - 1;
        }

        public static long LayerBits(long bin)
        {
            return bin ^ (bin + 1);
        }

        public static long Parent(long bin)
        {
            long bits = LayerBits(bin);
            long negativeBits = -2 - bits;
            return (bin | bits) & negativeBits;
        }

        public static List<long> ChunkToBin(long schunk, long echunk)
        {
            long s = OffsetToBin(0, schunk);
            long e = OffsetToBin(0, echunk);

            long cur = s;

            List<long> buff = new List<long>();

            while (true)
            {
                if (BaseLeft(Parent(cur)) < s || BaseRight(Parent(cur)) > e)
                {
                    buff.Add(cur);

                    if (BaseLeft(Parent(cur)) < s)
                        cur = OffsetToBin(0, LayerOffset(BaseRight(Parent(cur))) + 1);
                    else
                        cur = OffsetToBin(0, LayerOffset(BaseRight(cur)) + 1);

                    if (cur <= e)
                    {
                        if (cur == e)
                            buff.Add(e);

                        break;
                    }
                }
                else
                {
                    cur = Parent(cur);
                }
            }

            return buff;
        }
    }
}
