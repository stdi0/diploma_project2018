namespace Solution
{
    partial class Clients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clients));
            this.clientsTable = new System.Windows.Forms.DataGridView();
            this.addClientBtn = new System.Windows.Forms.Button();
            this.editClientBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.clientName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.country = new System.Windows.Forms.ComboBox();
            this.city = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.searchNameBtn = new System.Windows.Forms.Button();
            this.filterCountryBtn = new System.Windows.Forms.Button();
            this.filterCityBtn = new System.Windows.Forms.Button();
            this.delClientBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterCancelBtn = new System.Windows.Forms.Button();
            this.logoutPanel = new System.Windows.Forms.Panel();
            this.logoutLbl = new System.Windows.Forms.Label();
            this.logoutBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.analyticsBtn = new System.Windows.Forms.Button();
            this.requestsAndContractsBtn = new System.Windows.Forms.Button();
            this.clientsBtn = new System.Windows.Forms.Button();
            this.managersBtn = new System.Windows.Forms.Button();
            this.servicesBtn = new System.Windows.Forms.Button();
            this.refsBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.reconnectBtn = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.exceptPanel = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.clientsTable)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.logoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.exceptPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clientsTable
            // 
            this.clientsTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.clientsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.clientsTable.Location = new System.Drawing.Point(269, 56);
            this.clientsTable.Name = "clientsTable";
            this.clientsTable.Size = new System.Drawing.Size(1103, 310);
            this.clientsTable.TabIndex = 1;
            this.clientsTable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clientsTable_CellClick);
            //this.clientsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clientsTable_CellContentClick);
            //this.clientsTable.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.clientsTable_CellStateChanged);
            // 
            // addClientBtn
            // 
            this.addClientBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.addClientBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.addClientBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addClientBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addClientBtn.ForeColor = System.Drawing.Color.White;
            this.addClientBtn.Location = new System.Drawing.Point(661, 16);
            this.addClientBtn.Name = "addClientBtn";
            this.addClientBtn.Size = new System.Drawing.Size(120, 34);
            this.addClientBtn.TabIndex = 3;
            this.addClientBtn.Text = "Добавить";
            this.addClientBtn.UseVisualStyleBackColor = false;
            this.addClientBtn.Click += new System.EventHandler(this.addClientBtn_Click);
            // 
            // editClientBtn
            // 
            this.editClientBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.editClientBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.editClientBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editClientBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.editClientBtn.ForeColor = System.Drawing.Color.White;
            this.editClientBtn.Location = new System.Drawing.Point(787, 16);
            this.editClientBtn.Name = "editClientBtn";
            this.editClientBtn.Size = new System.Drawing.Size(179, 34);
            this.editClientBtn.TabIndex = 4;
            this.editClientBtn.Text = "Редактировать";
            this.editClientBtn.UseVisualStyleBackColor = false;
            this.editClientBtn.Click += new System.EventHandler(this.editClientBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(18, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "По названию:";
            // 
            // clientName
            // 
            this.clientName.Location = new System.Drawing.Point(137, 31);
            this.clientName.Multiline = true;
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(326, 28);
            this.clientName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(18, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "По стране:";
            // 
            // country
            // 
            this.country.FormattingEnabled = true;
            this.country.Location = new System.Drawing.Point(137, 79);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(326, 29);
            this.country.TabIndex = 8;
            // 
            // city
            // 
            this.city.FormattingEnabled = true;
            this.city.Location = new System.Drawing.Point(672, 31);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(304, 29);
            this.city.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(569, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 21);
            this.label4.TabIndex = 9;
            this.label4.Text = "По городу:";
            // 
            // searchNameBtn
            // 
            this.searchNameBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.searchNameBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.searchNameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchNameBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.searchNameBtn.ForeColor = System.Drawing.Color.White;
            this.searchNameBtn.Location = new System.Drawing.Point(469, 31);
            this.searchNameBtn.Name = "searchNameBtn";
            this.searchNameBtn.Size = new System.Drawing.Size(73, 28);
            this.searchNameBtn.TabIndex = 11;
            this.searchNameBtn.Text = "Поиск";
            this.searchNameBtn.UseVisualStyleBackColor = false;
            this.searchNameBtn.Click += new System.EventHandler(this.searchNameBtn_Click);
            // 
            // filterCountryBtn
            // 
            this.filterCountryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.filterCountryBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.filterCountryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterCountryBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterCountryBtn.ForeColor = System.Drawing.Color.White;
            this.filterCountryBtn.Location = new System.Drawing.Point(469, 79);
            this.filterCountryBtn.Name = "filterCountryBtn";
            this.filterCountryBtn.Size = new System.Drawing.Size(73, 29);
            this.filterCountryBtn.TabIndex = 12;
            this.filterCountryBtn.Text = "Отбор";
            this.filterCountryBtn.UseVisualStyleBackColor = false;
            this.filterCountryBtn.Click += new System.EventHandler(this.filterCountryBtn_Click);
            // 
            // filterCityBtn
            // 
            this.filterCityBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.filterCityBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.filterCityBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterCityBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterCityBtn.ForeColor = System.Drawing.Color.White;
            this.filterCityBtn.Location = new System.Drawing.Point(982, 30);
            this.filterCityBtn.Name = "filterCityBtn";
            this.filterCityBtn.Size = new System.Drawing.Size(73, 29);
            this.filterCityBtn.TabIndex = 13;
            this.filterCityBtn.Text = "Отбор";
            this.filterCityBtn.UseVisualStyleBackColor = false;
            this.filterCityBtn.Click += new System.EventHandler(this.filterCityBtn_Click);
            // 
            // delClientBtn
            // 
            this.delClientBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.delClientBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.delClientBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delClientBtn.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.delClientBtn.ForeColor = System.Drawing.Color.White;
            this.delClientBtn.Location = new System.Drawing.Point(972, 16);
            this.delClientBtn.Name = "delClientBtn";
            this.delClientBtn.Size = new System.Drawing.Size(107, 34);
            this.delClientBtn.TabIndex = 15;
            this.delClientBtn.Text = "Удалить";
            this.delClientBtn.UseVisualStyleBackColor = false;
            this.delClientBtn.Click += new System.EventHandler(this.delClientBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.filterCancelBtn);
            this.groupBox1.Controls.Add(this.clientName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.filterCityBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.filterCountryBtn);
            this.groupBox1.Controls.Add(this.country);
            this.groupBox1.Controls.Add(this.searchNameBtn);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.city);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(285, 375);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1072, 129);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск / Отбор";
            //this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // filterCancelBtn
            // 
            this.filterCancelBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.filterCancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.filterCancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterCancelBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.filterCancelBtn.ForeColor = System.Drawing.Color.White;
            this.filterCancelBtn.Location = new System.Drawing.Point(845, 77);
            this.filterCancelBtn.Name = "filterCancelBtn";
            this.filterCancelBtn.Size = new System.Drawing.Size(210, 29);
            this.filterCancelBtn.TabIndex = 46;
            this.filterCancelBtn.Text = "Отменить фильтры";
            this.filterCancelBtn.UseVisualStyleBackColor = false;
            this.filterCancelBtn.Click += new System.EventHandler(this.filterCancelBtn_Click_1);
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
            //this.logoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
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
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
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
            this.servicesBtn.Click += new System.EventHandler(this.servicesBtn_Click);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.delClientBtn);
            this.panel1.Controls.Add(this.editClientBtn);
            this.panel1.Controls.Add(this.addClientBtn);
            this.panel1.Location = new System.Drawing.Point(270, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 62);
            this.panel1.TabIndex = 43;
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
            this.label5.Size = new System.Drawing.Size(110, 32);
            this.label5.TabIndex = 1;
            this.label5.Text = "Клиенты";
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
            this.panel5.TabIndex = 44;
            // 
            // reconnectBtn
            // 
            this.reconnectBtn.AutoSize = true;
            this.reconnectBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reconnectBtn.ForeColor = System.Drawing.Color.White;
            this.reconnectBtn.Location = new System.Drawing.Point(48, 623);
            this.reconnectBtn.Name = "reconnectBtn";
            this.reconnectBtn.Size = new System.Drawing.Size(187, 17);
            this.reconnectBtn.TabIndex = 52;
            this.reconnectBtn.Text = "Переподключиться к серверу";
            this.reconnectBtn.Click += new System.EventHandler(this.reconnectBtn_Click);
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
            this.pictureBox6.TabIndex = 53;
            this.pictureBox6.TabStop = false;
            //this.pictureBox6.Click += new System.EventHandler(this.pictureBox6_Click);
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
            this.exceptPanel.TabIndex = 47;
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
            // Clients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1370, 722);
            this.Controls.Add(this.exceptPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.clientsTable);
            this.Controls.Add(this.groupBox1);
            this.Name = "Clients";
            this.Text = "Клиенты";
            //this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Clients_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Clients_FormClosed);
            this.Load += new System.EventHandler(this.Clients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clientsTable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.logoutPanel.ResumeLayout(false);
            this.logoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.exceptPanel.ResumeLayout(false);
            this.exceptPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView clientsTable;
        private System.Windows.Forms.Button addClientBtn;
        private System.Windows.Forms.Button editClientBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox clientName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox country;
        private System.Windows.Forms.ComboBox city;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button searchNameBtn;
        private System.Windows.Forms.Button filterCountryBtn;
        private System.Windows.Forms.Button filterCityBtn;
        private System.Windows.Forms.Button delClientBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel logoutPanel;
        private System.Windows.Forms.Label logoutLbl;
        private System.Windows.Forms.PictureBox logoutBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button analyticsBtn;
        private System.Windows.Forms.Button requestsAndContractsBtn;
        private System.Windows.Forms.Button clientsBtn;
        private System.Windows.Forms.Button managersBtn;
        private System.Windows.Forms.Button servicesBtn;
        private System.Windows.Forms.Button refsBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button filterCancelBtn;
        private System.Windows.Forms.Label reconnectBtn;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel exceptPanel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}