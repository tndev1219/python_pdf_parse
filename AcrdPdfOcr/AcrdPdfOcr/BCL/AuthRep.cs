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
    public class AuthRep
    {
        #region Variable Declaration
        PdfReader reader;
        AccordForm aForm;
        public const string authRepRegionDetectionText = "AUTHORIZED REPRESENTATIVE";
        public const string accordCorpRegionDetectionText = "ACORD CORPORATION. All rights reserved.";

        public Rectangle authRepDescription;
        public string authRepText = "";
        #endregion

        public AuthRep(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            FindCoordinatesOfText();
        }
      
        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            PdfWordCoordinates authRep = null;
            PdfWordCoordinates accordCorp = null;
           
            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            authRep = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, authRepRegionDetectionText);

            accordCorp = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (aForm.liLinesSorted, accordCorpRegionDetectionText);
          
            authRepDescription = new Rectangle(authRep.RectOuter.Left - aForm.constantNRulesObj.smallMoveLeft, accordCorp.RectOuter.Top , accordCorp.RectInner.Right, authRep.RectOuter.Top + aForm.constantNRulesObj.smallMoveBottom);
            getInsuredTextFromRegion(authRepDescription);
            return liProducer;
        }

       

        public void getInsuredTextFromRegion(Rectangle rect)
        {
            
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm, true);
            result[0] = result[0].Replace("AUTHORIZED REPRESENTATIVE", "");
            aForm.AuthRep = result[0];
            aForm.acrdFrmForOcrObj.AuthRep = result[1];
            aForm.acrdFrmForType3Obj.AuthRep = result[2];
            //DoOcr(rect);
            // return result;
        }
      
    }
}


