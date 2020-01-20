using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace BoolPgiaUI
{
    class FormOfColorChoosing : Form
    {
        private const string k_FormText = "Pick A Color:";
        private const int k_ButtonWidth = 48;
        private const int k_ButtonHeight = k_ButtonWidth;
        private const int k_ButtonsRelativeDistance = 5;
        private const int k_FormHeight = 150;

        private readonly List<Color> r_ChoicesOfColors = new List<Color> { Color.Purple, Color.Red, Color.LawnGreen, Color.Aqua, Color.Blue, Color.Yellow, Color.Brown, Color.White };
        private readonly Point r_StartPosition = new Point(k_ButtonsRelativeDistance , k_ButtonsRelativeDistance);
        private readonly Size r_ButtonSize = new Size(k_ButtonWidth, k_ButtonHeight);
        private readonly int r_NumOfButtons;

        private List<Button> m_ColoredButtons = new List<Button>();
        private Color m_SelectedColor;

        public FormOfColorChoosing()
        {
            r_NumOfButtons = r_ChoicesOfColors.Count;
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size((k_ButtonWidth + k_ButtonsRelativeDistance) * (r_ChoicesOfColors.Count / 2) + (2* k_ButtonsRelativeDistance), k_FormHeight);
            Text = k_FormText;
            initComponents();
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        public List<Color> ChoicesOfColors
        {
            get { return r_ChoicesOfColors; }
        }

        public Color SelectedColor
        {
            get { return m_SelectedColor; }
        }

        private void initButtons()
        {
            Button currentButton;

            for (int i = 0; i < r_NumOfButtons; i++)
            {
                currentButton = new Button();
                currentButton.Size = r_ButtonSize;
                currentButton.BackColor = r_ChoicesOfColors[i];
                currentButton.Click += new EventHandler(buttonColoredButton_Click);
                m_ColoredButtons.Add(currentButton);
                Controls.Add(currentButton);
            }
        }

        private void initButtonsPosition(int i_Index)
        {
            m_ColoredButtons[0].Location = r_StartPosition;
            for (int i = 1; i < m_ColoredButtons.Count; i++)
            {
                if (i != i_Index)
                {
                    m_ColoredButtons[i].Location = new Point(m_ColoredButtons[i - 1].Left + r_ButtonSize.Width + k_ButtonsRelativeDistance, m_ColoredButtons[i - 1].Top);
                }
                else
                {
                    m_ColoredButtons[i].Location = new Point(r_StartPosition.X, r_StartPosition.Y + r_ButtonSize.Height + k_ButtonsRelativeDistance);
                }
            }
        }

        private void initComponents()
        {
            initButtons();
            initButtonsPosition((r_NumOfButtons + 1) / 2);
        }

        private void buttonColoredButton_Click(object i_Sender, EventArgs i_E)
        {
            m_SelectedColor = (i_Sender as Button).BackColor;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormOfColorChoosing
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "FormOfColorChoosing";
            this.Load += new System.EventHandler(this.FormOfColorChoosing_Load);
            this.ResumeLayout(false);

        }

        private void FormOfColorChoosing_Load(object sender, EventArgs e)
        {

        }
    }
}
