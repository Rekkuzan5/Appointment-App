
namespace Appointment_App
{
    partial class AddAppointment
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
            this.dateTimePicker3 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateLabel = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.titleLabel = new System.Windows.Forms.Label();
            this.CreateAppointmentButton = new System.Windows.Forms.Button();
            this.AppointmentCancelButton = new System.Windows.Forms.Button();
            this.typeLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.endLabel = new System.Windows.Forms.Label();
            this.custLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.formLabel = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dateTimePicker3
            // 
            this.dateTimePicker3.CustomFormat = "hh:mm tt";
            this.dateTimePicker3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker3.Location = new System.Drawing.Point(158, 329);
            this.dateTimePicker3.Name = "dateTimePicker3";
            this.dateTimePicker3.ShowUpDown = true;
            this.dateTimePicker3.Size = new System.Drawing.Size(125, 24);
            this.dateTimePicker3.TabIndex = 13;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "hh:mm tt";
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(19, 329);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(125, 24);
            this.dateTimePicker2.TabIndex = 13;
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateLabel.Location = new System.Drawing.Point(16, 234);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(39, 18);
            this.dateLabel.TabIndex = 12;
            this.dateLabel.Text = "Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(19, 263);
            this.dateTimePicker1.MinimumSize = new System.Drawing.Size(123, 26);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(125, 26);
            this.dateTimePicker1.TabIndex = 11;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(16, 161);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(72, 18);
            this.titleLabel.TabIndex = 10;
            this.titleLabel.Text = "Appt. Title";
            // 
            // CreateAppointmentButton
            // 
            this.CreateAppointmentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateAppointmentButton.Location = new System.Drawing.Point(49, 391);
            this.CreateAppointmentButton.Name = "CreateAppointmentButton";
            this.CreateAppointmentButton.Size = new System.Drawing.Size(100, 30);
            this.CreateAppointmentButton.TabIndex = 8;
            this.CreateAppointmentButton.Text = "Create";
            this.CreateAppointmentButton.UseVisualStyleBackColor = true;
            this.CreateAppointmentButton.Click += new System.EventHandler(this.CreateAppointmentButton_Click);
            // 
            // AppointmentCancelButton
            // 
            this.AppointmentCancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AppointmentCancelButton.Location = new System.Drawing.Point(155, 391);
            this.AppointmentCancelButton.Name = "AppointmentCancelButton";
            this.AppointmentCancelButton.Size = new System.Drawing.Size(100, 30);
            this.AppointmentCancelButton.TabIndex = 8;
            this.AppointmentCancelButton.Text = "Cancel";
            this.AppointmentCancelButton.UseVisualStyleBackColor = true;
            this.AppointmentCancelButton.Click += new System.EventHandler(this.AppointmentCancelButton_Click);
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(153, 234);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(77, 18);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Appt. Type";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLabel.Location = new System.Drawing.Point(18, 300);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(76, 18);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = "Appt. Start";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endLabel.Location = new System.Drawing.Point(153, 300);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(71, 18);
            this.endLabel.TabIndex = 4;
            this.endLabel.Text = "Appt. End";
            // 
            // custLabel
            // 
            this.custLabel.AutoSize = true;
            this.custLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custLabel.Location = new System.Drawing.Point(16, 95);
            this.custLabel.Name = "custLabel";
            this.custLabel.Size = new System.Drawing.Size(63, 18);
            this.custLabel.TabIndex = 4;
            this.custLabel.Text = "Member";
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.IntegralHeight = false;
            this.typeComboBox.Items.AddRange(new object[] {
            "Online",
            "On-site",
            "Phone"});
            this.typeComboBox.Location = new System.Drawing.Point(158, 263);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(123, 26);
            this.typeComboBox.TabIndex = 1;
            // 
            // customerComboBox
            // 
            this.customerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.customerComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(21, 124);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(260, 26);
            this.customerComboBox.TabIndex = 1;
            // 
            // formLabel
            // 
            this.formLabel.AutoSize = true;
            this.formLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formLabel.Location = new System.Drawing.Point(13, 40);
            this.formLabel.Name = "formLabel";
            this.formLabel.Size = new System.Drawing.Size(221, 31);
            this.formLabel.TabIndex = 0;
            this.formLabel.Text = "Add Appointment";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTextBox.Location = new System.Drawing.Point(19, 190);
            this.titleTextBox.MinimumSize = new System.Drawing.Size(123, 26);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(262, 24);
            this.titleTextBox.TabIndex = 9;
            // 
            // AddAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 433);
            this.Controls.Add(this.dateTimePicker3);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.CreateAppointmentButton);
            this.Controls.Add(this.AppointmentCancelButton);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.endLabel);
            this.Controls.Add(this.custLabel);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.formLabel);
            this.Name = "AddAppointment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Appointment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker3;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button CreateAppointmentButton;
        private System.Windows.Forms.Button AppointmentCancelButton;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Label custLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.Label formLabel;
        private System.Windows.Forms.TextBox titleTextBox;
    }
}