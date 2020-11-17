
namespace Sudoku
{
    partial class NumpadGrid4Dialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumpadGrid4Dialog));
            this.btnNumber1 = new System.Windows.Forms.Button();
            this.btnNumber2 = new System.Windows.Forms.Button();
            this.btnNumber3 = new System.Windows.Forms.Button();
            this.btnNumber4 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNumber1
            // 
            this.btnNumber1.BackColor = System.Drawing.Color.DarkGreen;
            this.btnNumber1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNumber1.FlatAppearance.BorderSize = 0;
            this.btnNumber1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNumber1.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnNumber1.Location = new System.Drawing.Point(18, 12);
            this.btnNumber1.Name = "btnNumber1";
            this.btnNumber1.Size = new System.Drawing.Size(56, 29);
            this.btnNumber1.TabIndex = 0;
            this.btnNumber1.Text = "1";
            this.btnNumber1.UseVisualStyleBackColor = false;
            this.btnNumber1.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // btnNumber2
            // 
            this.btnNumber2.BackColor = System.Drawing.Color.DarkGreen;
            this.btnNumber2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNumber2.FlatAppearance.BorderSize = 0;
            this.btnNumber2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNumber2.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnNumber2.Location = new System.Drawing.Point(96, 12);
            this.btnNumber2.Name = "btnNumber2";
            this.btnNumber2.Size = new System.Drawing.Size(56, 29);
            this.btnNumber2.TabIndex = 1;
            this.btnNumber2.Text = "2";
            this.btnNumber2.UseVisualStyleBackColor = false;
            this.btnNumber2.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // btnNumber3
            // 
            this.btnNumber3.BackColor = System.Drawing.Color.DarkGreen;
            this.btnNumber3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNumber3.FlatAppearance.BorderSize = 0;
            this.btnNumber3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNumber3.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnNumber3.Location = new System.Drawing.Point(18, 57);
            this.btnNumber3.Name = "btnNumber3";
            this.btnNumber3.Size = new System.Drawing.Size(56, 29);
            this.btnNumber3.TabIndex = 2;
            this.btnNumber3.Text = "3";
            this.btnNumber3.UseVisualStyleBackColor = false;
            this.btnNumber3.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // btnNumber4
            // 
            this.btnNumber4.BackColor = System.Drawing.Color.DarkGreen;
            this.btnNumber4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNumber4.FlatAppearance.BorderSize = 0;
            this.btnNumber4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNumber4.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnNumber4.Location = new System.Drawing.Point(96, 57);
            this.btnNumber4.Name = "btnNumber4";
            this.btnNumber4.Size = new System.Drawing.Size(56, 29);
            this.btnNumber4.TabIndex = 3;
            this.btnNumber4.Text = "4";
            this.btnNumber4.UseVisualStyleBackColor = false;
            this.btnNumber4.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Maroon;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnClear.Location = new System.Drawing.Point(88, 107);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 29);
            this.btnClear.TabIndex = 9;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.GhostWhite;
            this.btnCancel.Location = new System.Drawing.Point(14, 107);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 29);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // NumpadGrid4Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(171, 149);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnNumber4);
            this.Controls.Add(this.btnNumber3);
            this.Controls.Add(this.btnNumber2);
            this.Controls.Add(this.btnNumber1);
            this.Font = new System.Drawing.Font("Maiandra GD", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NumpadGrid4Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Number";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNumber1;
        private System.Windows.Forms.Button btnNumber2;
        private System.Windows.Forms.Button btnNumber3;
        private System.Windows.Forms.Button btnNumber4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
    }
}