namespace A22_Ex05
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ColorPickForm : Form
    {
        private readonly byte numberOfOptionsToPick;
        private readonly int offsetX = 5;
        private readonly int offsetY = 5;
        private readonly int buttonGap = 45;
        private readonly List<Button> buttonsColorPik = new List<Button>();
        private Button buttonToChangeColor;

        internal Button ButtonToChangeColor
        {
            set { this.buttonToChangeColor = value; }
        }

        internal ColorPickForm(byte numberOfOptionsToPick)
        {
            this.numberOfOptionsToPick = numberOfOptionsToPick;
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            int buttonwidth;
            int buttonheight;
            for (int i = 0; i < this.numberOfOptionsToPick; i++)
            {
                Button buttonColorPick = new Button();
                buttonwidth = this.offsetX + (this.buttonGap * (i / 2));
                buttonheight = this.offsetY + (this.buttonGap * (i % 2));
                buttonColorPick.Location = new Point(buttonwidth, buttonheight);
                buttonColorPick.Size = new Size(40, 40);
                buttonColorPick.BackColor = AvailableColors.ColorsList[i];
                buttonColorPick.Click += new EventHandler(this.SelectColor);
                this.buttonsColorPik.Add(buttonColorPick);
                this.Controls.Add(buttonColorPick);
            }

            int width = (this.offsetX * 2) + (this.buttonGap * ((this.numberOfOptionsToPick / 2) + (this.numberOfOptionsToPick % 2)));
            int height = (this.offsetY * 2) + (this.buttonGap * 2);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new Size(width, height);
            this.Name = "ColorPickForm";
            this.ResumeLayout(false);
        }

        private void SelectColor(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;
            int indexToRelease = AvailableColors.ColorsList.IndexOf(this.buttonToChangeColor.BackColor);
            this.buttonToChangeColor.BackColor = buttonSender.BackColor;
            if (indexToRelease != -1)
            {
                this.buttonsColorPik[indexToRelease].Enabled = true;
            }

            buttonSender.Enabled = false;
            this.Close();
        }
    }
}
