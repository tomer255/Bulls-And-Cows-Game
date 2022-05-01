namespace A22_Ex05
{
    using System;

    internal class ByteGenerator
    {
        private readonly byte maxRange;
        private readonly byte[] bytePool;
        private readonly byte sequenceSize;
        private readonly Random random = new Random();

        internal byte[] RandomSequence { get; }

        internal ByteGenerator(byte maxRange, byte sequenceSize)
        {
            this.maxRange = maxRange;
            this.sequenceSize = sequenceSize;
            this.RandomSequence = new byte[sequenceSize];
            this.bytePool = this.GeneratePoolOfByte();
        }

        private void SwapByte(ref byte firstCharToSwap, ref byte secondCharToSwap)
        {
            byte charToSwap = firstCharToSwap;
            firstCharToSwap = secondCharToSwap;
            secondCharToSwap = charToSwap;
        }

        private byte[] GeneratePoolOfByte()
        {
            byte[] numberPool = new byte[this.maxRange];
            for (byte index = 0; index < this.maxRange; index++)
            {
                numberPool[index] = index;
            }

            return numberPool;
        }

        private void ShuffleByte()
        {
            for (byte index = 0; index < this.maxRange; index++)
            {
                int indexToSwap = this.random.Next(this.maxRange);
                this.SwapByte(ref this.bytePool[index], ref this.bytePool[indexToSwap]);
            }
        }

        internal void GenerateSequence()
        {
            this.ShuffleByte();
            for (byte index = 0; index < this.sequenceSize; index++)
            {
                this.RandomSequence[index] = this.bytePool[index];
            }
        }
    }
}
