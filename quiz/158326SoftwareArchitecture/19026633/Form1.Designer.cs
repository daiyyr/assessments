namespace _19026633
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LabelUniversityName = new System.Windows.Forms.Label();
            this.ButtonShowStudents = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(60, 148);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(659, 233);
            this.dataGridView1.TabIndex = 0;
            // 
            // LabelUniversityName
            // 
            this.LabelUniversityName.AutoSize = true;
            this.LabelUniversityName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LabelUniversityName.Location = new System.Drawing.Point(299, 44);
            this.LabelUniversityName.Name = "LabelUniversityName";
            this.LabelUniversityName.Size = new System.Drawing.Size(168, 20);
            this.LabelUniversityName.TabIndex = 2;
            this.LabelUniversityName.Text = "LabelUniversityName";
            // 
            // ButtonShowStudents
            // 
            this.ButtonShowStudents.Location = new System.Drawing.Point(303, 90);
            this.ButtonShowStudents.Name = "ButtonShowStudents";
            this.ButtonShowStudents.Size = new System.Drawing.Size(158, 36);
            this.ButtonShowStudents.TabIndex = 3;
            this.ButtonShowStudents.Text = "Show Students";
            this.ButtonShowStudents.UseVisualStyleBackColor = true;
            this.ButtonShowStudents.Click += new System.EventHandler(this.ButtonShowStudents_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 445);
            this.Controls.Add(this.ButtonShowStudents);
            this.Controls.Add(this.LabelUniversityName);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label LabelUniversityName;
        private System.Windows.Forms.Button ButtonShowStudents;
    }
}

