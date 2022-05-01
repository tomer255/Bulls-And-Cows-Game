namespace A22_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class GuessRow
    {
        private readonly byte numberOfColorsToGuess;
        private readonly int buttonGap = 45;
        private readonly ColorPickForm colorPickForm;

        internal List<Button> ButtonGuessList { get; } = new List<Button>();

        internal Button ButtonSumbit { get; } = new Button();

        internal List<Button> ButtonFeedbackList { get; } = new List<Button>();

        internal GuessRow(byte numberOfColorsToGuess, byte numberOfOptionsToPick, int xPosition, int yPosition, int index)
        {
            this.numberOfColorsToGuess = numberOfColorsToGuess;
            this.InitializeGuessRowComponent(xPosition, yPosition + (index * this.buttonGap));
            this.InitializeSumbitButtonComponent(xPosition + (this.buttonGap * this.numberOfColorsToGuess), yPosition + (index * this.buttonGap));
            this.InitializeFeedbackComponent(xPosition + (this.buttonGap * (this.numberOfColorsToGuess + 1)), yPosition + (index * this.buttonGap));
            this.colorPickForm = new ColorPickForm(numberOfOptionsToPick);
        }

        private void InitializeGuessRowComponent(int xPosition, int yPosition)
        {
            for (int i = 0; i < this.numberOfColorsToGuess; i++)
            {
                Button buttonGuess = new Button();
                buttonGuess.Location = new Point(xPosition + (this.buttonGap * i), yPosition);
                buttonGuess.Size = new Size(40, 40);
                buttonGuess.Click += new EventHandler(this.AssignButton);
                buttonGuess.BackColorChanged += this.EnabledButtonSumbitIfRowFull;
                buttonGuess.Enabled = false;
                this.ButtonGuessList.Add(buttonGuess);
            }
        }

        private void InitializeSumbitButtonComponent(int xPosition, int yPosition)
        {
            this.ButtonSumbit.Location = new Point(xPosition, yPosition + 10);
            this.ButtonSumbit.Size = new Size(40, 20);
            this.ButtonSumbit.Text = "-->>";
            this.ButtonSumbit.Enabled = false;
        }

        private void InitializeFeedbackComponent(int xPosition, int yPosition)
        {
            for (int i = 0; i < this.numberOfColorsToGuess; i++)
            {
                Button buttonFeedback = new Button();
                buttonFeedback.Location = new Point(xPosition + (20 * (i / 2)), 3 + yPosition + ((i % 2) * 20));
                buttonFeedback.Size = new Size(15, 15);
                buttonFeedback.Enabled = false;
                this.ButtonFeedbackList.Add(buttonFeedback);
            }
        }

        private void AssignButton(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;
            this.colorPickForm.ButtonToChangeColor = buttonSender;
            this.colorPickForm.ShowDialog();
        }

        internal void EnabledButtonGuess(bool isEnabled)
        {
            foreach (Button buttonGuess in this.ButtonGuessList)
            {
                buttonGuess.Enabled = isEnabled;
            }
        }

        internal void DisplayFeedback(byte perfectGuessScore, byte appearanceGuessScour)
        {
            int i;
            for (i = 0; i < perfectGuessScore; i++)
            {
                this.ButtonFeedbackList[i].BackColor = Color.Black;
            }

            for (; i < perfectGuessScore + appearanceGuessScour; i++)
            {
                this.ButtonFeedbackList[i].BackColor = Color.Yellow;
            }
        }

        private void EnabledButtonSumbitIfRowFull(object sender, EventArgs e)
        {
            this.ButtonSumbit.Enabled = true;
            foreach (Button buttonGuess in this.ButtonGuessList)
            {
                if (buttonGuess.UseVisualStyleBackColor)
                {
                    this.ButtonSumbit.Enabled = false;
                }
            }
        }
    }
}
