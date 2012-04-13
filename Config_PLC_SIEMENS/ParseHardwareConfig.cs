using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Runtime.Remoting.Messaging;


namespace Config_PLC_SIEMENS
{
    
    class ParseHardwareConfig
    {
        public delegate void ParceHarswareConfigCompleteD(object sender, ParceHarswareConfigEventArgs e);
        public delegate void ParceHarswareConfigAsynk(string pathConfig, ref int countModule, ref int countTag, 
            bool newConfig);
        public event ParceHarswareConfigCompleteD ParceHarswareConfigComplete;
        ConfigPLCStore configClass;

        string _regexp_module_rack = "(?<module_rack>RACK [\\d ,]*SLOT[\\d ,]*[\\w\\d\\s\"-]*, \"[\\w/\\]*\"\r\nBEGIN[ ]* \r\n([ ]*[\\w \\d\"]*\r\n)*([ ]+[\\w \\d, ]+\r\n)+([ ]*[\\w \\d\"]*\r\n)*([ ]+[\\w \\d, ]+\r\n)+)";
        string _regexp_module_dp = "(?<module_dp>DPSUBSYSTEM [\\d ,]*DPADDRESS[\\d ,]*SLOT[\\d ,]*\"[\\d\\s-\\w/.]*\", \"[\\w]*\"\r\nBEGIN[ ]*\r\n([ ]*[\\w \\d\"]*\r\n)*[ ]*[\\w ,\\d]*)";
        string _regexp_type_module = "(?<typeModule>( \"AO\\d+\\WAI\\d+)|( \"AI\\d+\\WAO\\d+)|( \"AO\\d*)|( \"\\d*AO)|( \"AI\\d*)|( \"\\d*AI))";
        string _regexp_address_channel = "(?<address_channel>ADDRESS[ ]*\\d*,[ ]*\\d*,[ ]*\\d*,)";

        public ParseHardwareConfig()
        {
            configClass = new ConfigPLCStore();
        }
        public void ParceConfig(string pathConfig, bool newConfig)
        {
            int countModule = 0;
            int countTag = 0;
            ParceHarswareConfigAsynk parceHardwareConfigAsynk = new ParceHarswareConfigAsynk(ParceConfigAsynk);
            IAsyncResult iaResult = parceHardwareConfigAsynk.BeginInvoke(pathConfig, ref countModule,
                ref countTag,  newConfig,
                new AsyncCallback(ParceConfigAsynkComplete), null);
        }

        private void ParceConfigAsynk(string pathConfig, ref int countModule, ref int countTag,
            bool newConfig)
        {
            string cfg = "";
            countModule = 0;
            countTag = 0;
            bool rackModulFound = false;
            using (StreamReader cfgReader = new StreamReader(pathConfig))
            {
                cfg = cfgReader.ReadToEnd();
            }
            string[] module = GetModuleData(cfg, _regexp_module_rack, "module_rack");
            if (module != null && module.Length > 0)
            {
                rackModulFound = true;
                AddConfig(module, ref countModule, ref countTag, true, "R");
            }
            module = GetModuleData(cfg, _regexp_module_dp, "module_dp");
            if (module != null && module.Length > 0)
            {
                AddConfig(module, ref countModule, ref countTag, !rackModulFound, "DP");
            }
        }
        private void AddConfig(string[] Module, ref int CountModule, ref int countTag,
            bool newConfig, string typeNositelModule)
        {
            //XElement configACh = ConfigFile.Descendants("ConfigAChannel").First();
           // XElement configAM = ConfigFile.Descendants("ConfigAModul").First();
            if (newConfig)
            {
                configClass.ClearConfig();
              //  configACh.RemoveNodes();
              //  configAM.RemoveNodes();
            }
            for (int i = 0; i < Module.Length; i++)
            {
                int[] type = {-1, -1};
                int[] startAddress = {-1, -1};
                int[] channelCount = {-1, -1};
                int countModule = 0;
                //string name = "";
                GetModulParam(Module[i], ref type, ref startAddress, ref channelCount);
                for (int k = 0; k < type.Length; k++)
                {
                   
                    if (type[k] != -1 && startAddress[k] != -1 && channelCount[k] != -1)
                    {
                        Modul module = new Modul(CountModule, type[k], channelCount[k]);
                        countModule = CountModule;
                       // configAM.Add(new XElement("m", new XAttribute("channelcount", channelCount[k].ToString()),
                       //     new XAttribute("type", type[k].ToString()), new XAttribute("mid", CountModule.ToString())));
                       // XElement modul = ConfigFile.Descendants("m").Where(eL => (int)eL.Attribute("mid") == countModule).First();
                        for (int j = 0; j < channelCount[k]; j++)
                        {
                           // name = "NONE_" + (type[k] == 0 ? "AI" : "AO") + "_" + TypeNositelModule + "_" + startAddress[k].ToString();
                            ATag atag = new ATag(countTag++);
                            Channel channel = new Channel(j);
                            atag.namePLC = atag.nameSCADA = "NONE_" + (type[k] == 0 ? "AI" : "AO") + "_" + typeNositelModule + "_" + startAddress[k].ToString();
                            atag.type = type[k] == 0 ? 0 : 10;
                            atag.address = channel.address = startAddress[k];
                            channel.typeChannel = type[k];
                            channel.tagMount = atag;
                            module.ChannelMount[j] = channel;

                           // XElement tag = new XElement("ac", new XAttribute("id", (CountTag++).ToString()),
                           //     new XAttribute("rawMIN", "0"), new XAttribute("rawMAX", "32768"),
                           //     new XAttribute("minEU", "0"), new XAttribute("maxEU", "100"),
                            //    new XAttribute("type", type[k] == 0 ? "0" : "10"), new XAttribute("namePLC", name),
                            //    new XAttribute("nameSCADA", name), new XAttribute("address", startAddress[k].ToString()),
                           //     new XAttribute("fTime", "0"));
                           // XElement ch = new XElement("adr", new XAttribute("address", startAddress[k].ToString()),
                           //     new XAttribute("cid", j.ToString()), new XAttribute("use_code", "0"));
                           // ch.Value = name;
                            startAddress[k] += 2;
                            configClass.SaveTag(atag);
                           // configACh.Add(tag);
                           // modul.Add(ch);
                          }
                        configClass.SaveModul(module);
                        CountModule++;
                    }
                }
            }
        }

        private string[] GetModuleData(string configData, string regexp, string nameGroup)
        {
            string[] result = null;
            Regex objReg = new Regex(regexp);
            MatchCollection matchResult = objReg.Matches(configData);
            if (matchResult.Count > 0)
            {
                
                    result = new string[matchResult.Count];
                    for (int i = 0; i < matchResult.Count; i++)
                    {
                        result[i] = matchResult[i].Groups[nameGroup].Value;                    
                    }
              
            }
            return result;
        }
        private string GetModuleDataOne(string configData, string regexp, string nameGroup)
        {
            string result = "";
            Regex objReg = new Regex(regexp);
            Match matchResult = objReg.Match(configData);
            if (matchResult.Success)
            {

                result = matchResult.Groups[nameGroup].Value;
            }
            return result;
        }
        private string GetModuleDataOne(string configData, string regexp)
        {
            string result = "";
            Match objReg = Regex.Match(configData, regexp);
            if(objReg.Success)
            {
                result = objReg.Value;
            }
            return result;
        }
        private void GetModulParam(string configData, ref int[] type, ref int[] startAddress, ref int[] channelCount)
        {
            string result = GetModuleDataOne(configData, _regexp_type_module, "typeModule");
            if (result == "")
                return;
            string[] modul = result.Replace("\\", "/").Split("/".ToCharArray());
            type = new int[modul.Length];
            startAddress = new int[modul.Length];
            channelCount = new int[modul.Length];
            string[] address = GetModuleData(configData, _regexp_address_channel, "address_channel");
            for (int i = 0; i < modul.Length; i++)
            {
                if (modul[i].IndexOf("AI") != -1)
                {
                    type[i] = 0;
                }
                if (modul[i].IndexOf("AO") != -1)
                {
                    type[i] = 1;
                }
                result = GetModuleDataOne(modul[i], "[0-9]+");
                if (result != "")
                {
                    try
                    {
                        channelCount[i] = Convert.ToInt32(result);
                    }
                    catch
                    {
                        channelCount[i] = -1;
                    }
                }
                if (address != null)
                {
                    string[] addressNumber = GetModuleData(address[i], "(?<address>[0-9]+)", "address");
                    if (addressNumber != null && addressNumber.Length > 0)
                    {
                        try
                        {
                            startAddress[i] = Convert.ToInt32(addressNumber[0]);
                        }
                        catch
                        {
                            startAddress[i] = -1;
                        }
                    }
                }
                else
                {
                    startAddress[i] = -1;
                }
            }
        }
        private void ParceConfigAsynkComplete(IAsyncResult iaResult)
        {
            int countModule = 0;
            int countTag = 0;
            AsyncResult ar = (AsyncResult)iaResult;
            ParceHarswareConfigAsynk gSt = (ParceHarswareConfigAsynk)ar.AsyncDelegate;
            gSt.EndInvoke(ref countModule, ref countTag, iaResult);
            if (ParceHarswareConfigComplete == null) return;
            int err = 0;
            if(countModule <= 0 || countTag <= 0)
                err = 1;
            ParceHarswareConfigEventArgs e = new ParceHarswareConfigEventArgs(countModule, countTag, err);
            ParceHarswareConfigComplete(this, e);
        }


    }
}
