
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.customerComboBox = new System.Windows.Forms.ComboBox();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.custLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.startLabel = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.CreateAppointmentButton = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(13, 34);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(258, 48);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Add Appointment";
            // 
            // customerComboBox
            // 
            this.customerComboBox.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerComboBox.FormattingEnabled = true;
            this.customerComboBox.Location = new System.Drawing.Point(21, 131);
            this.customerComboBox.Name = "customerComboBox";
            this.customerComboBox.Size = new System.Drawing.Size(260, 34);
            this.customerComboBox.TabIndex = 1;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionTextBox.Location = new System.Drawing.Point(310, 131);
            this.descriptionTextBox.MinimumSize = new System.Drawing.Size(400, 100);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(400, 166);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // custLabel
            // 
            this.custLabel.AutoSize = true;
            this.custLabel.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.custLabel.Location = new System.Drawing.Point(16, 102);
            this.custLabel.Name = "custLabel";
            this.custLabel.Size = new System.Drawing.Size(87, 26);
            this.custLabel.TabIndex = 4;
            this.custLabel.Text = "Customer";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(16, 168);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(149, 26);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Appointment Type";
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descLabel.Location = new System.Drawing.Point(305, 102);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(200, 26);
            this.descLabel.TabIndex = 6;
            this.descLabel.Text = "Appointment Description";
            // 
            // startLabel
            // 
            this.startLabel.AutoSize = true;
            this.startLabel.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startLabel.Location = new System.Drawing.Point(155, 237);
            this.startLabel.Name = "startLabel";
            this.startLabel.Size = new System.Drawing.Size(90, 26);
            this.startLabel.TabIndex = 4;
            this.startLabel.Text = "Appt. Time";
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date.Location = new System.Drawing.Point(16, 234);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(47, 26);
            this.date.TabIndex = 4;
            this.date.Text = "Date";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(160, 263);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 34);
            this.comboBox1.TabIndex = 7;
            // 
            // CancelButton
            // 
            this.CancelButton.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(560, 337);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(150, 46);
            this.CancelButton.TabIndex = 8;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // CreateAppointmentButton
            // 
            this.CreateAppointmentButton.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateAppointmentButton.Location = new System.Drawing.Point(404, 337);
            this.CreateAppointmentButton.Name = "CreateAppointmentButton";
            this.CreateAppointmentButton.Size = new System.Drawing.Size(150, 46);
            this.CreateAppointmentButton.TabIndex = 8;
            this.CreateAppointmentButton.Text = "Create";
            this.CreateAppointmentButton.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "hh:mm tt";
            this.dateTimePicker1.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(21, 263);
            this.dateTimePicker1.MinimumSize = new System.Drawing.Size(121, 34);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 34);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // typeComboBox
            // 
            this.typeComboBox.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "Initial",
            "General",
            "Advanced",
            "Exit"});
            this.typeComboBox.Location = new System.Drawing.Point(21, 197);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(260, 34);
            this.typeComboBox.TabIndex = 1;
            // 
            // AddAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 401);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.CreateAppointmentButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.typeLabel);
            this.Controls.Add(this.date);
            this.Controls.Add(this.startLabel);
            this.Controls.Add(this.custLabel);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.customerComboBox);
            this.Controls.Add(this.titleLabel);
            this.Name = "AddAppointment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddAppointment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ComboBox customerComboBox;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label custLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label descLabel;
        private System.Windows.Forms.Label startLabel;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button CreateAppointmentButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox typeComboBox;
    }
}