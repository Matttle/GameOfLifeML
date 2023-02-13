
namespace GameOfLifeML
{
    partial class CustomDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.TimeInterval = new System.Windows.Forms.NumericUpDown();
            this.UniverseWidth = new System.Windows.Forms.NumericUpDown();
            this.UniverseHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Accept = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TimeInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UniverseWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UniverseHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(110, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Time Interval (Milliseconds)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TimeInterval
            // 
            this.TimeInterval.Location = new System.Drawing.Point(314, 70);
            this.TimeInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.TimeInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TimeInterval.Name = "TimeInterval";
            this.TimeInterval.Size = new System.Drawing.Size(60, 26);
            this.TimeInterval.TabIndex = 1;
            this.TimeInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TimeInterval.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // UniverseWidth
            // 
            this.UniverseWidth.Location = new System.Drawing.Point(314, 102);
            this.UniverseWidth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.UniverseWidth.Name = "UniverseWidth";
            this.UniverseWidth.Size = new System.Drawing.Size(60, 26);
            this.UniverseWidth.TabIndex = 2;
            // 
            // UniverseHeight
            // 
            this.UniverseHeight.Location = new System.Drawing.Point(314, 134);
            this.UniverseHeight.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.UniverseHeight.Name = "UniverseHeight";
            this.UniverseHeight.Size = new System.Drawing.Size(60, 26);
            this.UniverseHeight.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Universe Width (By Cells)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Universe Height (By Cells)";
            // 
            // Accept
            // 
            this.Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Accept.Location = new System.Drawing.Point(291, 249);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 32);
            this.Accept.TabIndex = 6;
            this.Accept.Text = "OK";
            this.Accept.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(372, 249);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 32);
            this.Cancel.TabIndex = 7;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CustomDialog
            // 
            this.AcceptButton = this.Accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(483, 293);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UniverseHeight);
            this.Controls.Add(this.UniverseWidth);
            this.Controls.Add(this.TimeInterval);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.TimeInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UniverseWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UniverseHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown TimeInterval;
        private System.Windows.Forms.NumericUpDown UniverseWidth;
        private System.Windows.Forms.NumericUpDown UniverseHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cancel;
    }
}