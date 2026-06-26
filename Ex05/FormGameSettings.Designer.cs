using System;

namespace Ex05
{
    partial class FormGameSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PlayersLabel = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.boardSizeLabel = new System.Windows.Forms.Label();
            this.RowsLabel = new System.Windows.Forms.Label();
            this.RowSize = new System.Windows.Forms.NumericUpDown();
            this.ColumnSize = new System.Windows.Forms.NumericUpDown();
            this.ColomsLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnSize)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayersLabel
            // 
            this.PlayersLabel.AutoSize = true;
            this.PlayersLabel.Location = new System.Drawing.Point(34, 29);
            this.PlayersLabel.Name = "PlayersLabel";
            this.PlayersLabel.Size = new System.Drawing.Size(68, 20);
            this.PlayersLabel.TabIndex = 0;
            this.PlayersLabel.Text = "Players: ";
            
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.Location = new System.Drawing.Point(48, 62);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(69, 20);
            this.Player1Label.TabIndex = 1;
            this.Player1Label.Text = "Player 1:";
           
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(153, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(154, 26);
            this.textBox1.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(52, 111);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(22, 21);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.PlayAgainstHumanChecked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(80, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Player 2:";
            
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(155, 111);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(152, 26);
            this.textBox2.TabIndex = 5;
            this.textBox2.Text = "[Computer]";
          
            // 
            // boardSizeLabel
            // 
            this.boardSizeLabel.AutoSize = true;
            this.boardSizeLabel.Location = new System.Drawing.Point(34, 174);
            this.boardSizeLabel.Name = "boardSizeLabel";
            this.boardSizeLabel.Size = new System.Drawing.Size(92, 20);
            this.boardSizeLabel.TabIndex = 6;
            this.boardSizeLabel.Text = "Board size: ";
           
            // 
            // RowsLabel
            // 
            this.RowsLabel.AutoSize = true;
            this.RowsLabel.Location = new System.Drawing.Point(45, 220);
            this.RowsLabel.Name = "RowsLabel";
            this.RowsLabel.Size = new System.Drawing.Size(57, 20);
            this.RowsLabel.TabIndex = 7;
            this.RowsLabel.Text = "Rows: ";
            // 
            // RowSize
            // 
            this.RowSize.Location = new System.Drawing.Point(242, 220);
            this.RowSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RowSize.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.RowSize.Name = "RowSize";
            this.RowSize.Size = new System.Drawing.Size(52, 26);
            this.RowSize.TabIndex = 8;
            this.RowSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.RowSize.ValueChanged += new System.EventHandler(this.RowSize_ValueChanged);
            // 
            // ColumnSize
            // 
            this.ColumnSize.Location = new System.Drawing.Point(108, 220);
            this.ColumnSize.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ColumnSize.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.ColumnSize.Name = "ColumnSize";
            this.ColumnSize.Size = new System.Drawing.Size(50, 26);
            this.ColumnSize.TabIndex = 9;
            this.ColumnSize.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.ColumnSize.ValueChanged += new System.EventHandler(this.ColumnSize_ValueChanged);
            // 
            // ColomsLabel
            // 
            this.ColomsLabel.AutoSize = true;
            this.ColomsLabel.Location = new System.Drawing.Point(188, 222);
            this.ColomsLabel.Name = "ColomsLabel";
            this.ColomsLabel.Size = new System.Drawing.Size(48, 20);
            this.ColomsLabel.TabIndex = 10;
            this.ColomsLabel.Text = "Cols: ";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(52, 283);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(258, 31);
            this.StartButton.TabIndex = 11;
            this.StartButton.Text = "Start! ";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 344);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ColomsLabel);
            this.Controls.Add(this.ColumnSize);
            this.Controls.Add(this.RowSize);
            this.Controls.Add(this.RowsLabel);
            this.Controls.Add(this.boardSizeLabel);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Player1Label);
            this.Controls.Add(this.PlayersLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.FormGameSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion

        private System.Windows.Forms.Label PlayersLabel;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label boardSizeLabel;
        private System.Windows.Forms.Label RowsLabel;
        private System.Windows.Forms.NumericUpDown RowSize;
        private System.Windows.Forms.NumericUpDown ColumnSize;
        private System.Windows.Forms.Label ColomsLabel;
        private System.Windows.Forms.Button StartButton;
    }
}