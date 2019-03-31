using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigFile
{
    public interface IConfigObject
    {
        /// <summary>
        /// Use default parameters to configure 
        /// </summary>
        void Fix();
    }
}
