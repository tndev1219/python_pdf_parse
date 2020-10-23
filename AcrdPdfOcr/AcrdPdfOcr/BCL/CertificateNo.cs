using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AccordCL.BCL
{
    public class CertificateNo
    {
        #region Variable Declaration
        AccordForm aForm;
        PdfReader reader;
        public const string certificateNoRegionDetectionText = "CERTIFICATE NUMBER:"; // find text below this
        public const string revisionNoRegionDetectionText = "REVISION NUMBER:"; // find above this
        public const string thisIsToCertifyRegionDetectionText = "THIS IS TO CERTIFY THAT THE POLICIES OF INSURANCE LISTED BELOW HAVE BEEN ISSUED TO THE INSURED NAMED ABOVE FOR THE POLICY PERIOD"; // find above this
        public Rectangle rectCertNo;
        public string certNoText = "";
        #endregion

        public CertificateNo(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            FindCoordinatesOfText();
        }

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates  pdfWCCertificateNo = null;
            PdfWordCoordinates pdfWCRevisionNo = null;
            PdfWordCoordinates pdfWCthisIsToCertify = null;

            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            pdfWCCertificateNo = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, certificateNoRegionDetectionText);

            pdfWCRevisionNo = new UtilityFn().
                 FindPdfWordCordinatesOfGivenTextEquals
                 (aForm.liLinesSorted, revisionNoRegionDetectionText);


            pdfWCthisIsToCertify = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (aForm.liLinesSorted, thisIsToCertifyRegionDetectionText);
            
            // check whether PRODUCER co-ordinates satisfiy rules or not
            rectCertNo = new Rectangle(pdfWCCertificateNo.LiPdfCordinates[pdfWCCertificateNo.LiPdfCordinates.Count - 1].RectInner.Right + aForm.constantNRulesObj.smallMoveRight, pdfWCthisIsToCertify.RectOuter.Top + aForm.constantNRulesObj.smallMoveTop, pdfWCRevisionNo.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft, pdfWCCertificateNo.LiPdfCordinates[pdfWCCertificateNo.LiPdfCordinates.Count - 1].RectInner.Top + aForm.constantNRulesObj.smallMoveTop);
            getInsuredTextFromRegion(rectCertNo);
            return liProducer;
        }
       
        public void getInsuredTextFromRegion(Rectangle rect)
        {
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
            aForm.CertificateNo = result[0];
            aForm.acrdFrmForOcrObj.CertificateNo = result[1];
            aForm.acrdFrmForType3Obj.CertificateNo = result[2];

            //DoOcr(rect);
            //return result;
        }
    }
}


