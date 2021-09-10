namespace EndTermClient
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.Exitbutton = new System.Windows.Forms.Button();
            this.ReglinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.PasstextBox = new System.Windows.Forms.TextBox();
            this.Loginbutton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.UsrtextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Exitbutton
            // 
            this.Exitbutton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.Exitbutton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exitbutton.ForeColor = System.Drawing.Color.White;
            this.Exitbutton.Location = new System.Drawing.Point(226, 264);
            this.Exitbutton.Name = "Exitbutton";
            this.Exitbutton.Size = new System.Drawing.Size(105, 38);
            this.Exitbutton.TabIndex = 13;
            this.Exitbutton.Text = "Exit";
            this.Exitbutton.UseVisualStyleBackColor = false;
            this.Exitbutton.Click += new System.EventHandler(this.Exitbutton_Click);
            // 
            // ReglinkLabel1
            // 
            this.ReglinkLabel1.AutoSize = true;
            this.ReglinkLabel1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReglinkLabel1.Location = new System.Drawing.Point(27, 320);
            this.ReglinkLabel1.Name = "ReglinkLabel1";
            this.ReglinkLabel1.Size = new System.Drawing.Size(386, 25);
            this.ReglinkLabel1.TabIndex = 14;
            this.ReglinkLabel1.TabStop = true;
            this.ReglinkLabel1.Text = "Don\'t have account? Create an account now.";
            this.ReglinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ReglinkLabel1_LinkClicked);
            // 
            // PasstextBox
            // 
            this.PasstextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasstextBox.ForeColor = System.Drawing.Color.Silver;
            this.PasstextBox.Location = new System.Drawing.Point(95, 180);
            this.PasstextBox.Name = "PasstextBox";
            this.PasstextBox.Size = new System.Drawing.Size(249, 33);
            this.PasstextBox.TabIndex = 9;
            this.PasstextBox.Text = "Password";
            this.PasstextBox.Enter += new System.EventHandler(this.PasstextBox_Enter);
            this.PasstextBox.Leave += new System.EventHandler(this.PasstextBox_Leave);
            // 
            // Loginbutton
            // 
            this.Loginbutton.BackColor = System.Drawing.Color.MediumAquamarine;
            this.Loginbutton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loginbutton.ForeColor = System.Drawing.Color.White;
            this.Loginbutton.Location = new System.Drawing.Point(110, 264);
            this.Loginbutton.Name = "Loginbutton";
            this.Loginbutton.Size = new System.Drawing.Size(105, 38);
            this.Loginbutton.TabIndex = 12;
            this.Loginbutton.Text = "Login";
            this.Loginbutton.UseVisualStyleBackColor = false;
            this.Loginbutton.Click += new System.EventHandler(this.Loginbutton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(226, 228);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(118, 21);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Show Password";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // UsrtextBox
            // 
            this.UsrtextBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsrtextBox.ForeColor = System.Drawing.Color.Silver;
            this.UsrtextBox.Location = new System.Drawing.Point(95, 122);
            this.UsrtextBox.Name = "UsrtextBox";
            this.UsrtextBox.Size = new System.Drawing.Size(249, 33);
            this.UsrtextBox.TabIndex = 8;
            this.UsrtextBox.Text = "Username";
            this.UsrtextBox.Enter += new System.EventHandler(this.UsrtextBox_Enter);
            this.UsrtextBox.Leave += new System.EventHandler(this.UsrtextBox_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(174, 31);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 60);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 385);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Exitbutton);
            this.Controls.Add(this.ReglinkLabel1);
            this.Controls.Add(this.PasstextBox);
            this.Controls.Add(this.Loginbutton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.UsrtextBox);
            this.Name = "LoginForm";
            this.Text = "LoginV1.0";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Exitbutton;
        private System.Windows.Forms.LinkLabel ReglinkLabel1;
        private System.Windows.Forms.TextBox PasstextBox;
        private System.Windows.Forms.Button Loginbutton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox UsrtextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

