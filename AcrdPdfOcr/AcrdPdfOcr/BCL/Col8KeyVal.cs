using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class Col8KeyVal
    {
        public Col8KeyVal(string key, string val)
        {
            keyVal = key;
            valVal = val;
        }
        public string keyVal { get; set; }
        public string valVal { get; set; }
    }
}
