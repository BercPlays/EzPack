using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzPack.HelperClasses
{
    static class MCMETA
    {
        public static string GetDescription(string dir)
        {
            using (StreamReader sr = new StreamReader(dir))
            {
                Root root = JsonConvert.DeserializeObject<Root>(sr.ReadToEnd());
                sr.Close();
                return root.Pack.Description;
            }
            
        }
        public static int GetVersion(string dir)
        {
            using (StreamReader sr = new StreamReader(dir))
            {
                Root root = JsonConvert.DeserializeObject<Root>(sr.ReadToEnd());
                sr.Close();
                return root.Pack.PackFormat;
            }

        }
        public class Pack
        {
            [JsonProperty("pack_format")]
            public int PackFormat { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }

        public class Root
        {
            [JsonProperty("pack")]
            public Pack Pack { get; set; }
        }
    }
}
