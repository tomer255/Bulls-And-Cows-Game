namespace A22_Ex05
{
    internal class GameLogic
    {
        private readonly byte numberOfSymbolsToGuess;
        private readonly byte numberOfGuesses;
        private readonly ByteGenerator generator;

        internal byte[] RandomSequence
        {
            get
            {
                return this.generator.RandomSequence;
            }
        }

        internal GameStatus.eGameStatus CurrentGameStatus { get; set; } = GameStatus.eGameStatus.PreRun;

        internal byte Turn { get; private set; }

        internal GameLogic(byte numberOfGuesses, byte numberOfSymbolsToGuess, byte numberOfSymbolsToChoose)
        {
            this.numberOfSymbolsToGuess = numberOfSymbolsToGuess;
            this.numberOfGuesses = numberOfGuesses;
            this.generator = new ByteGenerator(numberOfSymbolsToChoose, this.numberOfSymbolsToGuess);
            this.Turn = 0;
        }

        internal void StartGame()
        {
            this.generator.GenerateSequence();
            this.Turn = 0;
            this.CurrentGameStatus = GameStatus.eGameStatus.Running;
        }

        internal void HandleGuess(byte[] guess, out byte perfectGuessScore, out byte appearanceGuessScour)
        {
            perfectGuessScore = this.CheckPerfectGuess(guess);
            appearanceGuessScour = this.CheckAppearanceGuess(guess);
            appearanceGuessScour -= perfectGuessScore;
            this.Turn++;
            this.CheckIfGameOver(perfectGuessScore);
        }

        private void CheckIfGameOver(byte perfectGuessScore)
        {
            if (this.Turn == this.numberOfGuesses)
            {
                this.CurrentGameStatus = GameStatus.eGameStatus.Lose;
            }

            if (perfectGuessScore == this.numberOfSymbolsToGuess)
            {
                this.CurrentGameStatus = GameStatus.eGameStatus.Win;
            }
        }

        private byte CheckPerfectGuess(byte[] guess)
        {
            byte score = 0;
            for (int index = 0; index < this.numberOfSymbolsToGuess; index++)
            {
                if (this.generator.RandomSequence[index] == guess[index])
                {
                    score++;
                }
            }

            return score;
        }

        private byte CheckAppearanceGuess(byte[] i_Guess)
        {
            byte score = 0;
            foreach (byte byteGuess in i_Guess)
            {
                foreach (byte byteSequence in this.generator.RandomSequence)
                {
                    if (byteGuess == byteSequence)
                    {
                        score++;
                    }
                }
            }

            return score;
        }
    }
}
