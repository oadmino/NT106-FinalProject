namespace EndTermClient
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PasstextBox = new System.Windows.Forms.TextBox();
            this.UsrtextBox = new System.Windows.Forms.TextBox();
            this.ConfirmtextBox = new System.Windows.Forms.TextBox();
            this.Signbutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(174, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // PasstextBox
            // 
            this.PasstextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasstextBox.ForeColor = System.Drawing.Color.Silver;
            this.PasstextBox.Location = new System.Drawing.Point(95, 180);
            this.PasstextBox.Name = "PasstextBox";
            this.PasstextBox.Size = new System.Drawing.Size(249, 33);
            this.PasstextBox.TabIndex = 20;
            this.PasstextBox.Text = "Password";
            this.PasstextBox.Enter += new System.EventHandler(this.PasstextBox_Enter);
            this.PasstextBox.Leave += new System.EventHandler(this.PasstextBox_Leave);
            // 
            // UsrtextBox
            // 
            this.UsrtextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsrtextBox.ForeColor = System.Drawing.Color.Silver;
            this.UsrtextBox.Location = new System.Drawing.Point(95, 122);
            this.UsrtextBox.Name = "UsrtextBox";
            this.UsrtextBox.Size = new System.Drawing.Size(249, 33);
            this.UsrtextBox.TabIndex = 19;
            this.UsrtextBox.Text = "Username";
            this.UsrtextBox.Enter += new System.EventHandler(this.UsrtextBox_Enter);
            this.UsrtextBox.Leave += new System.EventHandler(this.UsrtextBox_Leave);
            // 
            // ConfirmtextBox
            // 
            this.ConfirmtextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmtextBox.ForeColor = System.Drawing.Color.Silver;
            this.ConfirmtextBox.Location = new System.Drawing.Point(95, 235);
            this.ConfirmtextBox.Name = "ConfirmtextBox";
            this.ConfirmtextBox.Size = new System.Drawing.Size(249, 33);
            this.ConfirmtextBox.TabIndex = 21;
            this.ConfirmtextBox.Text = "Password Confirm";
            this.ConfirmtextBox.Enter += new System.EventHandler(this.ConfirmtextBox_Enter);
            this.ConfirmtextBox.Leave += new System.EventHandler(this.ConfirmtextBox_Leave);
            // 
            // Signbutton
            // 
            this.Signbutton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.Signbutton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Signbutton.ForeColor = System.Drawing.Color.White;
            this.Signbutton.Location = new System.Drawing.Point(128, 294);
            this.Signbutton.Name = "Signbutton";
            this.Signbutton.Size = new System.Drawing.Size(186, 38);
            this.Signbutton.TabIndex = 22;
            this.Signbutton.Text = "SIGN UP FOR FREE";
            this.Signbutton.UseVisualStyleBackColor = false;
            this.Signbutton.Click += new System.EventHandler(this.Signbutton_Click);
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 385);
            this.Controls.Add(this.Signbutton);
            this.Controls.Add(this.ConfirmtextBox);
            this.Controls.Add(this.PasstextBox);
            this.Controls.Add(this.UsrtextBox);
            this.Controls.Add(this.pictureBox1);
            this.Name = "RegisterForm";
            this.Text = "RegisterForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox PasstextBox;
        private System.Windows.Forms.TextBox UsrtextBox;
        private System.Windows.Forms.TextBox ConfirmtextBox;
        private System.Windows.Forms.Button Signbutton;
    }
}