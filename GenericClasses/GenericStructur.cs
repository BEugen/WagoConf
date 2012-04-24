using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RtpWagoConf
{
    public struct PLC
    {
        public int numberPLC;
        public string namePLC;
        public string typePLC;
    }
    public struct ATag
    {
        public int id;
        public int rawMIN;
        public int rawMAX;
        public double EU_MIN;
        public double EU_MAX;
        public int type;
        public int address;
        public int ftime;
        public int idReg;
        public string namePLC;
        public string nameSCADA;
        public string description;
        public ATag(int ID)
        {
            id = ID;
            rawMIN = 0;
            rawMAX = 32768;
            EU_MIN = 0.0;
            EU_MAX = 100.0;
            type = -1;
            address = -1;
            ftime = 0;
            idReg = -1;
            namePLC = "";
            nameSCADA = "";
            description = "";
        }
    }
    public struct Channel
    {
        public int parentID;
        public int id;
        public int address;
        public int typeChannel;
        public bool use_to_code_PLC;
        public ATag tagMount;
        public Channel(int ID)
        {
            parentID = -1;
            tagMount = new ATag(-1);
            id = ID;
            address = -1;
            typeChannel = -1;
            use_to_code_PLC = false;
        }
    }
    public struct Modul
    {
        public int id;
        public int typeModul;
        public List<Channel> ChannelMount;
        public Modul(int ID)
        {
            id = ID;
            typeModul = 0;
            ChannelMount = new List<Channel>();
        }
        public Modul(int ID, int TypeModul, int ChannelCount)
        {
            id = ID;
            typeModul = TypeModul;
            ChannelMount = new List<Channel>();
            for (int i = 0; i < ChannelCount; i++)
            {
                Channel channel = new Channel(i);
                ChannelMount.Add(channel);
            }
        }
    }
    public struct StaticConfig
    {
        public int CountDinamicConfig;
        public int CountStaticConfig;
        public int TimeOut;
        public int  TimeIntervalSeconds;
        public double TimeSamlesMSeconds;
        public string PathStaticConfig;
        public string DateStaticConfig;
        public string Assembly;
    }
    public struct rPID
    {
        public byte PID_Mode; //PID_Mode - индикатор ручного режима, bit 0 - true - ручной режим *, bit - 7 - true - OFF
        public int id;
        public double T0_n; //T0 - период дискретизации, сек
        public double Kp; //коэффициент пропорциональности
        public double Ti; //коэффициент при интегральной части, сек
        public double Td; //Td - коэффициент при диф части, сек
        public double N; //N - параметр передаточной функции диф.части (Kp*Td*p/(Td/N * p+ 1)
        public double Us_min;
        public double Us_max; //Us_min, Us_max - ограничение уставки
        public double E_min;
        public double E_max; //Emin, Emax - минимальная и максимальная граница нечувствительности
        public double TempUst; //TempU - максимальная скорость изменения уставки на выходе задатчика интенсивности, ед_сек
        public double TempOut; //TempOut - максимальная скорость изменения выхода регулятора, проц_сек
        public double O_Min;
        public double O_Max; //O_Min, O_Max - ограничение выходного сигнала
        public string namePID;
        public string Description;
        public ATag atagPV; //procces value выход объекта
        public ATag atagSP; //set point уставка
        public ATag atagCV; //control value управляющее воздействие на объект
        public ATag atagCV_MANUAL; //control value вход для пользовательских управляющих воздействий в ручном режиме
        public rPID(int ID)
        {
            id = ID;
            PID_Mode = 129;
            T0_n = 0.0;
            Kp = 0.0;
            Ti = 0.0;
            Td = 0.0;
            N = 0.0;
            Us_min = 0.0;
            Us_max = 0.0;
            E_min = 0.0;
            E_max = 0.0;
            TempUst = 0.0;
            TempOut = 0.0;
            O_Min = 0.0;
            O_Max = 0.0;
            namePID = "";
            Description = "";
            atagPV = new ATag(-1);
            atagSP = new ATag(-1);
            atagCV = new ATag(-1);
            atagCV_MANUAL = new ATag(-1);
        }
    }

    public struct ValuePIDChannel
    {
        public DateTime DateValue;
        public Dictionary<string, double> TagValue; //ScadaTagName
    }

   
}
