using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    // currently this class is not in use, but it has been added
    // to improve the program further in future if required
    public class AccordRow
    {
        public int RowNum { get; set; }
        public float RowStartX { get; set; }
        public float RowEndX { get; set; }
        public float RowStartY { get; set; }
        public float RowEndY { get; set; }
        public float RowWidth { get; set; }
        public float RowHeight { get; set; }
    }
}
