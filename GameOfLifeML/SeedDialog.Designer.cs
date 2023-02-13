
namespace GameOfLifeML
{
    partial class SeedDialog
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
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SeedLabel = new System.Windows.Forms.Label();
            this.SeedNum = new System.Windows.Forms.NumericUpDown();
            this.RandomizeSeed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SeedNum)).BeginInit();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(301, 182);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(98, 36);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(405, 182);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(98, 36);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // SeedLabel
            // 
            this.SeedLabel.AutoSize = true;
            this.SeedLabel.Location = new System.Drawing.Point(111, 89);
            this.SeedLabel.Name = "SeedLabel";
            this.SeedLabel.Size = new System.Drawing.Size(47, 20);
            this.SeedLabel.TabIndex = 2;
            this.SeedLabel.Text = "Seed";
            // 
            // SeedNum
            // 
            this.SeedNum.Location = new System.Drawing.Point(173, 87);
            this.SeedNum.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.SeedNum.Minimum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            -2147483648});
            this.SeedNum.Name = "SeedNum";
            this.SeedNum.Size = new System.Drawing.Size(134, 26);
            this.SeedNum.TabIndex = 3;
            // 
            // RandomizeSeed
            // 
            this.RandomizeSeed.Location = new System.Drawing.Point(324, 87);
            this.RandomizeSeed.Name = "RandomizeSeed";
            this.RandomizeSeed.Size = new System.Drawing.Size(123, 26);
            this.RandomizeSeed.TabIndex = 4;
            this.RandomizeSeed.Text = "Randomize";
            this.RandomizeSeed.UseVisualStyleBackColor = true;
            this.RandomizeSeed.Click += new System.EventHandler(this.RandomizeSeed_Click);
            // 
            // SeedDialog
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(518, 233);
            this.Controls.Add(this.RandomizeSeed);
            this.Controls.Add(this.SeedNum);
            this.Controls.Add(this.SeedLabel);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeedDialog";
            this.Text = "Seed";
            ((System.ComponentModel.ISupportInitialize)(this.SeedNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label SeedLabel;
        private System.Windows.Forms.NumericUpDown SeedNum;
        private System.Windows.Forms.Button RandomizeSeed;
    }
}