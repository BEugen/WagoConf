using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            _selectedTab = tabConfiпWago.SelectedIndex; 
            switch (tabConfiпWago.SelectedIndex)
            {
                case 0:
                    LoadAllModuleChannel();
                    break;
                case 1:
                    typeWork.SelectedIndex = 1;                  
                    SetLoadChannelMount();
                    CheckHardwareConfigError();
                    break;
                case 2:
                    LoadGroupConfig();
                    break;
                default:
                    break;

            }
            ChangeEnableButtons(tabConfiпWago.SelectedIndex);
        }

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
                case 0: case 1:
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
            var commandid = mount.First().commandid;
            if (commandid != null && (mount.Count > 0 && commandid.Value == (int)CommandName.MountChannel))
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
                
                tabConfiпWago.Enabled= false;

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
                tabConfiпWago.Enabled = true;
                set_pan_mount_wait.Visible = false;
                set_treeview_mount.Enabled = true;
                set_gb_channel_mount.Enabled = true;
                set_gb_type_module.Enabled = true;
                set_menu.Enabled = true;
                pan_tag_wait.Visible = false;
                set_text_mount_wait.Text = "";
            }
        }


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
                if (commandid != null && (mount.Count > 0 && commandid.Value == (int)CommandName.MountChannel))
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
        }


        private void ConfigPlcS7Load(object sender, EventArgs e)
        {
            LoadAllModuleChannel();
            ChangeEnableButtons(tabConfiпWago.SelectedIndex);
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
        void ActiveXCtrlLostFocus(object sender, EventArgs e)
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
                    var signalsIdForGroupId = data.GetSignalsIdForGroupId(selectedgroup.signalgroup, set_ddl_type_modul.SelectedIndex);
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

        private void LoadGroupConfig()
        {
            groupSetup.ColumnHeadersDefaultCellStyle.Font = new Font(new FontFamily("Arial Narrow"), 10);
            groupSetup.Rows.Clear();
            RtpConfigDataContext data = new RtpConfigDataContext();
            var groupShibers = data.GetGroupShiberSetup(_rtpid).ToList();
            var groups = data.GetGroupForGroupLoad(_rtpid).ToList();
            var shibers = data.GetRtpSignalGroups().Where(ex => (ex.signalgroup == 1)).ToList();
            foreach (var getGroupShiberSetupResult in groupShibers)
            {
                int rnumber = groupSetup.Rows.Add();
                groupSetup.Rows[rnumber].Cells[0].Value = getGroupShiberSetupResult.id;
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
                    ((DataGridViewComboBoxCell)groupSetup.Rows[rnumber].Cells[9]).Items.Add(
                        CutShiberName(shiber.signalgroupdescription));
                }
                groupSetup.Rows[rnumber].Cells[4].Value = CutShiberName( getGroupShiberSetupResult.shiberdecription1);
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

        void GroupSelectedIndexChanged(object sender, EventArgs e)
        {
            double timedoze = 0;
            double timeopen = 0;
            double timeclose = 0;
            string timekoeff = "";
            RtpConfigDataContext data = new RtpConfigDataContext();
            if (sender == null)
                return;
            DataGridViewComboBoxEditingControl dataGridViewComboBoxCell = (DataGridViewComboBoxEditingControl)sender;
            int selecedIndex = dataGridViewComboBoxCell.Items.IndexOf(dataGridViewComboBoxCell.SelectedItem);
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 2)//group change
            {
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[15].Value = selecedIndex + 1;
                var shibers = data.GetShibersConfigByGroupNumber(_rtpid, selecedIndex + 1).ToList();
                if (shibers.Count > 0)
                {
                    var shiberOneTwo = shibers.First(); 
                    
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Value = CutShiberName(shiberOneTwo.shiberdecription1);

                    timekoeff = CalcKoeffOpenClose(shiberOneTwo.timeOpen1, shiberOneTwo.timeClose1,
                                                   ref timeopen, ref timeclose, ref timedoze);

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[5].Value = timedoze.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[7].Value = timeopen.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[8].Value = timeclose.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[6].Value = timekoeff;

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Value = CutShiberName(shiberOneTwo.shiberdecription2);
                    timekoeff = CalcKoeffOpenClose(shiberOneTwo.timeOpen2, shiberOneTwo.timeClose2,
                                                   ref timeopen, ref timeclose, ref timedoze);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[10].Value = timedoze.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[12].Value = timeopen.ToString("0.0");
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[13].Value = timeclose.ToString("0.0");

                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[11].Value = timekoeff;
                }
              }  
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 4)//shiber 1 change
            {

                if ((selecedIndex+1) ==
                    (int)groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value)
                {
                  //  MessageBox.Show("В одной группе не могут быть 2 одинаковых шибера", "Ошибка",
                  //         MessageBoxButtons.OK, MessageBoxIcon.Error);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Style.BackColor = Color.FromArgb(244, 144, 131);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Style.BackColor = Color.FromArgb(244, 144, 131);
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].ErrorText =
                        "В одной группе не могут быть 2 одинаковых шибера";
                    groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].ErrorText =
                        "В одной группе не могут быть 2 одинаковых шибера";
                    groupSetup.Update();
                    return;
                }
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].Style.BackColor = System.Drawing.Color.Gainsboro;
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].Style.BackColor = System.Drawing.Color.Gainsboro;
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[4].ErrorText = "";
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[9].ErrorText = "";
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[16].Value = selecedIndex + 1;
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
           
            if (dataGridViewComboBoxCell.EditingControlDataGridView.CurrentCell.ColumnIndex == 9)//shiber 2 change
            {
                groupSetup.Rows[dataGridViewComboBoxCell.EditingControlRowIndex].Cells[17].Value = selecedIndex + 1;
            }
        }

        private void GroupSetupCellEndEdit(object sender, DataGridViewCellEventArgs e)
        
        {
groupSetup.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.FromArgb(172, 232, 172);
        }


        private string CalcKoeffOpenClose(int? timeOpen, int? timeClose, ref double timeOpenDoub, ref double timeCloseDoub   , ref double timeAll)
        {
            timeCloseDoub = 0;
            timeOpenDoub = 0;
            timeAll = 0;
            string result = "";
            if (timeOpen != null)
            {
                timeOpenDoub = (double)timeOpen.Value/100;
            }
            if (timeClose != null)
            {
                timeCloseDoub = (double)timeClose.Value / 100;
            }
            timeAll = timeOpenDoub + timeCloseDoub;
            if (timeAll != 0)
                result = (timeOpenDoub/timeAll).ToString("0.0") + " / " + (timeCloseDoub/timeAll).ToString("0.0");

            return result;
        }

        private string CutShiberName(string name)
        {
           return  name.Substring(0, 4) + ". "  + name.Substring(name.Length - 5, 5);
        }
        private string CutGroupName(int number)
        {
            return number + " группа";
        }

        private void GroupSetupCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }
    }
}
 
