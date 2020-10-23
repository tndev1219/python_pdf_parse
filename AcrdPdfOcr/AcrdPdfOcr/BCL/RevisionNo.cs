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
    public class RevisionNo
    {
        #region Variable Declaration
        AccordForm aForm;
        PdfReader reader;
        public const string revisionNoRegionDetectionText = "REVISION NUMBER:"; // find text below this
        public const string thisIsToCertifyRegionDetectionText = "THIS IS TO CERTIFY THAT THE POLICIES OF INSURANCE LISTED BELOW HAVE BEEN ISSUED TO THE INSURED NAMED ABOVE FOR THE POLICY PERIOD"; // find above this
        public Rectangle rectRevNo;
        public string revNoText = "";
        #endregion

        public RevisionNo(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            FindCoordinatesOfText();
        }

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates  pdfWCRevisionNo = null;
            PdfWordCoordinates pdfWCthisIsToCertify = null;

            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            pdfWCRevisionNo = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, revisionNoRegionDetectionText);

            pdfWCthisIsToCertify = new UtilityFn().
                 FindPdfWordCordinatesOfGivenTextContains
                 (aForm.liLinesSorted, thisIsToCertifyRegionDetectionText);

            rectRevNo = new Rectangle(pdfWCRevisionNo.LiPdfCordinates[pdfWCRevisionNo.LiPdfCordinates.Count - 1].RectInner.Right + aForm.constantNRulesObj.smallMoveRight, pdfWCthisIsToCertify.YStartCoordinate + aForm.constantNRulesObj.smallMoveTop,  aForm.constantNRulesObj.pdfFileWidth - aForm.constantNRulesObj.smallMoveLeft, pdfWCRevisionNo.LiPdfCordinates[pdfWCRevisionNo.LiPdfCordinates.Count - 1].RectInner.Top + aForm.constantNRulesObj.smallMoveTop);
            getInsuredTextFromRegion(rectRevNo);
            return liProducer;
        }

       

        public void getInsuredTextFromRegion(Rectangle rect)
        {
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
            aForm.RevisionNo = result[0];
            aForm.acrdFrmForOcrObj.RevisionNo = result[1];
            aForm.acrdFrmForType3Obj.RevisionNo = result[2];

            //DoOcr(rect);
            //return result;
        }
    }
}


