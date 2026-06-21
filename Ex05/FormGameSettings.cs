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
                textBox2.Text = "[Computer]";
            }
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            bool isAgasinstComputer = false;
           string player1Name = textBox1.Text;
           string player2Name = textBox2.Text;
           int bordSize = (int)RowSIze.Value;
            if (!checkBox1.Checked)
            {
                isAgasinstComputer = true;
            }
           Game newGame = new Game( player1Name, player2Name , isAgasinstComputer, bordSize );
           this.Hide();
           FormGame NewGameDisplay = new FormGame(newGame );
           NewGameDisplay.ShowDialog();
            this.Close();

        }
        private void RowSIze_ValueChanged(object sender, EventArgs e)
        {
            ColomSize.Value = RowSIze.Value;
        }
        private void ColomSize_ValueChanged(object sender, EventArgs e)
        {
            
            RowSIze.Value = ColomSize.Value;
        }
    }
}
