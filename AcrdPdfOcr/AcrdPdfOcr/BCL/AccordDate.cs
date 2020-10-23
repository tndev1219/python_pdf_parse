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
    // this class extracts Date information from Accord Form 25
    public class AccordDate
    {
        public AccordDate(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            FindCoordinatesOfText();
        }

        #region Variable Declaration
        PdfReader reader;
        AccordForm aForm;
        public const string dateRegionDetectionText = "DATE (MM/DD/YYYY)"; // find text below this
        public const string dateRegionDetectionText_b = "DATE(MM/DD/YYYY)"; // find text below this

        public const string thisCertIsIssuedRegionDetectionText = "THIS CERTIFICATE IS ISSUED AS A MATTER OF INFORMATION ONLY AND CONFERS NO RIGHTS UPON THE CERTIFICATE HOLDER. THIS"; // find above this
        public const string certificateOfLiabilityRegionDetectionText = "CERTIFICATE OF LIABILITY INSURANCE"; // find above this
        public Rectangle accordDateDescription;
        public string accordDateText = "";
        #endregion

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates dateRegion = null;
            PdfWordCoordinates thisCertificate = null;
            PdfWordCoordinates certificateOfLiability = null;

            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            dateRegion = new UtilityFn().FindPdfWordCordinatesOfGivenTextContains(aForm.liLinesSorted, dateRegionDetectionText);

            if (dateRegion == null)
            {
                dateRegion = new UtilityFn().FindPdfWordCordinatesOfGivenTextContains(aForm.liLinesSorted, dateRegionDetectionText_b); // sujit

            }
            thisCertificate = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (aForm.liLinesSorted, thisCertIsIssuedRegionDetectionText);

            if((thisCertificate.XStartCoordinate > 0 ) && (thisCertificate.XEndCoordinate > 0) && (thisCertificate.YStartCoordinate > 0) && (thisCertificate.YEndCoordinate > 0))
            {
                aForm.ThisCertIsIssuesFoundAsText = true;
            }
            certificateOfLiability = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (aForm.liLinesSorted, certificateOfLiabilityRegionDetectionText);

            accordDateDescription = new Rectangle(certificateOfLiability.LiPdfCordinates[certificateOfLiability.LiPdfCordinates.Count - 1].RectInner.Right + aForm.constantNRulesObj.smallMoveRight, thisCertificate.RectOuter.Top + aForm.constantNRulesObj.smallMoveTop, aForm.constantNRulesObj.pdfFileWidth, dateRegion.RectOuter.Bottom - aForm.constantNRulesObj.smallMoveBottom);

            //rectProducer = new Rectangle(liProducer[0].rectInner.Left, liProducer[0].rectInner.Bottom - producerHeight, liProducer[0].rectInner.Left + producerWidht, liProducer[0].rectInner.Top);
            getInsuredTextFromRegion(accordDateDescription);
            return liProducer;
        }

   
        public void getInsuredTextFromRegion(Rectangle rect)
        {
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
            aForm.CertificateDate = result[0];
            aForm.acrdFrmForOcrObj.CertificateDate = result[1];
            aForm.acrdFrmForType3Obj.CertificateDate = result[2];
            // DoOcr(rect);
            //return result;
        }
    }
}


