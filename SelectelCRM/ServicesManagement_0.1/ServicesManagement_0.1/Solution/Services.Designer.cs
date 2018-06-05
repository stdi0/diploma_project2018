namespace Solution
{
    partial class Services
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Services));
            this.servicesTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.addServiceBtn = new System.Windows.Forms.Button();
            this.editServiceBtn = new System.Windows.Forms.Button();
            this.editGroupBtn = new System.Windows.Forms.Button();
            this.addGroupBtn = new System.Windows.Forms.Button();
            this.groupsTable = new System.Windows.Forms.DataGridView();
            this.delGroupBtn = new System.Windows.Forms.Button();
            this.delServiceBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.exceptPanel = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.reconnectBtn = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.refsBtn = new System.Windows.Forms.Button();
            this.servicesBtn = new System.Windows.Forms.Button();
            this.managersBtn = new System.Windows.Forms.Button();
            this.clientsBtn = new System.Windows.Forms.Button();
            this.requestsAndContractsBtn = new System.Windows.Forms.Button();
            this.analyticsBtn = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.logoutPanel = new System.Windows.Forms.Panel();
            this.logoutLbl = new System.Windows.Forms.Label();
            this.logoutBtn = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.servicesTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsTable)).BeginInit();
            this.panel1.SuspendLayout();
            this.exceptPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.logoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // servicesTable
            // 
            this.servicesTable.AllowUserToAddRows = false;
            this.servicesTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.servicesTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servicesTable.Location = new System.Drawing.Point(269, 358);
            this.servicesTable.Name = "servicesTable";
            this.servicesTable.Size = new System.Drawing.Size(1103, 250);
            this.servicesTable.TabIndex = 7;
            this.servicesTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.servicesTable_CellClick);
            //this.servicesTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.servicesTable_CellContentClick);
            //this.servicesTable.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.servicesTable_CellStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(720, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Услуги";
            //this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // addServiceBtn
            // 
            this.addServiceBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.addServiceBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addServiceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addServiceBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addServiceBtn.ForeColor = System.Drawing.Color.White;
            this.addServiceBtn.Location = new System.Drawing.Point(660, 11);
            this.addServiceBtn.Name = "addServiceBtn";
            this.addServiceBtn.Size = new System.Drawing.Size(121, 35);
            this.addServiceBtn.TabIndex = 9;
            this.addServiceBtn.Text = "Добавить";
            this.addServiceBtn.UseVisualStyleBackColor = false;
            this.addServiceBtn.Click += new System.EventHandler(this.addServiceBtn_Click);
            // 
            // editServiceBtn
            // 
            this.editServiceBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.editServiceBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.editServiceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editServiceBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editServiceBtn.ForeColor = System.Drawing.Color.White;
            this.editServiceBtn.Location = new System.Drawing.Point(787, 11);
            this.editServiceBtn.Name = "editServiceBtn";
            this.editServiceBtn.Size = new System.Drawing.Size(179, 35);
            this.editServiceBtn.TabIndex = 10;
            this.editServiceBtn.Text = "Редактировать";
            this.editServiceBtn.UseVisualStyleBackColor = false;
            this.editServiceBtn.Click += new System.EventHandler(this.editServiceBtn_Click);
            // 
            // editGroupBtn
            // 
            this.editGroupBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.editGroupBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.editGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editGroupBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editGroupBtn.ForeColor = System.Drawing.Color.White;
            this.editGroupBtn.Location = new System.Drawing.Point(787, 16);
            this.editGroupBtn.Name = "editGroupBtn";
            this.editGroupBtn.Size = new System.Drawing.Size(179, 34);
            this.editGroupBtn.TabIndex = 14;
            this.editGroupBtn.Text = "Редактировать";
            this.editGroupBtn.UseVisualStyleBackColor = false;
            this.editGroupBtn.Click += new System.EventHandler(this.editGroupBtn_Click);
            // 
            // addGroupBtn
            // 
            this.addGroupBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.addGroupBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addGroupBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addGroupBtn.ForeColor = System.Drawing.Color.White;
            this.addGroupBtn.Location = new System.Drawing.Point(660, 16);
            this.addGroupBtn.Name = "addGroupBtn";
            this.addGroupBtn.Size = new System.Drawing.Size(121, 34);
            this.addGroupBtn.TabIndex = 13;
            this.addGroupBtn.Text = "Добавить";
            this.addGroupBtn.UseVisualStyleBackColor = false;
            this.addGroupBtn.Click += new System.EventHandler(this.addGroupBtn_Click);
            // 
            // groupsTable
            // 
            this.groupsTable.AllowUserToAddRows = false;
            this.groupsTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.groupsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.groupsTable.Location = new System.Drawing.Point(269, 57);
            this.groupsTable.Name = "groupsTable";
            this.groupsTable.Size = new System.Drawing.Size(1103, 250);
            this.groupsTable.TabIndex = 12;
            this.groupsTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.groupsTable_CellClick);
            //this.groupsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.groupsTable_CellContentClick);
            //this.groupsTable.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.groupsTable_CellParsing);
            //this.groupsTable.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.groupsTable_CellStateChanged);
            // 
            // delGroupBtn
            // 
            this.delGroupBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.delGroupBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delGroupBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delGroupBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delGroupBtn.ForeColor = System.Drawing.Color.White;
            this.delGroupBtn.Location = new System.Drawing.Point(972, 16);
            this.delGroupBtn.Name = "delGroupBtn";
            this.delGroupBtn.Size = new System.Drawing.Size(107, 34);
            this.delGroupBtn.TabIndex = 15;
            this.delGroupBtn.Text = "Удалить";
            this.delGroupBtn.UseVisualStyleBackColor = false;
            this.delGroupBtn.Click += new System.EventHandler(this.delGroupBtn_Click);
            // 
            // delServiceBtn
            // 
            this.delServiceBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.delServiceBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delServiceBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delServiceBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delServiceBtn.ForeColor = System.Drawing.Color.White;
            this.delServiceBtn.Location = new System.Drawing.Point(972, 11);
            this.delServiceBtn.Name = "delServiceBtn";
            this.delServiceBtn.Size = new System.Drawing.Size(107, 35);
            this.delServiceBtn.TabIndex = 16;
            this.delServiceBtn.Text = "Удалить";
            this.delServiceBtn.UseVisualStyleBackColor = false;
            this.delServiceBtn.Click += new System.EventHandler(this.delServiceBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.panel1.Controls.Add(this.delGroupBtn);
            this.panel1.Controls.Add(this.editGroupBtn);
            this.panel1.Controls.Add(this.addGroupBtn);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(270, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 62);
            this.panel1.TabIndex = 44;
            //this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(19, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 32);
            this.label5.TabIndex = 1;
            this.label5.Text = "Группы услуг";
            // 
            // exceptPanel
            // 
            this.exceptPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.exceptPanel.Controls.Add(this.label22);
            this.exceptPanel.Controls.Add(this.label23);
            this.exceptPanel.Controls.Add(this.richTextBox1);
            this.exceptPanel.Location = new System.Drawing.Point(975, 4);
            this.exceptPanel.Name = "exceptPanel";
            this.exceptPanel.Size = new System.Drawing.Size(391, 269);
            this.exceptPanel.TabIndex = 50;
            this.exceptPanel.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(367, 5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 17);
            this.label22.TabIndex = 2;
            this.label22.Text = "X";
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(10, 10);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(205, 21);
            this.label23.TabIndex = 1;
            this.label23.Text = "Упс...Что-то пошло не так :)";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 38);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(385, 232);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel5.Controls.Add(this.reconnectBtn);
            this.panel5.Controls.Add(this.pictureBox6);
            this.panel5.Controls.Add(this.refsBtn);
            this.panel5.Controls.Add(this.servicesBtn);
            this.panel5.Controls.Add(this.managersBtn);
            this.panel5.Controls.Add(this.clientsBtn);
            this.panel5.Controls.Add(this.requestsAndContractsBtn);
            this.panel5.Controls.Add(this.analyticsBtn);
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Controls.Add(this.label14);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Controls.Add(this.logoutPanel);
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(270, 876);
            this.panel5.TabIndex = 45;
            //this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // reconnectBtn
            // 
            this.reconnectBtn.AutoSize = true;
            this.reconnectBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reconnectBtn.ForeColor = System.Drawing.Color.White;
            this.reconnectBtn.Location = new System.Drawing.Point(48, 623);
            this.reconnectBtn.Name = "reconnectBtn";
            this.reconnectBtn.Size = new System.Drawing.Size(187, 17);
            this.reconnectBtn.TabIndex = 56;
            this.reconnectBtn.Text = "Переподключиться к серверу";
            this.reconnectBtn.Click += new System.EventHandler(this.reconnectBtn_Click_1);
            this.reconnectBtn.MouseLeave += new System.EventHandler(this.reconnectBtn_MouseLeave);
            this.reconnectBtn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.reconnectBtn_MouseMove);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.BackgroundImage")));
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.Location = new System.Drawing.Point(22, 619);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(22, 25);
            this.pictureBox6.TabIndex = 57;
            this.pictureBox6.TabStop = false;
            // 
            // refsBtn
            // 
            this.refsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.refsBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.refsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refsBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.refsBtn.ForeColor = System.Drawing.Color.White;
            this.refsBtn.Location = new System.Drawing.Point(20, 374);
            this.refsBtn.Name = "refsBtn";
            this.refsBtn.Size = new System.Drawing.Size(230, 37);
            this.refsBtn.TabIndex = 51;
            this.refsBtn.Text = "Справочники";
            this.refsBtn.UseVisualStyleBackColor = false;
            this.refsBtn.Click += new System.EventHandler(this.refsBtn_Click);
            // 
            // servicesBtn
            // 
            this.servicesBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.servicesBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.servicesBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.servicesBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.servicesBtn.ForeColor = System.Drawing.Color.White;
            this.servicesBtn.Location = new System.Drawing.Point(20, 323);
            this.servicesBtn.Name = "servicesBtn";
            this.servicesBtn.Size = new System.Drawing.Size(230, 37);
            this.servicesBtn.TabIndex = 50;
            this.servicesBtn.Text = "Услуги";
            this.servicesBtn.UseVisualStyleBackColor = false;
            //this.servicesBtn.Click += new System.EventHandler(this.servicesBtn_Click);
            // 
            // managersBtn
            // 
            this.managersBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.managersBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.managersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.managersBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.managersBtn.ForeColor = System.Drawing.Color.White;
            this.managersBtn.Location = new System.Drawing.Point(20, 270);
            this.managersBtn.Name = "managersBtn";
            this.managersBtn.Size = new System.Drawing.Size(230, 37);
            this.managersBtn.TabIndex = 49;
            this.managersBtn.Text = "Менеджеры";
            this.managersBtn.UseVisualStyleBackColor = false;
            this.managersBtn.Click += new System.EventHandler(this.managersBtn_Click);
            // 
            // clientsBtn
            // 
            this.clientsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.clientsBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.clientsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clientsBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientsBtn.ForeColor = System.Drawing.Color.White;
            this.clientsBtn.Location = new System.Drawing.Point(20, 218);
            this.clientsBtn.Name = "clientsBtn";
            this.clientsBtn.Size = new System.Drawing.Size(230, 37);
            this.clientsBtn.TabIndex = 48;
            this.clientsBtn.Text = "Клиенты";
            this.clientsBtn.UseVisualStyleBackColor = false;
            this.clientsBtn.Click += new System.EventHandler(this.clientsBtn_Click);
            // 
            // requestsAndContractsBtn
            // 
            this.requestsAndContractsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.requestsAndContractsBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.requestsAndContractsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.requestsAndContractsBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.requestsAndContractsBtn.ForeColor = System.Drawing.Color.White;
            this.requestsAndContractsBtn.Location = new System.Drawing.Point(20, 166);
            this.requestsAndContractsBtn.Name = "requestsAndContractsBtn";
            this.requestsAndContractsBtn.Size = new System.Drawing.Size(230, 37);
            this.requestsAndContractsBtn.TabIndex = 47;
            this.requestsAndContractsBtn.Text = "Заявки/Договоры";
            this.requestsAndContractsBtn.UseVisualStyleBackColor = false;
            this.requestsAndContractsBtn.Click += new System.EventHandler(this.requestsAndContractsBtn_Click);
            // 
            // analyticsBtn
            // 
            this.analyticsBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.analyticsBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.analyticsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analyticsBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.analyticsBtn.ForeColor = System.Drawing.Color.White;
            this.analyticsBtn.Location = new System.Drawing.Point(20, 119);
            this.analyticsBtn.Name = "analyticsBtn";
            this.analyticsBtn.Size = new System.Drawing.Size(230, 37);
            this.analyticsBtn.TabIndex = 46;
            this.analyticsBtn.Text = "Аналитика";
            this.analyticsBtn.UseVisualStyleBackColor = false;
            this.analyticsBtn.Click += new System.EventHandler(this.analyticsBtn_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(26, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 20);
            this.label16.TabIndex = 43;
            this.label16.Text = "Аналитика";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(25, 275);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(100, 20);
            this.label15.TabIndex = 43;
            this.label15.Text = "Менеджеры";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(25, 226);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 20);
            this.label14.TabIndex = 43;
            this.label14.Text = "Клиенты";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(25, 375);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 20);
            this.label13.TabIndex = 45;
            this.label13.Text = "Справочники";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(26, 327);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 20);
            this.label12.TabIndex = 44;
            this.label12.Text = "Услуги";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(25, 174);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(143, 20);
            this.label11.TabIndex = 43;
            this.label11.Text = "Заявки/Договоры";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(33, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 40);
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // logoutPanel
            // 
            this.logoutPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.logoutPanel.Controls.Add(this.logoutLbl);
            this.logoutPanel.Controls.Add(this.logoutBtn);
            this.logoutPanel.Location = new System.Drawing.Point(0, 652);
            this.logoutPanel.Name = "logoutPanel";
            this.logoutPanel.Size = new System.Drawing.Size(270, 100);
            this.logoutPanel.TabIndex = 9;
            this.logoutPanel.Click += new System.EventHandler(this.logoutPanel_Click);
            //this.logoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.logoutPanel_Paint);
            // 
            // logoutLbl
            // 
            this.logoutLbl.AutoSize = true;
            this.logoutLbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.logoutLbl.ForeColor = System.Drawing.Color.White;
            this.logoutLbl.Location = new System.Drawing.Point(90, 26);
            this.logoutLbl.Name = "logoutLbl";
            this.logoutLbl.Size = new System.Drawing.Size(132, 21);
            this.logoutLbl.TabIndex = 9;
            this.logoutLbl.Text = "Завершить сеанс";
            this.logoutLbl.Click += new System.EventHandler(this.logoutLbl_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logoutBtn.BackgroundImage")));
            this.logoutBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logoutBtn.Location = new System.Drawing.Point(23, 12);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(66, 45);
            this.logoutBtn.TabIndex = 9;
            this.logoutBtn.TabStop = false;
            this.logoutBtn.Click += new System.EventHandler(this.logoutLbl_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.panel2.Controls.Add(this.editServiceBtn);
            this.panel2.Controls.Add(this.addServiceBtn);
            this.panel2.Controls.Add(this.delServiceBtn);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(270, 302);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1103, 58);
            this.panel2.TabIndex = 46;
            //this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(19, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 32);
            this.label3.TabIndex = 16;
            this.label3.Text = "Услуги";
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1370, 722);
            this.Controls.Add(this.exceptPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupsTable);
            this.Controls.Add(this.servicesTable);
            this.Controls.Add(this.label1);
            this.Name = "Services";
            this.Text = "Услуги";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Services_FormClosed);
            this.Load += new System.EventHandler(this.Services_Load);
            ((System.ComponentModel.ISupportInitialize)(this.servicesTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupsTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.exceptPanel.ResumeLayout(false);
            this.exceptPanel.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.logoutPanel.ResumeLayout(false);
            this.logoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.DataGridView servicesTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addServiceBtn;
        private System.Windows.Forms.Button editServiceBtn;
        private System.Windows.Forms.Button editGroupBtn;
        private System.Windows.Forms.Button addGroupBtn;
        public System.Windows.Forms.DataGridView groupsTable;
        private System.Windows.Forms.Button delGroupBtn;
        private System.Windows.Forms.Button delServiceBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button refsBtn;
        private System.Windows.Forms.Button servicesBtn;
        private System.Windows.Forms.Button managersBtn;
        private System.Windows.Forms.Button clientsBtn;
        private System.Windows.Forms.Button requestsAndContractsBtn;
        private System.Windows.Forms.Button analyticsBtn;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel logoutPanel;
        private System.Windows.Forms.Label logoutLbl;
        private System.Windows.Forms.PictureBox logoutBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel exceptPanel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label reconnectBtn;
        private System.Windows.Forms.PictureBox pictureBox6;
    }
}