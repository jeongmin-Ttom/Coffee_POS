﻿namespace Coffee_POS
{
    partial class Change
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
            this.PWD1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PWD2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(25, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "이전 비밀번호";
            // 
            // PWD1
            // 
            this.PWD1.Location = new System.Drawing.Point(166, 34);
            this.PWD1.Multiline = true;
            this.PWD1.Name = "PWD1";
            this.PWD1.Size = new System.Drawing.Size(204, 28);
            this.PWD1.TabIndex = 1;
            this.PWD1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(25, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "비밀번호";
            // 
            // PWD2
            // 
            this.PWD2.Location = new System.Drawing.Point(166, 77);
            this.PWD2.Multiline = true;
            this.PWD2.Name = "PWD2";
            this.PWD2.Size = new System.Drawing.Size(204, 28);
            this.PWD2.TabIndex = 1;
            this.PWD2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(99, 126);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(177, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "변경하기";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(389, 199);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PWD2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PWD1);
            this.Controls.Add(this.label1);
            this.Name = "Change";
            this.Text = "Change";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PWD1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PWD2;
        private System.Windows.Forms.Button button1;
    }
}