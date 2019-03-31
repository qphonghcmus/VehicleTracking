using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConfigFile
{
    [Export(typeof(IConfigManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ConfigManager : IConfigManager
    {

        public T Read<T>(string path) where T : IConfigObject
        {
            try
            {
                if (!File.Exists(path))
                {
                    var obj = Activator.CreateInstance<T>();
                    Write(obj, path);
                    return obj;
                }

                var fStream = File.Open(path, FileMode.Open);
                var reader = XmlReader.Create(fStream);
                var serialize = new DataContractSerializer(typeof(T));
                var config = (T)serialize.ReadObject(reader);
                reader.Dispose();
                fStream.Dispose();
                return config;
            }
            catch
            {

            }
            // return null
            return default(T);
        }

        public bool Write<T>(T obj, string path) where T : IConfigObject
        {
            try
            {
                if (obj == null) return false;
                if (File.Exists(path))
                    File.Delete(path);
                obj.Fix();

                var fStream = File.Create(path);
                var setting = new XmlWriterSettings()
                {
                    Indent = true, IndentChars = "\t"
                };

                var writer = XmlWriter.Create(fStream, setting);
                var serialize = new DataContractSerializer(obj.GetType());
                serialize.WriteObject(writer, obj);
                writer.Flush();
                writer.Dispose();
                fStream.Dispose();
                return true;
            }
            catch
            {

            }
            return false;
        }
    }
}
