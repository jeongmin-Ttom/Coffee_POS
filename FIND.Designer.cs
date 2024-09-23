namespace Coffee_POS
{
    partial class FIND
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
            this.Name = new System.Windows.Forms.TextBox();
            this.Find_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(22, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "사용자 이름";
            // 
            // Name
            // 
            this.Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name.Location = new System.Drawing.Point(138, 20);
            this.Name.Multiline = true;
            this.Name.Name = "Name";
            this.Name.Size = new System.Drawing.Size(159, 34);
            this.Name.TabIndex = 1;
            this.Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Find_Button
            // 
            this.Find_Button.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Find_Button.Location = new System.Drawing.Point(104, 76);
            this.Find_Button.Name = "Find_Button";
            this.Find_Button.Size = new System.Drawing.Size(110, 42);
            this.Find_Button.TabIndex = 2;
            this.Find_Button.Text = "찾기";
            this.Find_Button.UseVisualStyleBackColor = true;
            this.Find_Button.Click += new System.EventHandler(this.Find_Button_Click);
            // 
            // FIND
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(323, 144);
            this.Controls.Add(this.Find_Button);
            this.Controls.Add(this.Name);
            this.Controls.Add(this.label1);
            this.Text = "FIND";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Name;
        private System.Windows.Forms.Button Find_Button;
    }
}