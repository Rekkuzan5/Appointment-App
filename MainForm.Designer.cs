
namespace Appointment_App
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.customerDataGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.addCustomerButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.CustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customerDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // customerDataGrid
            // 
            this.customerDataGrid.AllowUserToAddRows = false;
            this.customerDataGrid.AllowUserToDeleteRows = false;
            this.customerDataGrid.AllowUserToResizeColumns = false;
            this.customerDataGrid.AllowUserToResizeRows = false;
            this.customerDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customerDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customerDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerID,
            this.CustomerName,
            this.CustomerAddress,
            this.CustomerPhone,
            this.IsActive});
            this.customerDataGrid.EnableHeadersVisualStyles = false;
            this.customerDataGrid.Location = new System.Drawing.Point(39, 95);
            this.customerDataGrid.MultiSelect = false;
            this.customerDataGrid.Name = "customerDataGrid";
            this.customerDataGrid.ReadOnly = true;
            this.customerDataGrid.RowHeadersVisible = false;
            this.customerDataGrid.RowHeadersWidth = 20;
            this.customerDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.customerDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customerDataGrid.Size = new System.Drawing.Size(798, 230);
            this.customerDataGrid.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(798, 63);
            this.label1.TabIndex = 1;
            this.label1.Text = "Customers";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(39, 765);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 46);
            this.button1.TabIndex = 2;
            this.button1.Text = "Create Appointment";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // addCustomerButton
            // 
            this.addCustomerButton.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addCustomerButton.Location = new System.Drawing.Point(39, 331);
            this.addCustomerButton.Name = "addCustomerButton";
            this.addCustomerButton.Size = new System.Drawing.Size(195, 46);
            this.addCustomerButton.TabIndex = 3;
            this.addCustomerButton.Text = "Add Customer";
            this.addCustomerButton.UseVisualStyleBackColor = true;
            this.addCustomerButton.Click += new System.EventHandler(this.addCustomerButton_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(240, 331);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(195, 46);
            this.button3.TabIndex = 4;
            this.button3.Text = "Modify Customer";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(441, 331);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(195, 46);
            this.button4.TabIndex = 5;
            this.button4.Text = "Delete Customer";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Poppins", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(39, 431);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(798, 59);
            this.label2.TabIndex = 6;
            this.label2.Text = "Appointment Calendar";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(39, 493);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(78, 30);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Month";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(123, 493);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 30);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Week";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(39, 529);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(798, 230);
            this.dataGridView2.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(642, 331);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(195, 46);
            this.button5.TabIndex = 10;
            this.button5.Text = "View Reports";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(240, 766);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(195, 46);
            this.button6.TabIndex = 11;
            this.button6.Text = "Modify Appointment";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(441, 766);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(195, 46);
            this.button7.TabIndex = 12;
            this.button7.Text = "Delete Appointment";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // mainFormBindingSource
            // 
            this.mainFormBindingSource.DataSource = typeof(Appointment_App.MainForm);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Poppins", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(711, 766);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 46);
            this.button2.TabIndex = 13;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CustomerID
            // 
            this.CustomerID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CustomerID.DataPropertyName = "customerId";
            this.CustomerID.FillWeight = 76.14214F;
            this.CustomerID.HeaderText = "ID";
            this.CustomerID.Name = "CustomerID";
            this.CustomerID.ReadOnly = true;
            this.CustomerID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerID.Width = 40;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "customerName";
            this.CustomerName.FillWeight = 105.9645F;
            this.CustomerName.HeaderText = "Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CustomerAddress
            // 
            this.CustomerAddress.DataPropertyName = "address";
            this.CustomerAddress.FillWeight = 105.9645F;
            this.CustomerAddress.HeaderText = "Address";
            this.CustomerAddress.Name = "CustomerAddress";
            this.CustomerAddress.ReadOnly = true;
            this.CustomerAddress.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerAddress.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CustomerPhone
            // 
            this.CustomerPhone.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CustomerPhone.DataPropertyName = "phone";
            this.CustomerPhone.FillWeight = 105.9645F;
            this.CustomerPhone.HeaderText = "Phone";
            this.CustomerPhone.Name = "CustomerPhone";
            this.CustomerPhone.ReadOnly = true;
            this.CustomerPhone.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerPhone.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CustomerPhone.Width = 150;
            // 
            // IsActive
            // 
            this.IsActive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsActive.DataPropertyName = "active";
            this.IsActive.FillWeight = 105.9645F;
            this.IsActive.HeaderText = "Active";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.IsActive.Width = 80;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 868);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.addCustomerButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customerDataGrid);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.customerDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView customerDataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button addCustomerButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.BindingSource mainFormBindingSource;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsActive;
    }
}