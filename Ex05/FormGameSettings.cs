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
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void PlayAgainstHumanChecked(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            {
                textBox2.Enabled = true;
                textBox2.Text = string.Empty;
            }
            else 
            {
                textBox2.Enabled = false;
                textBox2.Text = "Computer";
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            string player1Name;
            string player2Name;
            bool isAgainstComputer;
            int boardSize;

            getGameSettingsInputs(out player1Name, out player2Name, out isAgainstComputer, out boardSize);
            launchGame(player1Name, player2Name, isAgainstComputer, boardSize);
        }

        private void getGameSettingsInputs(out string o_Player1Name, out string o_Player2Name, out bool o_IsAgainstComputer, out int o_BoardSize)
        {
            o_Player1Name = textBox1.Text;
            o_IsAgainstComputer = !checkBox1.Checked;
            o_BoardSize = (int)RowSize.Value;
            if (o_IsAgainstComputer)
            {
                o_Player2Name = "Computer";
            }
            else
            {
                o_Player2Name = textBox2.Text;
            }
        }

        private void launchGame(string i_Player1Name, string i_Player2Name, bool i_IsAgainstComputer, int i_BoardSize)
        {
            Game newGame = new Game(i_Player1Name, i_Player2Name, i_IsAgainstComputer, i_BoardSize);

            this.Hide();
            FormGame newGameDisplay = new FormGame(newGame);
            newGameDisplay.ShowDialog();
            this.Close();
        }

        private void RowSize_ValueChanged(object sender, EventArgs e)
        {
            ColumnSize.Value = RowSize.Value;
        }

        private void ColumnSize_ValueChanged(object sender, EventArgs e)
        {    
            RowSize.Value = ColumnSize.Value;
        }
    }
}
