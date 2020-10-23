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
    public class Description
    {
        #region Variable Declaration
        AccordForm aForm;
        PdfReader reader;
        public const string descriptionRegionDetectionText = "DESCRIPTION OF OPERATIONS / LOCATIONS / VEHICLES";
        public const string certificateOfHolderRegionDetectionText = "CERTIFICATE HOLDER";
        public float descriptionWidth;
        public Rectangle rectDescription;
        public string descriptionText = "";
        #endregion

        public Description(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            descriptionWidth = aForm.constantNRulesObj.tableWidth;
            FindCoordinatesOfText();
        }

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates description = null;
            PdfWordCoordinates certificateHolder = null;
           
            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            description = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (aForm.liLinesSorted, descriptionRegionDetectionText);

            certificateHolder = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, certificateOfHolderRegionDetectionText);

            rectDescription = new Rectangle(description.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft -5, certificateHolder.YStartCoordinate + aForm.constantNRulesObj.smallMoveTop, description.XStartCoordinate + descriptionWidth, description.YStartCoordinate - aForm.constantNRulesObj.smallMoveBottom);
            getInsuredTextFromRegion(rectDescription);
            return liProducer;
        }

      

        public void getInsuredTextFromRegion(Rectangle rect)
        {
            //string result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
            aForm.DescriptionOfOperations = result[0];
            aForm.acrdFrmForOcrObj.DescriptionOfOperations = result[1];
            aForm.acrdFrmForType3Obj.DescriptionOfOperations = result[2];
            //DoOcr(rect);
            //return result;

        }
    }
}


