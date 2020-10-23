using iTextSharp.text;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class PdfWordCoordinates : IComparable<PdfWordCoordinates>
    {
        #region Variable Declaration
        public int SerialNo { get; set; }
        public List<string> LiWords { get; set; }
        public List<PdfWordCoordinates> LiPdfCordinates { get; set; }
        public string Word { get; set; }
        public float XStartCoordinate { get; set; }
        public float XEndCoordinate { get; set; }
        public float YStartCoordinate { get; set; }
        public float YEndCoordinate { get; set; }
        public int NoOfCharsInWord { get; set; }
        public float LengthOfWord { get; set; }
        public Rectangle RectInner { get; set; }
        public Rectangle RectOuter { get; set; }
        public Rectangle RectManual { get; set; }
        public float CharacterSpacing { get; set; }
        public Vector VectorStart { get; set; }
        public Vector VectorEnd { get; set; }
        #endregion

        public PdfWordCoordinates()
        {
            LiWords = new List<string>();
            LiPdfCordinates = new List<PdfWordCoordinates>();
        }
        public int CompareTo(PdfWordCoordinates other)
        {
            return XStartCoordinate.CompareTo(other.XStartCoordinate);
        }

        public int CompareTo(PdfWordCoordinates other, bool byYcordinate)
        {
            return YStartCoordinate.CompareTo(other.YStartCoordinate);
        }
    }
}
