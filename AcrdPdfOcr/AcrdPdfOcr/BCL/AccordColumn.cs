using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    // currently this class is not in use, but it has been added
    // to improve the program further in future if required
    public class AccordColumn
    {
        public int   ColumnNum { get; set; }
        public float ColumnStartX { get; set; }
        public float ColumnEndX { get; set; }
        public float ColumnStartY { get; set; }
        public float ColumnEndY { get; set; }
        public float ColumnWidth { get; set; }
        public float ColumnHeight { get; set; }
    }
}
