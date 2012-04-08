namespace Config_PLC_SIEMENS
{
    partial class ConfigPLC_S7
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigPLC_S7));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Модуль AI", 1, 1);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("PLC №", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.set_conmenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.set_conmenu_add = new System.Windows.Forms.ToolStripMenuItem();
            this.set_conmenu_del = new System.Windows.Forms.ToolStripMenuItem();
            this.set_images = new System.Windows.Forms.ImageList(this.components);
            this.set_menu = new System.Windows.Forms.ToolStrip();
            this.addTag = new System.Windows.Forms.ToolStripButton();
            this.editTag = new System.Windows.Forms.ToolStripButton();
            this.delTag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tunning_pid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.staticConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.set_menu_label = new System.Windows.Forms.ToolStripLabel();
            this.set_find_tag = new System.Windows.Forms.ToolStripTextBox();
            this.set_menu_search = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportHardwareConfig = new System.Windows.Forms.ToolStripButton();
            this.openConfigDialog = new System.Windows.Forms.OpenFileDialog();
            this.set_setting = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgw_hist_plc_config = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.l_data_static = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.l_samples = new System.Windows.Forms.Label();
            this.open_folder_dialog = new System.Windows.Forms.Button();
            this.p_plc_config = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.l_static = new System.Windows.Forms.Label();
            this.l_dinamic = new System.Windows.Forms.Label();
            this.l_version = new System.Windows.Forms.Label();
            this.set_mount = new System.Windows.Forms.TabPage();
            this.set_pan_mount_wait = new System.Windows.Forms.Panel();
            this.set_text_mount_wait = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.set_gb_type_module = new System.Windows.Forms.GroupBox();
            this.set_b_modul_param_cancel = new System.Windows.Forms.Button();
            this.set_b_modul_param_ok = new System.Windows.Forms.Button();
            this.set_nd_channel_count = new System.Windows.Forms.NumericUpDown();
            this.set_l_channel_count = new System.Windows.Forms.Label();
            this.set_ddl_type_modul = new System.Windows.Forms.ComboBox();
            this.set_l_type_modul = new System.Windows.Forms.Label();
            this.set_b_channel_mount_cancel = new System.Windows.Forms.Button();
            this.set_b_channel_mount_ok = new System.Windows.Forms.Button();
            this.set_gb_type_plc = new System.Windows.Forms.GroupBox();
            this.set_b_change_plc = new System.Windows.Forms.Button();
            this.set_inp_type_plc = new System.Windows.Forms.TextBox();
            this.set_l_type_plc = new System.Windows.Forms.Label();
            this.set_inp_number_plc = new System.Windows.Forms.TextBox();
            this.set_l_number_plc = new System.Windows.Forms.Label();
            this.set_inp_name_plc = new System.Windows.Forms.TextBox();
            this.set_l_name_plc = new System.Windows.Forms.Label();
            this.set_treeview_mount = new System.Windows.Forms.TreeView();
            this.set_gb_channel_mount = new System.Windows.Forms.GroupBox();
            this.set_dgv_channel_mount = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signalgroups = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.signals = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cok = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.signalid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tags = new System.Windows.Forms.TabPage();
            this.pan_tag_wait = new System.Windows.Forms.Panel();
            this.text_tag_wait = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tag_descr = new System.Windows.Forms.DataGridView();
            this.dw_tag_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw_tag_nameplc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw_tag_namescada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dw_tag_descr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabConfigPLC_S7 = new System.Windows.Forms.TabControl();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.set_conmenu.SuspendLayout();
            this.set_menu.SuspendLayout();
            this.set_setting.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgw_hist_plc_config)).BeginInit();
            this.set_mount.SuspendLayout();
            this.set_pan_mount_wait.SuspendLayout();
            this.set_gb_type_module.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.set_nd_channel_count)).BeginInit();
            this.set_gb_type_plc.SuspendLayout();
            this.set_gb_channel_mount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.set_dgv_channel_mount)).BeginInit();
            this.tags.SuspendLayout();
            this.pan_tag_wait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tag_descr)).BeginInit();
            this.tabConfigPLC_S7.SuspendLayout();
            this.SuspendLayout();
            // 
            // set_conmenu
            // 
            this.set_conmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.set_conmenu_add,
            this.set_conmenu_del});
            this.set_conmenu.Name = "set_conmenu";
            this.set_conmenu.Size = new System.Drawing.Size(201, 52);
            // 
            // set_conmenu_add
            // 
            this.set_conmenu_add.Image = ((System.Drawing.Image)(resources.GetObject("set_conmenu_add.Image")));
            this.set_conmenu_add.Name = "set_conmenu_add";
            this.set_conmenu_add.Size = new System.Drawing.Size(200, 24);
            this.set_conmenu_add.Text = "Добавить модуль";
            this.set_conmenu_add.Click += new System.EventHandler(this.SetConmenuAddClick);
            // 
            // set_conmenu_del
            // 
            this.set_conmenu_del.Image = ((System.Drawing.Image)(resources.GetObject("set_conmenu_del.Image")));
            this.set_conmenu_del.Name = "set_conmenu_del";
            this.set_conmenu_del.Size = new System.Drawing.Size(200, 24);
            this.set_conmenu_del.Text = "Удалить модуль";
            this.set_conmenu_del.Click += new System.EventHandler(this.SetConmenuDelClick);
            // 
            // set_images
            // 
            this.set_images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("set_images.ImageStream")));
            this.set_images.TransparentColor = System.Drawing.Color.Transparent;
            this.set_images.Images.SetKeyName(0, "drive-removable-media.png");
            this.set_images.Images.SetKeyName(1, "drive-harddisk.png");
            // 
            // set_menu
            // 
            this.set_menu.AutoSize = false;
            this.set_menu.BackColor = System.Drawing.SystemColors.ControlLight;
            this.set_menu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.set_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTag,
            this.editTag,
            this.delTag,
            this.toolStripSeparator3,
            this.tunning_pid,
            this.toolStripSeparator4,
            this.staticConfig,
            this.toolStripSeparator1,
            this.set_menu_label,
            this.set_find_tag,
            this.set_menu_search,
            this.toolStripSeparator2,
            this.exportHardwareConfig});
            this.set_menu.Location = new System.Drawing.Point(0, 0);
            this.set_menu.Name = "set_menu";
            this.set_menu.Size = new System.Drawing.Size(1024, 32);
            this.set_menu.TabIndex = 0;
            this.set_menu.Text = "Меню";
            // 
            // addTag
            // 
            this.addTag.AutoSize = false;
            this.addTag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.addTag.Image = ((System.Drawing.Image)(resources.GetObject("addTag.Image")));
            this.addTag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTag.Name = "addTag";
            this.addTag.Size = new System.Drawing.Size(32, 32);
            this.addTag.Text = "Добавить переменную";
            this.addTag.ToolTipText = "Добавить переменную";
            this.addTag.Click += new System.EventHandler(this.AddTagClick);
            // 
            // editTag
            // 
            this.editTag.AutoSize = false;
            this.editTag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editTag.Image = ((System.Drawing.Image)(resources.GetObject("editTag.Image")));
            this.editTag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTag.Name = "editTag";
            this.editTag.Size = new System.Drawing.Size(32, 32);
            this.editTag.Text = "Редактировать переменную";
            this.editTag.Click += new System.EventHandler(this.EditTagClick);
            // 
            // delTag
            // 
            this.delTag.AutoSize = false;
            this.delTag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delTag.Image = ((System.Drawing.Image)(resources.GetObject("delTag.Image")));
            this.delTag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delTag.Name = "delTag";
            this.delTag.Size = new System.Drawing.Size(32, 32);
            this.delTag.Text = "Удалить переменную";
            this.delTag.Click += new System.EventHandler(this.DelTagClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // tunning_pid
            // 
            this.tunning_pid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tunning_pid.Image = ((System.Drawing.Image)(resources.GetObject("tunning_pid.Image")));
            this.tunning_pid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tunning_pid.Name = "tunning_pid";
            this.tunning_pid.Size = new System.Drawing.Size(36, 29);
            this.tunning_pid.Text = "Настройка PID регулятора";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // staticConfig
            // 
            this.staticConfig.AutoSize = false;
            this.staticConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.staticConfig.Image = ((System.Drawing.Image)(resources.GetObject("staticConfig.Image")));
            this.staticConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.staticConfig.Name = "staticConfig";
            this.staticConfig.Size = new System.Drawing.Size(32, 32);
            this.staticConfig.Text = "Статическая конфигурация";
            this.staticConfig.Click += new System.EventHandler(this.StaticConfigClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // set_menu_label
            // 
            this.set_menu_label.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_menu_label.Name = "set_menu_label";
            this.set_menu_label.Size = new System.Drawing.Size(106, 29);
            this.set_menu_label.Text = "Найти канал";
            // 
            // set_find_tag
            // 
            this.set_find_tag.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.set_find_tag.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_find_tag.Name = "set_find_tag";
            this.set_find_tag.Size = new System.Drawing.Size(150, 32);
            this.set_find_tag.TextChanged += new System.EventHandler(this.SetFindTagTextChanged);
            // 
            // set_menu_search
            // 
            this.set_menu_search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.set_menu_search.Image = ((System.Drawing.Image)(resources.GetObject("set_menu_search.Image")));
            this.set_menu_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.set_menu_search.Name = "set_menu_search";
            this.set_menu_search.Size = new System.Drawing.Size(56, 29);
            this.set_menu_search.Text = "Найти";
            this.set_menu_search.Click += new System.EventHandler(this.SetMenuSearchClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // exportHardwareConfig
            // 
            this.exportHardwareConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportHardwareConfig.Image = ((System.Drawing.Image)(resources.GetObject("exportHardwareConfig.Image")));
            this.exportHardwareConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportHardwareConfig.Name = "exportHardwareConfig";
            this.exportHardwareConfig.Size = new System.Drawing.Size(36, 29);
            this.exportHardwareConfig.Text = "Экспортировать конфигурацию контроллера";
            this.exportHardwareConfig.Click += new System.EventHandler(this.ExportHardwareConfigClick);
            // 
            // openConfigDialog
            // 
            this.openConfigDialog.Filter = "Hardware Config files  * .cfg|*.cfg";
            // 
            // set_setting
            // 
            this.set_setting.Controls.Add(this.groupBox1);
            this.set_setting.Controls.Add(this.l_data_static);
            this.set_setting.Controls.Add(this.label6);
            this.set_setting.Controls.Add(this.l_samples);
            this.set_setting.Controls.Add(this.open_folder_dialog);
            this.set_setting.Controls.Add(this.p_plc_config);
            this.set_setting.Controls.Add(this.label5);
            this.set_setting.Controls.Add(this.l_static);
            this.set_setting.Controls.Add(this.l_dinamic);
            this.set_setting.Controls.Add(this.l_version);
            this.set_setting.Location = new System.Drawing.Point(4, 33);
            this.set_setting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_setting.Name = "set_setting";
            this.set_setting.Size = new System.Drawing.Size(1016, 699);
            this.set_setting.TabIndex = 3;
            this.set_setting.Text = "Настройка";
            this.set_setting.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgw_hist_plc_config);
            this.groupBox1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(18, 276);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 10, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(978, 411);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Конфигурация PLC";
            // 
            // dgw_hist_plc_config
            // 
            this.dgw_hist_plc_config.AllowUserToAddRows = false;
            this.dgw_hist_plc_config.AllowUserToDeleteRows = false;
            this.dgw_hist_plc_config.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgw_hist_plc_config.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgw_hist_plc_config.ColumnHeadersHeight = 50;
            this.dgw_hist_plc_config.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgw_hist_plc_config.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewButtonColumn1});
            this.dgw_hist_plc_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgw_hist_plc_config.Location = new System.Drawing.Point(3, 33);
            this.dgw_hist_plc_config.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgw_hist_plc_config.MultiSelect = false;
            this.dgw_hist_plc_config.Name = "dgw_hist_plc_config";
            this.dgw_hist_plc_config.RowHeadersVisible = false;
            this.dgw_hist_plc_config.RowTemplate.Height = 24;
            this.dgw_hist_plc_config.Size = new System.Drawing.Size(972, 376);
            this.dgw_hist_plc_config.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn4.FillWeight = 25F;
            this.dataGridViewTextBoxColumn4.HeaderText = "Номер";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.Format = "G";
            dataGridViewCellStyle9.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn5.FillWeight = 45F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Дата создания";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewComboBoxColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewComboBoxColumn1.FillWeight = 55F;
            this.dataGridViewComboBoxColumn1.HeaderText = "Количество изменений";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.FillWeight = 90F;
            this.dataGridViewButtonColumn1.HeaderText = "Востановить";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Width = 300;
            // 
            // l_data_static
            // 
            this.l_data_static.AutoSize = true;
            this.l_data_static.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.l_data_static.Location = new System.Drawing.Point(29, 56);
            this.l_data_static.Name = "l_data_static";
            this.l_data_static.Size = new System.Drawing.Size(280, 24);
            this.l_data_static.TabIndex = 10;
            this.l_data_static.Text = "Дата статической конфигурации:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(29, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 24);
            this.label6.TabIndex = 7;
            this.label6.Text = "Интервал тренда (сек):";
            // 
            // l_samples
            // 
            this.l_samples.AutoSize = true;
            this.l_samples.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.l_samples.Location = new System.Drawing.Point(29, 232);
            this.l_samples.Name = "l_samples";
            this.l_samples.Size = new System.Drawing.Size(274, 24);
            this.l_samples.TabIndex = 6;
            this.l_samples.Text = "Время обновления тренда (сек):";
            // 
            // open_folder_dialog
            // 
            this.open_folder_dialog.Location = new System.Drawing.Point(616, 152);
            this.open_folder_dialog.Name = "open_folder_dialog";
            this.open_folder_dialog.Size = new System.Drawing.Size(55, 32);
            this.open_folder_dialog.TabIndex = 5;
            this.open_folder_dialog.Text = "...";
            this.open_folder_dialog.UseVisualStyleBackColor = true;
            this.open_folder_dialog.Click += new System.EventHandler(this.OpenFolderDialogClick);
            // 
            // p_plc_config
            // 
            this.p_plc_config.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.p_plc_config.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.p_plc_config.Location = new System.Drawing.Point(309, 153);
            this.p_plc_config.Name = "p_plc_config";
            this.p_plc_config.ReadOnly = true;
            this.p_plc_config.Size = new System.Drawing.Size(309, 30);
            this.p_plc_config.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(29, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(276, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "Путь к файлам параметров PLC:";
            // 
            // l_static
            // 
            this.l_static.AutoSize = true;
            this.l_static.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.l_static.Location = new System.Drawing.Point(29, 122);
            this.l_static.Name = "l_static";
            this.l_static.Size = new System.Drawing.Size(239, 24);
            this.l_static.TabIndex = 2;
            this.l_static.Text = "Кол-во изменений (статика):";
            // 
            // l_dinamic
            // 
            this.l_dinamic.AutoSize = true;
            this.l_dinamic.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.l_dinamic.Location = new System.Drawing.Point(29, 89);
            this.l_dinamic.Name = "l_dinamic";
            this.l_dinamic.Size = new System.Drawing.Size(256, 24);
            this.l_dinamic.TabIndex = 1;
            this.l_dinamic.Text = "Кол-во изменений (динамика):";
            // 
            // l_version
            // 
            this.l_version.AutoSize = true;
            this.l_version.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.l_version.Location = new System.Drawing.Point(29, 27);
            this.l_version.Name = "l_version";
            this.l_version.Size = new System.Drawing.Size(176, 24);
            this.l_version.TabIndex = 0;
            this.l_version.Text = "Версия компонента:";
            // 
            // set_mount
            // 
            this.set_mount.Controls.Add(this.set_pan_mount_wait);
            this.set_mount.Controls.Add(this.set_gb_type_module);
            this.set_mount.Controls.Add(this.set_b_channel_mount_cancel);
            this.set_mount.Controls.Add(this.set_b_channel_mount_ok);
            this.set_mount.Controls.Add(this.set_gb_type_plc);
            this.set_mount.Controls.Add(this.set_treeview_mount);
            this.set_mount.Controls.Add(this.set_gb_channel_mount);
            this.set_mount.Location = new System.Drawing.Point(4, 33);
            this.set_mount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_mount.Name = "set_mount";
            this.set_mount.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_mount.Size = new System.Drawing.Size(1016, 699);
            this.set_mount.TabIndex = 1;
            this.set_mount.Text = "Привязка каналов";
            this.set_mount.UseVisualStyleBackColor = true;
            // 
            // set_pan_mount_wait
            // 
            this.set_pan_mount_wait.BackColor = System.Drawing.Color.Gainsboro;
            this.set_pan_mount_wait.Controls.Add(this.set_text_mount_wait);
            this.set_pan_mount_wait.Controls.Add(this.label1);
            this.set_pan_mount_wait.Location = new System.Drawing.Point(255, 239);
            this.set_pan_mount_wait.Margin = new System.Windows.Forms.Padding(4);
            this.set_pan_mount_wait.Name = "set_pan_mount_wait";
            this.set_pan_mount_wait.Size = new System.Drawing.Size(493, 159);
            this.set_pan_mount_wait.TabIndex = 1;
            this.set_pan_mount_wait.Visible = false;
            // 
            // set_text_mount_wait
            // 
            this.set_text_mount_wait.Location = new System.Drawing.Point(0, 64);
            this.set_text_mount_wait.Margin = new System.Windows.Forms.Padding(4);
            this.set_text_mount_wait.Multiline = true;
            this.set_text_mount_wait.Name = "set_text_mount_wait";
            this.set_text_mount_wait.ReadOnly = true;
            this.set_text_mount_wait.Size = new System.Drawing.Size(492, 94);
            this.set_text_mount_wait.TabIndex = 1;
            this.set_text_mount_wait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(104, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пожалуйста подождите идет \r\n       выполнение команды...";
            // 
            // set_gb_type_module
            // 
            this.set_gb_type_module.Controls.Add(this.set_b_modul_param_cancel);
            this.set_gb_type_module.Controls.Add(this.set_b_modul_param_ok);
            this.set_gb_type_module.Controls.Add(this.set_nd_channel_count);
            this.set_gb_type_module.Controls.Add(this.set_l_channel_count);
            this.set_gb_type_module.Controls.Add(this.set_ddl_type_modul);
            this.set_gb_type_module.Controls.Add(this.set_l_type_modul);
            this.set_gb_type_module.Location = new System.Drawing.Point(299, 6);
            this.set_gb_type_module.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_gb_type_module.Name = "set_gb_type_module";
            this.set_gb_type_module.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_gb_type_module.Size = new System.Drawing.Size(711, 146);
            this.set_gb_type_module.TabIndex = 7;
            this.set_gb_type_module.TabStop = false;
            this.set_gb_type_module.Text = "Тип модуля";
            this.set_gb_type_module.Visible = false;
            // 
            // set_b_modul_param_cancel
            // 
            this.set_b_modul_param_cancel.Location = new System.Drawing.Point(564, 96);
            this.set_b_modul_param_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_b_modul_param_cancel.Name = "set_b_modul_param_cancel";
            this.set_b_modul_param_cancel.Size = new System.Drawing.Size(140, 40);
            this.set_b_modul_param_cancel.TabIndex = 6;
            this.set_b_modul_param_cancel.Text = "Отмена";
            this.set_b_modul_param_cancel.UseVisualStyleBackColor = true;
            this.set_b_modul_param_cancel.Click += new System.EventHandler(this.SetBModulParamCancelClick);
            // 
            // set_b_modul_param_ok
            // 
            this.set_b_modul_param_ok.Location = new System.Drawing.Point(405, 96);
            this.set_b_modul_param_ok.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_b_modul_param_ok.Name = "set_b_modul_param_ok";
            this.set_b_modul_param_ok.Size = new System.Drawing.Size(140, 40);
            this.set_b_modul_param_ok.TabIndex = 5;
            this.set_b_modul_param_ok.Text = "Применить";
            this.set_b_modul_param_ok.UseVisualStyleBackColor = true;
            this.set_b_modul_param_ok.Click += new System.EventHandler(this.SetBModulParamOkClick);
            // 
            // set_nd_channel_count
            // 
            this.set_nd_channel_count.Location = new System.Drawing.Point(261, 80);
            this.set_nd_channel_count.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_nd_channel_count.Name = "set_nd_channel_count";
            this.set_nd_channel_count.Size = new System.Drawing.Size(128, 30);
            this.set_nd_channel_count.TabIndex = 3;
            this.set_nd_channel_count.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.set_nd_channel_count.ValueChanged += new System.EventHandler(this.SetNdChannelCountValueChanged);
            // 
            // set_l_channel_count
            // 
            this.set_l_channel_count.AutoSize = true;
            this.set_l_channel_count.Location = new System.Drawing.Point(48, 85);
            this.set_l_channel_count.Name = "set_l_channel_count";
            this.set_l_channel_count.Size = new System.Drawing.Size(167, 24);
            this.set_l_channel_count.TabIndex = 2;
            this.set_l_channel_count.Text = "Количество каналов";
            // 
            // set_ddl_type_modul
            // 
            this.set_ddl_type_modul.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.set_ddl_type_modul.FormattingEnabled = true;
            this.set_ddl_type_modul.Items.AddRange(new object[] {
            "Модуль аналогового вывода (AO)",
            "Модуль аналогового ввода (AI)",
            "Модуль дискретного вывода (DO)",
            "Модуль дискретного ввода (DI)"});
            this.set_ddl_type_modul.Location = new System.Drawing.Point(261, 27);
            this.set_ddl_type_modul.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_ddl_type_modul.Name = "set_ddl_type_modul";
            this.set_ddl_type_modul.Size = new System.Drawing.Size(444, 32);
            this.set_ddl_type_modul.TabIndex = 1;
            this.set_ddl_type_modul.SelectedIndexChanged += new System.EventHandler(this.SetDdlTypeModulSelectedIndexChanged);
            // 
            // set_l_type_modul
            // 
            this.set_l_type_modul.AutoSize = true;
            this.set_l_type_modul.Location = new System.Drawing.Point(48, 30);
            this.set_l_type_modul.Name = "set_l_type_modul";
            this.set_l_type_modul.Size = new System.Drawing.Size(101, 24);
            this.set_l_type_modul.TabIndex = 0;
            this.set_l_type_modul.Text = "Тип модуля";
            // 
            // set_b_channel_mount_cancel
            // 
            this.set_b_channel_mount_cancel.Location = new System.Drawing.Point(863, 650);
            this.set_b_channel_mount_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_b_channel_mount_cancel.Name = "set_b_channel_mount_cancel";
            this.set_b_channel_mount_cancel.Size = new System.Drawing.Size(140, 40);
            this.set_b_channel_mount_cancel.TabIndex = 4;
            this.set_b_channel_mount_cancel.Text = "Отмена";
            this.set_b_channel_mount_cancel.UseVisualStyleBackColor = true;
            this.set_b_channel_mount_cancel.Click += new System.EventHandler(this.SetBChannelMountCancelClick);
            // 
            // set_b_channel_mount_ok
            // 
            this.set_b_channel_mount_ok.Location = new System.Drawing.Point(704, 650);
            this.set_b_channel_mount_ok.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_b_channel_mount_ok.Name = "set_b_channel_mount_ok";
            this.set_b_channel_mount_ok.Size = new System.Drawing.Size(140, 40);
            this.set_b_channel_mount_ok.TabIndex = 3;
            this.set_b_channel_mount_ok.Text = "Применить";
            this.set_b_channel_mount_ok.UseVisualStyleBackColor = true;
            this.set_b_channel_mount_ok.Click += new System.EventHandler(this.SetBChannelMountOkClick);
            // 
            // set_gb_type_plc
            // 
            this.set_gb_type_plc.Controls.Add(this.set_b_change_plc);
            this.set_gb_type_plc.Controls.Add(this.set_inp_type_plc);
            this.set_gb_type_plc.Controls.Add(this.set_l_type_plc);
            this.set_gb_type_plc.Controls.Add(this.set_inp_number_plc);
            this.set_gb_type_plc.Controls.Add(this.set_l_number_plc);
            this.set_gb_type_plc.Controls.Add(this.set_inp_name_plc);
            this.set_gb_type_plc.Controls.Add(this.set_l_name_plc);
            this.set_gb_type_plc.Location = new System.Drawing.Point(299, 6);
            this.set_gb_type_plc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_gb_type_plc.Name = "set_gb_type_plc";
            this.set_gb_type_plc.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_gb_type_plc.Size = new System.Drawing.Size(711, 146);
            this.set_gb_type_plc.TabIndex = 1;
            this.set_gb_type_plc.TabStop = false;
            this.set_gb_type_plc.Text = "Параметры PLC";
            // 
            // set_b_change_plc
            // 
            this.set_b_change_plc.Location = new System.Drawing.Point(564, 91);
            this.set_b_change_plc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_b_change_plc.Name = "set_b_change_plc";
            this.set_b_change_plc.Size = new System.Drawing.Size(141, 34);
            this.set_b_change_plc.TabIndex = 6;
            this.set_b_change_plc.Text = "Изменить";
            this.set_b_change_plc.UseVisualStyleBackColor = true;
            this.set_b_change_plc.Click += new System.EventHandler(this.SetBChangePlcClick);
            // 
            // set_inp_type_plc
            // 
            this.set_inp_type_plc.Location = new System.Drawing.Point(128, 94);
            this.set_inp_type_plc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_inp_type_plc.Name = "set_inp_type_plc";
            this.set_inp_type_plc.Size = new System.Drawing.Size(151, 30);
            this.set_inp_type_plc.TabIndex = 5;
            // 
            // set_l_type_plc
            // 
            this.set_l_type_plc.AutoSize = true;
            this.set_l_type_plc.Location = new System.Drawing.Point(20, 96);
            this.set_l_type_plc.Name = "set_l_type_plc";
            this.set_l_type_plc.Size = new System.Drawing.Size(75, 24);
            this.set_l_type_plc.TabIndex = 4;
            this.set_l_type_plc.Text = "Тип PLC";
            // 
            // set_inp_number_plc
            // 
            this.set_inp_number_plc.Location = new System.Drawing.Point(436, 41);
            this.set_inp_number_plc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_inp_number_plc.Name = "set_inp_number_plc";
            this.set_inp_number_plc.Size = new System.Drawing.Size(151, 30);
            this.set_inp_number_plc.TabIndex = 3;
            // 
            // set_l_number_plc
            // 
            this.set_l_number_plc.AutoSize = true;
            this.set_l_number_plc.Location = new System.Drawing.Point(324, 44);
            this.set_l_number_plc.Name = "set_l_number_plc";
            this.set_l_number_plc.Size = new System.Drawing.Size(97, 24);
            this.set_l_number_plc.TabIndex = 2;
            this.set_l_number_plc.Text = "Номер PLC";
            // 
            // set_inp_name_plc
            // 
            this.set_inp_name_plc.Location = new System.Drawing.Point(128, 41);
            this.set_inp_name_plc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_inp_name_plc.Name = "set_inp_name_plc";
            this.set_inp_name_plc.Size = new System.Drawing.Size(151, 30);
            this.set_inp_name_plc.TabIndex = 1;
            // 
            // set_l_name_plc
            // 
            this.set_l_name_plc.AutoSize = true;
            this.set_l_name_plc.Location = new System.Drawing.Point(20, 44);
            this.set_l_name_plc.Name = "set_l_name_plc";
            this.set_l_name_plc.Size = new System.Drawing.Size(79, 24);
            this.set_l_name_plc.TabIndex = 0;
            this.set_l_name_plc.Text = "Имя PLC";
            // 
            // set_treeview_mount
            // 
            this.set_treeview_mount.Dock = System.Windows.Forms.DockStyle.Left;
            this.set_treeview_mount.ImageIndex = 0;
            this.set_treeview_mount.ImageList = this.set_images;
            this.set_treeview_mount.Location = new System.Drawing.Point(3, 2);
            this.set_treeview_mount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_treeview_mount.Name = "set_treeview_mount";
            treeNode3.ContextMenuStrip = this.set_conmenu;
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "modul1";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "Модуль AI";
            treeNode4.ContextMenuStrip = this.set_conmenu;
            treeNode4.ImageIndex = 0;
            treeNode4.Name = "PLC";
            treeNode4.SelectedImageIndex = 0;
            treeNode4.Text = "PLC №";
            this.set_treeview_mount.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.set_treeview_mount.SelectedImageIndex = 0;
            this.set_treeview_mount.Size = new System.Drawing.Size(293, 695);
            this.set_treeview_mount.TabIndex = 0;
            this.set_treeview_mount.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SetTreeviewMountNodeMouseClick);
            // 
            // set_gb_channel_mount
            // 
            this.set_gb_channel_mount.Controls.Add(this.set_dgv_channel_mount);
            this.set_gb_channel_mount.Location = new System.Drawing.Point(299, 156);
            this.set_gb_channel_mount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_gb_channel_mount.Name = "set_gb_channel_mount";
            this.set_gb_channel_mount.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_gb_channel_mount.Size = new System.Drawing.Size(711, 476);
            this.set_gb_channel_mount.TabIndex = 2;
            this.set_gb_channel_mount.TabStop = false;
            this.set_gb_channel_mount.Text = "Привязка каналов";
            // 
            // set_dgv_channel_mount
            // 
            this.set_dgv_channel_mount.AllowUserToAddRows = false;
            this.set_dgv_channel_mount.AllowUserToDeleteRows = false;
            this.set_dgv_channel_mount.AllowUserToResizeRows = false;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.set_dgv_channel_mount.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.set_dgv_channel_mount.ColumnHeadersHeight = 50;
            this.set_dgv_channel_mount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.set_dgv_channel_mount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.chnumber,
            this.signalgroups,
            this.signals,
            this.cok,
            this.groupid,
            this.signalid});
            this.set_dgv_channel_mount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.set_dgv_channel_mount.GridColor = System.Drawing.SystemColors.Control;
            this.set_dgv_channel_mount.Location = new System.Drawing.Point(3, 25);
            this.set_dgv_channel_mount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.set_dgv_channel_mount.MultiSelect = false;
            this.set_dgv_channel_mount.Name = "set_dgv_channel_mount";
            this.set_dgv_channel_mount.RowHeadersVisible = false;
            this.set_dgv_channel_mount.RowTemplate.Height = 24;
            this.set_dgv_channel_mount.Size = new System.Drawing.Size(705, 449);
            this.set_dgv_channel_mount.TabIndex = 0;
            this.set_dgv_channel_mount.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SetDgvChannelMountCellContentClick);
            this.set_dgv_channel_mount.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.SetDgvChannelMountEditingControlShowing);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // chnumber
            // 
            this.chnumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chnumber.FillWeight = 25F;
            this.chnumber.HeaderText = "Номер канала";
            this.chnumber.Name = "chnumber";
            this.chnumber.ReadOnly = true;
            // 
            // signalgroups
            // 
            this.signalgroups.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.signalgroups.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.signalgroups.FillWeight = 90F;
            this.signalgroups.HeaderText = "Группа сигналов";
            this.signalgroups.Name = "signalgroups";
            // 
            // signals
            // 
            this.signals.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.signals.FillWeight = 70F;
            this.signals.HeaderText = "Сигнал";
            this.signals.Name = "signals";
            // 
            // cok
            // 
            this.cok.FillWeight = 80F;
            this.cok.HeaderText = "Применить";
            this.cok.Name = "cok";
            this.cok.Width = 110;
            // 
            // groupid
            // 
            this.groupid.HeaderText = "groupid";
            this.groupid.Name = "groupid";
            this.groupid.Visible = false;
            // 
            // signalid
            // 
            this.signalid.HeaderText = "signalid";
            this.signalid.Name = "signalid";
            this.signalid.Visible = false;
            // 
            // tags
            // 
            this.tags.Controls.Add(this.pan_tag_wait);
            this.tags.Controls.Add(this.tag_descr);
            this.tags.Location = new System.Drawing.Point(4, 33);
            this.tags.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tags.Name = "tags";
            this.tags.Size = new System.Drawing.Size(1016, 699);
            this.tags.TabIndex = 2;
            this.tags.Text = "Переменные";
            this.tags.UseVisualStyleBackColor = true;
            // 
            // pan_tag_wait
            // 
            this.pan_tag_wait.BackColor = System.Drawing.Color.Gainsboro;
            this.pan_tag_wait.Controls.Add(this.text_tag_wait);
            this.pan_tag_wait.Controls.Add(this.label3);
            this.pan_tag_wait.Location = new System.Drawing.Point(268, 220);
            this.pan_tag_wait.Margin = new System.Windows.Forms.Padding(4);
            this.pan_tag_wait.Name = "pan_tag_wait";
            this.pan_tag_wait.Size = new System.Drawing.Size(493, 159);
            this.pan_tag_wait.TabIndex = 5;
            this.pan_tag_wait.Visible = false;
            // 
            // text_tag_wait
            // 
            this.text_tag_wait.Location = new System.Drawing.Point(0, 64);
            this.text_tag_wait.Margin = new System.Windows.Forms.Padding(4);
            this.text_tag_wait.Multiline = true;
            this.text_tag_wait.Name = "text_tag_wait";
            this.text_tag_wait.ReadOnly = true;
            this.text_tag_wait.Size = new System.Drawing.Size(492, 94);
            this.text_tag_wait.TabIndex = 1;
            this.text_tag_wait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(104, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 48);
            this.label3.TabIndex = 0;
            this.label3.Text = "Пожалуйста подождите идет \r\n       выполнение команды...";
            // 
            // tag_descr
            // 
            this.tag_descr.AllowUserToAddRows = false;
            this.tag_descr.AllowUserToDeleteRows = false;
            this.tag_descr.AllowUserToResizeColumns = false;
            this.tag_descr.AllowUserToResizeRows = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tag_descr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.tag_descr.ColumnHeadersHeight = 40;
            this.tag_descr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tag_descr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dw_tag_id,
            this.dw_tag_nameplc,
            this.dw_tag_namescada,
            this.dw_tag_descr});
            this.tag_descr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tag_descr.Location = new System.Drawing.Point(0, 0);
            this.tag_descr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tag_descr.MultiSelect = false;
            this.tag_descr.Name = "tag_descr";
            this.tag_descr.RowHeadersVisible = false;
            this.tag_descr.RowTemplate.Height = 25;
            this.tag_descr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.tag_descr.Size = new System.Drawing.Size(1016, 699);
            this.tag_descr.TabIndex = 4;
            // 
            // dw_tag_id
            // 
            this.dw_tag_id.HeaderText = "id";
            this.dw_tag_id.Name = "dw_tag_id";
            this.dw_tag_id.ReadOnly = true;
            this.dw_tag_id.Visible = false;
            // 
            // dw_tag_nameplc
            // 
            this.dw_tag_nameplc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dw_tag_nameplc.FillWeight = 170F;
            this.dw_tag_nameplc.HeaderText = "Имя PLC";
            this.dw_tag_nameplc.Name = "dw_tag_nameplc";
            this.dw_tag_nameplc.ReadOnly = true;
            // 
            // dw_tag_namescada
            // 
            this.dw_tag_namescada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dw_tag_namescada.FillWeight = 170F;
            this.dw_tag_namescada.HeaderText = "Имя SCADA";
            this.dw_tag_namescada.Name = "dw_tag_namescada";
            this.dw_tag_namescada.ReadOnly = true;
            // 
            // dw_tag_descr
            // 
            this.dw_tag_descr.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dw_tag_descr.FillWeight = 500F;
            this.dw_tag_descr.HeaderText = "Описание";
            this.dw_tag_descr.Name = "dw_tag_descr";
            this.dw_tag_descr.ReadOnly = true;
            this.dw_tag_descr.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // tabConfigPLC_S7
            // 
            this.tabConfigPLC_S7.Controls.Add(this.tags);
            this.tabConfigPLC_S7.Controls.Add(this.set_mount);
            this.tabConfigPLC_S7.Controls.Add(this.set_setting);
            this.tabConfigPLC_S7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabConfigPLC_S7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabConfigPLC_S7.Location = new System.Drawing.Point(0, 32);
            this.tabConfigPLC_S7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabConfigPLC_S7.Name = "tabConfigPLC_S7";
            this.tabConfigPLC_S7.SelectedIndex = 0;
            this.tabConfigPLC_S7.Size = new System.Drawing.Size(1024, 736);
            this.tabConfigPLC_S7.TabIndex = 0;
            this.tabConfigPLC_S7.SelectedIndexChanged += new System.EventHandler(this.TabConfigPlcS7SelectedIndexChanged);
            // 
            // folderDialog
            // 
            this.folderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // ConfigPLC_S7
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tabConfigPLC_S7);
            this.Controls.Add(this.set_menu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ConfigPLC_S7";
            this.Size = new System.Drawing.Size(1024, 768);
            this.Load += new System.EventHandler(this.ConfigPlcS7Load);
            this.set_conmenu.ResumeLayout(false);
            this.set_menu.ResumeLayout(false);
            this.set_menu.PerformLayout();
            this.set_setting.ResumeLayout(false);
            this.set_setting.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgw_hist_plc_config)).EndInit();
            this.set_mount.ResumeLayout(false);
            this.set_pan_mount_wait.ResumeLayout(false);
            this.set_pan_mount_wait.PerformLayout();
            this.set_gb_type_module.ResumeLayout(false);
            this.set_gb_type_module.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.set_nd_channel_count)).EndInit();
            this.set_gb_type_plc.ResumeLayout(false);
            this.set_gb_type_plc.PerformLayout();
            this.set_gb_channel_mount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.set_dgv_channel_mount)).EndInit();
            this.tags.ResumeLayout(false);
            this.pan_tag_wait.ResumeLayout(false);
            this.pan_tag_wait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tag_descr)).EndInit();
            this.tabConfigPLC_S7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip set_menu;
        private System.Windows.Forms.ToolStripLabel set_menu_label;
        private System.Windows.Forms.ToolStripTextBox set_find_tag;
        private System.Windows.Forms.ToolStripButton set_menu_search;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList set_images;
        private System.Windows.Forms.ContextMenuStrip set_conmenu;
        private System.Windows.Forms.ToolStripMenuItem set_conmenu_add;
        private System.Windows.Forms.ToolStripMenuItem set_conmenu_del;
        private System.Windows.Forms.ToolStripButton addTag;
        private System.Windows.Forms.ToolStripButton editTag;
        private System.Windows.Forms.ToolStripButton delTag;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton staticConfig;
        private System.Windows.Forms.ToolStripButton exportHardwareConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.OpenFileDialog openConfigDialog;
        private System.Windows.Forms.TabPage set_setting;
        private System.Windows.Forms.TabPage set_mount;
        private System.Windows.Forms.Panel set_pan_mount_wait;
        private System.Windows.Forms.TextBox set_text_mount_wait;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox set_gb_type_module;
        private System.Windows.Forms.Button set_b_modul_param_cancel;
        private System.Windows.Forms.Button set_b_modul_param_ok;
        private System.Windows.Forms.NumericUpDown set_nd_channel_count;
        private System.Windows.Forms.Label set_l_channel_count;
        private System.Windows.Forms.ComboBox set_ddl_type_modul;
        private System.Windows.Forms.Label set_l_type_modul;
        private System.Windows.Forms.Button set_b_channel_mount_cancel;
        private System.Windows.Forms.Button set_b_channel_mount_ok;
        private System.Windows.Forms.GroupBox set_gb_type_plc;
        private System.Windows.Forms.Button set_b_change_plc;
        private System.Windows.Forms.TextBox set_inp_type_plc;
        private System.Windows.Forms.Label set_l_type_plc;
        private System.Windows.Forms.TextBox set_inp_number_plc;
        private System.Windows.Forms.Label set_l_number_plc;
        private System.Windows.Forms.TextBox set_inp_name_plc;
        private System.Windows.Forms.Label set_l_name_plc;
        private System.Windows.Forms.TreeView set_treeview_mount;
        private System.Windows.Forms.GroupBox set_gb_channel_mount;
        private System.Windows.Forms.DataGridView set_dgv_channel_mount;
        private System.Windows.Forms.TabPage tags;
        private System.Windows.Forms.Panel pan_tag_wait;
        private System.Windows.Forms.TextBox text_tag_wait;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView tag_descr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw_tag_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw_tag_nameplc;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw_tag_namescada;
        private System.Windows.Forms.DataGridViewTextBoxColumn dw_tag_descr;
        private System.Windows.Forms.TabControl tabConfigPLC_S7;
        private System.Windows.Forms.ToolStripButton tunning_pid;
        private System.Windows.Forms.Label l_samples;
        private System.Windows.Forms.Button open_folder_dialog;
        private System.Windows.Forms.TextBox p_plc_config;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label l_static;
        private System.Windows.Forms.Label l_dinamic;
        private System.Windows.Forms.Label l_version;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private CustomControl.DigitTextBox inp_time_samples;
        private CustomControl.DigitTextBox inp_time_interval;
        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.Label l_data_static;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgw_hist_plc_config;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn chnumber;
        private System.Windows.Forms.DataGridViewComboBoxColumn signalgroups;
        private System.Windows.Forms.DataGridViewComboBoxColumn signals;
        private System.Windows.Forms.DataGridViewButtonColumn cok;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupid;
        private System.Windows.Forms.DataGridViewTextBoxColumn signalid;
    }
}
