using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace AccordCL.BCL
{
    public class Insured
    {
        #region Variable Declaration
        AccordForm aForm;
        PdfReader reader;
        public const string insuredRegionDetectionText = "INSURED";
        public const string coveragesRegionDetectionText = "COVERAGES";
        public const string coveragesRegionDetectionText1 = "CERTIFICATE NUMBER:";
        public const string contactRegionDetectionText = "CONTACT";
        public Rectangle rectInsurer;
        public string insurerText = "";
        #endregion

        public Insured(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            FindCoordinatesOfText();
        }

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates insured = null;
            PdfWordCoordinates coverages = null;
            PdfWordCoordinates contact = null;
            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            insured = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, insuredRegionDetectionText);

            coverages = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, coveragesRegionDetectionText);

            if (coverages == null)
            {
                coverages = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, coveragesRegionDetectionText1);
            }

            contact = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, contactRegionDetectionText);

            rectInsurer = new Rectangle(insured.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft, coverages.RectOuter.Top + aForm.constantNRulesObj.smallMoveTop, contact.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft, insured.RectOuter.Top + aForm.constantNRulesObj.smallMoveTop);
            getInsuredTextFromRegion(rectInsurer);
            return liProducer;
        }

     

        public void getInsuredTextFromRegion(Rectangle rect)
        {
            //string result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, aForm.constantNRulesObj.tableStartXcoordinate - 2.0f, rect.Bottom, rect.Right, rect.Top, aForm);

            //result = result.TrimStart("INSURED".ToCharArray()).Trim();
            result[0] = result[0].Trim();
            StringBuilder sb = new StringBuilder();
            string r = "";
            foreach (string str in result[0].Split('\n'))
            {
                r = str.Trim().Replace("INSURED","");
                if (r != "INSURED")
                    if (r != "")
                        sb.AppendLine(r);
            }
            aForm.Insured = sb.ToString();
            aForm.acrdFrmForOcrObj.Insured = result[1];
            aForm.acrdFrmForType3Obj.Insured = result[2];

            // DoOcr(rect);
            // return result;

        }  
    }
}


