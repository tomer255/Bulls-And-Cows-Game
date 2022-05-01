namespace A22_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GameTableForm : Form
    {
        private readonly int buttonGap = 45;
        private readonly int offsetX = 5;
        private readonly int offsetY = 10;
        private readonly byte numberOfColorsToGuess;
        private readonly byte numberOfOptionsToPick;
        private readonly byte numberOfGuesses;
        private readonly List<GuessRow> guessRowList = new List<GuessRow>();
        private readonly GameLogic gameLogic;
        private readonly List<Button> resultRow = new List<Button>();

        internal GameTableForm(byte numberOfGuesses, byte numberOfColorsToGuess, byte numberOfOptionsToPick)
        {
            this.numberOfGuesses = numberOfGuesses;
            this.numberOfColorsToGuess = numberOfColorsToGuess;
            this.numberOfOptionsToPick = numberOfOptionsToPick;
            this.gameLogic = new GameLogic(numberOfGuesses, numberOfColorsToGuess, numberOfOptionsToPick);
            this.gameLogic.StartGame();
            this.InitializeComponent();
            this.guessRowList[0].EnabledButtonGuess(true);
        }

        private void InitializeComponent()
        {
            this.InitializeBoradComponent(this.offsetX, this.offsetY);

            // GameTableForm
            int width = (this.offsetX * 2) + (this.buttonGap * (this.numberOfColorsToGuess + 1)) + (20 * ((this.numberOfColorsToGuess / 2) + (this.numberOfColorsToGuess % 2)));
            int height = (this.offsetY * 2) + this.buttonGap + (this.numberOfGuesses * this.buttonGap);
            this.ClientSize = new Size(width, height);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "GameTableForm";
            this.ResumeLayout(false);
        }

        private void InitializeBoradComponent(int xPosition, int yPosition)
        {
            this.InitializeResultRowComponent(xPosition, yPosition);
            yPosition += this.buttonGap + 10;

            for (int i = 0; i < this.numberOfGuesses; i++)
            {
                GuessRow guessRow = new GuessRow(this.numberOfColorsToGuess, this.numberOfOptionsToPick, xPosition, yPosition, i);
                for (int j = 0; j < this.numberOfColorsToGuess; j++)
                {
                    this.Controls.Add(guessRow.ButtonGuessList[j]);
                    this.Controls.Add(guessRow.ButtonFeedbackList[j]);
                }

                guessRow.ButtonSumbit.Click += new EventHandler(this.ButtonSumbitClick);
                this.Controls.Add(guessRow.ButtonSumbit);
                this.guessRowList.Add(guessRow);
            }
        }

        private void InitializeResultRowComponent(int xPosition, int yPosition)
        {
            for (int i = 0; i < this.numberOfColorsToGuess; i++)
            {
                Button buttonResult = new Button();
                buttonResult.Location = new Point(xPosition + (45 * i), yPosition);
                buttonResult.Size = new Size(40, 40);
                buttonResult.BackColor = Color.Black;
                buttonResult.Enabled = false;
                this.resultRow.Add(buttonResult);
                this.Controls.Add(buttonResult);
            }
        }

        private void ButtonSumbitClick(object sender, EventArgs e)
        {
            byte[] colorAsBytes = new byte[this.numberOfColorsToGuess];
            for (int i = 0; i < this.numberOfColorsToGuess; i++)
            {
                colorAsBytes[i] = (byte)AvailableColors.ColorsList.IndexOf(this.guessRowList[this.gameLogic.Turn].ButtonGuessList[i].BackColor);
            }

            byte perfectGuessScore, appearanceGuessScour;
            this.gameLogic.HandleGuess(colorAsBytes, out perfectGuessScore, out appearanceGuessScour);
            this.guessRowList[this.gameLogic.Turn - 1].DisplayFeedback(perfectGuessScore, appearanceGuessScour);
            this.guessRowList[this.gameLogic.Turn - 1].EnabledButtonGuess(false);
            this.guessRowList[this.gameLogic.Turn - 1].ButtonSumbit.Enabled = false;
            if (this.gameLogic.CurrentGameStatus == GameStatus.eGameStatus.Lose || this.gameLogic.CurrentGameStatus == GameStatus.eGameStatus.Win)
            {
                this.EndGame();
            }
            else
            {
                this.guessRowList[this.gameLogic.Turn].EnabledButtonGuess(true);
            }
        }

        private void RevealSequence()
        {
            for (int i = 0; i < this.numberOfColorsToGuess; i++)
            {
                this.resultRow[i].BackColor = AvailableColors.ColorsList[this.gameLogic.RandomSequence[i]];
            }
        }

        private void EndGame()
        {
            this.RevealSequence();
            MessageBox.Show(string.Format("You {0}", this.gameLogic.CurrentGameStatus));
        }
    }
}
