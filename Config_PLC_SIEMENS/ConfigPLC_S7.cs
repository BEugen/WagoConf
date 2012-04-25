using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace RtpWagoConf
{


    [ProgId("ConfigWagoRtp")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof (IScadaInterfaceEvent))]
    [Guid("DDBFAC4B-2024-4A48-B929-97AA484FE19D")]
    public partial class ConfigPLC_S7 : UserControl, IScadaInterface
    {

        protected struct InternalCommandStackParam
        {
            public int Command;
            public object Value;
            public int[] CallBack;
        }

        protected struct CommandToPlc
        {
            public int CommandNumber;
            public int[] Values;
        }

        protected enum CommandName
        {
            SetupTimeShibers1 = 1,
            SetupReopenShibers = 2,
            MountChannel = 3,
            MountModul = 4,
            MountGenericSignals = 5,
            MountShiberToOneSequency = 6,
            MountShiberToGroupSequency = 7,
            MountShiberNumberToGroupSequency = 8,
            OnOffBypassShiber = 9,
            SetupTimeShibers2 = 10,
            SetupTimeBetwinCycle = 11
        }

        private int _selectedTab;

        #region VariableForPropertisSCADA

        private int _rtpid = 0;
        private int _command = -1;
        private int _accept = -1;
        private int[] _params = {0, 0, 0, 0, 0, 0};


        #endregion

        private delegate void Ui(bool eDwait);

        private delegate void Ui1(int tagId);

        private delegate void GridGroupCheck(int columnIndex);

        private delegate void WaitMessageArg(string text, bool waitVisible);

        private delegate void CheckShiberSetup(int indexRow, int indexColumn);

        private Queue<CommandToPlc> commandToPlc;
        private readonly System.Timers.Timer _tmrElapsedCmd;
        private Dictionary<int, int> checkedRow = new Dictionary<int, int>();
        private bool _shangevaluecycle = false;
        private int _valuecycle = 0;
        private int _minAccessLevelToConfigurePlc = 9999;
        private int _currentAccessLevelToConfigurePlc = 0;
        private int _selectgroup = 0;
        private int _selectsingle = 0;

        public ConfigPLC_S7()
        {
            InitializeComponent();
            set_gb_channel_mount.Visible = false;
            set_b_channel_mount_ok.Visible = false;
            _tmrElapsedCmd = new System.Timers.Timer {Interval = 1000*60 /*_parametrsConfig.TimeOut*/};
            _tmrElapsedCmd.Elapsed += TmrElapsedCmdElapsed;
            commandToPlc = new Queue<CommandToPlc>();
            _accept = _command = 0;
            _params = new[] {0, 0, 0, 0, 0, 0};
            // For the Click event that is re-defined.
            //base.Click += new EventHandler(ActiveXCtrl_Click);

            // These functions are used to handle Tab-stops for the ActiveX 
            // control (including its child controls) when the control is 
            // hosted in a container.
            LostFocus += ActiveXCtrlLostFocus;
            ControlAdded += ActiveXCtrlControlAdded;

            // Raise custom Load event
            OnCreateControl();

        }

        protected override sealed void OnCreateControl()
        {
            base.OnCreateControl();
        }


        private void TmrElapsedCmdElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _tmrElapsedCmd.Stop();
            RtpConfigDataContext data = new RtpConfigDataContext();
            switch (_command)
            {
                case (int)CommandName.MountChannel: 
                case (int)CommandName.MountModul:
                case (int)CommandName.MountGenericSignals:
                    data.SetErrorDownloadToPlc(_rtpid, 1, 1);
                    break;
                case (int)CommandName.MountShiberNumberToGroupSequency:
                case (int)CommandName.MountShiberToGroupSequency:
                    data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                    break;
                case (int)CommandName.MountShiberToOneSequency:
                    data.SetErrorDownloadToPlc(_rtpid, 3, 1);
                    break;
                case (int)CommandName.SetupReopenShibers:
                case (int)CommandName.SetupTimeShibers1:
                case (int)CommandName.SetupTimeShibers2:
                    data.SetErrorDownloadToPlc(_rtpid, 4, 1);
                    break;
            }
            
            Ui ui = WaitMount;
            set_treeview_mount.BeginInvoke(ui, new object[] {false});
            MessageBox.Show(_accept == 5 ? "Ошибка связи с системой визуализации" : "Ошибка связи с PLC", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            commandToPlc.Clear();
            _command = -1;
            _params = new[] {0, 0, 0, 0, 0, 0};
            _accept = 1;
        }

        #region PublicForSCADA

        public int Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public int Accept
        {
            //[return: MarshalAs(UnmanagedType.SysInt)]
            get { return _accept; }
            // [param: MarshalAs(UnmanagedType.SysInt)]
            set
            {
                if (value > 1)
                {
                    _accept = value;
                    AcceptForPlc();
                }
            }
        }

        public int P1
        {
            //  [return: MarshalAs(UnmanagedType.I2)]
            get { return _params[0]; }
            set { _params[0] = value; }
        }

        public int P2
        {
            // [return: MarshalAs(UnmanagedType.I2)]
            get { return _params[1]; }
            set { _params[1] = value; }
        }

        public int P3
        {
            //  [return: MarshalAs(UnmanagedType.I2)]
            get { return _params[2]; }
            set { _params[2] = value; }
        }

        public int P4
        {
            // [return: MarshalAs(UnmanagedType.I2)]
            get { return _params[3]; }
            set { _params[3] = value; }

        }

        public int P5
        {

            get { return _params[4]; }
            set { _params[4] = value; }
        }

        public int P6
        {
            get { return _params[5]; }
            set { _params[5] = value; }
        }

        public int ShiberSelect
        {
            set
            {
                SelectShiberConfig(value);
            }
            get { return -1; }
        }

        public int GroupSetup 
        { 
            set 
            {
                typeWorkToGroupSetup.SelectedIndex = 1;
                LoadGroupConfig();
                tabConfiпWago.SelectedIndex = 2;
                _selectgroup = 1;
                _selectsingle = 0;
            }
            get { return _selectgroup; }
        }
        public int SingleSetup 
        { 
            set
            {
                typeWorkToSingleSetup.SelectedIndex = 1;
                LoadSingleConfig();
                tabConfiпWago.SelectedIndex = 3;
                _selectgroup = 0;
                _selectsingle = 1;
            }
            get { return _selectsingle; }
        }

        public int CurrentAccessLevel
        {
            get { return _currentAccessLevelToConfigurePlc; }
            set
            {
                _currentAccessLevelToConfigurePlc = value;
                CheckAccessToConfigPlc();
            }
        }
        public int MinAccessLevelToConfigPlc
        {
            get { return _minAccessLevelToConfigurePlc; }
            set
            {
                _minAccessLevelToConfigurePlc = value;
                CheckAccessToConfigPlc();
            }
        }
        #endregion

        #region Event

        [ComVisible(false)]
        public delegate void CommandEventHandler();

        public event CommandEventHandler CommandEvent = null;

        #endregion

        private bool CheckAccessToConfigPlc()
        {
            
            set_mount.Enabled = false;
            tag_descr.Enabled = false;
            set_setting.Enabled = false;
            if (_currentAccessLevelToConfigurePlc >= _minAccessLevelToConfigurePlc)
            {
                set_mount.Enabled = true;
                tag_descr.Enabled = true;
                set_setting.Enabled = true;
                group_setup.Enabled = true;
                single_setup.Enabled = true;
                shiber_setup.Enabled = true;
                return true;
            }

            if(_currentAccessLevelToConfigurePlc < 1000)
            {
                group_setup.Enabled = false;
                single_setup.Enabled = false;
                shiber_setup.Enabled = false;
               return false;
            }           
            group_setup.Enabled = true;
            single_setup.Enabled = true;
            shiber_setup.Enabled = true;
            return false;
        }

        private void SetTreeviewMountNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                set_conmenu_del.Visible = false;
                set_gb_type_plc.Visible = true;
                set_gb_type_module.Visible = false;
                set_gb_channel_mount.Visible = false;
                set_b_channel_mount_ok.Visible = false;
                PlcInfLoad();
            }
            else
            {
                set_conmenu_del.Visible = true;
                set_gb_type_plc.Visible = false;
                set_gb_type_module.Visible = true;
                set_gb_channel_mount.Visible = true;
                set_b_channel_mount_ok.Visible = true;
                set_treeview_mount.SelectedNode = e.Node;
                LoadChannelMount(Convert.ToInt32(e.Node.Tag));
            }
        }

        private void LoadChannelMount(int selectedModul)
        {

            RtpConfigDataContext data = new RtpConfigDataContext();
            var signalGroups = data.GetRtpSignalGroups().ToArray();
            var channels = data.GetChannel(_rtpid, selectedModul).ToList();
            set_ddl_type_modul.Items.Clear();
            set_ddl_type_modul.Tag = 0;
            set_nd_channel_count.Tag = 0;
            var modultypes = data.GetModulType().ToList();
            foreach (var modultype in modultypes)
            {
                set_ddl_type_modul.Items.Add(modultype.descript);
            }
            set_ddl_type_modul.Tag = set_ddl_type_modul.SelectedIndex = channels.First().channeltype;
            set_nd_channel_count.Value = channels.Count();
            set_dgv_channel_mount.Rows.Clear();
            foreach (var channel in channels)
            {
                int rnumber = set_dgv_channel_mount.Rows.Add();
                set_dgv_channel_mount.Rows[rnumber].Cells[0].Value = channel.id;
                set_dgv_channel_mount.Rows[rnumber].Cells[1].Value = channel.channelnumber;
                ((DataGridViewComboBoxCell) set_dgv_channel_mount.Rows[rnumber].Cells[2]).Items.Clear();
                foreach (var signalGroup in signalGroups)
                {
                    ((DataGridViewComboBoxCell) set_dgv_channel_mount.Rows[rnumber].Cells[2]).Items.Add(
                        signalGroup.signalgroupdescription);
                }
                if (channel.groupid == null)
                {
                    set_dgv_channel_mount.Rows[rnumber].Cells[3].ReadOnly = true;

                }
                else
                {
                    set_dgv_channel_mount.Rows[rnumber].Cells[5].Value = channel.groupid;
                    set_dgv_channel_mount.Rows[rnumber].Cells[6].Value = channel.signalid;
                    set_dgv_channel_mount.Rows[rnumber].Cells[2].Value =
                        signalGroups[channel.groupid.Value].signalgroupdescription;
                    var getRtpSignalsResults = data.GetRtpSignals(channel.groupid, channel.channeltype).ToArray();
                    ((DataGridViewComboBoxCell) set_dgv_channel_mount.Rows[rnumber].Cells[3]).Items.Clear();
                    foreach (var signal in getRtpSignalsResults)
                    {
                        ((DataGridViewComboBoxCell) set_dgv_channel_mount.Rows[rnumber].Cells[3]).Items.Add(
                            signal.signaldescription);
                    }
                }
                set_dgv_channel_mount.Rows[rnumber].Cells[2].Value = channel.signalgroupdescription;
                set_dgv_channel_mount.Rows[rnumber].Cells[3].Value = channel.signaldescription;
                set_dgv_channel_mount.Rows[rnumber].Cells[4].Value = "Применить";
            }
        }

        private void PlcInfLoad()
        {

            //PLC plc = configClass.GetPlc();
            //set_inp_name_plc.Text = plc.namePLC;
            //set_inp_type_plc.Text = plc.typePLC;
            //set_inp_number_plc.Text = plc.numberPLC.ToString();
            //set_treeview_mount.Nodes[0].Text = "PLC №" + set_inp_number_plc.Text;


        }

        private void SetConmenuAddClick(object sender, EventArgs e)
        {
            Set_form_module_add setFormModuleAdd = new Set_form_module_add();
            if (setFormModuleAdd.ShowDialog() != DialogResult.OK) return;
            RtpConfigDataContext data = new RtpConfigDataContext();
            data.AddNewModul(_rtpid, setFormModuleAdd.CountChannel, setFormModuleAdd.ModuleType);
            SetLoadChannelMount();
        }

        private void TabConfigPlcS7SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedTab = tabConfiпWago.SelectedIndex;
            text_wait.Text = "Идет загрузка данных";
            WaitMount(true);
            switch (tabConfiпWago.SelectedIndex)
            {
                case 0:
                    LoadAllModuleChannel();
                    break;
                case 1:
                    typeWork.SelectedIndex = 1;
                    SetLoadChannelMount();
                    
                    break;
                case 2:
                    typeWorkToGroupSetup.SelectedIndex = 1;
                    LoadGroupConfig();
                    break;
                case 3:
                    typeWorkToSingleSetup.SelectedIndex = 1;
                    LoadSingleConfig();
                    break;
                case 4:
                    typeWorkToShiberSetup.SelectedIndex = 1;
                    LoadShiberSetup();
                    break;
                default:
                    break;

            }
            CheckHardwareConfigError();
            WaitMount(false);
            ChangeEnableButtons(tabConfiпWago.SelectedIndex);
        }

        private void CheckHardwareConfigError()
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            var flag = data.GetErrorDownloadToPlc(_rtpid).First();
            if (flag.changehardware == 1)
            {
                checkHardwareIcon.Image = set_images.Images[3];
                checkHardwareIcon.ToolTipText = "Конфигурация контроллера не соотвествует базе";
            }
            else
            {
                checkHardwareIcon.Image = set_images.Images[2];
                checkHardwareIcon.ToolTipText = "Конфигурация контроллера соответсвует базе";
            }
            if (flag.changegroupconfig == 1)
            {
                checkGroupSetup.Image = set_images.Images[3];
                checkGroupSetup.ToolTipText = "Настройка группового режима не соотвествует базе";
            }
            else
            {
                checkGroupSetup.Image = set_images.Images[2];
                checkGroupSetup.ToolTipText = "Настройка группового режима соответсвует базе";
            }
            if (flag.changesingleconfig == 1)
            {
                checkSingleSetup.Image = set_images.Images[3];
                checkSingleSetup.ToolTipText = "Настройка одиночного режима не соотвествует базе";
            }
            else
            {
                checkSingleSetup.Image = set_images.Images[2];
                checkSingleSetup.ToolTipText = "Настройка одиночного режима соответсвует базе";
            }
            if (flag.changeshiberconfig == 1)
            {
                checkShiberSetup.Image = set_images.Images[3];
                checkShiberSetup.ToolTipText = "Настройка шибера не соотвествует базе";
            }
            else
            {
                checkShiberSetup.Image = set_images.Images[2];
                checkShiberSetup.ToolTipText = "Настройка шибера соответсвует базе";
            }

        }

        private void LoadAllModuleChannel()
        {
            try
            {

                RtpConfigDataContext data = new RtpConfigDataContext();
                var modulchannel = data.GetAllModuleChannel(_rtpid).ToList();
                tag_descr.Rows.Clear();
                foreach (var getAllModuleChannelResult in modulchannel)
                {
                    int rnumber = tag_descr.Rows.Add();
                    tag_descr.Rows[rnumber].Cells[0].Value = getAllModuleChannelResult.descript;
                    tag_descr.Rows[rnumber].Cells[1].Value = getAllModuleChannelResult.modulnumber;
                    tag_descr.Rows[rnumber].Cells[2].Value = getAllModuleChannelResult.channelnumber;
                    tag_descr.Rows[rnumber].Cells[3].Value = getAllModuleChannelResult.signalgroupdescription;
                    tag_descr.Rows[rnumber].Cells[4].Value = getAllModuleChannelResult.signaldescription;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ошибка загрузки данных (" + ex.Message + ")", "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменяет доступность кнопок меню
        /// </summary>
        /// <param name="tabSelectIndex">Индекс вкладки</param>
        private void ChangeEnableButtons(int tabSelectIndex)
        {
            downloadConfig.Enabled = false;

            switch (tabSelectIndex)
            {
                case 0:
                case 1:
                    downloadConfig.Enabled =
                        true;
                    break;
                case 3:
                    downloadConfig.Enabled =
                        true;
                    break;
                default:
                    break;
            }
        }

        private void SetLoadChannelMount()
        {
            try
            {


                set_treeview_mount.Nodes[0].Nodes.Clear();
                    RtpConfigDataContext data = new RtpConfigDataContext();
                    var moduls = data.GetModule(_rtpid);
                    foreach (var modul in moduls)
                    {
                        TreeNode trNode = new TreeNode();
                        trNode.ImageIndex = 1;
                        trNode.SelectedImageIndex = 1;
                        trNode.Tag = modul.modulnumber;
                        trNode.ContextMenuStrip = set_treeview_mount.Nodes[0].ContextMenuStrip;
                        trNode.Text = "Модуль " + modul.descript + " №" + modul.modulnumber;
                        set_treeview_mount.Nodes[0].Nodes.Add(trNode);
                    }
                set_treeview_mount.SelectedNode = set_treeview_mount.Nodes[0];
                TreeNodeMouseClickEventArgs e = new TreeNodeMouseClickEventArgs(set_treeview_mount.SelectedNode,
                                                                                MouseButtons.Left, 0, 0, 0);
                SetTreeviewMountNodeMouseClick(set_treeview_mount, e);
                PlcInfLoad();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка загрузки данных (" + ex.Message + ")", "Ошибка", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void SetBModulParamOkClick(object sender, EventArgs e)
        {
            int result;
            RtpConfigDataContext data = new RtpConfigDataContext();
            if (set_treeview_mount.SelectedNode.Tag == null)
            {
                MessageBox.Show("Не выбран модуль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (set_nd_channel_count.Tag != null && (int) set_nd_channel_count.Tag == 1)
            {
                result = data.ChangeCountChannel(_rtpid, Convert.ToInt32(set_treeview_mount.SelectedNode.Tag),
                                                 (int) set_nd_channel_count.Value);
                if (result != 0)
                {
                    MessageBox.Show("Ошибка изменения числа каналов", "Ошибка", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }
                LoadChannelMount(Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            }
            if (set_ddl_type_modul.Tag != null && (int) set_ddl_type_modul.Tag == 1)
            {
                result = data.ChangeModulType(_rtpid, Convert.ToInt32(set_treeview_mount.SelectedNode.Tag),
                                              set_ddl_type_modul.SelectedIndex);
                if (result != 0)
                {
                    MessageBox.Show("Ошибка изменения типа модуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SetLoadChannelMount();
            }
        }


        private void SetDgvChannelMountCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (set_dgv_channel_mount.Rows[e.RowIndex].Cells[2].Value != null &&
                    set_dgv_channel_mount.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    MessageBox.Show("Не указана привязка сигнала \nМодуль №" +
                                    Convert.ToInt32(set_treeview_mount.SelectedNode.Tag) +
                                    " Канал №" + (e.RowIndex + 1)
                                    , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                CheckOldMount(Convert.ToInt32(set_treeview_mount.SelectedNode.Tag),
                              Convert.ToInt32(set_dgv_channel_mount.Rows[e.RowIndex].Cells[1].Value));
                NewMount(e.RowIndex);
                if (typeWork.SelectedIndex == 1)
                    CommandForPlc();
                else
                {
                    commandToPlc.Clear();
                    RtpConfigDataContext data = new RtpConfigDataContext();
                    data.SetErrorDownloadToPlc(_rtpid, 1, 1);
                }
            }
            set_dgv_channel_mount.RefreshEdit();
        }

        private void CheckOldMount(int modulnumber, int channelnumber)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            int[] paramset1 = new int[6];
            int[] paramset2 = new int[6];

            var mount = data.CheckMountChannel(_rtpid, modulnumber, channelnumber).ToList();

            var commandOne = new CommandToPlc();
            if (mount.Count > 0 && mount.First().commandid != null &&
                mount.First().commandid.Value == (int) CommandName.MountChannel)
            {
                var shibernumber = mount.First().shibernumber;
                if (shibernumber != null)
                {
                    paramset1[0] = shibernumber.Value;
                    paramset2[0] = shibernumber.Value;
                }
                for (int i = 0; i < mount.Count && i < 4; i++)
                {
                    if (mount[i].signaltype < paramset1.Length)
                    {
                        paramset1[mount[i].signaltype + 1] = mount[i].offsetChannel == null
                                                                 ? 0
                                                                 : mount[i].offsetChannel.Value;

                        if (mount[i].channelnumber != null && mount[i].channelnumber.Value == channelnumber &&
                            mount[i].modulnumber != null && mount[i].modulnumber.Value == modulnumber)
                        {
                            paramset2[mount[i].signaltype + 1] = -1;
                        }
                        else
                        {
                            paramset2[mount[i].signaltype + 1] = mount[i].offsetModul == null
                                                                     ? 0
                                                                     : mount[i].offsetModul.Value;
                        }

                    }
                }
                commandOne.CommandNumber = (int) CommandName.MountChannel;
                commandOne.Values = paramset1;
                var commandTwo = new CommandToPlc();
                commandTwo.CommandNumber = (int) CommandName.MountModul;
                commandTwo.Values = paramset2;
                commandToPlc.Enqueue(commandTwo);
                commandToPlc.Enqueue(commandOne);

            }
            if (mount.Count > 0 && mount.First().commandid == (int) CommandName.MountGenericSignals)
            {
                var paramset = mount.First();
                paramset1[0] = paramset.signaltype;
                paramset1[1] = paramset.channelnumber == null ? 0 : paramset.channelnumber.Value - 1;
                paramset1[2] = paramset.signalcontrain;
                paramset1[3] = paramset.modulnumber == null ? 0 : paramset.modulnumber.Value - 1;
                commandOne.CommandNumber = (int) CommandName.MountGenericSignals;
                commandOne.Values = paramset1;
                commandToPlc.Enqueue(commandOne);

            }
        }

        private void NewMount(int rowIndex)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            int[] paramset1 = new int[6];
            int[] paramset2 = new int[6];

            var mount =
                data.GetChannelCurrentShibers(_rtpid,
                                              set_dgv_channel_mount.Rows[rowIndex].Cells[0].Value == null
                                                  ? -1
                                                  : Convert.ToInt32(set_dgv_channel_mount.Rows[rowIndex].Cells[0].Value),
                                              set_dgv_channel_mount.Rows[rowIndex].Cells[5].Value == null
                                                  ? -1
                                                  : Convert.ToInt32(set_dgv_channel_mount.Rows[rowIndex].Cells[5].Value),
                                              set_dgv_channel_mount.Rows[rowIndex].Cells[6].Value == null
                                                  ? -1
                                                  : Convert.ToInt32(set_dgv_channel_mount.Rows[rowIndex].Cells[6].Value))
                    .ToList();

            var commandOne = new CommandToPlc();
            var commandid = mount.First().commandid;
            if (commandid != null && (mount.Count > 0 && commandid.Value == (int) CommandName.MountChannel))
            {
                var shibernumber = mount.First().shibernumber;
                if (shibernumber != null)
                {
                    paramset1[0] = shibernumber.Value;
                    paramset2[0] = shibernumber.Value;
                }
                for (int i = 0; i < mount.Count && i < 4; i++)
                {
                    if (mount[i].signaltype < paramset1.Length)
                    {
                        paramset1[mount[i].signaltype + 1] = mount[i].offsetChannel == null
                                                                 ? 0
                                                                 : mount[i].offsetChannel.Value;

                        paramset2[mount[i].signaltype + 1] = mount[i].modulnumber == null
                                                                 ? -1
                                                                 : mount[i].offsetModul.Value;
                    }
                }
                commandOne.CommandNumber = (int) CommandName.MountChannel;
                commandOne.Values = paramset1;
                var commandTwo = new CommandToPlc();
                commandTwo.CommandNumber = (int) CommandName.MountModul;
                commandTwo.Values = paramset2;
                commandToPlc.Enqueue(commandTwo);
                commandToPlc.Enqueue(commandOne);

            }
            if (mount.Count > 0 && mount.First().commandid == (int) CommandName.MountGenericSignals)
            {
                var paramset = mount.First();
                paramset1[0] = paramset.signaltype;
                paramset1[1] = paramset.channelnumber == null ? 0 : paramset.channelnumber.Value - 1;
                paramset1[2] = paramset.signalcontrain;
                paramset1[3] = paramset.modulnumber == null ? 0 : paramset.modulnumber.Value - 1;
                commandOne.CommandNumber = (int) CommandName.MountGenericSignals;
                commandOne.Values = paramset1;
                commandToPlc.Enqueue(commandOne);

            }
        }

        private void CommandForPlc()
        {
            if (commandToPlc.Count == 0)
                return;
            var comandAndParam = commandToPlc.Dequeue();
            _tmrElapsedCmd.Start();
            _command = comandAndParam.CommandNumber;
            _params = comandAndParam.Values;
            text_wait.Text = "Команда PLC: " + _command + "; P1: " + _params[0] +
                             "; P2: " + _params[1] +
                             ";\nP3: " + _params[2] +
                             "; P4: " + _params[3] +
                             "; P5: " + _params[4] +
                             "; P6: " + _params[5] +
                             "\n Ожидаем ответ PLC " + "" +
                             " секунд";
            if (!pan_command_wait.Visible)
                WaitMount(true);
            if (null != CommandEvent)
                CommandEvent();
        }

        private void WaitMount(bool enableDisable)
        {
            if (enableDisable)
            {

                tabConfiпWago.Enabled = false;
                set_treeview_mount.Enabled = false;
                set_gb_channel_mount.Enabled = false;
                set_gb_type_module.Enabled = false;
                set_menu.Enabled = false;
                pan_command_wait.Top = Height/2 - pan_command_wait.Height/2;
                pan_command_wait.Left = Width/2 - pan_command_wait.Width/2;
                pan_command_wait.Visible = true;


            }
            else
            {
                tabConfiпWago.Enabled = true;
                set_treeview_mount.Enabled = true;
                set_gb_channel_mount.Enabled = true;
                set_gb_type_module.Enabled = true;
                set_menu.Enabled = true;
                pan_command_wait.Visible = false;
            }
        }

        private void AcceptForPlc()
        {
            // WaitMount(false);
            switch (_accept)
            {

                case 10:
                case 20:
                case 30:
                case 40:
                case 50:
                case 60:
                case 70:
                case 80:
                case 90:
                case 100:
                    _accept = -1;
                    ExternalCommand();

                    break;
                default:
                    _accept = -1;
                    break;
            }


        }

        private void ExternalCommand()
        {

            if (commandToPlc.Count > 0)
            {
                CommandForPlc();
            }
            else
            {
                WaitMount(false);
            }
            _tmrElapsedCmd.Stop();
        }

        private void SetConmenuDelClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить модуль?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.No)
                return;
            RtpConfigDataContext data = new RtpConfigDataContext();
            int result = data.DeleteModule(_rtpid, Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            if (result >= 0)
            {
                SetLoadChannelMount();
                return;
            }
            MessageBox.Show("Ошибка удаления модуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SetBChannelMountOkClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow tableRows in set_dgv_channel_mount.Rows)
            {
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(4, tableRows.Index);
                SetDgvChannelMountCellContentClick(tableRows, arg);
            }
        }

        private void SetBChangePlcClick(object sender, EventArgs e)
        {
            //PLC plc = new PLC
            //              {
            //                  namePLC = set_inp_name_plc.Text,
            //                  typePLC = set_inp_type_plc.Text,
            //                  numberPLC = Convert.ToInt32(set_inp_number_plc.Text)
            //              };
            //set_treeview_mount.Nodes[0].Text = "PLC №" + set_inp_number_plc.Text;
            //if (!configClass.SavePlc(plc))
            //{
            //    MessageBox.Show("Ошибка сохранения параметров ПЛК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        private void LoadHardwareConfig(object sender, EventArgs e)
        {
            int[] paramset1 = new int[6];
            int[] paramset2 = new int[6];
            var commandOne = new CommandToPlc();
            RtpConfigDataContext data = new RtpConfigDataContext();
            var groupssignal = data.GetRtpSignalGroups().ToList();
            foreach (var getRtpSignalGroupsResult in groupssignal)
            {
                var mount = data.GetMountForSignalsGroup(_rtpid, getRtpSignalGroupsResult.signalattrnumber,
                                                         getRtpSignalGroupsResult.signalgroup).ToList();
                var commandid = mount.First().commandid;
                if (commandid != null && (mount.Count > 0 && commandid.Value == (int) CommandName.MountChannel))
                {
                    var shibernumber = mount.First().shibernumber;
                    if (shibernumber != null)
                    {
                        paramset1[0] = shibernumber.Value;
                        paramset2[0] = shibernumber.Value;
                    }
                    for (int i = 0; i < mount.Count && i < 4; i++)
                    {
                        if (mount[i].signaltype < paramset1.Length)
                        {
                            paramset1[mount[i].signaltype + 1] = mount[i].offsetChannel == null
                                                                     ? 0
                                                                     : mount[i].offsetChannel.Value;

                            paramset2[mount[i].signaltype + 1] = mount[i].modulnumber == null
                                                                     ? -1
                                                                     : mount[i].offsetModul.Value;
                        }
                    }
                    commandOne.CommandNumber = (int) CommandName.MountChannel;
                    commandOne.Values = paramset1;
                    var commandTwo = new CommandToPlc();
                    commandTwo.CommandNumber = (int) CommandName.MountModul;
                    commandTwo.Values = paramset2;
                    commandToPlc.Enqueue(commandTwo);
                    commandToPlc.Enqueue(commandOne);

                }
                if (mount.Count > 0 && mount.First().commandid == (int) CommandName.MountGenericSignals)
                {
                    var paramset = mount.First();
                    paramset1[0] = paramset.signaltype;
                    paramset1[1] = paramset.channelnumber == null ? 0 : paramset.channelnumber.Value - 1;
                    paramset1[2] = paramset.signalcontrain;
                    paramset1[3] = paramset.modulnumber == null ? 0 : paramset.modulnumber.Value - 1;
                    commandOne.CommandNumber = (int) CommandName.MountGenericSignals;
                    commandOne.Values = paramset1;
                    commandToPlc.Enqueue(commandOne);

                }
            }
            data.SetErrorDownloadToPlc(_rtpid, 1, 0); //clear error
            CommandForPlc();
        }


        private void ConfigPlcS7Load(object sender, EventArgs e)
        {
            if (CheckAccessToConfigPlc())
            {
                LoadAllModuleChannel();
                tabConfiпWago.SelectedIndex = 0;
            }
            else
            {
                LoadGroupConfig();
                tabConfiпWago.SelectedIndex = 2;
                
            }
            ChangeEnableButtons(tabConfiпWago.SelectedIndex);
            CheckHardwareConfigError();
        }


        #region ActiveX Control Registration

        // These routines perform the additional COM registration needed by 
        // ActiveX controls

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComRegisterFunction()]
        public static void Register(Type t)
        {
            try
            {
                ActiveXCtrlHelper.RegasmRegisterControl(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw; // Re-throw the exception
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [ComUnregisterFunction()]
        public static void Unregister(Type t)
        {
            try
            {
                ActiveXCtrlHelper.RegasmUnregisterControl(t);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log the error
                throw; // Re-throw the exception
            }
        }

        #endregion

        private void ActiveXCtrlControlAdded(object sender, ControlEventArgs e)
        {
            // Register tab handler and focus-related event handlers for 
            // the control and its child controls.
            ActiveXCtrlHelper.WireUpHandlers(e.Control, ValidationHandler);
        }

        // Ensures that the Validating and Validated events fire properly
        internal void ValidationHandler(object sender, EventArgs e)
        {
            if (ContainsFocus) return;

            OnLeave(e); // Raise Leave event

            if (CausesValidation)
            {
                CancelEventArgs validationArgs = new CancelEventArgs();
                OnValidating(validationArgs);

                if (validationArgs.Cancel && ActiveControl != null)
                    ActiveControl.Focus();
                else
                    OnValidated(e); // Raise Validated event
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            const int wmSetfocus = 0x7;
            const int wmParentnotify = 0x210;
            const int wmDestroy = 0x2;
            const int wmLbuttondown = 0x201;
            const int wmRbuttondown = 0x204;

            if (m.Msg == wmSetfocus)
            {
                // Raise Enter event
                OnEnter(EventArgs.Empty);
            }
            else if (m.Msg == wmParentnotify && (
                                                    m.WParam.ToInt32() == wmLbuttondown ||
                                                    m.WParam.ToInt32() == wmRbuttondown))
            {
                if (!ContainsFocus)
                {
                    // Raise Enter event
                    OnEnter(EventArgs.Empty);
                }
            }
            else if (m.Msg == wmDestroy &&
                     !IsDisposed && !Disposing)
            {
                // Used to ensure the cleanup of the control
                Dispose();
            }

            base.WndProc(ref m);
        }

        // Ensures that tabbing across the container and the .NET controls
        // works as expected
        private void ActiveXCtrlLostFocus(object sender, EventArgs e)
        {
            ActiveXCtrlHelper.HandleFocus(this);
        }





        private void SetDgvChannelMountEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cb = e.Control as ComboBox;
            if (cb != null)
            {
                // first remove event handler to keep from attaching multiple:
                cb.SelectedIndexChanged -= SbSelectedIndexChanged;

                // now attach the event handler
                cb.SelectedIndexChanged += SbSelectedIndexChanged;
            }

        }

        private void SbSelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl dataGridViewComboBoxCell = (DataGridViewComboBoxEditingControl) sender;
            int selecedIndex = dataGridViewComboBoxCell.Items.IndexOf(dataGridViewComboBoxCell.SelectedItem);
            RtpConfigDataContext data = new RtpConfigDataContext();
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 2)
            {


                var signalgroup = data.GetRtpSignalGroups().ToList();
                if (signalgroup.Count > 0)
                {
                    var selectedgroup = signalgroup[selecedIndex];
                    set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value =
                        selectedgroup.id;
                    var signalsIdForGroupId = data.GetSignalsIdForGroupId(selectedgroup.signalgroup,
                                                                          set_ddl_type_modul.SelectedIndex);
                    ((DataGridViewComboBoxCell)
                     set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[3]).Items.Clear();
                    foreach (var signal in signalsIdForGroupId)
                    {
                        ((DataGridViewComboBoxCell)
                         set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[3]).Items.Add
                            (
                                signal.signaldescription);
                    }
                    set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[3].ReadOnly =
                        false;
                }
            }
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 3 &&
                set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value != null)
            {
                var signalForSelect =
                    data.GetRtpSignals(
                        (int) set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value,
                        set_ddl_type_modul.SelectedIndex).ToList();
                if (signalForSelect.Count > 0)
                {
                    var selectedsignals = signalForSelect[selecedIndex];

                    set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[6].Value =
                        selectedsignals.id;
                }
            }
        }

        private void SetDdlTypeModulSelectedIndexChanged(object sender, EventArgs e)
        {
            set_ddl_type_modul.Tag = 1;
        }

        private void SetNdChannelCountValueChanged(object sender, EventArgs e)
        {
            set_nd_channel_count.Tag = 1;
        }

        private void TypeWorkSelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeWork.SelectedIndex == 1)
                downloadConfig.Enabled = true;
            else
                downloadConfig.Enabled = false;
        }

        private void LoadGroupConfig()
        {
            groupSetup.ColumnHeadersDefaultCellStyle.Font = new Font(new FontFamily("Arial Narrow"), 10);
            groupSetup.Rows.Clear();
            RtpConfigDataContext data = new RtpConfigDataContext();
            var groupShibers = data.GetGroupShiberSetup(_rtpid).ToList();
            var groups = data.GetGroupForGroupLoad(_rtpid).ToList();
            var shibers = data.GetRtpSignalGroups().Where(ex => (ex.signalgroup == 1)).ToList();
            var timeBetwenCycle = groupShibers.First().timeBetwenCycle;
            if (timeBetwenCycle != null)
                inp_timeCycleGroup.Value = timeBetwenCycle.Value/100;
            foreach (var getGroupShiberSetupResult in groupShibers)
            {
                int rnumber = groupSetup.Rows.Add();
                groupSetup.Rows[rnumber].Cells[0].Value = "";
                groupSetup.Rows[rnumber].Cells[1].Value = getGroupShiberSetupResult.sequencenumber;
                var groupBox = ((DataGridViewComboBoxCell) groupSetup.Rows[rnumber].Cells[2]);
                groupBox.Items.Clear();
                foreach (var group in groups)
                {
                    groupBox.Items.Add(CutGroupName(group.groupnumber));
                }
                groupSetup.Rows[rnumber].Cells[2].Value = CutGroupName(getGroupShiberSetupResult.groupnumber);
                if (getGroupShiberSetupResult.timeBetwenGroupLoad != null)
                    groupSetup.Rows[rnumber].Cells[3].Value =
                        (((double) getGroupShiberSetupResult.timeBetwenGroupLoad)/100).ToString("0.0");
                ((DataGridViewComboBoxCell) groupSetup.Rows[rnumber].Cells[4]).Items.Clear();
                ((DataGridViewComboBoxCell) groupSetup.Rows[rnumber].Cells[9]).Items.Clear();
                foreach (var shiber in shibers)
                {
                    ((DataGridViewComboBoxCell) groupSetup.Rows[rnumber].Cells[4]).Items.Add(
                        CutShiberName(shiber.signalgroupdescription));
                    ((DataGridViewComboBoxCell) groupSetup.Rows[rnumber].Cells[9]).Items.Add(
                        CutShiberName(shiber.signalgroupdescription));
                }
                groupSetup.Rows[rnumber].Cells[4].Value = CutShiberName(getGroupShiberSetupResult.shiberdecription1);
                double timedoze = 0;
                double timeopen = 0;
                double timeclose = 0;
                string timekoeff = "";
                timekoeff = CalcKoeffOpenClose(getGroupShiberSetupResult.timeOpen1, getGroupShiberSetupResult.timeClose1,
                                               ref timeopen, ref timeclose, ref timedoze);

                groupSetup.Rows[rnumber].Cells[5].Value = timedoze.ToString("0.0");
                groupSetup.Rows[rnumber].Cells[7].Value = timeopen.ToString("0.0");
                groupSetup.Rows[rnumber].Cells[8].Value = timeclose.ToString("0.0");
                groupSetup.Rows[rnumber].Cells[6].Value = timekoeff;

                groupSetup.Rows[rnumber].Cells[9].Value = CutShiberName(getGroupShiberSetupResult.shiberdecription2);
                timekoeff = CalcKoeffOpenClose(getGroupShiberSetupResult.timeOpen2, getGroupShiberSetupResult.timeClose2,
                                               ref timeopen, ref timeclose, ref timedoze);
                groupSetup.Rows[rnumber].Cells[10].Value = timedoze.ToString("0.0");
                groupSetup.Rows[rnumber].Cells[12].Value = timeopen.ToString("0.0");
                groupSetup.Rows[rnumber].Cells[13].Value = timeclose.ToString("0.0");

                groupSetup.Rows[rnumber].Cells[11].Value = timekoeff;
                groupSetup.Rows[rnumber].Cells[14].Value = "Применить";
                groupSetup.Rows[rnumber].Cells[15].Value = getGroupShiberSetupResult.groupnumber;
                groupSetup.Rows[rnumber].Cells[16].Value = getGroupShiberSetupResult.shibernumber1;
                groupSetup.Rows[rnumber].Cells[17].Value = getGroupShiberSetupResult.shibernumber2;
                groupSetup.Rows[rnumber].Cells[18].Value = 1;
                groupSetup.Rows[rnumber].Cells[19].Value = 1;
                groupSetup.Rows[rnumber].Cells[20].Value = 1;
            }
        }

        private void GroupSetupEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cb = e.Control as ComboBox;
            if (cb != null)
            {
                // first remove event handler to keep from attaching multiple:
                cb.SelectedIndexChanged -= GroupSelectedIndexChanged;

                // now attach the event handler
                cb.SelectedIndexChanged += GroupSelectedIndexChanged;
            }
        }

        private void GroupSelectedIndexChanged(object sender, EventArgs e)
        {
            double timedoze = 0;
            double timeopen = 0;
            double timeclose = 0;
            string timekoeff = "";
            RtpConfigDataContext data = new RtpConfigDataContext();
            if (sender == null)
                return;
            DataGridViewComboBoxEditingControl dataGridViewComboBoxCell = (DataGridViewComboBoxEditingControl) sender;
            int selecedIndex = dataGridViewComboBoxCell.Items.IndexOf(dataGridViewComboBoxCell.SelectedItem);
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 2 &&
                (int) groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[15].Value !=
                (selecedIndex + 1)) //group change
            {
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[15].Value = selecedIndex + 1;
                var shibers = data.GetShibersConfigByGroupNumber(_rtpid, selecedIndex + 1).ToList();
                if (shibers.Count > 0)
                {
                    var shiberOneTwo = shibers.First();

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Value =
                        CutShiberName(shiberOneTwo.shiberdecription1);

                    timekoeff = CalcKoeffOpenClose(shiberOneTwo.timeOpen1, shiberOneTwo.timeClose1,
                                                   ref timeopen, ref timeclose, ref timedoze);

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value =
                        timedoze.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[7].Value =
                        timeopen.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[8].Value =
                        timeclose.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[6].Value = timekoeff;

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Value =
                        CutShiberName(shiberOneTwo.shiberdecription2);
                    timekoeff = CalcKoeffOpenClose(shiberOneTwo.timeOpen2, shiberOneTwo.timeClose2,
                                                   ref timeopen, ref timeclose, ref timedoze);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[10].Value =
                        timedoze.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[12].Value =
                        timeopen.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[13].Value =
                        timeclose.ToString("0.0");

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[11].Value = timekoeff;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[15].Value = selecedIndex + 1;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[16].Value =
                        shiberOneTwo.shibernumber1;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value =
                        shiberOneTwo.shibernumber2;
                }
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[18].Value = "";
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[19].Value = "";
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[20].Value = "";
                groupSetup.BeginInvoke(new GridGroupCheck(EndEdit), new object[] {0});
            }
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 4 &&
                (int) groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[16].Value !=
                (selecedIndex + 1)) //shiber 1 change
            {
                if ((selecedIndex + 1) ==
                    (int) groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value)
                {
                    //  MessageBox.Show("В одной группе не могут быть 2 одинаковых шибера", "Ошибка",
                    //         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Style.BackColor =
                        Color.FromArgb(244, 144, 131);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Style.BackColor =
                        Color.FromArgb(244, 144, 131);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].ErrorText =
                        "В одной группе не могут быть 2 одинаковых шибера";
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].ErrorText =
                        "В одной группе не могут быть 2 одинаковых шибера";
                    groupSetup.Update();
                }
                else
                {
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].ErrorText = "";
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].ErrorText = "";

                    var shibersSetup = data.GetCurrentShiberConfigByShiberNumber(_rtpid, selecedIndex + 1).ToList();
                    if (shibersSetup.Count > 0)
                    {
                        var shiberSetup = shibersSetup.First();
                        timekoeff = CalcKoeffOpenClose(shiberSetup.timeOpen, shiberSetup.timeClose,
                                                       ref timeopen, ref timeclose, ref timedoze);

                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value =
                            timedoze.ToString("0.0");
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[7].Value =
                            timeopen.ToString("0.0");
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[8].Value =
                            timeclose.ToString("0.0");
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[6].Value = timekoeff;
                    }
                }
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[16].Value = selecedIndex + 1;
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[18].Value = "";
                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Cells[2].Value != null &&
                        row.Cells[2].Value.ToString() ==
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[2].Value.ToString() &&
                        row.Index != dataGridViewComboBoxCell.EditingControlRowIndex &&
                        !checkedRow.ContainsKey(dataGridViewComboBoxCell.EditingControlRowIndex))
                    {
                        checkedRow.Add(row.Index, selecedIndex);

                    }
                }
                groupSetup.BeginInvoke(new GridGroupCheck(EndEdit), new object[] {0});

            }

            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 9 &&
                (int) groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value !=
                (selecedIndex + 1)) //shiber 2 change
            {
                if ((selecedIndex + 1) ==
                    (int) groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[16].Value)
                {
                    //  MessageBox.Show("В одной группе не могут быть 2 одинаковых шибера", "Ошибка",
                    //         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Style.BackColor =
                        Color.FromArgb(244, 144, 131);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Style.BackColor =
                        Color.FromArgb(244, 144, 131);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].ErrorText =
                        "В одной группе не могут быть 2 одинаковых шибера";
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].ErrorText =
                        "В одной группе не могут быть 2 одинаковых шибера";
                    groupSetup.Update();
                }
                else
                {
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].ErrorText = "";
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].ErrorText = "";

                    var shibersSetup = data.GetCurrentShiberConfigByShiberNumber(_rtpid, selecedIndex + 1).ToList();
                    if (shibersSetup.Count > 0)
                    {
                        var shiberSetup = shibersSetup.First();
                        timekoeff = CalcKoeffOpenClose(shiberSetup.timeOpen, shiberSetup.timeClose,
                                                       ref timeopen, ref timeclose, ref timedoze);

                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[10].Value =
                            timedoze.ToString("0.0");
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[12].Value =
                            timeopen.ToString("0.0");
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[13].Value =
                            timeclose.ToString("0.0");
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[11].Value = timekoeff;
                    }
                }
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value = selecedIndex + 1;
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[19].Value = "";
                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Cells[2].Value != null &&
                        row.Cells[2].Value.ToString() ==
                        groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[2].Value.ToString() &&
                        row.Index != dataGridViewComboBoxCell.EditingControlRowIndex &&
                        !checkedRow.ContainsKey(dataGridViewComboBoxCell.EditingControlRowIndex))
                    {
                        if (checkedRow.ContainsKey(row.Index))
                            checkedRow[row.Index] = selecedIndex;
                        else
                            checkedRow.Add(row.Index, selecedIndex);

                    }
                }
                groupSetup.BeginInvoke(new GridGroupCheck(EndEdit), new object[] {0});
            }
            groupSetup.BeginInvoke(new CheckShiberSetup(CheckChangeSetup), new object[]
                                                                               {
                                                                                   dataGridViewComboBoxCell.
                                                                                       EditingControlRowIndex,
                                                                                   dataGridViewComboBoxCell.
                                                                                       EditingControlDataGridView.
                                                                                       CurrentCell.ColumnIndex
                                                                               });
        }

        private void GroupSetupCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double time;
            SetColorToChangeRows(e.RowIndex);
            groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
            if (!Double.TryParse(groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out time) &&
                e.ColumnIndex != 2 && e.ColumnIndex != 4 && e.ColumnIndex != 9)
            {
                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0.0;
                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Введите правильное значение";
                return;
            }

            if (e.ColumnIndex == 4 || e.ColumnIndex == 9)
            {
                groupSetup.BeginInvoke(new GridGroupCheck(CheckGroupsSelect), new object[] {e.ColumnIndex});
            }
            if (e.ColumnIndex == 5 || e.ColumnIndex == 10)
            {
                double timeopen;
                double timeclose;


                GetTimeOpenClose(groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                                 groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString(),
                                 out timeopen, out timeclose);
                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = (timeopen).ToString("0.0");
                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = (timeclose).ToString("0.0");
                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].ErrorText = "";
                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].ErrorText = "";

                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Cells[4].Value.ToString() ==
                        groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString())
                    {
                        row.Cells[5].Value = groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[7].Value = (timeopen).ToString("0.0");
                        row.Cells[8].Value = (timeclose).ToString("0.0");

                        row.Cells[5].ErrorText = "";
                        row.Cells[7].ErrorText = "";
                        row.Cells[8].ErrorText = "";

                        row.Cells[18].Value = 1;
                        SetColorToChangeRows(row.Index);

                    }
                    if (row.Cells[9].Value.ToString() ==
                        groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString())
                    {
                        row.Cells[10].Value = groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[12].Value = (timeopen).ToString("0.0");
                        row.Cells[13].Value = (timeclose).ToString("0.0");

                        row.Cells[10].ErrorText = "";
                        row.Cells[12].ErrorText = "";
                        row.Cells[13].ErrorText = "";

                        row.Cells[19].Value = 1;
                        SetColorToChangeRows(row.Index);
                    }
                }
            }
            if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 12 || e.ColumnIndex == 13)
            {
                double timeopen = 0;
                double timeclose = 0;
                int indColumnTimeDose;
                int indColumnTimeOpen;
                int indColumnTimeClose;
                if (e.ColumnIndex < 10)
                {
                    indColumnTimeDose = 5;
                    indColumnTimeOpen = 7;
                    indColumnTimeClose = 8;
                }
                else
                {
                    indColumnTimeDose = 10;
                    indColumnTimeOpen = 12;
                    indColumnTimeClose = 13;
                }
                try
                {
                    timeopen = Convert.ToDouble(groupSetup.Rows[e.RowIndex].Cells[indColumnTimeOpen].Value);
                }
                catch
                {

                    timeopen = 0;
                }
                try
                {
                    timeclose = Convert.ToDouble(groupSetup.Rows[e.RowIndex].Cells[indColumnTimeClose].Value);
                }
                catch
                {

                    timeclose = 0;
                }
                groupSetup.Rows[e.RowIndex].Cells[indColumnTimeDose].Value = (timeopen + timeclose).ToString("0.0");
                groupSetup.Rows[e.RowIndex].Cells[indColumnTimeDose].ErrorText = "";
                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Cells[4].Value.ToString() ==
                        groupSetup.Rows[e.RowIndex].Cells[indColumnTimeDose - 1].Value.ToString())
                    {

                        row.Cells[5].Value = (timeopen + timeclose).ToString("0.0");
                        row.Cells[7].Value = (timeopen).ToString("0.0");
                        row.Cells[8].Value = (timeclose).ToString("0.0");

                        row.Cells[5].ErrorText = "";
                        row.Cells[7].ErrorText = "";
                        row.Cells[8].ErrorText = "";

                        row.Cells[18].Value = 1;
                        SetColorToChangeRows(row.Index);

                    }
                    if (row.Cells[9].Value.ToString() ==
                        groupSetup.Rows[e.RowIndex].Cells[indColumnTimeDose - 1].Value.ToString())
                    {
                        row.Cells[10].Value = (timeopen + timeclose).ToString("0.0");
                        row.Cells[12].Value = (timeopen).ToString("0.0");
                        row.Cells[13].Value = (timeclose).ToString("0.0");

                        row.Cells[10].ErrorText = "";
                        row.Cells[12].ErrorText = "";
                        row.Cells[13].ErrorText = "";

                        row.Cells[19].Value = 1;
                        SetColorToChangeRows(row.Index);
                    }
                }

            }
            if (e.ColumnIndex == 3)
            {
                groupSetup.Rows[e.RowIndex].Cells[20].Value = 1;
                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Cells[2].Value.ToString() == groupSetup.Rows[e.RowIndex].Cells[2].Value.ToString())
                    {
                        row.Cells[3].Value = groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[3].ErrorText = "";
                        SetColorToChangeRows(row.Index);
                    }
                }

            }

        }

        private void CheckGroupsSelect(int columnIndex)
        {

            foreach (var i in checkedRow)
            {
                groupSetup.CurrentCell = groupSetup.Rows[i.Key].Cells[columnIndex];
                groupSetup.BeginEdit(true);
                DataGridViewComboBoxEditingControl cb =
                    (DataGridViewComboBoxEditingControl) groupSetup.EditingControl;
                cb.SelectedIndex = i.Value;

            }
            checkedRow.Clear();
        }

        private void EndEdit(int stub)
        {
            groupSetup.EndEdit();
        }


        private string CalcKoeffOpenClose(int? timeOpen, int? timeClose, ref double timeOpenDoub,
                                          ref double timeCloseDoub, ref double timeAll)
        {
            timeCloseDoub = 0;
            timeOpenDoub = 0;
            timeAll = 0;
            string result = "";
            if (timeOpen != null)
            {
                timeOpenDoub = (double) timeOpen.Value/100;
            }
            if (timeClose != null)
            {
                timeCloseDoub = (double) timeClose.Value/100;
            }
            timeAll = timeOpenDoub + timeCloseDoub;
            if (timeAll != 0)
                result = (timeOpenDoub/timeAll).ToString("0.0") + " / " + (timeCloseDoub/timeAll).ToString("0.0");

            return result;
        }

        private string CutShiberName(string name)
        {
            return name.Substring(0, 4) + ". " + name.Substring(name.Length - 5, 5);
        }

        private string CutGroupName(int number)
        {
            return number + " группа";
        }

        private void GroupSetupCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void GroupSetupCellClick(object sender, DataGridViewCellEventArgs e)
        {
            checkedRow.Clear();
            if (e.ColumnIndex == 14)
            {
                foreach (DataGridViewCell cell in groupSetup.Rows[e.RowIndex].Cells)
                {
                    if (cell.ErrorText != "")
                    {
                        MessageBox.Show("Неверные параметры (ячейка: " + cell.ColumnIndex + ")", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (CommangChangeGroupConfig(e.RowIndex, false) == 0)
                {
                    groupSetup.Rows[e.RowIndex].Cells[1].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                    if (typeWorkToGroupSetup.SelectedIndex == 1)
                        CommandForPlc();
                    else
                    {
                        commandToPlc.Clear();
                        RtpConfigDataContext data = new RtpConfigDataContext();
                        data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                    }
                    CommandForPlc();
                }
                else
                {
                    groupSetup.Rows[e.RowIndex].Cells[1].Style.BackColor =
                        Color.FromArgb(244, 144, 131);
                }
            }
        }

        private void SetColorToChangeRows(int rowIndex)
        {
            groupSetup.Rows[rowIndex].Cells[0].Value = 1;
            groupSetup.Rows[rowIndex].Cells[1].Style.BackColor = Color.FromArgb(172, 232, 172);
        }

        private void SetColorToChangeRowsSingleSetup(int rowIndex, int columnIndex)
        {
            singleSetup.Rows[rowIndex].Cells[columnIndex < 11? 0 : 11].Value = 1;
            singleSetup.Rows[rowIndex].Cells[columnIndex < 11? 1 : 12].Style.BackColor = Color.FromArgb(172, 232, 172);
        }

        private void GetTimeOpenClose(string timeDoze, string koeff, out double timeOpen, out double timeClose)
        {
            double timedoze = 0.0;
            timeOpen = 0;
            timeClose = 0;
            try
            {
                timedoze = Convert.ToDouble(timeDoze);
            }
            catch
            {

                timedoze = 0;
            }
            string[] koeffar = koeff.Replace(" ", "").Split("/".ToArray());
            if (koeffar.Length > 1)
            {
                try
                {
                    timeOpen = Convert.ToDouble(koeffar[0]);
                }
                catch
                {
                    timeOpen = 0;

                }
                try
                {
                    timeClose = Convert.ToDouble(koeffar[1]);
                }
                catch
                {

                    timeClose = 0;
                }

            }
            timeClose = timedoze*timeClose;
            timeOpen = timedoze*timeOpen;

        }

        private void GroupSetupCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 || e.ColumnIndex == 11)
            {
                ChangeKoeff dChangeKoeff = new ChangeKoeff();
                double timedoze = 0.0;
                string[] koeffval =
                    groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace(" ", "").Split(
                        "/".ToArray());
                if (koeffval.Length > 1)
                {
                    try
                    {
                        dChangeKoeff.KoeffOpen = Convert.ToDouble(koeffval[0]);
                    }
                    catch
                    {

                        dChangeKoeff.KoeffOpen = 0;
                    }
                    try
                    {
                        dChangeKoeff.KoeffClose = Convert.ToDouble(koeffval[1]);
                    }
                    catch
                    {
                        dChangeKoeff.KoeffClose = 0;
                    }
                    try
                    {
                        timedoze = Convert.ToDouble(groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value);
                    }
                    catch
                    {

                        timedoze = 0;
                    }
                }
                else
                {
                    dChangeKoeff.KoeffClose = 0;
                    dChangeKoeff.KoeffOpen = 0;
                }
                if (dChangeKoeff.ShowDialog() == DialogResult.OK)
                {
                    groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dChangeKoeff.KoeffOpen.ToString("0.0") +
                                                                             " / " +
                                                                             dChangeKoeff.KoeffClose.ToString("0.0");
                    groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value =
                        (timedoze*dChangeKoeff.KoeffOpen).ToString("0.0");
                    groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value =
                        (timedoze*dChangeKoeff.KoeffClose).ToString("0.0");
                    SetColorToChangeRows(e.RowIndex);
                    foreach (DataGridViewRow row in groupSetup.Rows)
                    {
                        if (row.Cells[4].Value.ToString() ==
                            groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString()
                            )
                        {
                            row.Cells[6].Value =
                                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                                dChangeKoeff.KoeffOpen.ToString("0.0") +
                                " / " +
                                dChangeKoeff.KoeffClose.ToString("0.0");

                            row.Cells[7].Value = (timedoze*dChangeKoeff.KoeffOpen).ToString("0.0");
                            row.Cells[8].Value = (timedoze*dChangeKoeff.KoeffClose).ToString("0.0");
                            row.Cells[18].Value = 1;
                            SetColorToChangeRows(row.Index);

                        }
                        if (row.Cells[9].Value.ToString() ==
                            groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString())
                        {
                            row.Cells[11].Value =
                                groupSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                                dChangeKoeff.KoeffOpen.ToString("0.0") +
                                " / " +
                                dChangeKoeff.KoeffClose.ToString("0.0");
                            row.Cells[12].Value = (timedoze*dChangeKoeff.KoeffOpen).ToString("0.0");
                            row.Cells[13].Value = (timedoze*dChangeKoeff.KoeffClose).ToString("0.0");
                            row.Cells[19].Value = 1;
                            SetColorToChangeRows(row.Index);
                        }
                    }
                }
            }
        }

        private void CheckChangeSetup(int rowIndex, int columnIndex)
        {
            if (columnIndex == 2)
            {
                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Index != rowIndex &&
                        row.Cells[4].Value.ToString() == groupSetup.Rows[rowIndex].Cells[4].Value.ToString() &&
                        row.Cells[18].Value.ToString() == "1")
                    {
                        groupSetup.Rows[rowIndex].Cells[5].Value = row.Cells[5].Value;
                        groupSetup.Rows[rowIndex].Cells[6].Value = row.Cells[6].Value;
                        groupSetup.Rows[rowIndex].Cells[7].Value = row.Cells[7].Value;
                        groupSetup.Rows[rowIndex].Cells[8].Value = row.Cells[8].Value;

                        groupSetup.Rows[rowIndex].Cells[5].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[6].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[7].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[8].ErrorText = "";

                        groupSetup.Rows[rowIndex].Cells[18].Value = "1";
                    }
                    if (row.Index != rowIndex && row.Cells[9].Value == groupSetup.Rows[rowIndex].Cells[9].Value &&
                        row.Cells[19].Value.ToString() == "1")
                    {
                        groupSetup.Rows[rowIndex].Cells[10].Value = row.Cells[10].Value;
                        groupSetup.Rows[rowIndex].Cells[11].Value = row.Cells[11].Value;
                        groupSetup.Rows[rowIndex].Cells[12].Value = row.Cells[12].Value;
                        groupSetup.Rows[rowIndex].Cells[13].Value = row.Cells[13].Value;

                        groupSetup.Rows[rowIndex].Cells[10].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[11].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[12].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[13].ErrorText = "";

                        groupSetup.Rows[rowIndex].Cells[19].Value = "1";
                    }
                    if (row.Index != rowIndex &&
                        row.Cells[2].Value.ToString() == groupSetup.Rows[rowIndex].Cells[2].Value.ToString()
                        && row.Cells[20].Value.ToString() == "1")
                    {
                        groupSetup.Rows[rowIndex].Cells[3].Value = row.Cells[3].Value;
                        groupSetup.Rows[rowIndex].Cells[3].ErrorText = "";

                        groupSetup.Rows[rowIndex].Cells[20].Value = "1";
                    }
                }
            }
            if (columnIndex == 4 || columnIndex == 9)
            {
                bool cell9 = false;
                foreach (DataGridViewRow row in groupSetup.Rows)
                {
                    if (row.Index != rowIndex &&
                        ((row.Cells[4].Value.ToString() == groupSetup.Rows[rowIndex].Cells[columnIndex].Value.ToString() &&
                          row.Cells[18].Value.ToString() == "1") ||
                         ((cell9 =
                           (row.Cells[9].Value.ToString() ==
                            groupSetup.Rows[rowIndex].Cells[columnIndex].Value.ToString())) &&
                          row.Cells[19].Value.ToString() == "1")))
                    {
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 1].Value = row.Cells[cell9 ? 10 : 5].Value;
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 2].Value = row.Cells[cell9 ? 11 : 6].Value;
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 3].Value = row.Cells[cell9 ? 12 : 7].Value;
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 4].Value = row.Cells[cell9 ? 13 : 8].Value;

                        groupSetup.Rows[rowIndex].Cells[columnIndex + 1].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 2].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 3].ErrorText = "";
                        groupSetup.Rows[rowIndex].Cells[columnIndex + 4].ErrorText = "";

                        groupSetup.Rows[rowIndex].Cells[columnIndex == 4 ? 18 : 19].Value = "1";
                    }
                }
            }
        }

        private int CommangChangeGroupConfig(int rowIndex, bool noStore)
        {
            int result = 0;
            try
            {
                int timeBetwen;
                int timeOpen1;
                int timeClose1;
                int timeOpen2;
                int timeClose2;
                int sequencenumber;
                int groupnumber;
                int shibernumber1;
                int shibernumber2;
                int[] paramset = new int[6];
                var commandOne = new CommandToPlc();
                RtpConfigDataContext data = new RtpConfigDataContext();
                GetParamToSaveGroupConfig(rowIndex, out timeBetwen, out timeOpen1, out timeClose1, out timeOpen2,
                                          out timeClose2);
                sequencenumber = Convert.ToInt32(groupSetup.Rows[rowIndex].Cells[1].Value);
                groupnumber = Convert.ToInt32(groupSetup.Rows[rowIndex].Cells[15].Value);
                shibernumber1 = Convert.ToInt32(groupSetup.Rows[rowIndex].Cells[16].Value);
                shibernumber2 = Convert.ToInt32(groupSetup.Rows[rowIndex].Cells[17].Value);

                if (!noStore)
                    data.SaveGroupSequence(_rtpid, sequencenumber, groupnumber); //setup group to sequence

                paramset[0] = sequencenumber;
                paramset[1] = groupnumber;
                commandOne.CommandNumber = (int) CommandName.MountShiberToGroupSequency;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

                if (!noStore)
                    data.SaveGroupConfig(_rtpid, groupnumber, shibernumber1, shibernumber2, timeBetwen);

                paramset = new int[6];
                commandOne = new CommandToPlc();
                paramset[0] = groupnumber;
                paramset[1] = shibernumber1;
                paramset[2] = shibernumber2;
                paramset[3] = timeBetwen;
                commandOne.CommandNumber = (int) CommandName.MountShiberNumberToGroupSequency;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

                if (!noStore)
                    data.SaveShiberConfigForGroup(_rtpid, shibernumber1, timeOpen1, timeClose1, shibernumber2, timeOpen2,
                                                  timeClose2);
                paramset = new int[6];
                commandOne = new CommandToPlc();
                paramset[0] = shibernumber1;
                paramset[1] = timeOpen1;
                paramset[2] = timeClose1;
                commandOne.CommandNumber = (int) CommandName.SetupTimeShibers1;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

                paramset = new int[6];
                commandOne = new CommandToPlc();
                paramset[0] = shibernumber2;
                paramset[1] = timeOpen2;
                paramset[2] = timeClose2;
                commandOne.CommandNumber = (int) CommandName.SetupTimeShibers1;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);
                result = 0;

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка сохранения параметров (" + ex.Message + ")", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = -1;
            }

            return result;
        }

        private void GetParamToSaveGroupConfig(int rowIndex, out int timeBetwen, out int timeOpen1, out int timeClose1,
                                               out int timeOpen2, out int timeClose2)
        {
            try
            {
                timeBetwen = (int) (Convert.ToDouble(groupSetup.Rows[rowIndex].Cells[3].Value)*100);
            }
            catch
            {
                timeBetwen = 0;
            }
            try
            {
                timeOpen1 = (int) (Convert.ToDouble(groupSetup.Rows[rowIndex].Cells[7].Value)*100);
            }
            catch
            {
                timeOpen1 = 0;
            }
            try
            {
                timeClose1 = (int) (Convert.ToDouble(groupSetup.Rows[rowIndex].Cells[8].Value)*100);
            }
            catch
            {
                timeClose1 = 0;
            }

            try
            {
                timeOpen2 = (int) (Convert.ToDouble(groupSetup.Rows[rowIndex].Cells[12].Value)*100);
            }
            catch
            {
                timeOpen2 = 0;
            }
            try
            {
                timeClose2 = (int) (Convert.ToDouble(groupSetup.Rows[rowIndex].Cells[13].Value)*100);
            }
            catch
            {
                timeClose2 = 0;
            }
        }

        private void TypeWorkToGroupSetupSelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeWorkToGroupSetup.SelectedIndex == 1)
                downloadGroupConfigAll.Enabled = true;
            else
                downloadGroupConfigAll.Enabled = false;
        }

        private void DownloadGroupConfigAllClick(object sender, EventArgs e)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            foreach (DataGridViewRow row in groupSetup.Rows)
            {
                if (CommangChangeGroupConfig(row.Index, false) != 0)
                {
                    row.Cells[1].Style.BackColor = Color.FromArgb(244, 144, 131);
                    data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                    commandToPlc.Clear();
                    return;
                }
                row.Cells[1].Style.BackColor =
                    System.Drawing.Color.Gainsboro;
            }
            if (AddCommandToTimeBetwincycle(inp_timeCycleGroup) != 0)
            {
                inp_timeCycleGroup.BackColor = Color.FromArgb(244, 144, 131);
                data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                commandToPlc.Clear();
                return;
            }
            data.SetErrorDownloadToPlc(_rtpid, 2, 0);
            CommandForPlc();
        }

        private void ApplyClick(object sender, EventArgs e)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            foreach (DataGridViewRow row in groupSetup.Rows)
            {
                if (row.Cells[0].Value.ToString() == "1")
                {
                    if (CommangChangeGroupConfig(row.Index, false) != 0)
                    {
                        row.Cells[1].Style.BackColor = Color.FromArgb(244, 144, 131);
                        data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                        commandToPlc.Clear();
                        return;
                    }
                    row.Cells[1].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                }
            }
            if (AddCommandToTimeBetwincycle(inp_timeCycleGroup) != 0)
            {
                inp_timeCycleGroup.BackColor = Color.FromArgb(244, 144, 131);
                data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                commandToPlc.Clear();
                return;
            }
           

            if (typeWorkToGroupSetup.SelectedIndex == 1) 
                CommandForPlc();
            else
            {
                commandToPlc.Clear();            
                data.SetErrorDownloadToPlc(_rtpid, 2, 1);
            }
        }


        private void LoadSingleConfig()
        {
            singleSetup.ColumnHeadersDefaultCellStyle.Font = new Font(new FontFamily("Arial Narrow"), 10);
            singleSetup.Rows.Clear();
            RtpConfigDataContext data = new RtpConfigDataContext();
            var singlesetups = data.GetSingleShiberSetup(_rtpid).ToList();
            var timeBetwenCycle = singlesetups.First().timeBetwenCycle;
            if (timeBetwenCycle != null)
                inp_timeCycleSingle.Value = (double)timeBetwenCycle/100;
            var shibers = data.GetRtpSignalGroups().Where(ex => (ex.signalgroup == 1)).ToList();
            int halfcount = singlesetups.Count/2 - 1;
            int offsetc = 0;
            int ind = 0;
            int rnumber = 0;
            foreach (GetSingleShiberSetupResult getSingleShiberSetupResult in singlesetups)
            {
                if (ind > halfcount)
                {
                    offsetc = 11;
                    rnumber = ind - singleSetup.Rows.Count;
                }
                else
                {
                    rnumber = singleSetup.Rows.Add();
                }

                singleSetup.Rows[rnumber].Cells[0 + offsetc].Value = "";
                singleSetup.Rows[rnumber].Cells[1 + offsetc].Value = getSingleShiberSetupResult.sequencenumber;
                var shiberb = ((DataGridViewComboBoxCell) singleSetup.Rows[rnumber].Cells[2 + offsetc]);
                shiberb.Items.Clear();
                foreach (GetRtpSignalGroupsResult shiber in shibers)
                {
                    shiberb.Items.Add(CutShiberName(shiber.signalgroupdescription));
                }
                singleSetup.Rows[rnumber].Cells[2 + offsetc].Value =
                    CutShiberName(getSingleShiberSetupResult.signalgroupdescription);
                double timedoze = 0;
                double timeopen = 0;
                double timeclose = 0;
                string timekoeff = "";
                timekoeff = CalcKoeffOpenClose(getSingleShiberSetupResult.timeOpen, getSingleShiberSetupResult.timeClose,
                                               ref timeopen, ref timeclose, ref timedoze);
                singleSetup.Rows[rnumber].Cells[4 + offsetc].Value = timekoeff;
                singleSetup.Rows[rnumber].Cells[3 + offsetc].Value = timedoze.ToString("0.0");
                singleSetup.Rows[rnumber].Cells[5 + offsetc].Value = timeopen.ToString("0.0");
                singleSetup.Rows[rnumber].Cells[6 + offsetc].Value = timeclose.ToString("0.0");
                if (getSingleShiberSetupResult.timeBetwenShiber != null)
                    singleSetup.Rows[rnumber].Cells[7 + offsetc].Value =
                        ((double) getSingleShiberSetupResult.timeBetwenShiber/100).ToString("0.0");
                singleSetup.Rows[rnumber].Cells[8 + offsetc].Value = "Применить";
                singleSetup.Rows[rnumber].Cells[9 + offsetc].Value =  getSingleShiberSetupResult.shibernumber;
                singleSetup.Rows[rnumber].Cells[10 + offsetc].Value = 1;
                ind++;
            }
        }

        private void SingleSetupCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double time;
            SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
            singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
            if (!Double.TryParse(singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out time) &&
                e.ColumnIndex != 2 && e.ColumnIndex != 13)
            {
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0.0;
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Введите правильное значение";
                return;
            }

            if (e.ColumnIndex == 3 || e.ColumnIndex == 14)
            {
                double timeopen;
                double timeclose;


                GetTimeOpenClose(singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                                 singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString(),
                                 out timeopen, out timeclose);
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = (timeopen).ToString("0.0");
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = (timeclose).ToString("0.0");
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].ErrorText = "";
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].ErrorText = "";

                foreach (DataGridViewRow row in singleSetup.Rows)
                {
                    if (row.Cells[2].Value.ToString() ==
                        singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString())
                    {
                        row.Cells[3].Value = singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[5].Value = (timeopen).ToString("0.0");
                        row.Cells[6].Value = (timeclose).ToString("0.0");

                        row.Cells[3].ErrorText = "";
                        row.Cells[5].ErrorText = "";
                        row.Cells[6].ErrorText = "";

                        row.Cells[10].Value = 1;
                        SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                    }
                    if (row.Cells[13].Value.ToString() ==
                       singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString())
                    {
                        row.Cells[14].Value = singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[16].Value = (timeopen).ToString("0.0");
                        row.Cells[17].Value = (timeclose).ToString("0.0");

                        row.Cells[14].ErrorText = "";
                        row.Cells[16].ErrorText = "";
                        row.Cells[17].ErrorText = "";

                        row.Cells[21].Value = 1;
                        SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                    }
                }
            }
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 16 || e.ColumnIndex == 17)
            {
                double timeopen = 0;
                double timeclose = 0;
                try
                {
                    timeopen = Convert.ToDouble(singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 5 : 16].Value);
                }
                catch
                {

                    timeopen = 0;
                }
                try
                {
                    timeclose = Convert.ToDouble(singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 6 : 17].Value);
                }
                catch
                {

                    timeclose = 0;
                }
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 3 : 14].Value = (timeopen + timeclose).ToString("0.0");
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 3 : 14].ErrorText = "";
                foreach (DataGridViewRow row in singleSetup.Rows)
                {
                    if (row.Cells[2].Value.ToString() ==
                        singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 2 : 13].Value.ToString())
                    {

                        row.Cells[3].Value = (timeopen + timeclose).ToString("0.0");
                        row.Cells[5].Value = (timeopen).ToString("0.0");
                        row.Cells[6].Value = (timeclose).ToString("0.0");

                        row.Cells[3].ErrorText = "";
                        row.Cells[5].ErrorText = "";
                        row.Cells[6].ErrorText = "";

                        row.Cells[10].Value = 1;
                        SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);

                    }
                    if (row.Cells[13].Value.ToString() ==
                        singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 2 : 13].Value.ToString())
                    {
                        row.Cells[14].Value = (timeopen + timeclose).ToString("0.0");
                        row.Cells[16].Value = (timeopen).ToString("0.0");
                        row.Cells[17].Value = (timeclose).ToString("0.0");

                        row.Cells[14].ErrorText = "";
                        row.Cells[16].ErrorText = "";
                        row.Cells[17].ErrorText = "";

                        row.Cells[21].Value = 1;
                        SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                    }
                }

            }
            if (e.ColumnIndex == 7 || e.ColumnIndex == 18)
            {
                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11 ? 10 : 21].Value = 1;
                foreach (DataGridViewRow row in singleSetup.Rows)
                {

                    if (row.Cells[2].Value.ToString() ==
                        singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 5].Value.ToString())
                    {
                        row.Cells[7].Value = singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[10].Value = 1;
                        SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                    }
                    if (row.Cells[13].Value.ToString() ==
                       singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 5].Value.ToString())
                    {
                        row.Cells[18].Value = singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        row.Cells[21].Value = 1;
                        SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                    }
                }

            }
        }

        private void SingleSetupCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 4 || e.ColumnIndex == 15)
            {
                ChangeKoeff dChangeKoeff = new ChangeKoeff();
                double timedoze = 0.0;
                string[] koeffval =
                   singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace(" ", "").Split(
                        "/".ToArray());
                if (koeffval.Length > 1)
                {
                    try
                    {
                        dChangeKoeff.KoeffOpen = Convert.ToDouble(koeffval[0]);
                    }
                    catch
                    {

                        dChangeKoeff.KoeffOpen = 0;
                    }
                    try
                    {
                        dChangeKoeff.KoeffClose = Convert.ToDouble(koeffval[1]);
                    }
                    catch
                    {
                        dChangeKoeff.KoeffClose = 0;
                    }
                    try
                    {
                        timedoze = Convert.ToDouble(singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value);
                    }
                    catch
                    {

                        timedoze = 0;
                    }
                }
                else
                {
                    dChangeKoeff.KoeffClose = 0;
                    dChangeKoeff.KoeffOpen = 0;
                }
                if (dChangeKoeff.ShowDialog() == DialogResult.OK)
                {
                    singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dChangeKoeff.KoeffOpen.ToString("0.0") +
                                                                             " / " +
                                                                             dChangeKoeff.KoeffClose.ToString("0.0");
                    singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value =
                        (timedoze * dChangeKoeff.KoeffOpen).ToString("0.0");
                    singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value =
                        (timedoze * dChangeKoeff.KoeffClose).ToString("0.0");
                    SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                    foreach (DataGridViewRow row in singleSetup.Rows)
                    {
                        if (row.Cells[2].Value.ToString() ==
                            singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString()
                            )
                        {
                            row.Cells[4].Value =
                                 singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                                dChangeKoeff.KoeffOpen.ToString("0.0") +
                                " / " +
                                dChangeKoeff.KoeffClose.ToString("0.0");

                            row.Cells[5].Value = (timedoze * dChangeKoeff.KoeffOpen).ToString("0.0");
                            row.Cells[6].Value = (timedoze * dChangeKoeff.KoeffClose).ToString("0.0");
                            row.Cells[10].Value = 1;
                            SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);

                        }
                        if (row.Cells[13].Value.ToString() ==
                            singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 2].Value.ToString())
                        {
                            row.Cells[15].Value =
                                singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value =
                                dChangeKoeff.KoeffOpen.ToString("0.0") +
                                " / " +
                                dChangeKoeff.KoeffClose.ToString("0.0");
                            row.Cells[16].Value = (timedoze * dChangeKoeff.KoeffOpen).ToString("0.0");
                            row.Cells[17].Value = (timedoze * dChangeKoeff.KoeffClose).ToString("0.0");
                            row.Cells[21].Value = 1;
                            SetColorToChangeRowsSingleSetup(e.RowIndex, e.ColumnIndex);
                        }
                    }
                }
            }
        }

        private void SingleSetupEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cb = e.Control as ComboBox;
            if (cb != null)
            {
                // first remove event handler to keep from attaching multiple:
                cb.SelectedIndexChanged -= SingleSelectedIndexChanged;

                // now attach the event handler
                cb.SelectedIndexChanged += SingleSelectedIndexChanged;
            }
        }

        private void SingleSelectedIndexChanged(object sender, EventArgs e)
        {
            double timedoze = 0;
            double timeopen = 0;
            double timeclose = 0;
            string timekoeff = "";
            RtpConfigDataContext data = new RtpConfigDataContext();
            if (sender == null)
                return;
            DataGridViewComboBoxEditingControl dataGridViewComboBoxCell = (DataGridViewComboBoxEditingControl) sender;
            int selecedIndex = dataGridViewComboBoxCell.Items.IndexOf(dataGridViewComboBoxCell.SelectedItem);
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 2 &&
                (int) singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Value !=
                (selecedIndex + 1)) //shiber 1 change
            {

                var shibersSetup = data.GetCurrentShiberConfigByShiberNumber(_rtpid, selecedIndex + 1).ToList();
                if (shibersSetup.Count > 0)
                {
                    var shiberSetup = shibersSetup.First();
                    timekoeff = CalcKoeffOpenClose(shiberSetup.timeOpen, shiberSetup.timeClose,
                                                   ref timeopen, ref timeclose, ref timedoze);

                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[3].Value =
                        timedoze.ToString("0.0");
                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value =
                        timeopen.ToString("0.0");
                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[6].Value =
                        timeclose.ToString("0.0");
                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Value = timekoeff;
                }
                singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Value = selecedIndex + 1;
                singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[10].Value = "";
                singleSetup.BeginInvoke(new GridGroupCheck(EndSingleEdit), new object[] { 0 });

            }

            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 13 &&
                (int) singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[20].Value !=
                (selecedIndex + 1)) //shiber 2 change
            {

                var shibersSetup = data.GetCurrentShiberConfigByShiberNumber(_rtpid, selecedIndex + 1).ToList();
                if (shibersSetup.Count > 0)
                {
                    var shiberSetup = shibersSetup.First();
                    timekoeff = CalcKoeffOpenClose(shiberSetup.timeOpen, shiberSetup.timeClose,
                                                   ref timeopen, ref timeclose, ref timedoze);

                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[14].Value =
                        timedoze.ToString("0.0");
                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[16].Value =
                        timeopen.ToString("0.0");
                    singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value =
                        timeclose.ToString("0.0");
                   singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[15].Value = timekoeff;
                }
            }
            singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[20].Value = selecedIndex + 1;
            singleSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[21].Value = "";
            singleSetup.BeginInvoke(new GridGroupCheck(EndSingleEdit), new object[] { 0 });
            singleSetup.BeginInvoke(new CheckShiberSetup(CheckSingleChangeSetup), new object[]
                                                                               {
                                                                                   dataGridViewComboBoxCell.
                                                                                       EditingControlRowIndex,
                                                                                   dataGridViewComboBoxCell.
                                                                                       EditingControlDataGridView.
                                                                                       CurrentCell.ColumnIndex

                                                                               });

        }
        private void EndSingleEdit(int stub)
        {
            singleSetup.EndEdit();
        }

        private void CheckSingleChangeSetup(int rowIndex, int columnIndex)
        {
           
            if (columnIndex == 2 || columnIndex == 13)
            {
                bool cell13 = false;
                foreach (DataGridViewRow row in singleSetup.Rows)
                {
                    if (
                        ((row.Cells[2].Value.ToString() == singleSetup.Rows[rowIndex].Cells[columnIndex].Value.ToString() &&
                          row.Cells[10].Value.ToString() == "1") ||
                         ((cell13 =
                           (row.Cells[13].Value.ToString() ==
                            singleSetup.Rows[rowIndex].Cells[columnIndex].Value.ToString())) &&
                          row.Cells[21].Value.ToString() == "1")))
                    {
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 1].Value = row.Cells[cell13 ? 14 : 3].Value;
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 2].Value = row.Cells[cell13 ? 15 : 4].Value;
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 3].Value = row.Cells[cell13 ? 16 : 5].Value;
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 4].Value = row.Cells[cell13 ? 17 : 6].Value;
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 5].Value = row.Cells[cell13 ? 18 : 7].Value;

                        singleSetup.Rows[rowIndex].Cells[columnIndex + 1].ErrorText = "";
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 2].ErrorText = "";
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 3].ErrorText = "";
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 4].ErrorText = "";
                        singleSetup.Rows[rowIndex].Cells[columnIndex + 5].ErrorText = "";

                        singleSetup.Rows[rowIndex].Cells[columnIndex == 4 ? 10 : 21].Value = "1";
                    }
                }
            }
        }

        private void ApplySingleClick(object sender, EventArgs e)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            foreach (DataGridViewRow row in singleSetup.Rows)
            {
                if (row.Cells[0].Value.ToString() == "1")
                {
                    if (CommangChangeSingleConfig(row.Index, 0, false) != 0)
                        break;
                    row.Cells[1].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                }
                if (row.Cells[11].Value.ToString() == "1")
                {
                    if (CommangChangeSingleConfig(row.Index, 11, false) != 0)
                        break;
                    row.Cells[12].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                }
            }
            if (AddCommandToTimeBetwincycle(inp_timeCycleSingle) != 0)
            {
                inp_timeCycleSingle.BackColor = Color.FromArgb(244, 144, 131);
                data.SetErrorDownloadToPlc(_rtpid, 3, 1);
                commandToPlc.Clear();
                return;
            }
            if (typeWorkToGroupSetup.SelectedIndex == 1)
                CommandForPlc();
            else
            {
                commandToPlc.Clear();
                data.SetErrorDownloadToPlc(_rtpid, 3, 1);
            }
        }

        private int CommangChangeSingleConfig(int rowIndex, int colIndex, bool noStore)
        {
            int result = 0;
            try
            {
                int timeBetwen;
                int timeOpen;
                int timeClose;
                int sequencenumber;
                int shibernumber;
                int[] paramset = new int[6];
                var commandOne = new CommandToPlc();
                RtpConfigDataContext data = new RtpConfigDataContext();
                sequencenumber = Convert.ToInt32(singleSetup.Rows[rowIndex].Cells[colIndex < 11? 1 : 12].Value);
                shibernumber = Convert.ToInt32(singleSetup.Rows[rowIndex].Cells[colIndex <11? 9 : 20].Value);
                timeOpen = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[colIndex < 11 ? 5 : 16].Value)*100);
                timeClose = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[colIndex < 11 ? 6 : 17].Value)*100);
                timeBetwen = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[colIndex < 11 ? 7 : 18].Value)*100);

                if (!noStore)
                    data.SaveSingleSequence(_rtpid, sequencenumber, shibernumber);

                paramset[0] = sequencenumber;
                paramset[1] = shibernumber;
                paramset[2] = timeBetwen;
                commandOne.CommandNumber = (int) CommandName.MountShiberToOneSequency;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

                if (!noStore)
                    data.SaveShiberConfigForSingle(_rtpid, shibernumber, timeOpen, timeClose, timeBetwen);
           
                paramset = new int[6];
                commandOne = new CommandToPlc();
                paramset[0] = shibernumber;
                paramset[1] = timeOpen;
                paramset[2] = timeClose;
                commandOne.CommandNumber = (int)CommandName.SetupTimeShibers1;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка сохранения параметров (" + ex.Message + ")", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = -1;
            }

            return result;
        }

        private void SingleSetupCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8 || e.ColumnIndex == 19)
            {
                foreach (DataGridViewCell cell in singleSetup.Rows[e.RowIndex].Cells)
                {
                    if (cell.ErrorText != "" && cell.ColumnIndex > (e.ColumnIndex - 7) && cell.ColumnIndex < e.ColumnIndex)
                    {
                        MessageBox.Show("Неверные параметры (ячейка: " + cell.ColumnIndex + ")", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (CommangChangeSingleConfig(e.RowIndex, e.ColumnIndex, false) == 0)
                {
                    singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11? 1 : 12].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                    if (typeWorkToSingleSetup.SelectedIndex == 1)
                        CommandForPlc();
                    else
                    {
                        commandToPlc.Clear();
                        RtpConfigDataContext data = new RtpConfigDataContext();
                        data.SetErrorDownloadToPlc(_rtpid, 3, 1);
                    }
                    CommandForPlc();
                }
                else
                {
                    singleSetup.Rows[e.RowIndex].Cells[e.ColumnIndex < 11? 1 : 12].Style.BackColor =
                        Color.FromArgb(244, 144, 131);
                }
            }
        }


        private void LoadShiberSetup()
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            var shibers = data.GetShiberSetup(_rtpid).ToList();
            shibersetup_applyAll.Visible = true;
            shibersetup_back.Visible = true;
            shiberSetup.Rows.Clear();
            foreach (GetShiberSetupResult getShiberSetupResult in shibers)
            {
                int index = shiberSetup.Rows.Add();
                shiberSetup.Rows[index].Cells[0].Value = "";
                shiberSetup.Rows[index].Cells[1].Value = getShiberSetupResult.shibernumber;
                shiberSetup.Rows[index].Cells[2].Value = CutShiberName(getShiberSetupResult.signalgroupdescription);

                double timedoze = 0;
                double timeopen = 0;
                double timeclose = 0;
                string timekoeff = "";
                timekoeff = CalcKoeffOpenClose(getShiberSetupResult.timeOpen, getShiberSetupResult.timeClose,
                                               ref timeopen, ref timeclose, ref timedoze);                
                shiberSetup.Rows[index].Cells[3].Value = timedoze.ToString("0.0");
                shiberSetup.Rows[index].Cells[4].Value = timekoeff;
                shiberSetup.Rows[index].Cells[5].Value = timeopen.ToString("0.0");
                shiberSetup.Rows[index].Cells[6].Value = timeclose.ToString("0.0");
                shiberSetup.Rows[index].Cells[7].Value = ((double)getShiberSetupResult.timeBetwenShiber / 100).ToString("0.0");
                shiberSetup.Rows[index].Cells[8].Value = ((double)getShiberSetupResult.timeAOpen / 100).ToString("0.0");
                shiberSetup.Rows[index].Cells[9].Value = ((double)getShiberSetupResult.timeAClose / 100).ToString("0.0");
                shiberSetup.Rows[index].Cells[10].Value = getShiberSetupResult.reopenCountMax;
                shiberSetup.Rows[index].Cells[11].Value = "Применить";
                shiberSetup.Rows[index].Cells[12].Value = 1;
            }
        }

        private void ShiberSetupCellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ShiberSetupCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (shiberSetup.Rows[e.RowIndex].ReadOnly)
                return;
            if (e.ColumnIndex == 4)
            {
                ChangeKoeff dChangeKoeff = new ChangeKoeff();
                double timedoze = 0.0;
                string[] koeffval =
                   shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Replace(" ", "").Split(
                        "/".ToArray());
                if (koeffval.Length > 1)
                {
                    try
                    {
                        dChangeKoeff.KoeffOpen = Convert.ToDouble(koeffval[0]);
                    }
                    catch
                    {

                        dChangeKoeff.KoeffOpen = 0;
                    }
                    try
                    {
                        dChangeKoeff.KoeffClose = Convert.ToDouble(koeffval[1]);
                    }
                    catch
                    {
                        dChangeKoeff.KoeffClose = 0;
                    }
                    try
                    {
                        timedoze = Convert.ToDouble(shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value);
                    }
                    catch
                    {

                        timedoze = 0;
                    }
                }
                else
                {
                    dChangeKoeff.KoeffClose = 0;
                    dChangeKoeff.KoeffOpen = 0;
                }
                if (dChangeKoeff.ShowDialog() == DialogResult.OK)
                {
                    shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dChangeKoeff.KoeffOpen.ToString("0.0") +
                                                                             " / " +
                                                                             dChangeKoeff.KoeffClose.ToString("0.0");
                    shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value =
                        (timedoze * dChangeKoeff.KoeffOpen).ToString("0.0");
                    shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value =
                        (timedoze * dChangeKoeff.KoeffClose).ToString("0.0");
                    SetColorToShiberSetup(e.RowIndex);

                }
            }

        }

        private void ShiberSetupCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            double time;
            SetColorToShiberSetup(e.RowIndex);
            shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
            if (!Double.TryParse(shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out time))
            {
                shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0.0;
                shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Введите правильное значение";
                return;
            }

            if (e.ColumnIndex == 3)
            {
                double timeopen;
                double timeclose;


                GetTimeOpenClose(shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                                 shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString(),
                                 out timeopen, out timeclose);
                shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = (timeopen).ToString("0.0");
                shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = (timeclose).ToString("0.0");
                shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].ErrorText = "";
                shiberSetup.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].ErrorText = "";

                
            }
            if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
            {
                double timeopen = 0;
                double timeclose = 0;
                try
                {
                    timeopen = Convert.ToDouble(shiberSetup.Rows[e.RowIndex].Cells[5].Value);
                }
                catch
                {

                    timeopen = 0;
                }
                try
                {
                    timeclose = Convert.ToDouble(shiberSetup.Rows[e.RowIndex].Cells[6].Value);
                }
                catch
                {

                    timeclose = 0;
                }
                shiberSetup.Rows[e.RowIndex].Cells[3].Value = (timeopen + timeclose).ToString("0.0");
                shiberSetup.Rows[e.RowIndex].Cells[3].ErrorText = "";
               
            }
            if (e.ColumnIndex >= 7 && e.ColumnIndex <= 10)
            {
                shiberSetup.Rows[e.RowIndex].Cells[12].Value = 1;
            }

        }

        private void SetColorToShiberSetup(int rowIndex)
        {
            shiberSetup.Rows[rowIndex].Cells[0].Value = 1;
            shiberSetup.Rows[rowIndex].Cells[1].Style.BackColor = Color.FromArgb(172, 232, 172);
        }
        
        private int CommangChangeShiberConfig(int rowIndex, bool noStore)
        {
            int result = 0;
            try
            {
                int timeBetwen;
                int timeOpen;
                int timeClose;
                int shibernumber;
                int timeAOpen;
                int timeAClose;
                int maxReopenCount;
                int[] paramset = new int[6];
                var commandOne = new CommandToPlc();
                RtpConfigDataContext data = new RtpConfigDataContext();
                shibernumber = Convert.ToInt32(singleSetup.Rows[rowIndex].Cells[1].Value);
                timeOpen = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[5]) * 100);
                timeClose = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[6]) * 100);
                timeBetwen = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[7].Value) * 100);
                timeAOpen = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[8].Value) * 100);
                timeAClose = (int)(Convert.ToDouble(singleSetup.Rows[rowIndex].Cells[9].Value) * 100);
                maxReopenCount = Convert.ToInt32(singleSetup.Rows[rowIndex].Cells[10].Value);

                if (!noStore)
                    data.SaveShiberSetup(_rtpid, shibernumber, timeOpen, timeClose, timeAOpen, timeAClose, timeBetwen,
                                         maxReopenCount);

                paramset[0] = shibernumber;
                paramset[1] = timeOpen;
                paramset[2] = timeClose;
                commandOne.CommandNumber = (int) CommandName.SetupTimeShibers1;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

                paramset = new int[6];
                commandOne = new CommandToPlc();
                paramset[0] = shibernumber;
                paramset[1] = timeAOpen;
                paramset[2] = timeAClose;
                paramset[3] = timeBetwen;
                commandOne.CommandNumber = (int)CommandName.SetupTimeShibers2;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

                paramset = new int[6];
                commandOne = new CommandToPlc();
                paramset[0] = shibernumber;
                paramset[1] = maxReopenCount;
                commandOne.CommandNumber = (int) CommandName.SetupReopenShibers;
                commandOne.Values = paramset;
                commandToPlc.Enqueue(commandOne);

            }
            catch (Exception ex)
            {

                MessageBox.Show("Ошибка сохранения параметров (" + ex.Message + ")", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = -1;
            }

            return result;
        }

        private void SelectShiberConfig(int shibernumber)
        {
            tabConfiпWago.SelectedIndex = 4;
            foreach (DataGridViewRow row in shiberSetup.Rows)
            {
                if ((row.Index + 1) == shibernumber)
                {
                   // row.Selected = true;
                    row.ReadOnly = false;
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 123, 140, 189);
                    row.DefaultCellStyle.ForeColor = Color.White;
                    shiberSetup.CurrentCell = row.Cells[1];
                    continue;
                }
                row.ReadOnly = true;
                row.Cells[11].ReadOnly = true;
                row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 173, 173, 173);
                row.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
                row.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 173, 173, 173);
            }
            shibersetup_applyAll.Visible = false;
            shibersetup_back.Visible = false;
        }

        private void ShibersetupApplyAllClick(object sender, EventArgs e)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            foreach (DataGridViewRow row in shiberSetup.Rows)
            {
                if (row.Cells[0].Value.ToString() == "1")
                {
                    if (CommangChangeShiberConfig(row.Index, false) != 0)
                    {
                        row.Cells[1].Style.BackColor = Color.FromArgb(244, 144, 131);
                        data.SetErrorDownloadToPlc(_rtpid, 4, 1);
                        commandToPlc.Clear();
                        return;
                    }
                    row.Cells[1].Style.BackColor =
                        System.Drawing.Color.Gainsboro;
                }
            }
            if (typeWorkToShiberSetup.SelectedIndex == 1)
                CommandForPlc();
            else
            {
                commandToPlc.Clear();
                data.SetErrorDownloadToPlc(_rtpid, 4, 1);
            }
        }

        private void TypeWorkToSingleSetupSelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeWorkToSingleSetup.SelectedIndex == 1)
                downloadSingleConfigAll.Enabled = true;
            else
                downloadSingleConfigAll.Enabled = false;
        }

        private void DownloadShiberConfigAllClick(object sender, EventArgs e)
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            foreach (DataGridViewRow row in shiberSetup.Rows)
            {
                if (CommangChangeGroupConfig(row.Index, false) != 0)
                {
                    row.Cells[1].Style.BackColor = Color.FromArgb(244, 144, 131);
                    data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                    commandToPlc.Clear();
                    return;
                }
                row.Cells[1].Style.BackColor =
                    System.Drawing.Color.Gainsboro;
            }
            if (AddCommandToTimeBetwincycle(inp_timeCycleGroup) != 0)
            {
                inp_timeCycleGroup.BackColor = Color.FromArgb(244, 144, 131);
                data.SetErrorDownloadToPlc(_rtpid, 2, 1);
                commandToPlc.Clear();
                return;
            }
            data.SetErrorDownloadToPlc(_rtpid, 2, 0);
            CommandForPlc();
        }

        private void TypeWorkToShiberSetupSelectedIndexChanged(object sender, EventArgs e)
        {
            if (typeWorkToShiberSetup.SelectedIndex == 1)
                downloadShiberConfigAll.Enabled = true;
            else
                downloadShiberConfigAll.Enabled = false;
        }

        private void InpTimeCycleSingleKeyUp(object sender, KeyEventArgs e)
        {
            ((CustomControl.DigitTextBox)sender).BackColor = Color.FromArgb(172, 232, 172);
            _shangevaluecycle = true;
            _valuecycle = (int) (((CustomControl.DigitTextBox) sender).Value*100);
        }

        private void DownloadSingleConfigAllClick(object sender, EventArgs e)
        {
             RtpConfigDataContext data = new RtpConfigDataContext();
            foreach (DataGridViewRow row in singleSetup.Rows)
            {
                if (CommangChangeShiberConfig(row.Index, false) != 0)
                {
                    row.Cells[1].Style.BackColor = Color.FromArgb(244, 144, 131);
                    data.SetErrorDownloadToPlc(_rtpid, 4, 1);
                    commandToPlc.Clear();
                    return;
                }
                row.Cells[1].Style.BackColor =
                    System.Drawing.Color.Gainsboro;
            }
            data.SetErrorDownloadToPlc(_rtpid, 4, 0);
            CommandForPlc();
        }

        private int AddCommandToTimeBetwincycle(object sender)
        {
            try
            {
                if (_shangevaluecycle)
                {
                    RtpConfigDataContext data = new RtpConfigDataContext();
                    int[] paramset = new int[6];
                    var commandOne = new CommandToPlc();
                    paramset[0] = _valuecycle;
                    commandOne.CommandNumber = (int)CommandName.SetupTimeBetwinCycle;
                    commandOne.Values = paramset;
                    commandToPlc.Enqueue(commandOne);
                    data.SaveTimeBetwenCycle(_rtpid, _valuecycle);
                   ((CustomControl.DigitTextBox)sender).BackColor = Color.Gainsboro;

                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения параметров (" + ex.Message + ")", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
    }
}

