using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Config_PLC_SIEMENS
{
    public class ConfigPidCompleteArgs : EventArgs
    {
        rPID _pid;
        DialogResult _dresult;
        bool _noerror;
        public ConfigPidCompleteArgs(rPID PID, DialogResult Result, bool NoError)
        {
            _pid = PID;
            _dresult = Result;
            _noerror = NoError;
        }
        public rPID PID
        {
            get
            {
                return _pid;
            }
        }
        public DialogResult Result
        {
            get
            {
                return _dresult;
            }
        }
        public bool NoError
        {
            get
            {
                return _noerror;
            }
        }
    }
    public class ParceHarswareConfigEventArgs : EventArgs
    {
        int _ModulCount;
        int _TagCreate;
        int _Error;
        public ParceHarswareConfigEventArgs(int ModulCount, int TagCreate, int Error)
        {
            _ModulCount = ModulCount;
            _TagCreate = TagCreate;
            _Error = Error;
        }
        public int ModulCount
        {
            get
            {
                return _ModulCount;
            }
        }
        public int TagCreate
        {
            get
            {
                return _TagCreate;
            }
        }
        public int Error
        {
            get
            {
                return _Error;
            }
        }
    }
    public class ChagePidParamArgs : EventArgs
    {
        rPID _pid;
        public ChagePidParamArgs(rPID PID)
        {
            _pid = PID;
        }
        public rPID PID
        {
            get
            {
                return _pid;
            }
        }
    }


        public class ChangeModeArgs : EventArgs
        {
            double cv_manual;
            double sp;
            bool automaticMode;
            public ChangeModeArgs(double CV_Manual, double SP, bool AutomaticMode)
            {
                cv_manual = CV_Manual;
                sp = SP;
                automaticMode = AutomaticMode;
            }
            public bool ModePid
            {
                get
                {
                    return automaticMode;
                }
            }
            public double CV_Manual
            {
                get
                {
                    return cv_manual;
                }
            }
            public double SP
            {
                get
                {
                    return sp;
                }
            }
        }

}
