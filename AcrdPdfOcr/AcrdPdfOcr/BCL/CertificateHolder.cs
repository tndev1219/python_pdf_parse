using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class CertificateHolder
    {
        #region Variable Declaration

        AccordForm aForm;
        PdfReader reader;
        public const string certificateHolderRegionDetectionText = "CERTIFICATE HOLDER";
        public const string accord25RegionDetectionText = "All rights reserved";
        public const string authRepRegionDetectionText = "AUTHORIZED REPRESENTATIVE";
        public Rectangle certificateHolderDescription;
        public string certificateHolderText = "";
        PdfWordCoordinates authRep = null;

        #endregion

        public CertificateHolder(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            FindCoordinatesOfText();
        }

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates certificateHolder = null;
            PdfWordCoordinates accord25 = null;
           
            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            certificateHolder = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, certificateHolderRegionDetectionText);

            accord25 = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (aForm.liLinesSorted, accord25RegionDetectionText);

            authRep = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, authRepRegionDetectionText);

            
            certificateHolderDescription = new Rectangle(certificateHolder.RectOuter.Left - aForm.constantNRulesObj.smallMoveLeft, accord25.YStartCoordinate + aForm.constantNRulesObj.smallMoveTop, authRep.RectOuter.Left - aForm.constantNRulesObj.smallMoveLeft , certificateHolder.RectOuter.Bottom - aForm.constantNRulesObj.smallMoveBottom);
            getInsuredTextFromRegion(certificateHolderDescription);
            return liProducer;
        }

       

        public void getInsuredTextFromRegion(Rectangle rect)
        {
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);

            StringBuilder sb = new StringBuilder();
            string aLine, aParagraph = null;
            StringReader strReader = new StringReader(result[0]);
            while (true)
            {
                aLine = strReader.ReadLine();
                if (aLine != null)
                    sb.Append(aLine + " ");
                else
                    break;

                //if (aLine != null)
                //{
                //    if (!(aLine.Any(char.IsDigit)))
                //    {
                //        sb.Append(aLine + " ");
                //    }
                //}
                //else
                //{
                //   // aParagraph = aParagraph + "\n";
                //    break;
                //}
            }

            string certHolder = "";
            //if(sb.ToString().ToUpper().Trim().Contains("INC"))
            //{
            //    certHolder = sb.ToString().Trim();
            //    //certHolder = certHolder.Substring(0, certHolder.IndexOf())
            //}
            //aForm.CertificateHolder = result[0];
            aForm.CertificateHolder = sb.ToString().Trim();

            aForm.acrdFrmForOcrObj.CertificateHolder = result[1];
            aForm.acrdFrmForType3Obj.CertificateHolder = result[2];
            // DoOcr(rect);
            //return result;
        }
    }
}


