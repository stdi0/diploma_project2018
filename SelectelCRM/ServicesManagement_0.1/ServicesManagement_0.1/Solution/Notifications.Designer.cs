namespace Solution
{
    partial class Notifications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Notifications));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.servicesTable = new System.Windows.Forms.DataGridView();
            this.notificationsTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.delNoticeBtn = new System.Windows.Forms.Button();
            this.editNoticeBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servicesTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(62, 538);
            this.panel2.TabIndex = 50;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(0, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(62, 53);
            this.panel3.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(16, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(29, 39);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(62, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(789, 53);
            this.panel4.TabIndex = 49;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(24, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(227, 32);
            this.label11.TabIndex = 4;
            this.label11.Text = "Email уведомления";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(268, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 51;
            this.label1.Text = "alexeev@qwer.ty";
            // 
            // servicesTable
            // 
            this.servicesTable.AllowUserToAddRows = false;
            this.servicesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servicesTable.Location = new System.Drawing.Point(92, 103);
            this.servicesTable.Name = "servicesTable";
            this.servicesTable.ReadOnly = true;
            this.servicesTable.Size = new System.Drawing.Size(585, 148);
            this.servicesTable.TabIndex = 52;
            this.servicesTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.servicesTable_CellClick);
            // 
            // notificationsTable
            // 
            this.notificationsTable.AllowUserToAddRows = false;
            this.notificationsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notificationsTable.Location = new System.Drawing.Point(92, 305);
            this.notificationsTable.Name = "notificationsTable";
            this.notificationsTable.ReadOnly = true;
            this.notificationsTable.Size = new System.Drawing.Size(585, 148);
            this.notificationsTable.TabIndex = 54;
            this.notificationsTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.notificationsTable_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(87, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 25);
            this.label2.TabIndex = 55;
            this.label2.Text = "Услуги в договоре № ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(87, 268);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 56;
            this.label3.Text = "Уведомления";
            // 
            // delNoticeBtn
            // 
            this.delNoticeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.delNoticeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.delNoticeBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delNoticeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delNoticeBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delNoticeBtn.ForeColor = System.Drawing.Color.Black;
            this.delNoticeBtn.Location = new System.Drawing.Point(216, 453);
            this.delNoticeBtn.Name = "delNoticeBtn";
            this.delNoticeBtn.Size = new System.Drawing.Size(101, 41);
            this.delNoticeBtn.TabIndex = 58;
            this.delNoticeBtn.Text = "Удалить";
            this.delNoticeBtn.UseVisualStyleBackColor = false;
            this.delNoticeBtn.Click += new System.EventHandler(this.delNoticeBtn_Click);
            // 
            // editNoticeBtn
            // 
            this.editNoticeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.editNoticeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.editNoticeBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.editNoticeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editNoticeBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editNoticeBtn.ForeColor = System.Drawing.Color.Black;
            this.editNoticeBtn.Location = new System.Drawing.Point(92, 453);
            this.editNoticeBtn.Name = "editNoticeBtn";
            this.editNoticeBtn.Size = new System.Drawing.Size(124, 41);
            this.editNoticeBtn.TabIndex = 57;
            this.editNoticeBtn.Text = "Редактировать";
            this.editNoticeBtn.UseVisualStyleBackColor = false;
            this.editNoticeBtn.Click += new System.EventHandler(this.editNoticeBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(276, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 25);
            this.label4.TabIndex = 59;
            this.label4.Text = "label4";
            // 
            // Notifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 512);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.delNoticeBtn);
            this.Controls.Add(this.editNoticeBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.notificationsTable);
            this.Controls.Add(this.servicesTable);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.Name = "Notifications";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Уведомления";
            this.Load += new System.EventHandler(this.Notifications_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.servicesTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView servicesTable;
        public System.Windows.Forms.DataGridView notificationsTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button delNoticeBtn;
        private System.Windows.Forms.Button editNoticeBtn;
        private System.Windows.Forms.Label label4;
    }
}