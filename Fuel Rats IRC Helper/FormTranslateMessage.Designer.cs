namespace Fuel_Rats_IRC_Helper
{
    partial class FormTranslateMessage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewTranslatedMessage = new System.Windows.Forms.DataGridView();
            this.ColumnSender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOriginalMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTranslatedMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTranslatedMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTranslatedMessage
            // 
            this.dataGridViewTranslatedMessage.AllowUserToAddRows = false;
            this.dataGridViewTranslatedMessage.AllowUserToDeleteRows = false;
            this.dataGridViewTranslatedMessage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewTranslatedMessage.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewTranslatedMessage.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewTranslatedMessage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTranslatedMessage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSender,
            this.ColumnOriginalMessage,
            this.ColumnTranslatedMessage});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTranslatedMessage.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTranslatedMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTranslatedMessage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewTranslatedMessage.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewTranslatedMessage.MultiSelect = false;
            this.dataGridViewTranslatedMessage.Name = "dataGridViewTranslatedMessage";
            this.dataGridViewTranslatedMessage.ReadOnly = true;
            this.dataGridViewTranslatedMessage.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewTranslatedMessage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTranslatedMessage.Size = new System.Drawing.Size(800, 450);
            this.dataGridViewTranslatedMessage.TabIndex = 0;
            // 
            // ColumnSender
            // 
            this.ColumnSender.HeaderText = "Sender";
            this.ColumnSender.Name = "ColumnSender";
            this.ColumnSender.ReadOnly = true;
            this.ColumnSender.Width = 66;
            // 
            // ColumnOriginalMessage
            // 
            this.ColumnOriginalMessage.HeaderText = "Original Message";
            this.ColumnOriginalMessage.Name = "ColumnOriginalMessage";
            this.ColumnOriginalMessage.ReadOnly = true;
            this.ColumnOriginalMessage.Width = 104;
            // 
            // ColumnTranslatedMessage
            // 
            this.ColumnTranslatedMessage.HeaderText = "Translated Message";
            this.ColumnTranslatedMessage.Name = "ColumnTranslatedMessage";
            this.ColumnTranslatedMessage.ReadOnly = true;
            this.ColumnTranslatedMessage.Width = 117;
            // 
            // FormTranslateMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewTranslatedMessage);
            this.Name = "FormTranslateMessage";
            this.Text = "Translated message";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTranslatedMessage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTranslatedMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSender;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOriginalMessage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTranslatedMessage;
    }
}