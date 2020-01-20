using System;
using System.Drawing;
using System.Windows.Forms;

namespace BoolPgiaUI
{
    class LineOfButtons
    {
        private const bool k_Enable = true;
        private const string k_CheckResultButtonIcon = "-->>";
        private const int k_RelativeDistance = 5;
        private readonly Button[] r_GuessButtons;
        private readonly Button[] r_ResultButtons;
        private readonly Button r_CheckResultButton;

        internal LineOfButtons(Size i_GuessButtonsSize, Size i_ResultButtonsSize, Size i_CheckResultButtonSize, int i_NumOFButtons)
        {
            r_GuessButtons = new Button[i_NumOFButtons];
            r_ResultButtons = new Button[i_NumOFButtons];
            r_CheckResultButton = new Button();
            InitalizeButtonsLine(i_GuessButtonsSize, i_ResultButtonsSize, i_CheckResultButtonSize);
        }

        internal Button[] GuessButtons
        {
            get { return r_GuessButtons; }
        }

        internal void PaintResultButtons(Color i_ColorToPaint, int i_IndexToPaint)
        {
            r_ResultButtons[i_IndexToPaint].BackColor = i_ColorToPaint;
        }

        internal void InitalizeButtonsLine(Size i_GuessButtonsSize, Size i_ResultButtonsSize, Size i_CheckResultButtonSize)
        {
            int currButtonsLeft;
            int currButtonsTop = 0;

            for (int i = 0; i < GuessButtons.Length; i++)
            {
                r_GuessButtons[i] = new Button();
                GameForm.InitButton(r_GuessButtons[i], i_GuessButtonsSize, i_ResultButtonsSize.Width + (i * (k_RelativeDistance + i_GuessButtonsSize.Width)), 0);
            }

            GameForm.InitButton(r_CheckResultButton, i_CheckResultButtonSize, i_GuessButtonsSize.Width + k_RelativeDistance + r_GuessButtons[GuessButtons.Length - 1].Left, i_GuessButtonsSize.Width / 4);
            r_CheckResultButton.Text = k_CheckResultButtonIcon;
            currButtonsLeft = r_CheckResultButton.Left + r_CheckResultButton.Width + (k_RelativeDistance * 2);
            for (int i = 0; i < r_ResultButtons.Length; i++)
            {
                r_ResultButtons[i] = new Button();
                GameForm.InitButton(r_ResultButtons[i], i_ResultButtonsSize, currButtonsLeft, currButtonsTop);
                if (i + 1 != (r_ResultButtons.Length + 1) / 2)
                {
                    currButtonsLeft += r_ResultButtons[i].Width + k_RelativeDistance;
                }
                else
                {
                    currButtonsLeft = r_CheckResultButton.Left + r_CheckResultButton.Width + (k_RelativeDistance * 2);
                    currButtonsTop = (i_GuessButtonsSize.Height + k_RelativeDistance) / 2;
                }
            }
        }

        private void enableAllGuessButtons(bool i_ToEnable)
        {
            foreach (Button guessButton in GuessButtons)
            {
                guessButton.Enabled = i_ToEnable;
            }
        }

        internal void EnableAllGuessButtons()
        {
            enableAllGuessButtons(k_Enable);
        }

        internal void EnableCheckResultButton(bool i_ToEnable)
        {
            r_CheckResultButton.Enabled = i_ToEnable;
        }

        internal void DisableLine()
        {
            enableAllGuessButtons(!k_Enable);
            EnableCheckResultButton(!k_Enable);
        }

        private void setLinesTop(int i_LinesTop)
        {
            foreach (Button guessButton in GuessButtons)
            {
                guessButton.Top += i_LinesTop;
            }

            foreach (Button resultButton in r_ResultButtons)
            {
                resultButton.Top += i_LinesTop;
            }

            r_CheckResultButton.Top += i_LinesTop;
        }

        internal void AddLineToForm(Form i_Form, int i_TopPosition)
        {
            setLinesTop(i_TopPosition);
            i_Form.Controls.AddRange(GuessButtons);
            i_Form.Controls.AddRange(r_ResultButtons);
            i_Form.Controls.Add(r_CheckResultButton);
        }

        internal void AddEventToGuessButtons(EventHandler i_ClickEvent)
        {
            addButtonsClickEvent(i_ClickEvent, GuessButtons);
        }

        internal void AddEventToCheckResultButton(EventHandler i_ClickEvent)
        {
            addButtonsClickEvent(i_ClickEvent, r_CheckResultButton);
        }

        private void addButtonsClickEvent(EventHandler i_ClickEvent, params Button[] i_Button)
        {
            foreach (Button button in i_Button)
            {
                button.Click += i_ClickEvent;
            }
        }
    }
}
