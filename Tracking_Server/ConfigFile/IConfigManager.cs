using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile
{

    public interface IConfigManager
    {
        /// <summary>
        /// Read config file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        T Read<T>(string path) where T : IConfigObject;

        /// <summary>
        /// Write config file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        bool Write<T>(T obj, string path) where T : IConfigObject;
    }
}
