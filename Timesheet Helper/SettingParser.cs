using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using System.IO;

namespace TimesheetHelper
{
    static class SettingParser
    {
        private const string DEFAULT_FILE = "settings.json";

        public static void Save(Settings options, string fileName = DEFAULT_FILE)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
            FileStream fileStream = null;
            StreamWriter writer = null;
            
            try
            {
                fileStream = new FileStream(fileName, FileMode.Create);
                writer = new StreamWriter(fileStream);
                serializer.Serialize(writer, options);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // TODO(batuhan): Diagnostics.
            }
            finally
            {
                //fileStream?.Close();                
                writer?.Close();
            }
        }

        public static Settings Load(string fileName = DEFAULT_FILE)
        {
            Settings options = null;
            if (System.IO.File.Exists(fileName))
            {
                options = ParseOptions(fileName);
            }
            else
            {                
                options = Settings.InitialSettings();
                Save(options);
            }

            return options;
        }

        private static Settings ParseOptions(string fileName)
        {
            Settings options = new Settings();
            var deserializer = new Newtonsoft.Json.JsonSerializer();
            FileStream fileStream = null;
            StreamReader reader = null;

            try
            {
                fileStream = new FileStream(fileName, FileMode.Open);
                reader = new StreamReader(fileStream);
                options = (Settings)deserializer.Deserialize(reader, options.GetType());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                // NOTE(batuhan): Failed to parse? What to do?
                options = Settings.InitialSettings();               
            }
            finally
            {
                //fileStream?.Close();                
                reader?.Close();
            }

            return options;
        }
    }
}
