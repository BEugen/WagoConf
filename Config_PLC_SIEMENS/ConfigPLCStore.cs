using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;
using System.IO;

namespace Config_PLC_SIEMENS
{

    
    public class ConfigPLCStore
    {

        private bool LoadXDoc(ref XDocument xDoc)
        {
            try
            {
                xDoc = XDocument.Load(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) +
                   "\\ConfigWagoRtp.xml");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<string> GetNamesPlc()
        {
            List<string> namesPlc = new List<string>();
            ATag[] tags = GetTags();
            foreach (ATag tag in tags)
            {
                namesPlc.Add(tag.namePLC);
            }
            return namesPlc;
        }
        public List<string> GetNamesScada()
        {
            List<string> namesScada= new List<string>();
            ATag[] tags = GetTags();
            foreach (ATag tag in tags)
            {
                namesScada.Add(tag.nameSCADA);
            }
            return namesScada;
        }
        public List<string> GetNameAi()
        {
            List<string> namesPlc = new List<string>();
            ATag[] tags = GetTags();
            foreach (ATag tag in tags)
            {
                if(tag.type < 10 && tag.type != -1)
                  namesPlc.Add(tag.namePLC);
            }
            return namesPlc;
        }
        public List<string> GetNameAo()
        {
            List<string> namesPlc = new List<string>();
            ATag[] tags = GetTags();
            foreach (ATag tag in tags)
            {
                if (tag.type > 9)
                    namesPlc.Add(tag.namePLC);
            }
            return namesPlc;
        }
        public List<string> GetNameNone()
        {
            List<string> namesPlc = new List<string>();
            ATag[] tags = GetTags();
            foreach (ATag tag in tags)
            {
                if (tag.type == -1)
                    namesPlc.Add(tag.namePLC);
            }
            return namesPlc;
        }
        //возвращает описание контроллера
        public PLC GetPlc()
        {
            PLC plc = new PLC();
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return plc;
            }
            XElement xEl = xDoc.Descendants("ConfigAModul").First();
           plc.namePLC = xEl.Attribute("PLC_NAME").Value;
           plc.typePLC = xEl.Attribute("PLC_TYPE").Value;
           plc.numberPLC = Convert.ToInt32(xEl.Attribute("PLC_NUMBER").Value);
           return plc;
        }
        //возвращет структуру - модуль с привязанными переменными
        public Modul GetModul(int id)
        {       
            Modul mod = new Modul(-1);
            try
            {              
                XDocument xDoc = null;
                if (!LoadXDoc(ref xDoc))
                {
                    return mod;
                }
                XElement xEl = xDoc.Descendants("m").Where(eL => (Convert.ToInt32(eL.Attribute("mid").Value) == id)).First();
                mod.id = Convert.ToInt32(xEl.Attribute("mid").Value);
                mod.typeModul = Convert.ToInt32(xEl.Attribute("type").Value);
               // mod.ChannelMount = new List<Channel>();
                foreach (XElement xElChild in xEl.Elements())
                {
                    Channel chModul = new Channel
                                          {
                                              typeChannel = mod.typeModul,
                                              parentID = mod.id,
                                              id = Convert.ToInt32(xElChild.Attribute("cid").Value),
                                              address =
                                                  xElChild.Attribute("address").Value == ""
                                                      ? -1
                                                      : Convert.ToInt32(xElChild.Attribute("address").Value),
                                              tagMount = GetTag(Convert.ToInt32(xElChild.Value)),
                                              use_to_code_PLC =
                                                  xElChild.Attribute("use_code").Value == "0" ? false : true
                                          };
                    mod.ChannelMount.Add(chModul);
                }
                return mod;
            }
            catch
            {
                return mod;
            }
            
        }
        //возвращает структуру - перерменную по её имени (имя в контроллере)
        public ATag GetTag(string namePlc)
        {
            ATag tag = new ATag(-1);
            XDocument xDoc = null;
            if (namePlc == "" || !LoadXDoc(ref xDoc))
            {
                    return tag;
            }
            try
            {
                XElement xEl = xDoc.Descendants("ac").Where(el => (string)el.Attribute("namePLC") != "" &&
                   ((string)el.Attribute("namePLC") == namePlc)).First();
                return _GetTag(xEl, tag);
            }
            catch
            {
                return tag;
            }
        }
        //возвращает структуру - перерменную по её ID
        public ATag GetTag(int ID)
        {
            ATag tag = new ATag(-1);
            XDocument xDoc = null;
            if (ID == -1 || !LoadXDoc(ref xDoc))
            {
                return tag;
            }
            try
            {
                XElement xEl = xDoc.Descendants("ac").Where(el => Convert.ToInt32(el.Attribute("id").Value)== ID).First();
                return _GetTag(xEl, tag);
            }
            catch
            {
                return tag;
            }
        }
        //возвращает структуру - канал по её ID
        public Channel GetChannel(int ID, int ParentID)
        {
              XDocument xDoc = null;
            Channel ch = new Channel(-1);
            if (!LoadXDoc(ref xDoc))
            {
                return ch;
            }
            try
            {
                XElement xEl = xDoc.Descendants("m").Where(el => (Convert.ToInt32(el.Attribute("mid").Value) == ParentID)).First();
                ch.typeChannel = Convert.ToInt32(xEl.Attribute("type").Value);
                xEl = xEl.Descendants("adr").Where(el => Convert.ToInt32(el.Attribute("cid").Value) == ID).First();
                ch.parentID = ParentID;
                ch.id = Convert.ToInt32(xEl.Attribute("cid").Value);
                ch.address = xEl.Attribute("address").Value == "" ? -1 : Convert.ToInt32(xEl.Attribute("address").Value);
                ch.tagMount = GetTag(Convert.ToInt32(xEl.Value));
                ch.use_to_code_PLC = xEl.Attribute("use_code").Value == "0" ? false : true;
                return ch;
            }
            catch
            {
                return ch;
            }

        }
        //возвращает структуру - регулятор
        public rPID GetPID(int ID)
        {
            rPID rpid = new rPID(-1);
            XDocument xDoc = null;
            if (ID == -1 || !LoadXDoc(ref xDoc))
            {
                return rpid;
            }
            try
            {
                XElement xEl = xDoc.Descendants("rPID").Where(el => Convert.ToInt32(el.Attribute("id").Value) == ID).First();
                return _GetPID(xEl, rpid);
            }
            catch
            {
                return rpid;
            }
        }
        //возващает массив структур - переменных 
        public ATag[] GetTags()
        {
            ATag[] tags = null;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return tags;
            }
            try
            {
                IEnumerable<XElement> xNodes =
                       from eX in xDoc.Descendants("ac")
                       orderby ((int)eX.Attribute("id"))
                       select eX;
                tags = new ATag[xNodes.Count()];
                int i = 0;
                foreach (XElement xEl in xNodes)
                {
                    ATag tag = new ATag(-1);
                    tags[i++] = _GetTag(xEl, tag);
                }
                return tags;
            }
            catch
            {
                return tags;
            }
        }
        //возващает массив структур - регуляторов 
        public rPID[] GetPIDs()
        {
            rPID[] rpids = null;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return rpids;
            }
            try
            {
                IEnumerable<XElement> xNodes =
                       from eX in xDoc.Descendants("rPID")
                       orderby ((int)eX.Attribute("id"))
                       select eX;
                rpids = new rPID[xNodes.Count()];
                int i = 0;
                foreach (XElement xEl in xNodes)
                {
                    rPID rpid = new rPID(-1);
                    rpids[i++] = _GetPID(xEl, rpid);
                }
                return rpids;
            }
            catch
            {
                return rpids;
            }
        }
        //возващает массив структур - модулей 
        public Modul[] GetModuls()
        {
            Modul[] moduls = null;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return moduls;
            }
            try
            {
                IEnumerable<XElement> xNodes =
               from e in xDoc.Descendants("m")
               orderby ((int)e.Attribute("mid"))
               select e;
                moduls = new Modul[xNodes.Count()];
                int i = 0;
                foreach (XElement xEl in xNodes)
                {
                    moduls[i++] = GetModul(Convert.ToInt32(xEl.Attribute("mid").Value));
                }
                return moduls;
            }
            catch
            {
                return moduls;
            }

        }
        //возвращает структуру - пременную по XElement
        private ATag _GetTag(XElement xEl, ATag tag)
        {
            try
            {
                tag.id = Convert.ToInt32(xEl.Attribute("id").Value);
                tag.rawMIN = Convert.ToInt32(xEl.Attribute("rawMIN").Value);
                tag.rawMAX = Convert.ToInt32(xEl.Attribute("rawMAX").Value);
                tag.EU_MIN = Convert.ToDouble(xEl.Attribute("minEU").Value);
                tag.EU_MAX = Convert.ToDouble(xEl.Attribute("maxEU").Value);
                tag.type = Convert.ToInt32(xEl.Attribute("type").Value);
                tag.namePLC = xEl.Attribute("namePLC").Value;
                tag.nameSCADA = xEl.Attribute("nameSCADA").Value;
                tag.address = Convert.ToInt32(xEl.Attribute("address").Value);
                tag.ftime = Convert.ToInt32(xEl.Attribute("fTime").Value);
                tag.idReg = Convert.ToInt32(xEl.Attribute("regID").Value);
                tag.description = xEl.Value;
                return tag;
            }
            catch
            {
                return tag;
            }
        }
        //возвращает структуру - регулятор по XElement
        private rPID _GetPID(XElement xEl, rPID rpid)
        {
            try
            {
                rpid.id = Convert.ToInt32(xEl.Attribute("id").Value);
                rpid.namePID = xEl.Attribute("namePID").Value;
                rpid.atagPV = GetTag(Convert.ToInt32(xEl.Attribute("PV").Value));
                rpid.atagSP = GetTag(Convert.ToInt32(xEl.Attribute("SP").Value));
                rpid.atagCV = GetTag(Convert.ToInt32(xEl.Attribute("CV").Value));
                rpid.atagCV_MANUAL = GetTag(Convert.ToInt32(xEl.Attribute("CV_MANUAL").Value));
                rpid.T0_n = Convert.ToDouble(xEl.Attribute("T0_n").Value);
                rpid.Kp = Convert.ToDouble(xEl.Attribute("Kp").Value);
                rpid.Ti = Convert.ToDouble(xEl.Attribute("Ti").Value);
                rpid.Td = Convert.ToDouble(xEl.Attribute("Td").Value);
                rpid.N = Convert.ToDouble(xEl.Attribute("N").Value);
                rpid.Us_min = Convert.ToDouble(xEl.Attribute("Us_min").Value);
                rpid.Us_max = Convert.ToDouble(xEl.Attribute("Us_max").Value);
                rpid.E_min = Convert.ToDouble(xEl.Attribute("E_min").Value);
                rpid.E_max = Convert.ToDouble(xEl.Attribute("E_max").Value);
                rpid.TempUst = Convert.ToDouble(xEl.Attribute("TempUst").Value);
                rpid.TempOut = Convert.ToDouble(xEl.Attribute("TempOut").Value);
                rpid.O_Min = Convert.ToDouble(xEl.Attribute("O_Min").Value);
                rpid.O_Max = Convert.ToDouble(xEl.Attribute("O_Max").Value);
                rpid.PID_Mode = Convert.ToByte(xEl.Attribute("PID_Mode").Value);
                rpid.Description = xEl.Value;
                return rpid;
            }
            catch
            {
                return rpid;
            }
        }
        //удаляет переменную по её ID и отвязывает её от монтажа
        public bool RemoveTag(int ID)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                XElement xEl = xDoc.Descendants("ac").Where(el => Convert.ToInt32(el.Attribute("id").Value) == ID).First();
                IEnumerable<XElement> xNodesT = xDoc.Descendants("adr").Where(el => el.Value == xEl.Attribute("id").Value);
                foreach (XElement xElA in xNodesT)
                {
                    xElA.Value = "";
                }
                //TODO с появлением регуяторов при удаленниии переменной необходимо добавить удаление переменной у канала регулятора
                xNodesT = xDoc.Descendants("ac").Where(el => (int)el.Attribute("id") > ID);
                foreach (XElement xElT in xNodesT)
                {
                    xElT.Attribute("id").Value = (Convert.ToInt32(xElT.Attribute("id").Value) - 1).ToString();
                }
                xEl.Remove();
                IEnumerable<XElement> xPid =
                    xDoc.Descendants("rPID").Where(
                        el =>
                        (Convert.ToInt32(el.Attribute("PV").Value) == ID ||
                         Convert.ToInt32(el.Attribute("SP").Value) == ID ||
                         Convert.ToInt32(el.Attribute("CV").Value) == ID ||
                         Convert.ToInt32(el.Attribute("CV_MANUAL").Value) == ID));

                foreach (XElement xElrPid in xPid)
                {
                    if (Convert.ToInt32(xElrPid.Attribute("PV").Value) == ID)
                        xElrPid.Attribute("PV").Value = "-1";
                    if (Convert.ToInt32(xElrPid.Attribute("SP").Value) == ID)
                        xElrPid.Attribute("SP").Value = "-1";
                    if (Convert.ToInt32(xElrPid.Attribute("CV").Value) == ID)
                        xElrPid.Attribute("CV").Value = "-1";
                    if (Convert.ToInt32(xElrPid.Attribute("CV_MANUAL").Value) == ID)
                        xElrPid.Attribute("CV_MANUAL").Value = "-1";
                }
                SaveSet(xDoc);
                return true;

            }
            catch
            {
                return false;
            }

        }
        //удаляет модуль по ID
        public bool RemoveModul(int ID)
        {
             XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                 XElement xEl = xDoc.Descendants("m").Where(el => (Convert.ToInt32(el.Attribute("mid").Value) == ID)).First();//.Remove();
                 foreach (XElement channel in xEl.Elements())
                 {
                     if (channel.Value != "")
                     {
                         ATag tag = GetTag(Convert.ToInt32(channel.Value));
                         tag.address = -1;
                         tag.type = -1;
                         _SaveTag(ref xDoc, tag);
                     }
                 }
                 xEl.Remove();
                IEnumerable<XElement> xNodesM = xDoc.Descendants("m").Where(el => (int)el.Attribute("mid") > ID);
                foreach (XElement xElM in xNodesM)
                {
                    xElM.Attribute("mid").Value = (Convert.ToInt32(xElM.Attribute("mid").Value) - 1).ToString();
                }
                SaveSet(xDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //удаляет канал по ID модуля и ID канала
        public bool RemoveChannel(int ParentID, int ID)
        {
             XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                XElement xEl = xDoc.Descendants("m").Where(el => (Convert.ToInt32(el.Attribute("mid").Value) == ParentID)).First();
                xEl = xEl.Descendants("adr").Where(el => Convert.ToInt32(el.Attribute("cid").Value) == ID).First();
                if (xEl.Value != "" && xEl.Value != "-1")
                {
                    ATag tag = GetTag(Convert.ToInt32(xEl.Value));
                    tag.address = -1;
                    tag.type = -1;
                    _SaveTag(ref xDoc, tag);
                }
                xEl.Remove();
                SaveSet(xDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //удаляет регулятор по ID
        public bool RemovePID(int id)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                XElement xEl = xDoc.Descendants("rPID").Where(el => (Convert.ToInt32(el.Attribute("id").Value) == id)).First();
                rPID rpid = GetPID(id);
                ATag atag = rpid.atagSP;
                atag.id = -1;
                CheckAndSaveChangesATag(ref xDoc, rpid.atagSP, atag);
                CheckAndSaveChangesATag(ref xDoc, rpid.atagPV, atag);
                CheckAndSaveChangesATag(ref xDoc, rpid.atagCV, atag);
                CheckAndSaveChangesATag(ref xDoc, rpid.atagCV_MANUAL, atag);
                xEl.Remove();
                SaveSet(xDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //сохранить переменную
        public bool SaveTag(ATag tag)
        {
             XDocument xDoc = null;
             
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                bool result = false;
                result = _SaveTag(ref xDoc, tag);
                
                //if (xDoc.Descendants("ac").Where(el => Convert.ToInt32(el.Attribute("id").Value) == tag.id).Count() == 0)
                //{
                //    tag.id = GetCountTag();
                //    result = AddTag(ref xDoc, tag);
                //}
                //else
                //{
                //    XElement xEl = xDoc.Descendants("ac").Where(el => Convert.ToInt32(el.Attribute("id").Value) == tag.id).First();
                //    xEl.Attribute("rawMIN").Value = tag.rawMIN.ToString();
                //    xEl.Attribute("rawMAX").Value = tag.rawMAX.ToString();
                //    xEl.Attribute("minEU").Value = tag.EU_MIN.ToString();
                //    xEl.Attribute("maxEU").Value = tag.EU_MAX.ToString();
                //    xEl.Attribute("type").Value = tag.type.ToString();
                //    xEl.Attribute("namePLC").Value = tag.namePLC;
                //    xEl.Attribute("nameSCADA").Value = tag.nameSCADA;
                //    xEl.Attribute("address").Value = tag.address.ToString();
                //    xEl.Attribute("fTime").Value = tag.ftime.ToString();
                //    xEl.Attribute("regID").Value = tag.idReg.ToString();
                //}
                SaveSet(xDoc);
                return result;
            }
            catch
            {
                return false;
            }
        }
        private bool _SaveTag(ref XDocument xDoc, ATag tag)
        {
            
            try
            {
                bool result = false;
                if (xDoc.Descendants("ac").Where(el => Convert.ToInt32(el.Attribute("id").Value) == tag.id).Count() == 0)
                {
                    tag.id = GetCountTag();
                    result = AddTag(ref xDoc, tag);
                }
                else
                {
                    XElement xEl = xDoc.Descendants("ac").Where(el => Convert.ToInt32(el.Attribute("id").Value) == tag.id).First();
                    xEl.Attribute("rawMIN").Value = tag.rawMIN.ToString();
                    xEl.Attribute("rawMAX").Value = tag.rawMAX.ToString();
                    xEl.Attribute("minEU").Value = tag.EU_MIN.ToString();
                    xEl.Attribute("maxEU").Value = tag.EU_MAX.ToString();
                    xEl.Attribute("type").Value = tag.type.ToString();
                    xEl.Attribute("namePLC").Value = tag.namePLC;
                    xEl.Attribute("nameSCADA").Value = tag.nameSCADA;
                    xEl.Attribute("address").Value = tag.address.ToString();
                    xEl.Attribute("fTime").Value = tag.ftime.ToString();
                    xEl.Attribute("regID").Value = tag.idReg.ToString();
                    xEl.Value = tag.description;
                    result = true;
                }
              //  SaveSet(xDoc);
                return result;
            }
            catch
            {
                return false;
            }

        }
        private bool _SavePID(ref XDocument xDoc, rPID rpid)
        {
            
            try
            {
                bool result = false;
                if (xDoc.Descendants("rPID").Where(el => Convert.ToInt32(el.Attribute("id").Value) == rpid.id).Count() == 0)
                {
                    rpid.id = GetCountPID();
                    result = AddPID(ref xDoc, rpid);
                }
                else
                {
                    XElement xEl = xDoc.Descendants("rPID").Where(el => Convert.ToInt32(el.Attribute("id").Value) == rpid.id).First();
                    xEl.Attribute("namePID").Value = rpid.namePID;
                    xEl.Attribute("PV").Value = rpid.atagPV.id.ToString();
                    xEl.Attribute("SP").Value = rpid.atagSP.id.ToString();
                    xEl.Attribute("CV").Value = rpid.atagCV.id.ToString();
                    xEl.Attribute("CV_MANUAL").Value = rpid.atagCV_MANUAL.id.ToString();
                    xEl.Attribute("T0_n").Value = rpid.T0_n.ToString();
                    xEl.Attribute("Kp").Value = rpid.Kp.ToString();
                    xEl.Attribute("Ti").Value = rpid.Ti.ToString();
                    xEl.Attribute("Td").Value = rpid.Td.ToString();
                    xEl.Attribute("N").Value = rpid.N.ToString();
                    xEl.Attribute("Us_min").Value = rpid.Us_min.ToString();
                    xEl.Attribute("Us_max").Value = rpid.Us_max.ToString();
                    xEl.Attribute("E_min").Value = rpid.E_min.ToString();
                    xEl.Attribute("E_max").Value = rpid.E_max.ToString();
                    xEl.Attribute("TempUst").Value = rpid.TempUst.ToString();
                    xEl.Attribute("TempOut").Value = rpid.TempOut.ToString();
                    xEl.Attribute("O_Min").Value = rpid.O_Min.ToString();
                    xEl.Attribute("O_Max").Value = rpid.O_Max.ToString();
                    xEl.Attribute("PID_Mode").Value = rpid.PID_Mode.ToString();
                    xEl.Value = rpid.Description;
                    result = true;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }
        //сохранить каннал
        public bool SaveChannel(Channel channel)
        {
              XDocument xDoc = null;
              
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                bool result = false;
                XElement xElMod = xDoc.Descendants("m").Where(eL => Convert.ToInt32(eL.Attribute("mid").Value) == channel.parentID).First();

                if (xElMod.Descendants("adr").Where(el => Convert.ToInt32(el.Attribute("cid").Value) == channel.id).Count() == 0)
                {
                    result = AddChannel(ref xDoc, channel);
                    SaveSet(xDoc);
                }
                else
                {   
                    XElement xElChannel = xElMod.Descendants("adr").Where(el => Convert.ToInt32(el.Attribute("cid").Value) == channel.id).First();
                    xElChannel.Attribute("address").Value = channel.address.ToString();
                    xElChannel.Attribute("use_code").Value = channel.use_to_code_PLC ? "1" : "0";
                    xElChannel.Value = channel.tagMount.id.ToString();
                    Channel ch = GetChannel(channel.id, channel.parentID);
                   
                    result = true;
                    if (ch.tagMount.id != -1 && channel.tagMount.id == -1 || ch.tagMount.id != channel.tagMount.id)
                    {
                       ATag atag = GetTag(ch.tagMount.id);
                        atag.address = -1;
                        atag.type = -1;
                        _SaveTag(ref xDoc, atag);
                    }
                  //  else
                //   {
                       // ATag atag = GetTag(ch.tagMount.id);
                       // Modul m = GetModul(channel.parentID);
                       // atag.address = channel.address;
                       // if (m.typeModul == 0 && atag.type > 9)
                      //  {
                      //      atag.type -= 10;
                      //  }
                      //  if (m.typeModul == 1 && atag.type < 10)
                      //  {
                      //      atag.type += 10;
                      //  }
                    if(channel.tagMount.id != -1)
                        _SaveTag(ref xDoc, channel.tagMount);
                   // }
                    
                }  
                SaveSet(xDoc);          
                return result;
            }
            catch
            {
                return false;
            }
        }
        //сохраняем модуль 
        public bool SaveModul(Modul modul)
        {
            XDocument xDoc = null;
            
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                bool result = false;
                if(xDoc.Descendants("m").Where(eL => Convert.ToInt32(eL.Attribute("mid").Value) == modul.id).Count() == 0)
                {
                    modul.id = GetCountModul();
                   result = AddModul(ref xDoc, modul);
                }
                else
                {
                    XElement xElMod = xDoc.Descendants("m").Where(eL => Convert.ToInt32(eL.Attribute("mid").Value) == modul.id).First();
                    xElMod.Attribute("type").Value = modul.typeModul.ToString();
                    xElMod.RemoveNodes();
                     foreach (Channel ch in modul.ChannelMount)
                    {
                        Channel c = ch;
                        c.parentID = modul.id;
                        result = AddChannel(ref xDoc, c);
                    }
                }
                SaveSet(xDoc);
                return result;
            }
            catch
            {
                return false;
            }
        }
        //сохраняем данные плк
        public bool SavePlc(PLC plc)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                XElement xEl = xDoc.Descendants("ConfigAModul").First();
                xEl.Attribute("PLC_NAME").Value = plc.namePLC;
                xEl.Attribute("PLC_TYPE").Value = plc.typePLC;
                xEl.Attribute("PLC_NUMBER").Value = plc.numberPLC.ToString();
                SaveSet(xDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //сохранить параметры pid регулятора
        public bool SavePID(rPID rpid)
        {
             XDocument xDoc = null;
              
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {

                bool result = false;
                if (xDoc.Descendants("rPID").Where(el => Convert.ToInt32(el.Attribute("id").Value) == rpid.id).Count() == 0)
                {
                    result = AddPID(ref xDoc, rpid);
                    SaveSet(xDoc);
                }
                else
                {
                    _SavePID(ref xDoc, rpid);
                    rPID rpidSaved = GetPID(rpid.id);
                    CheckAndSaveChangesATag(ref xDoc, rpidSaved.atagSP, rpid.atagSP);
                    CheckAndSaveChangesATag(ref xDoc, rpidSaved.atagPV, rpid.atagPV);
                    CheckAndSaveChangesATag(ref xDoc, rpidSaved.atagCV, rpid.atagCV);
                    CheckAndSaveChangesATag(ref xDoc, rpidSaved.atagCV_MANUAL, rpid.atagCV_MANUAL);
                    SaveSet(xDoc);
                    result = true;

                }
                return result;
            }
            catch
            {
                return false;
            }

        }
        private void CheckAndSaveChangesATag(ref XDocument xDoc, ATag savedATag, ATag newATag)
        {
            if (savedATag.id != -1 && newATag.id == -1 || savedATag.id != newATag.id)
            {
                savedATag.idReg = -1;
                _SaveTag(ref xDoc, savedATag);
            }
            if (newATag.id != -1)
            {
                _SaveTag(ref xDoc, newATag);
            }
        }
        //сохраняем конфигурацию
        private void SaveSet(XDocument xDoc)
        {
            XElement xEl = xDoc.Descendants("pathStaticCode").First();
            int count = Convert.ToInt32(xEl.Attribute("count_dynamic").Value);
            xEl.Attribute("count_dynamic").Value = (++count).ToString();
            xDoc.Save(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath) +
                   "\\ConfigWagoRtp.xml");
        }
        //добавляет новую переменную
        private bool AddTag(ref XDocument xDoc, ATag tag)
        {
            try
            {
                XElement xEl = new XElement("ac", new XAttribute("id", tag.id.ToString()), new XAttribute("rawMIN", tag.rawMIN.ToString()),
                    new XAttribute("rawMAX", tag.rawMAX.ToString()), new XAttribute("minEU", tag.EU_MIN.ToString()),
                    new XAttribute("maxEU", tag.EU_MAX.ToString()), new XAttribute("type", tag.type.ToString()),
                    new XAttribute("namePLC", tag.namePLC), new XAttribute("nameSCADA", tag.nameSCADA),
                    new XAttribute("address", tag.address.ToString()), new XAttribute("fTime", tag.ftime.ToString()),
                    new XAttribute("regID", tag.idReg.ToString()));
                xDoc.Descendants("ConfigAChannel").First().Add(xEl);
                return true;
            }
            catch
            {
                return false;
            }
        }
        //добавляет новый канал
        private bool AddChannel(ref XDocument xDoc, Channel channel)
        {
            try
            {
                XElement xEl = new XElement("adr", new XAttribute("address", channel.address.ToString()),
                    new XAttribute("cid", channel.id.ToString()), new XAttribute("use_code", channel.use_to_code_PLC ? "1" : "0"));
               xEl.Value = channel.tagMount.id.ToString();
               xDoc.Descendants("m").Where(eL => Convert.ToInt32(eL.Attribute("mid").Value) == channel.parentID).First().Add(xEl);
               return true;
            }
            catch
            {
                return false;
            }
        }
        //добавляет новый модуль
        private bool AddModul(ref XDocument xDoc, Modul modul)
        {
            try
            {
                XElement xEl = new XElement("m", new XAttribute("type", modul.typeModul.ToString()),
                    new XAttribute("mid", modul.id.ToString()));
                xDoc.Descendants("ConfigAModul").First().Add(xEl);
                foreach (Channel ch in modul.ChannelMount)
                {
                    Channel c = ch;
                    c.parentID = modul.id;
                    c.typeChannel = modul.typeModul;
                    AddChannel(ref xDoc, c);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //добавляет новый регулятор
        private bool AddPID(ref XDocument xDoc, rPID rpid)
        {
            try
            {
                XElement xEl = new XElement("rPID", new XAttribute("id", rpid.id.ToString()), new XAttribute("namePID", rpid.namePID),
                    new XAttribute("PV", rpid.atagPV.id.ToString()), new XAttribute("SP", rpid.atagSP.id.ToString()),
                    new XAttribute("CV", rpid.atagCV.id.ToString()), new XAttribute("CV_MANUAL", rpid.atagCV_MANUAL.id.ToString()),
                    new XAttribute("T0_n", rpid.T0_n.ToString()), new XAttribute("Kp", rpid.Kp.ToString()),
                    new XAttribute("Td", rpid.Td.ToString()), new XAttribute("N", rpid.N.ToString()),
                    new XAttribute("Us_min", rpid.Us_min.ToString()), new XAttribute("Us_max", rpid.Us_max.ToString()),
                    new XAttribute("E_min", rpid.E_min.ToString()), new XAttribute("E_max", rpid.E_max.ToString()),
                    new XAttribute("TempUst", rpid.TempUst.ToString()), new XAttribute("TempOut", rpid.TempOut.ToString()),
                    new XAttribute("O_Min", rpid.O_Min.ToString()), new XAttribute("O_Max", rpid.O_Max.ToString()),
                    new XAttribute("PID_Mode", rpid.PID_Mode.ToString()));
                return true;
            }
            catch
            {
                return false;
            }
        }
        public int GetCountTag()
        {
            int count = 0;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return count;
            }
            count = xDoc.Descendants("ConfigAChannel").First().Nodes().Count();
            return count;
        }
        public int GetCountModul()
        {
            int count = 0;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return count;
            }
            count = xDoc.Descendants("ConfigAModul").First().Nodes().Count();
            return count;
        }
        public int GetCountPID()
        {
            int count = 0;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return count;
            }
            count = xDoc.Descendants("PID_reg").First().Nodes().Count();
            return count;
        }

        public bool CheckAddress(int address, int modulType, ref string description)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return true;
            }
            IEnumerable<XElement> xNodes = xDoc.Descendants("adr").Where(el => el.Attribute("address").Value != "" &&
                   Convert.ToInt32(el.Attribute("address").Value) >= address - 1  && 
                   Convert.ToInt32(el.Attribute("address").Value) <= address + 1);
            foreach (XElement xEl in xNodes)
            {
                if (Convert.ToInt32(xEl.Parent.Attribute("type").Value) == modulType)
                {
                    description = "Данный адрес уже используется\nМодуль №" +
                        ((int)xEl.Parent.Attribute("mid") + 1).ToString() +
                        " Канал №" + ((int)xEl.Attribute("cid") + 1).ToString();
                    return true;
                }
            }
            return false;
        }
        public bool CheckMountTag(string namePlc, ref string description)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return true;
            }
            ATag atag = GetTag(namePlc);
            if (atag.id == -1)
                return false;
            IEnumerable<XElement> xNodes = xDoc.Descendants("adr").Where(el => (Convert.ToInt32(el.Value) == atag.id));
            if (xNodes.Count() > 0)
            {
                description = "Данная переменная уже используется\nМодуль №" +
                   ((int)xNodes.First().Parent.Attribute("mid") + 1).ToString() +
                   " Канал №" + ((int)xNodes.First().Attribute("cid") + 1).ToString();
                return true;
            }
            return false;
        }
        public bool ClearConfig()
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return true;
            }
            try
            {
                xDoc.Descendants("ConfigAChannel").First().RemoveNodes();
                xDoc.Descendants("ConfigAModul").First().RemoveNodes();
                SaveSet(xDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckNamePlc(string namePlc)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return true;
            }
            try
            {

               return xDoc.Descendants("ac").Where(el => (string)el.Attribute("namePLC") == namePlc).Count() != 0;
            }
            catch
            {
                return false;
            }
        }
        public bool CheckNameSCADA(string nameScada)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return true;
            }
            try
            {

                return xDoc.Descendants("ac").Where(el => (string)el.Attribute("nameSCADA") == nameScada).Count() != 0;
            }
            catch
            {
                return false;
            }
        }

        public StaticConfig GetStaticConfigParam()
        {
            StaticConfig stConfig = new StaticConfig();
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return stConfig;
            }
            try
            {
                XElement xEl = xDoc.Descendants("pathStaticCode").First();
                stConfig.PathStaticConfig = xEl.Value;
                stConfig.DateStaticConfig = xEl.Attribute("data").Value;
                stConfig.CountStaticConfig = Convert.ToInt32(xEl.Attribute("count_static").Value);
                stConfig.CountDinamicConfig = Convert.ToInt32(xEl.Attribute("count_dynamic").Value);
                stConfig.TimeOut = Convert.ToInt32(xEl.Attribute("time_out").Value);
                stConfig.TimeIntervalSeconds = Convert.ToInt32(xEl.Attribute("time_interval").Value);
                stConfig.TimeSamlesMSeconds = Convert.ToInt32(xEl.Attribute("time_update").Value);
                stConfig.Assembly = System.Diagnostics.FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

                return stConfig;
            }
            catch
            {
                return stConfig;
            }

        }
        public bool SaveStaticConfigParam(StaticConfig staticConfigParam)
        {
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return false;
            }
            try
            {
                XElement xEl = xDoc.Descendants("pathStaticCode").First();
                xEl.Value = staticConfigParam.PathStaticConfig;
                xEl.Attribute("data").Value = staticConfigParam.DateStaticConfig;
                xEl.Attribute("count_static").Value = staticConfigParam.CountStaticConfig.ToString();
                xEl.Attribute("count_dynamic").Value = staticConfigParam.CountDinamicConfig.ToString();
                xEl.Attribute("time_out").Value = staticConfigParam.TimeOut.ToString();
                xEl.Attribute("time_interval").Value = staticConfigParam.TimeIntervalSeconds.ToString();
                xEl.Attribute("time_update").Value = staticConfigParam.TimeSamlesMSeconds.ToString();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public ATag[] FindTagsByName(string namePlc)
        {
            ATag[] tags = null;
            XDocument xDoc = null;
            if (!LoadXDoc(ref xDoc))
            {
                return tags;
            }
            try
            {

                IEnumerable<XElement> xNodes = xDoc.Descendants("ac").Where(el => ((string)el.Attribute("namePLC")).ToUpper().IndexOf(namePlc.ToUpper()) != -1);
                tags = new ATag[xNodes.Count()];
                int i = 0;
                foreach (XElement xEl in xNodes)
                {
                    ATag tag = new ATag(-1);
                    tags[i++] = _GetTag(xEl, tag);
                }
                return tags;
            }
            catch
            {
                return tags;
            }

        }

        //public int GetCountChannel(int ParenID)
        //{
        //}
    }
}
