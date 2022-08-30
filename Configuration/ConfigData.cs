using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxStore.DAL
{
    public class ConfigData
    {
        public int Expired { get; set; }
        public int MaxBoxes { get; set; }
        public int MinBoxes { get; set; }
        public double FirstRange { get; set; }
        public double SecondRange { get; set; }
        public double ThirdRange { get; set; }
    }
    public class Config
    {
        public static ConfigData Data { get; set; }
        static Config()
        {
            var currentDirectory= Environment.CurrentDirectory;
            var fileName = "jsconfig1.json";
            var configPath= Path.Combine(currentDirectory, fileName);
            var readText= File.ReadAllText(configPath);
            Data= JsonConvert.DeserializeObject<ConfigData>(readText);
        }
    }
}
