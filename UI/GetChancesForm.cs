using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoolPgiaUI
    {
        public class GetChancesForm : Form
        {
            private const string k_FormText = "Bool Pgia";
            private const string k_StartButtonText = "Start";
            private const string k_NumOfGuessesButtonText = "Number of chances: ";
            private const int k_MarginSize = 15;
            private const int k_FormWidth = 260;
            private const int k_FormHight = 160;
            private const int k_ChanceButtonWidth = 220;
            private const int k_StartButtonWidth = 85;
            private const int k_ButtonHight = 30;
            private const int k_StartButtonLeftMarginSize = k_MarginSize*10;
            private const int k_StartButtonTopMarginSize = k_MarginSize * 5;
            private const int k_MinimalNumOfGuesses = 4;
            private const int k_MaximalNumOfGuesses = 10;

            private int m_NumOfGuesses = k_MinimalNumOfGuesses;

            public int NumOfGuesses
            {
                get { return m_NumOfGuesses; }
            }

            public GetChancesForm()
            {
                this.Text = k_FormText;
                this.FormBorderStyle = FormBorderStyle.Fixed3D;
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.Size = new Size(k_FormWidth, k_FormHight);
                this.StartPosition = FormStartPosition.CenterScreen;
                initNumOfChancesButton();
                initStartButton();
            }

        private void initNumOfChancesButton()
        {
            Button numOfChances = new Button();

            numOfChances.Text = string.Format("{0}{1}", k_NumOfGuessesButtonText, m_NumOfGuesses);
            numOfChances.Size = new Size(k_ChanceButtonWidth, k_ButtonHight);
            numOfChances.Location = new Point(k_MarginSize, k_MarginSize);
            this.Controls.Add(numOfChances);
            numOfChances.Click += new EventHandler(numOfChancesButton_Click);
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

            private void numOfChancesButton_Click(object i_Sender, EventArgs i_E)
            {
                if (m_NumOfGuesses < k_MaximalNumOfGuesses)
                {
                    m_NumOfGuesses++;
                }
                else
                {
                    m_NumOfGuesses = k_MinimalNumOfGuesses;
                }

                (i_Sender as Button).Text = string.Format("{0}{1}", k_NumOfGuessesButtonText, m_NumOfGuesses);
            }
        }
}
