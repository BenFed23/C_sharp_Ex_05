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
            throw new NotImplementedException();
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
           string player1Name = textBox1.Text;
           bool twoPlayer = checkBox1.Checked;
           string player2Name = textBox2.Text;
           int bordSize = (int)RowSIze.Value;
           Game newGame = new Game( player1Name, player2Name ,twoPlayer , bordSize );
           this.Hide();
            // GameForm NewGameDisplay = new GameForm();
            //newBoard.ShowDialog();
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
