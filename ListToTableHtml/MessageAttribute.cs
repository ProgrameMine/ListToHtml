using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListToTableHtml
{
    public class MessageAttribute : Attribute
    {
        public string ColName { get; set; }

        public string Unit { get; set; }

        public MessageAttribute(string colName)
        {
            ColName = colName;
        }

        public MessageAttribute(string colName, string unit)
        {
            ColName = colName;
            Unit = unit;
        }
    }
}
