using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BoolPgiaLogic;

namespace BoolPgiaUI
{
    public class GameForm : Form
    { 
        private const string k_FormText = "Bool Pgia";
        private const int k_GuessButtonWidth = 45;
        private const int k_GuessButtonHeight = k_GuessButtonWidth;
        private const int k_ResultButtonWidth = 20;
        private const int k_ResultButtonHeight = k_ResultButtonWidth;
        private const int k_RelativeDistance = 5;
        private const bool k_ButtonEnabled = true;

        private readonly Size r_CheckResultButtonSize = new Size(k_GuessButtonWidth, k_GuessButtonHeight/2);
        private readonly Size r_GuessButtonSize = new Size(k_GuessButtonWidth, k_GuessButtonHeight);
        private readonly Size r_ResultButtonSize = new Size(k_ResultButtonWidth, k_ResultButtonHeight);
        private readonly List<Color> r_PossibleColors;
        private readonly int r_NumberOfDifferentColorsInGuess;
        private readonly int r_NumOfGuesses;
        private readonly FormOfColorChoosing r_FormOfColorChoosing = new FormOfColorChoosing();
        private readonly GameLogic<Color> r_GameLogic;

        private Button[] m_SequenceRow;
        private int m_CurrentLine = 0;
        private LineOfButtons[] m_RowsOfGuesses;

        public GameForm()
        {
            r_PossibleColors = r_FormOfColorChoosing.ChoicesOfColors;
            r_GameLogic = new GameLogic<Color>(r_PossibleColors);
            r_GameLogic.GenerateGame();
            r_NumberOfDifferentColorsInGuess = r_GameLogic.NumOfGuesses;
            r_NumOfGuesses = getNumberOfGuesses();
            if (r_NumOfGuesses != -1)
            {
                initFormData();
                initButtons();
                this.ShowDialog();
            }
            else
            {
                Close();
            }
        }

        private void initFormData()
        {
            Width = (int)((r_NumberOfDifferentColorsInGuess + 2) * k_GuessButtonWidth) + ((k_RelativeDistance * r_NumberOfDifferentColorsInGuess) + 1) + ((k_ResultButtonWidth + k_RelativeDistance) * ((r_NumberOfDifferentColorsInGuess + 1) / 2));
            Height = (k_GuessButtonWidth * 3) + ((k_GuessButtonHeight + k_RelativeDistance) * r_NumOfGuesses);
            this.Text = k_FormText;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private int getNumberOfGuesses()
        {
            int numOfGuesses;
            GetChancesForm numOfGuessesForm = new GetChancesForm();

            numOfGuessesForm.ShowDialog();
            numOfGuesses = numOfGuessesForm.DialogResult == DialogResult.OK ? numOfGuessesForm.NumOfGuesses : -1;

            return numOfGuesses;
        }

        private void initButtons()
        {
            createButtonLines();
            initRealSequenceButtonRow();
        }

        private void createButtonLines()
        {
            m_RowsOfGuesses = new LineOfButtons[r_NumOfGuesses];
            int topOfCurrentLine = k_GuessButtonWidth * 2;
            for (int i = 0; i < m_RowsOfGuesses.Length; i++)
            {
                m_RowsOfGuesses[i] = new LineOfButtons(r_GuessButtonSize, r_ResultButtonSize, r_CheckResultButtonSize, r_NumberOfDifferentColorsInGuess);
                m_RowsOfGuesses[i].AddEventToGuessButtons(new EventHandler(guessButton_Click));
                m_RowsOfGuesses[i].AddEventToCheckResultButton(new EventHandler(checkResultButton_Click));
                m_RowsOfGuesses[i].AddLineToForm(this, topOfCurrentLine);
                topOfCurrentLine += k_RelativeDistance + k_GuessButtonWidth;
            }

            m_RowsOfGuesses[0].EnableAllGuessButtons();
        }

        private void initRealSequenceButtonRow()
        {
            Button currentButton;

            m_SequenceRow = new Button[r_NumberOfDifferentColorsInGuess];
            for (int i = 0; i < r_NumberOfDifferentColorsInGuess; i++)
            {
                currentButton = new Button();
                InitButton(currentButton, k_GuessButtonWidth, k_GuessButtonWidth, k_ResultButtonWidth + (i * (k_GuessButtonWidth + k_RelativeDistance)), k_ResultButtonWidth);
                currentButton.BackColor = Color.Black;
                this.Controls.Add(currentButton);
                m_SequenceRow[i] = currentButton;
            }
        }

        private void checkResultButton_Click(object i_Sender, EventArgs i_E)
        {
            int bullsHitscounter = 0;
            Guess guessResult = r_GameLogic.CheckGuess(getCurrentLineGuessButtonsColors());

            while (bullsHitscounter < guessResult.Bulls)
            {
                m_RowsOfGuesses[m_CurrentLine].PaintResultButtons(Color.Black, bullsHitscounter);
                bullsHitscounter++;
            }

            while (bullsHitscounter < guessResult.Hits + guessResult.Bulls)
            {
                m_RowsOfGuesses[m_CurrentLine].PaintResultButtons(Color.Yellow, bullsHitscounter);
                bullsHitscounter++;
            }

            nextGuessHandler(guessResult.Bulls == r_NumberOfDifferentColorsInGuess);
        }

        private void displayRealSequence()
        {
            Color[] realSequence = r_GameLogic.ChosenSequence;

            for (int i = 0; i < m_SequenceRow.Length; i++)
            {
                m_SequenceRow[i].BackColor = realSequence[i];
            }
            NewGameForm newGameForm = new NewGameForm();
            newGameForm.ShowDialog();
            newGameForm.Close();
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            new GameForm();
        }

        private void nextGuessHandler(bool i_IsUserWon)
        {
            m_RowsOfGuesses[m_CurrentLine].DisableLine();
            if (!i_IsUserWon && m_CurrentLine < r_NumOfGuesses-1)
            {
                m_CurrentLine++;
                m_RowsOfGuesses[m_CurrentLine].EnableAllGuessButtons();
            }
            else
            {
                displayRealSequence();
            }
        }

        private void guessButton_Click(object i_Sender, EventArgs i_E)
        {
            Button clickedButton = i_Sender as Button;

            r_FormOfColorChoosing.ShowDialog();
            if (r_FormOfColorChoosing.DialogResult == DialogResult.OK)
            {
                clickedButton.BackColor = r_FormOfColorChoosing.SelectedColor;
            }

            r_FormOfColorChoosing.DialogResult = DialogResult.None;
            if (isCurrentLinesGuessIsValid())
            {
                m_RowsOfGuesses[m_CurrentLine].EnableCheckResultButton(true);
            }
        }

        private Color[] getCurrentLineGuessButtonsColors()
        {
            Color[] guessedColors = new Color[r_NumberOfDifferentColorsInGuess];

            for (int i = 0; i < r_NumberOfDifferentColorsInGuess; i++)
            {
                guessedColors[i] = m_RowsOfGuesses[m_CurrentLine].GuessButtons[i].BackColor;
            }

            return guessedColors;
        }

        private bool isCurrentLinesGuessIsValid()
        {
            bool isValidGuess = true;
            Color[] guessedColors = getCurrentLineGuessButtonsColors();

            for (int i = 0; i < guessedColors.Length - 1; i++)
            {
                for (int j = i + 1; j < guessedColors.Length; j++)
                {
                    if (guessedColors[i] == guessedColors[j] || guessedColors[j] == Control.DefaultBackColor || guessedColors[i] == Control.DefaultBackColor)
                    {
                        isValidGuess = false;
                    }
                }
            }
            return isValidGuess;
        }

        public static void InitButton(Button i_Button, int i_ButtonWidth, int i_ButtonHeight, int i_ButtonLeft, int i_ButtonTop)
        {
            i_Button.Size = new Size(i_ButtonWidth, i_ButtonHeight);
            i_Button.Location = new Point(i_ButtonLeft, i_ButtonTop);
            i_Button.Enabled = !(k_ButtonEnabled);
        }

        public static void InitButton(Button i_Button, Size i_ButtonSize, int i_ButtonLeft, int i_ButtonTop)
        {
            InitButton(i_Button, i_ButtonSize.Width, i_ButtonSize.Height, i_ButtonLeft, i_ButtonTop);
        }

    }
}
