using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using ICSharpCode.SharpZipLib.Zip;
using System.Runtime.Remoting.Messaging;

namespace Config_PLC_SIEMENS
{
    
    class StaticCodeGen
    {
        public delegate int GenStaticCode(string PathDir, int ID);
        public delegate void CodeGerateCompleteD(int ID);
        public event CodeGerateCompleteD CodeGenerateComplete;
        ConfigPLCStore configClass;
        public StaticCodeGen()
        {
            configClass = new ConfigPLCStore();
        }

        public void GenerateStaticCode(string Path, int ID)
        {
            GenStaticCode gSt = new GenStaticCode(GenerateCode);
            IAsyncResult iaResult = gSt.BeginInvoke(Path, ID, new AsyncCallback(GenerateCodeComplete), null);
        }

        private int GenerateZipArchive(string PathDir, int ID)
        {
            try
            {
                string[] files = Directory.GetFiles(PathDir + "\\SourceCurrent");
                string zipFile = PathDir + "\\SourceArchive\\Source_" + DateTime.Now.ToString("dMMyyyy_HHmm") + "00_" + ID.ToString() + ".zip";
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFile)))
                {
                    s.SetLevel(9);
                    byte[] buffer = new byte[4096];
                    foreach (string file in files)
                    {

                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
                return ID + 1;
            }
            catch
            {
                return -1;
            }
        }
        private int GenerateSourceFiles(string pathDir, int ID)
        {
            try
            {
                using (StreamWriter tagStream = new StreamWriter(pathDir + "\\SourceCurrent\\DB1_RAW_EU.SCL", false, Encoding.ASCII))
                {
                    StreamWriter tagStreamRaw = new StreamWriter(pathDir + "\\SourceCurrent\\DB2_RAW.SCL", false, Encoding.ASCII);
                    StreamWriter tagStreamEU = new StreamWriter(pathDir + "\\SourceCurrent\\DB3_REAL.SCL", false, Encoding.ASCII);
                    StreamWriter tagStreamMount = new StreamWriter(pathDir + "\\SourceCurrent\\DB4_ADDRESS_CHANNEL.SCL", false, Encoding.ASCII);
                    #region TagConfig
                    ATag[] atags = configClass.GetTags();
                    tagStream.WriteLine("DATA_BLOCK DB 1");
                    tagStream.WriteLine("AUTHOR : EugenB");
                    tagStream.WriteLine("FAMILY : ANALOG");
                    tagStream.WriteLine("NAME : A_RAW_EU");
                    tagStream.WriteLine("VERSION : " + ID.ToString() + ".0");
                    tagStream.WriteLine("STRUCT");
                    tagStream.WriteLine("    COUNT_TAG : INT := " + atags.Length.ToString() + ";");
                    #endregion
                    #region TagRAW
                    tagStreamRaw.WriteLine("DATA_BLOCK DB 2");
                    tagStreamRaw.WriteLine("AUTHOR : EugenB");
                    tagStreamRaw.WriteLine("FAMILY : ANALOG");
                    tagStreamRaw.WriteLine("NAME : A_RAW");
                    tagStreamRaw.WriteLine("VERSION : " + ID.ToString() + ".0");
                    tagStreamRaw.WriteLine("STRUCT");
                    #endregion
                    #region TagEU
                    tagStreamEU.WriteLine("DATA_BLOCK DB 3");
                    tagStreamEU.WriteLine("AUTHOR : EugenB");
                    tagStreamEU.WriteLine("FAMILY : ANALOG");
                    tagStreamEU.WriteLine("NAME : A_REAL");
                    tagStreamEU.WriteLine("VERSION : " + ID.ToString() + ".0");
                    tagStreamEU.WriteLine("STRUCT");
                    #endregion
                    #region TagMOUNT
                    tagStreamMount.WriteLine("DATA_BLOCK DB 4");
                    tagStreamMount.WriteLine("AUTHOR : EugenB");
                    tagStreamMount.WriteLine("FAMILY : PEREF");
                    tagStreamMount.WriteLine("NAME : P_ADDR");
                    tagStreamMount.WriteLine("VERSION : " + ID.ToString() + ".0");
                    tagStreamMount.WriteLine("STRUCT");
                    #endregion

                    
                    foreach (ATag atag in atags)
                    {
                        #region TagConfig
                        tagStream.WriteLine("    " + atag.namePLC + " : STRUCT // "  + atag.description);
                        tagStream.WriteLine("        rawMIN:WORD := W#16#" + Convert.ToUInt16(atag.rawMIN).ToString("X") + ";");
                        tagStream.WriteLine("        rawMAX:WORD :=W#16#" + Convert.ToUInt16(atag.rawMAX).ToString("X") + ";");
                        tagStream.WriteLine("        EU_MIN:REAL :=" + atag.EU_MIN.ToString("0.00000").Replace(",", ".") + ";");
                        tagStream.WriteLine("        EU_MAX:REAL :=" + atag.EU_MAX.ToString("0.00000").Replace(",", ".") + ";");
                        tagStream.WriteLine("        VFILTER:REAL :=0.0;");
                        tagStream.WriteLine("        TFILTER:INT := " + Convert.ToUInt16(atag.ftime).ToString() + ";");
                        tagStream.WriteLine("        ATYPE:INT := " + Convert.ToInt16(atag.type).ToString() + ";");
                        tagStream.WriteLine("    END_STRUCT;");
                        #endregion
                        #region TagRAW
                        tagStreamRaw.WriteLine("    " + atag.namePLC + " : WORD := w#16#0; // " + atag.description);
                        #endregion
                        #region TagEU
                        tagStreamEU.WriteLine("    " + atag.namePLC + " : REAL := 0.0; // " + atag.description);
                        #endregion
                        #region TagMOUNT
                        tagStreamMount.WriteLine("    " + atag.namePLC + " : WORD := w#16#" + Convert.ToInt16(atag.address).ToString("X") + "; // " + atag.description);
                        #endregion
                    }
                    tagStream.WriteLine("END_STRUCT");
                    tagStream.WriteLine("  BEGIN");
                    tagStream.WriteLine("  END_DATA_BLOCK");
                    tagStreamRaw.WriteLine("END_STRUCT");
                    tagStreamRaw.WriteLine("  BEGIN");
                    tagStreamRaw.WriteLine("  END_DATA_BLOCK");
                    tagStreamEU.WriteLine("END_STRUCT");
                    tagStreamEU.WriteLine("  BEGIN");
                    tagStreamEU.WriteLine("  END_DATA_BLOCK");
                    tagStreamMount.WriteLine("END_STRUCT");
                    tagStreamMount.WriteLine("  BEGIN");
                    tagStreamMount.WriteLine("  END_DATA_BLOCK");
                    tagStream.Close();
                    tagStreamRaw.Close();
                    tagStreamEU.Close();
                    tagStreamMount.Close();
                }
                using (StreamWriter pidIN_OUTStream = new StreamWriter(pathDir + "\\SourceCurrent\\DB6_PID_IN_OUT.SCL", false, Encoding.ASCII))
                {
                    StreamWriter pidStaticStream  = new StreamWriter(pathDir + "\\SourceCurrent\\DB7_PID_STATIC.SCL", false, Encoding.ASCII);
                    #region PID_IN_OUT
                    rPID[] rpids = configClass.GetPIDs();
                    pidIN_OUTStream.WriteLine("DATA_BLOCK DB 6");
                    pidIN_OUTStream.WriteLine("AUTHOR : EugenB");
                    pidIN_OUTStream.WriteLine("FAMILY : PID");
                    pidIN_OUTStream.WriteLine("NAME : PID_IO");
                    pidIN_OUTStream.WriteLine("VERSION : " + ID.ToString() + ".0");
                    pidIN_OUTStream.WriteLine("STRUCT");
                    pidIN_OUTStream.WriteLine("    COUNT_PID : INT := " + rpids.Length.ToString() + ";");
                    #endregion
                    #region PID_STATIC
                    pidIN_OUTStream.WriteLine("DATA_BLOCK DB 7");
                    pidIN_OUTStream.WriteLine("AUTHOR : EugenB");
                    pidIN_OUTStream.WriteLine("FAMILY : PID");
                    pidIN_OUTStream.WriteLine("NAME : PID_ST");
                    pidIN_OUTStream.WriteLine("VERSION : " + ID.ToString() + ".0");
                    pidIN_OUTStream.WriteLine("STRUCT");
                    #endregion

                    foreach (rPID rpid in rpids)
                    {
                        #region PID_IN_OUT
                        pidIN_OUTStream.WriteLine("    " + rpid.namePID + " : STRUCT // " + rpid.Description);
                        pidIN_OUTStream.WriteLine("        PV : INT := " + Convert.ToInt16(rpid.atagPV.id).ToString() + "; //procces value, выход объекта");
                        pidIN_OUTStream.WriteLine("        SP : INT := " + Convert.ToInt16(rpid.atagSP.id).ToString() + "; //set point, уставка");
                        pidIN_OUTStream.WriteLine("        CV : INT := " + Convert.ToInt16(rpid.atagCV.id).ToString() + "; //control value, управляющее воздействие на объект");
                        pidIN_OUTStream.WriteLine("        CV_MANUAL : INT := " + Convert.ToInt16(rpid.atagCV_MANUAL.id).ToString() + "; control value, вход для пользовательских" + 
                        "управляющих воздействий в ручном режиме");
                        pidIN_OUTStream.WriteLine("        T0_n : REAL := " + rpid.T0_n.ToString("0.00000").Replace(",", ".") + "; //T0 - период дискретизации, сек");
                        pidIN_OUTStream.WriteLine("        Kp : REAL := " + rpid.Kp.ToString("0.00000").Replace(",", ".") + "; //коэффициент пропорциональности");
                        pidIN_OUTStream.WriteLine("        Ti : REAL := " + rpid.Ti.ToString("0.00000").Replace(",", ".") + "; //коэффициент при интегральной части, сек");
                        pidIN_OUTStream.WriteLine("        Td : REAL := " + rpid.Td.ToString("0.00000").Replace(",", ".") + "; //Td - коэффициент при диф части, сек");
                        pidIN_OUTStream.WriteLine("        N : REAL := " + rpid.N.ToString("0.00000").Replace(",", ".") + "; //N - параметр передаточной функции" +
                            " диф.части (Kp*Td*p/(Td/N * p+ 1)");
                        pidIN_OUTStream.WriteLine("        Us_min : REAL := " + rpid.Us_min.ToString("0.00000").Replace(",", ".") + ";");
                        pidIN_OUTStream.WriteLine("        Us_max : REAL := " + rpid.Us_max.ToString("0.00000").Replace(",", ".") + "; //Us_min, Us_max - ограничение уставки");
                        pidIN_OUTStream.WriteLine("        E_min : REAL := " + rpid.E_min.ToString("0.00000").Replace(",", ".") + ";");
                        pidIN_OUTStream.WriteLine("        E_max : REAL := " + rpid.E_max.ToString("0.00000").Replace(",", ".") + "; //Emin, Emax - минимальная"+
                            " и максимальная граница нечувствительности");
                        pidIN_OUTStream.WriteLine("        TempUst : REAL := " + rpid.TempUst.ToString("0.00000").Replace(",", ".") + "; //TempU - максимальная скорость изменения"+
                            " уставки на выходе задатчика интенсивности, ед_сек");
                        pidIN_OUTStream.WriteLine("        TempOut : REAL := " + rpid.TempOut.ToString("0.00000").Replace(",", ".") + "; //TempOut - максимальная скорость "+
                            "изменения выхода регулятора, проц_сек");
                        pidIN_OUTStream.WriteLine("        O_Min : REAL := " + rpid.O_Min.ToString("0.00000").Replace(",", ".") + ";");
                        pidIN_OUTStream.WriteLine("        O_Max : REAL := " + rpid.O_Max.ToString("0.00000").Replace(",", ".") + "; //O_Min, O_Max - ограничение"+
                            " выходного сигнала");
                        pidIN_OUTStream.WriteLine("        Errs : WORD := W#16#0; //Errs - общий сигнал ошибок установки параметров регулятора (0-ошибок нет)");
                        pidIN_OUTStream.WriteLine("        PID_Mode :BYTE :=B#16#" + rpid.PID_Mode.ToString("X") + "; //PID_Mode - индикатор ручного режима"+
                            ", bit 0 - true - ручной режим *, bit - 7 - true - OFF");
                        pidIN_OUTStream.WriteLine("    END_STRUCT;");
                        #endregion
                        #region PID_STATIC
                        pidStaticStream.WriteLine("    " + rpid.namePID + " : STRUCT // " + rpid.Description);
                        pidStaticStream.WriteLine("        Up_1 :REAL := 0.0; //Up_1 это Up задержанная на 1 такт");
                        pidStaticStream.WriteLine("        Ud_1 : REAL := 0.0; //Ud_1 - предыдущий выход  диф звена");
                        pidStaticStream.WriteLine("        Ust_old : REAL := 0.0; //Ust_old - старое значение уставки - для определения момента ее изменения");
                        pidStaticStream.WriteLine("        Td_old : REAL := 0.0;");
                        pidStaticStream.WriteLine("        T0_old : REAL := 0.0;");
                        pidStaticStream.WriteLine("        N_old : REAL := 0.0; //Td_old, T0_old, N_old - старые значения параметров для определения изменения");
                        pidStaticStream.WriteLine("        Ad : REAL := 0.0;");
                        pidStaticStream.WriteLine("        Bd : REAL :=0.0; //Ad, Bd - параметры для реализации диф звена");
                        pidStaticStream.WriteLine("        T0_intern_old : REAL := 0.0;");
                        pidStaticStream.WriteLine("        Manual_old : BOOL := TRUE; //Manual_old - определение того, что предыдущий цикл был ручным");
                        pidStaticStream.WriteLine("    END_STRUCT;");
                        #endregion
                    }
                    pidIN_OUTStream.WriteLine("END_STRUCT");
                    pidIN_OUTStream.WriteLine("  BEGIN");
                    pidIN_OUTStream.WriteLine("  END_DATA_BLOCK");
                    pidStaticStream.WriteLine("END_STRUCT");
                    pidStaticStream.WriteLine("  BEGIN");
                    pidStaticStream.WriteLine("  END_DATA_BLOCK");
                }

                return ID;
            }
            catch
            {
                return -1;
            }
        }
        private int GenerateCode(string PathDir, int ID)
        {
            int result = 0;
            result = GenerateZipArchive(PathDir, ID);
            result = GenerateSourceFiles(PathDir, result);
            return result;
        }

        private void GenerateCodeComplete(IAsyncResult iaResult)
        {
            AsyncResult ar = (AsyncResult)iaResult;
            GenStaticCode gSt = (GenStaticCode)ar.AsyncDelegate;
            if(CodeGenerateComplete != null)
                CodeGenerateComplete(gSt.EndInvoke(iaResult));
        }
        
    }
}
