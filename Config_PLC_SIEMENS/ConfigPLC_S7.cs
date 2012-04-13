using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Config_PLC_SIEMENS
{
   
 
    [ProgId("ConfigWagoRtp")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComSourceInterfaces(typeof(IScadaInterfaceEvent))]
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
            SetupTimeShibers = 1,
            SetupReopenShibers = 2,
            MountChannel = 3,
            MountModul = 4,
            MountGenericSignals = 5,
            MountShiberToOneSequency = 6,
            MountShiberToGroupSequency = 7,
            MountShiberNumberToGroupSequency = 8         
        }
        string _oldAddress = "";
        int _internalCmd = -1;  
        object _valueForCommandStore;
        int _selectedTab;

        #region VariableForPropertisSCADA

        private int _rtpid = 0;
        int _command = -1;
        int _accept = -1;
        private int[] _params = {0, 0, 0, 0, 0, 0};

       
        #endregion

        delegate void Ui(bool eDwait);
        delegate void Ui1(int tagId);
        private Queue<CommandToPlc> commandToPlc;
        ConfigPLCStore configClass;
        private StaticConfig _parametrsConfig;
        readonly System.Timers.Timer _tmrElapsedCmd;
       // System.Timers.Timer tmrAdviseItem;
       
        public ConfigPLC_S7()
        {
            InitializeComponent();
            configClass = new ConfigPLCStore();
            set_gb_channel_mount.Visible = false;
            set_b_channel_mount_ok.Visible = false;
            _parametrsConfig = configClass.GetStaticConfigParam();
            _tmrElapsedCmd = new System.Timers.Timer {Interval = 1000*60 /*_parametrsConfig.TimeOut*/};
            _tmrElapsedCmd.Elapsed += TmrElapsedCmdElapsed;
            commandToPlc = new Queue<CommandToPlc>();
            _accept = _command =  0;
            _params = new []{0, 0, 0, 0, 0, 0};
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


        void TmrElapsedCmdElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _tmrElapsedCmd.Stop();
            _internalCmd = -1;
            RtpConfigDataContext data = new RtpConfigDataContext();
            data.SetErrorDownloadToPlc(_rtpid, 1);
            Ui ui = WaitMount;
            set_treeview_mount.BeginInvoke(ui, new object[]{false});
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
            get
            {
                return _command;
            }
            set
            {
                _command = value;
            }
        }
        public int Accept
        {
           //[return: MarshalAs(UnmanagedType.SysInt)]
            get
            {
               return _accept;
            }
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
            get
            {
                return _params[0];
            }
            set
            {
                _params[0] = value;
            }
        }
        public int P2
        {
           // [return: MarshalAs(UnmanagedType.I2)]
            get
            {
                return _params[1];
            }
            set
            {
                _params[1] = value;
            }
        }
        public int P3
        {
          //  [return: MarshalAs(UnmanagedType.I2)]
            get
            {
                return _params[2];
            }
            set
            {
                _params[2] = value;
            }
        }
        public int P4
        {
           // [return: MarshalAs(UnmanagedType.I2)]
            get
            {
                return _params[3];
            }
            set
            {
                _params[3] = value;
            }
            
        }
        public int P5
        {
           
            get
            {
                return _params[4];
            }
            set
            {
                _params[4] = value;
            }
        }
        public int P6
        {
            get
            {
                return _params[5];
            }
            set
            {
                _params[5] = value;
            }
        }
        
        #endregion

        #region Event
        [ComVisible(false)]
        public delegate void CommandEventHandler();
        public event CommandEventHandler CommandEvent = null;
        #endregion

       

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
          
            PLC plc = configClass.GetPlc();
            set_inp_name_plc.Text =plc.namePLC;
            set_inp_type_plc.Text = plc.typePLC;
            set_inp_number_plc.Text = plc.numberPLC.ToString();
            set_treeview_mount.Nodes[0].Text = "PLC №" + set_inp_number_plc.Text;

           
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
            _selectedTab = tabConfigPLC_S7.SelectedIndex; 
            switch (tabConfigPLC_S7.SelectedIndex)
            {
                case 0:
                    LoadAllModuleChannel();
                    break;
                case 1:
                    typeWork.SelectedIndex = 1;                  
                    SetLoadChannelMount();
                    CheckHardwareConfigError();
                    break;
                case 4:
                    VisualParamConfig();
                    break;
                default:
                    break;

            }
            ChangeEnableButtons(tabConfigPLC_S7.SelectedIndex);
        }

<<<<<<< HEAD
        private void CheckHardwareConfigError()
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            int flag = 1;
            flag = data.GetErrorDownloadToPlc(_rtpid).First().changehardware;
            if(flag == 1)
            {
                checkHardwareIcon.Image = set_images.Images[3];
                checkHardwareIcon.ToolTipText = "Конфигурация контроллера не соотвествует базе";
            }
            else
            {
                checkHardwareIcon.Image = set_images.Images[2];
                checkHardwareIcon.ToolTipText = "Конфигурация контроллера соответсвует базе";
            }
 
        }

=======
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac
        private void LoadAllModuleChannel()
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
        /// <summary>
        /// Изменяет доступность кнопок меню
        /// </summary>
        /// <param name="tabSelectIndex">Индекс вкладки</param>
        private void ChangeEnableButtons(int tabSelectIndex)
        {
<<<<<<< HEAD
            downloadConfig.Enabled = false;
=======
            donloadConfig.Enabled = false;
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac

            switch (tabSelectIndex)
            {
                case 0: case 1:
<<<<<<< HEAD
                    downloadConfig.Enabled =
                     true;
                    break;
                case 3:
                    downloadConfig.Enabled =
=======
                    donloadConfig.Enabled =
                     true;
                    break;
                case 3:
                    donloadConfig.Enabled =
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac
                    true;
                    break;
                default:
                    break;
            }
        }
       
        private void SetLoadChannelMount()
        {
            set_treeview_mount.Nodes[0].Nodes.Clear();
            if (configClass != null)
            {
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
            }
            set_treeview_mount.SelectedNode = set_treeview_mount.Nodes[0];
            TreeNodeMouseClickEventArgs e = new TreeNodeMouseClickEventArgs(set_treeview_mount.SelectedNode, MouseButtons.Left, 0, 0, 0);
            SetTreeviewMountNodeMouseClick(set_treeview_mount, e);
            PlcInfLoad();
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
            if (set_nd_channel_count.Tag != null && (int)set_nd_channel_count.Tag == 1)
            {
                result = data.ChangeCountChannel(_rtpid, Convert.ToInt32(set_treeview_mount.SelectedNode.Tag),
                                                     (int) set_nd_channel_count.Value);
                if(result != 0)
                {
                    MessageBox.Show("Ошибка изменения числа каналов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                LoadChannelMount(Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            }
            if (set_ddl_type_modul.Tag != null && (int)set_ddl_type_modul.Tag == 1)
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
                CheckOldMount(Convert.ToInt32(set_treeview_mount.SelectedNode.Tag), Convert.ToInt32(set_dgv_channel_mount.Rows[e.RowIndex].Cells[1].Value));
                NewMount(e.RowIndex);
                if (typeWork.SelectedIndex == 1)
                    CommandForPlc();
                else
                {
                    commandToPlc.Clear();
                    RtpConfigDataContext data = new RtpConfigDataContext();
                    data.SetErrorDownloadToPlc(_rtpid, 1);
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
            if (mount.Count > 0 && mount.First().commandid !=null && mount.First().commandid.Value == (int)CommandName.MountChannel)
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
                commandOne.CommandNumber = (int)CommandName.MountChannel;
                commandOne.Values = paramset1;
                var commandTwo = new CommandToPlc();
                commandTwo.CommandNumber = (int)CommandName.MountModul;
                commandTwo.Values = paramset2;
                commandToPlc.Enqueue(commandTwo);
                commandToPlc.Enqueue(commandOne);

            }
            if (mount.Count > 0 && mount.First().commandid == (int)CommandName.MountGenericSignals)
            {
                var paramset = mount.First();
                paramset1[0] = paramset.signaltype;
                paramset1[1] = paramset.channelnumber == null ? 0 : paramset.channelnumber.Value -1;
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

            var mount = data.GetChannelCurrentShibers(_rtpid, set_dgv_channel_mount.Rows[rowIndex].Cells[0].Value == null ? -1 : Convert.ToInt32(set_dgv_channel_mount.Rows[rowIndex].Cells[0].Value),
                                                         set_dgv_channel_mount.Rows[rowIndex].Cells[5].Value == null ? -1 : Convert.ToInt32(set_dgv_channel_mount.Rows[rowIndex].Cells[5].Value),
                                                      set_dgv_channel_mount.Rows[rowIndex].Cells[6].Value == null ? -1 : Convert.ToInt32(set_dgv_channel_mount.Rows[rowIndex].Cells[6].Value)).ToList();

            var commandOne = new CommandToPlc();
            if (mount.Count > 0 && mount.First().commandid.Value == (int)CommandName.MountChannel)
            {
                var shibernumber = mount.First().shibernumber;
                if (shibernumber != null)
                {paramset1[0] = shibernumber.Value;
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
                commandOne.CommandNumber = (int)CommandName.MountChannel;
                commandOne.Values = paramset1;
                var commandTwo = new CommandToPlc();
                commandTwo.CommandNumber = (int)CommandName.MountModul;
                commandTwo.Values = paramset2;
                commandToPlc.Enqueue(commandTwo);
                commandToPlc.Enqueue(commandOne);

            }
            if (mount.Count > 0 && mount.First().commandid == (int)CommandName.MountGenericSignals)
            {
                var paramset = mount.First();
                paramset1[0] = paramset.signaltype;
                paramset1[1] = paramset.channelnumber == null ? 0 : paramset.channelnumber.Value  - 1;
                paramset1[2] = paramset.signalcontrain;
                paramset1[3] = paramset.modulnumber == null ? 0 : paramset.modulnumber.Value - 1;
                commandOne.CommandNumber = (int)CommandName.MountGenericSignals;
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
            set_text_mount_wait.Text = "Команда PLC: " + _command + "; P1: " + _params[0] + 
                                                                     "; P2: " + _params[1] +
                                                                     ";\nP3: " + _params[2] +
                                                                     "; P4: " + _params[3] +
                                                                     "; P5: " + _params[4] +
                                                                     "; P6: " + _params[5] + 
                           "\n Ожидаем ответ PLC " + _parametrsConfig.TimeOut + " секунд";
            WaitMount(true);
            if (null != CommandEvent)
                CommandEvent();
        }

        private void WaitMount(bool enableDisable)
        {
            if (enableDisable)
            {
                
                tabConfigPLC_S7.Enabled= false;

                set_pan_mount_wait.Top = Height / 2 - set_pan_mount_wait.Height/ 2;
                set_pan_mount_wait.Left = Width / 2 - set_pan_mount_wait.Width/ 2;
                set_pan_mount_wait.Visible = true;               
                set_treeview_mount.Enabled = false;
                set_gb_channel_mount.Enabled = false;
                set_gb_type_module.Enabled = false;


                set_menu.Enabled = false;
        


                pan_tag_wait.Top = Height / 2 - pan_tag_wait.Height / 2;
                pan_tag_wait.Left = Width / 2 - pan_tag_wait.Width / 2;
                pan_tag_wait.Visible = true;


            }
            else
            {
                tabConfigPLC_S7.Enabled = true;
                set_pan_mount_wait.Visible = false;
                set_treeview_mount.Enabled = true;
                set_gb_channel_mount.Enabled = true;
                set_gb_type_module.Enabled = true;
                set_menu.Enabled = true;
                pan_tag_wait.Visible = false;
                set_text_mount_wait.Text = "";
            }
        }

<<<<<<< HEAD
=======
        void InternalCommand(int Command, object value, int[] callBack)
        {
            //InternalCommandStackParam internalParam = new InternalCommandStackParam();
            //switch(Command)
            //{
            //    case 0: //unmount tag for channel
            //        #region UnmountTagForChannel
            //        Channel chUnmount = (Channel)value;
            //        if (chUnmount.tagMount.id == -1)
            //        {
            //            WaitMount(false);
            //            if (!configClass.SaveChannel(chUnmount))
            //            {
            //                MessageBox.Show("Ошибка сохранения параметров канала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            _internalCmd = -1;
            //        }
            //        else
            //        {
            //            WaitMount(true);
            //            set_text_mount_wait.Text += "Команда PLC: 0; Адрес: " + chUnmount.tagMount.id + "; P1: -1;\n" +
            //                " P2: -1; P3: 0; P4: 0; P5: 0; P6: 0.\n Ожидаем ответ PLC " + _parametrsConfig.TimeOut + " секунд\n";
            //          //  CommandForPlc(0, chUnmount.tagMount.id, 1, -1, -1, 0, 0, 0.0, 0.0);
            //            chUnmount.tagMount.id = -1;
            //            _internalCmd = Command;
            //            _valueForCommandStore = (object)chUnmount;
            //        }
            //        #endregion
            //    break;
            //    case 1: //mount tag for channel
            //        #region MountTagForChannel
            //    Channel chMount = (Channel)value;
            //    if (chMount.tagMount.id != -1)
            //    {
            //        WaitMount(false);
            //        if (!configClass.SaveChannel(chMount))
            //        {
            //            MessageBox.Show("Ошибка сохранения параметров канала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        _internalCmd = -1;
            //    }
            //    else
            //    {
            //        if (chMount.tagMount.namePLC == "")
            //            return;
            //        WaitMount(true);
            //        chMount.tagMount = configClass.GetTag(chMount.tagMount.namePLC);
            //        chMount.tagMount.type = SetTypeAiAoChannel(chMount.typeChannel, chMount.tagMount.type);
            //        set_text_mount_wait.Text += "Команда PLC: 1; Адрес: " + chMount.tagMount.id.ToString() + "; P1: " +chMount.address + ";\n P2: "
            //               + chMount.tagMount.type + "; P3: 0; P4: 0; P5: 0; P6: 0.\n Ожидаем ответ PLC " + _parametrsConfig.TimeOut + " секунд";
            //        _internalCmd = Command;
            //        _valueForCommandStore = chMount;
            //      //  CommandForPlc(0, chMount.tagMount.id, 1, chMount.address, chMount.tagMount.type, 0, 0, 0.0, 0.0);
            //    }
            //        #endregion
            //    break;
            //    case 2://chnge modul type & count channel
            //        #region Change and Count Channel
            //        // CallBack[]
            //        // 0 - смена типа
            //        // 1 - количество каналов актуальное
            //        // 2 - индекс канала смененого типа
            //        // 3 - индекс канала на удаление
            //    Modul m = (Modul)value;
            //    if (callBack.Length > 3 && callBack[3] >= 0 && callBack[3] < m.ChannelMount.Count()) //удаление канала
            //    {
            //        if (!configClass.RemoveChannel(m.id, m.ChannelMount[callBack[3]].id))
            //        {
            //            MessageBox.Show("Ошибка удаления канала", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        m.ChannelMount.Remove(m.ChannelMount[callBack[3]]);
            //        LoadChannelMount(m.id);
            //    }
            //    if (callBack[0] == 1 && callBack[2] < callBack[1]) //смена типа канала
            //    {                   
            //        internalParam.Command = Command;
            //        internalParam.Value = m;
            //        internalParam.CallBack = callBack;                  
            //        Channel channel = m.ChannelMount[callBack[2]]; 
            //        internalParam.CallBack[2]++;
            //        intrenalCommandStack.Push(internalParam);
            //        channel.tagMount.id = -1;
            //        channel.typeChannel = m.typeModul;
            //        InternalCommand(1, channel, new[] { 0 });
            //    }
            //    if (callBack[0] == 1 && callBack[2] == callBack[1]) //смена типа канала
            //    {
            //        if (!configClass.SaveModul(m))
            //        {
            //            MessageBox.Show("Ошибка сохранения параметров модуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //    if (callBack[1] < m.ChannelMount.Count()) // отвязка канала и подготовка его у удалению
            //    {
            //        internalParam.Command = Command;
            //        internalParam.Value = m;
            //        internalParam.CallBack = callBack;
            //        internalParam.CallBack[3] = internalParam.CallBack[1];
            //        intrenalCommandStack.Push(internalParam);
            //        if(m.ChannelMount[callBack[1]].tagMount.id != -1)
            //           InternalCommand(0, m.ChannelMount[callBack[1]], new[] { 0 });
            //    }
            //       #endregion
            //    break;
            //    case 3:// delete modul
            //        #region DeleteModule
            //    Modul mDelete = (Modul)value;
            //    if (callBack.Length > 0 && callBack[0] == 1)
            //    {
            //        if (!configClass.RemoveModul(mDelete.id))
            //        {
            //            MessageBox.Show("Ошибка удаления модуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        internalParam.Command = Command;
            //        internalParam.Value = mDelete;
            //        internalParam.CallBack = new[] { 1 };
            //        intrenalCommandStack.Push(internalParam);
            //        InternalCommand(0, mDelete, new[] { 0, 0, -1, -1 });
            //    }
            //        #endregion
            //    break;
            //    case 4: //apply all config channel
            //        #region ApplyAll 
            //    if (callBack[0] < set_dgv_channel_mount.Rows.Count)
            //    {
            //        if (set_dgv_channel_mount.Rows[callBack[0]].Cells[1].Value.ToString() == "")
            //        {
            //            MessageBox.Show("Не указан адрес \nМодуль №" +
            //            (Convert.ToInt32(set_treeview_mount.SelectedNode.Tag) +1).ToString() +
            //            " Канал №" + (callBack[0] + 1).ToString()
            //        , "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //        internalParam.Command = Command;
            //        internalParam.Value = null;
            //        internalParam.CallBack = new[] { 1 };
            //        intrenalCommandStack.Push(internalParam);
            //        InternalCommand(0, null, new[] { callBack[0] + 1});
            //        if (set_dgv_channel_mount.Rows[callBack[0]].Cells[2].Value == null)
            //            set_dgv_channel_mount.Rows[callBack[0]].Cells[2].Value = "";
            //        if (set_dgv_channel_mount.Rows[callBack[0]].Cells[2].Value.ToString() == "")
            //        {

            //            Channel ch = configClass.GetChannel(Convert.ToInt32(set_dgv_channel_mount.Rows[callBack[0]].Cells[5].Value), Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            //            InternalCommand(0, ch, new[] {0});
            //        }
            //        else
            //        {
            //            Channel ch = configClass.GetChannel(Convert.ToInt32(set_dgv_channel_mount.Rows[callBack[0]].Cells[5].Value), Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            //            InternalCommand(1, ch, new[] { 0 });
            //        }
            //    }
            //        #endregion               
            //    break;
             
                
            //    default:
            //    break;
            //}
            //if (_internalCmd == -1 && intrenalCommandStack.Count > 0)
            //{
            //    InternalCommandStackParam internalCommandStackParam = intrenalCommandStack.Pop();
            //    InternalCommand(internalCommandStackParam.Command, internalCommandStackParam.Value, internalCommandStackParam.CallBack);
            //}
        }
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac

        void AcceptForPlc()
        {
            WaitMount(false);
            switch (_accept) 
            {                
                            
                case 30: case 40: case 50:
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
<<<<<<< HEAD
            {
                CommandForPlc();
            }
            _tmrElapsedCmd.Stop();
        }

        private void SetConmenuDelClick(object sender, EventArgs e)
        {
            if(MessageBox.Show("Удалить модуль?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            RtpConfigDataContext data = new RtpConfigDataContext();
            int result = data.DeleteModule(_rtpid, Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            if (result >= 0)
            {
                SetLoadChannelMount();
                return;
            }
=======
                CommandForPlc();
        }

        private void SetConmenuDelClick(object sender, EventArgs e)
        {
            if(MessageBox.Show("Удалить модуль?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            RtpConfigDataContext data = new RtpConfigDataContext();
            int result = data.DeleteModule(_rtpid, Convert.ToInt32(set_treeview_mount.SelectedNode.Tag));
            if (result >= 0)
            {
                SetLoadChannelMount();
                return;
            }
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac
            MessageBox.Show("Ошибка удаления модуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SetBChannelMountOkClick(object sender, EventArgs e)
        {
<<<<<<< HEAD
            foreach (DataGridViewRow tableRows in set_dgv_channel_mount.Rows)
            {
                DataGridViewCellEventArgs arg = new DataGridViewCellEventArgs(4, tableRows.Index);
                SetDgvChannelMountCellContentClick(tableRows, arg);
            }
=======
            InternalCommand(4, null, new[] {0});
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac
        }

        private void SetBChangePlcClick(object sender, EventArgs e)
        {
            PLC plc = new PLC
                          {
                              namePLC = set_inp_name_plc.Text,
                              typePLC = set_inp_type_plc.Text,
                              numberPLC = Convert.ToInt32(set_inp_number_plc.Text)
                          };
            set_treeview_mount.Nodes[0].Text = "PLC №" + set_inp_number_plc.Text;
            if (!configClass.SavePlc(plc))
            {
                MessageBox.Show("Ошибка сохранения параметров ПЛК", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LoadHardwareConfig(object sender, EventArgs e)
        {
<<<<<<< .mine            int[] paramset1 = new int[6];
            int[] paramset2 = new int[6];
            var commandOne = new CommandToPlc();
            RtpConfigDataContext data = new RtpConfigDataContext();
            var groupssignal = data.GetRtpSignalGroups().ToList();
            foreach (var getRtpSignalGroupsResult in groupssignal)
            {
                var mount = data.GetMountForSignalsGroup(_rtpid, getRtpSignalGroupsResult.signalattrnumber,
                                                         getRtpSignalGroupsResult.signalgroup).ToList();
                if (mount.Count > 0 && mount.First().commandid.Value == (int)CommandName.MountChannel)
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
=======<<<<<<< HEAD
        //    switch (tabConfigPLC_S7.SelectedIndex)
        //    {
        //        case 0:
        //        case 1:
        //            AddEditTag addEditTag = new AddEditTag {Edit = false};
        //            if (addEditTag.ShowDialog(this) == DialogResult.OK)
        //            {
        //                ATag atag = new ATag(-1);
        //                atag.rawMIN = addEditTag.RAWmin;
        //                atag.rawMAX = addEditTag.RAWmax;
        //                atag.EU_MAX = addEditTag.EUmax;
        //                atag.EU_MIN = addEditTag.EUmin;
        //                atag.namePLC = addEditTag.NamePLC;
        //                atag.nameSCADA = addEditTag.NameScada;
        //                atag.description = addEditTag.Description;
        //                configClass.SaveTag(atag);
        //            }
        //            break;
        //        default:
        //            break;
>>>>>>> .theirs
                            paramset2[mount[i].signaltype + 1] = mount[i].modulnumber == null
                                                                 ? -1
                                                                 : mount[i].offsetModul.Value;
                        }
                    }
                    commandOne.CommandNumber = (int)CommandName.MountChannel;
                    commandOne.Values = paramset1;
                    var commandTwo = new CommandToPlc();
                    commandTwo.CommandNumber = (int)CommandName.MountModul;
                    commandTwo.Values = paramset2;
                    commandToPlc.Enqueue(commandTwo);
                    commandToPlc.Enqueue(commandOne);

                }
                if (mount.Count > 0 && mount.First().commandid == (int)CommandName.MountGenericSignals)
                {
                    var paramset = mount.First();
                    paramset1[0] = paramset.signaltype;
                    paramset1[1] = paramset.channelnumber == null ? 0 : paramset.channelnumber.Value - 1;
                    paramset1[2] = paramset.signalcontrain;
                    paramset1[3] = paramset.modulnumber == null ? 0 : paramset.modulnumber.Value - 1;
                    commandOne.CommandNumber = (int)CommandName.MountGenericSignals;
                    commandOne.Values = paramset1;
                    commandToPlc.Enqueue(commandOne);

                }
            }
            CommandForPlc();
=======
            switch (tabConfigPLC_S7.SelectedIndex)
            {
                case 0:
                case 1:
                    AddEditTag addEditTag = new AddEditTag {Edit = false};
                    if (addEditTag.ShowDialog(this) == DialogResult.OK)
                    {
                        ATag atag = new ATag(-1);
                        atag.rawMIN = addEditTag.RAWmin;
                        atag.rawMAX = addEditTag.RAWmax;
                        atag.EU_MAX = addEditTag.EUmax;
                        atag.EU_MIN = addEditTag.EUmin;
                        atag.namePLC = addEditTag.NamePLC;
                        atag.nameSCADA = addEditTag.NameScada;
                        atag.description = addEditTag.Description;
                        configClass.SaveTag(atag);
                       // LoadATags(tabConfigPLC_S7.SelectedIndex);
                    }
                    break;
                default:
                    break;

            }    
>>>>>>> a0a36b5c3212f2e3f54a316800c6979999daa5ac
        }


        private void ConfigPlcS7Load(object sender, EventArgs e)
        {
            LoadAllModuleChannel();
            ChangeEnableButtons(tabConfigPLC_S7.SelectedIndex);
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
                throw;  // Re-throw the exception
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

        void ActiveXCtrlControlAdded(object sender, ControlEventArgs e)
        {
            // Register tab handler and focus-related event handlers for 
            // the control and its child controls.
            ActiveXCtrlHelper.WireUpHandlers(e.Control, ValidationHandler);
        }

        // Ensures that the Validating and Validated events fire properly
        internal void ValidationHandler(object sender, System.EventArgs e)
        {
            if (this.ContainsFocus) return;

            this.OnLeave(e); // Raise Leave event

            if (this.CausesValidation)
            {
                CancelEventArgs validationArgs = new CancelEventArgs();
                this.OnValidating(validationArgs);

                if (validationArgs.Cancel && this.ActiveControl != null)
                    this.ActiveControl.Focus();
                else
                    this.OnValidated(e); // Raise Validated event
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand,
            Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_SETFOCUS = 0x7;
            const int WM_PARENTNOTIFY = 0x210;
            const int WM_DESTROY = 0x2;
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_RBUTTONDOWN = 0x204;

            if (m.Msg == WM_SETFOCUS)
            {
                // Raise Enter event
                this.OnEnter(System.EventArgs.Empty);
            }
            else if (m.Msg == WM_PARENTNOTIFY && (
                m.WParam.ToInt32() == WM_LBUTTONDOWN ||
                m.WParam.ToInt32() == WM_RBUTTONDOWN))
            {
                if (!this.ContainsFocus)
                {
                    // Raise Enter event
                    this.OnEnter(System.EventArgs.Empty);
                }
            }
            else if (m.Msg == WM_DESTROY &&
                !this.IsDisposed && !this.Disposing)
            {
                // Used to ensure the cleanup of the control
                this.Dispose();
            }

            base.WndProc(ref m);
        }

        // Ensures that tabbing across the container and the .NET controls
        // works as expected
        void ActiveXCtrlLostFocus(object sender, EventArgs e)
        {
            ActiveXCtrlHelper.HandleFocus(this);
        }


        private void OpenFolderDialogClick(object sender, EventArgs e)
        {
            if(folderDialog.ShowDialog() == DialogResult.OK)
            {
                p_plc_config.Text = _parametrsConfig.PathStaticConfig = folderDialog.SelectedPath;
                configClass.SaveStaticConfigParam(_parametrsConfig);
            }
        }
        private void VisualParamConfig()
        {
            
            p_plc_config.Text = _parametrsConfig.PathStaticConfig;
            l_version.Text = "Версия компонента:           " + _parametrsConfig.Assembly;
            l_data_static.Text = "Дата статической конфигурации: " + _parametrsConfig.DateStaticConfig;
            l_dinamic.Text = "Кол-во изменений (динамика): " + _parametrsConfig.CountDinamicConfig;
            l_static.Text = "Кол-во изменений (статика):  " + _parametrsConfig.CountStaticConfig;
            inp_time_interval.Text = _parametrsConfig.TimeIntervalSeconds.ToString();
            inp_time_samples.Text = _parametrsConfig.TimeSamlesMSeconds.ToString();
            dgw_hist_plc_config.Rows.Clear();
            DirectoryInfo di = new DirectoryInfo(_parametrsConfig.PathStaticConfig + "\\SourceArchive");
            if (!di.Exists)
                return;
            foreach (FileInfo fi in di.GetFiles())
            {
                string[] dFile = fi.Name.Split("_".ToCharArray());
                if(dFile.Length == 4)
                {
                    int rnumber = dgw_hist_plc_config.Rows.Add();
                    dgw_hist_plc_config.Rows[rnumber].Cells[0].Value = "";
                    dgw_hist_plc_config.Rows[rnumber].Cells[1].Value = dFile[1].Insert(2, ".").Insert(5, ".") + " " +
                        (dFile[2].Length == 6 ? dFile[2].Insert(2, ":").Insert(5, ":") : dFile[2].Insert(1, ":").Insert(4, ":"));
                    dgw_hist_plc_config.Rows[rnumber].Cells[2].Value = dFile[3].Replace(".zip", "");
                    dgw_hist_plc_config.Rows[rnumber].Cells[3].Value = "Востановить";

                }
               
            } 
            dgw_hist_plc_config.Sort(dgw_hist_plc_config.Columns[1], ListSortDirection.Descending);
                for(var i= 0; i < dgw_hist_plc_config.Rows.Count; i++)
                {
                    dgw_hist_plc_config.Rows[i].Cells[0].Value = (i + 1);
                }
        }


        private void SetDgvChannelMountEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cb = e.Control as ComboBox;
            if (cb != null)
            {
                // first remove event handler to keep from attaching multiple:
                cb.SelectedIndexChanged -= new EventHandler(SbSelectedIndexChanged);

                // now attach the event handler
                cb.SelectedIndexChanged += new EventHandler(SbSelectedIndexChanged);
            }

        }

        void SbSelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl dataGridViewComboBoxCell = (DataGridViewComboBoxEditingControl)sender;
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
                    var signals = data.GetSignalsIdForGroupId(selectedgroup.signalgroup, set_ddl_type_modul.SelectedIndex);
                    ((DataGridViewComboBoxCell)
                         set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[3]).Items.Clear();
                    foreach (var signal in signals)
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
                set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value  != null)
            {
                var signalForSelect =
                    data.GetRtpSignals(
                        (int)set_dgv_channel_mount.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value,
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
    }
}
 
