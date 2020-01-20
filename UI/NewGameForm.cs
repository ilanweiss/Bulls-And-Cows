using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoolPgiaUI
{
    public class NewGameForm : Form
    {
        private const string k_FormText = "Bool Pgia";
        private const string k_StartButtonText = "New Game?";
        private const int k_MarginSize = 10;
        private const int k_FormWidth = 145;
        private const int k_FormHight = 90;
        private const int k_StartButtonWidth = 120;
        private const int k_ButtonHight = 35;
        private const int k_StartButtonLeftMarginSize = k_MarginSize;
        private const int k_StartButtonTopMarginSize = k_MarginSize;


        public NewGameForm()
            {
                this.Text = k_FormText;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Size = new Size(k_FormWidth, k_FormHight);
                this.StartPosition = FormStartPosition.CenterScreen;
                initStartButton();
            }

            private void initStartButton()
            {
                Button start = new Button();

                start.Text = k_StartButtonText;
                start.Size = new Size(k_StartButtonWidth, k_ButtonHight);
                start.Location = new Point(k_StartButtonLeftMarginSize, k_StartButtonTopMarginSize);
                this.Controls.Add(start);
                start.Click += new EventHandler(buttonStart_Click);
            }

            private void buttonStart_Click(object sender, EventArgs e)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
    }
}



