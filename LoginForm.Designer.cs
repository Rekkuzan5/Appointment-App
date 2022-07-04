
namespace Appointment_App
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
            this.userNameLoginTextBox = new System.Windows.Forms.TextBox();
            this.passwordLoginTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userNameLoginTextBox
            // 
            this.userNameLoginTextBox.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameLoginTextBox.Location = new System.Drawing.Point(92, 163);
            this.userNameLoginTextBox.Name = "userNameLoginTextBox";
            this.userNameLoginTextBox.Size = new System.Drawing.Size(162, 27);
            this.userNameLoginTextBox.TabIndex = 0;
            // 
            // passwordLoginTextBox
            // 
            this.passwordLoginTextBox.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordLoginTextBox.Location = new System.Drawing.Point(92, 196);
            this.passwordLoginTextBox.Name = "passwordLoginTextBox";
            this.passwordLoginTextBox.PasswordChar = '•';
            this.passwordLoginTextBox.Size = new System.Drawing.Size(162, 27);
            this.passwordLoginTextBox.TabIndex = 1;
            // 
            // LoginButton
            // 
            this.LoginButton.Font = new System.Drawing.Font("Poppins", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.Location = new System.Drawing.Point(92, 243);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(162, 40);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 62);
            this.label1.TabIndex = 5;
            this.label1.Text = "Log In";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.passwordLoginTextBox);
            this.Controls.Add(this.userNameLoginTextBox);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userNameLoginTextBox;
        private System.Windows.Forms.TextBox passwordLoginTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label1;
    }
}

