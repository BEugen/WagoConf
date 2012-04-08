﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.Security.Permissions;

namespace Config_PLC_SIEMENS
{

    [Guid("2B6B0F74-77D5-4F52-90E5-7D379988F854")]
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    public interface IScadaInterface
    {
        int Command { get; set; }
        int Accept { get; set; }
        int P1 { get; set; } 
        int P2 { get; set; }
        int P3 { get; set; }
        int P4 { get; set; }
        int P5 { get; set; }
        int P6 { get; set; }

    }
    [Guid("901EE2A0-C47C-43ec-B433-985C020051D5")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IScadaInterfaceEvent
    {
        [DispId(1)]
         void CommandEvent();
        [DispId(2)]
        void PidSetupEvent();
        [DispId(3)]
        void ChangeValueEvent();
    }
}
