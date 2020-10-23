using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordForm25.BCL
{
    public class pdfWordCordinatesUnsorted : IComparable<pdfWordCordinatesUnsorted>
    {
        public int serialNo { get; set; }
        public List<string> liWords { get; set; }
        public string word { get; set; }
        public float xCord { get; set; }
        public float yCord { get; set; }
        //public float StartXCord { get; set; }
        //public float StartYCord { get; set; }

            public pdfWordCordinatesUnsorted()
        {
            liWords = new List<string>();
        }

        public int CompareTo(pdfWordCordinatesUnsorted other)
        {
            return xCord.CompareTo(other.xCord);

        }

        public int CompareTo(pdfWordCordinatesUnsorted other, bool byYcordinate)
        {
            return yCord.CompareTo(other.yCord);

        }
    }
}
