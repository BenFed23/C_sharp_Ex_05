using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public partial class FormGameSettings : Form
    {
        public FormGameSettings()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void formGameSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void checkBoxPlayAgainstHuman_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
            textBox2.Text = checkBox1.Checked ? string.Empty : "Computer";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string player1Name;
            string player2Name;
            bool isAgainstComputer;
            int boardSize;

            getGameSettingsInputs(out player1Name, out player2Name, out isAgainstComputer, out boardSize);
            if (player1Name.Trim().Length == 0 || player2Name.Trim().Length == 0)
            {
                MessageBox.Show("Please enter valid names for both players.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                launchGame(player1Name, player2Name, isAgainstComputer, boardSize);
            }
        }

        private void getGameSettingsInputs(out string o_Player1Name, out string o_Player2Name, out bool o_IsAgainstComputer, out int o_BoardSize)
        {
            o_Player1Name = textBox1.Text;
            o_IsAgainstComputer = !checkBox1.Checked;
            o_BoardSize = (int)RowSize.Value;
            o_Player2Name = o_IsAgainstComputer ? "Computer" : textBox2.Text;
        }

        private void launchGame(string i_Player1Name, string i_Player2Name, bool i_IsAgainstComputer, int i_BoardSize)
        {
            GameManager newGameManager = new GameManager(i_Player1Name, i_Player2Name, i_IsAgainstComputer, i_BoardSize);

            Hide();
            FormGame newGameDisplay = new FormGame(newGameManager);
            newGameDisplay.ShowDialog();
            Close();
        }

        private void numericUpDownRowSize_ValueChanged(object sender, EventArgs e)
        {
            if (ColumnSize.Value != RowSize.Value)
            {
                ColumnSize.Value = RowSize.Value;
            }
        }

        private void numericUpDownColumnSize_ValueChanged(object sender, EventArgs e)
        {
            if (RowSize.Value != ColumnSize.Value)
            {
                RowSize.Value = ColumnSize.Value;
            }
        }
    }
}
