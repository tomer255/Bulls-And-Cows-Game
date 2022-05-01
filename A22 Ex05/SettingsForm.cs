using System;
using System.Windows.Forms;

namespace A22_Ex05
{
    internal class SettingsForm : Form
    {
        private readonly int minNumberOfChances = 4;
        private readonly int maxNumberOfChances = 10;
        private Button buttonNumberOfChances;
        private Button buttonStart;

        internal int NumberOfChances { get; private set; } = 8;

        internal SettingsForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.buttonNumberOfChances = new Button();
            this.buttonStart = new Button();
            this.SuspendLayout();

            // ButtonNumberOfChances
            this.buttonNumberOfChances.Location = new System.Drawing.Point(56, 45);
            this.buttonNumberOfChances.Name = "Number of chances";
            this.buttonNumberOfChances.Size = new System.Drawing.Size(337, 74);
            this.buttonNumberOfChances.TabIndex = 0;
            this.buttonNumberOfChances.Text = "Number of chances : 8";
            this.buttonNumberOfChances.UseVisualStyleBackColor = true;
            this.buttonNumberOfChances.Click += new System.EventHandler(this.IncreaseNumberOfChances);

            // ButtonStart
            this.buttonStart.Location = new System.Drawing.Point(276, 180);
            this.buttonStart.Name = "Start button";
            this.buttonStart.Size = new System.Drawing.Size(135, 45);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new EventHandler(this.Button2_Click);

            // SettingsForm
            this.ClientSize = new System.Drawing.Size(445, 260);
            this.Controls.Add(this.buttonNumberOfChances);
            this.Controls.Add(this.buttonStart);

            this.Name = "SettingsForm";
            this.ResumeLayout(false);
        }

        private void IncreaseNumberOfChances(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;
            this.NumberOfChances = ((this.NumberOfChances - this.minNumberOfChances + 1) % (this.maxNumberOfChances - this.minNumberOfChances + 1)) + this.minNumberOfChances;
            buttonSender.Text = string.Format("Number of chances : {0}", this.NumberOfChances);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            GameTableForm gameTableForm = new GameTableForm((byte)this.NumberOfChances, 4, 8);
            gameTableForm.ShowDialog();
            this.Close();
        }
    }
}
