using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class InsurerAtoFandNaic_ContactDetails
    {
        #region Variable Declaration
        AccordForm aForm;
        PdfReader reader;
        public const string contactRegionDetectionText = "CONTACT";
        public const string insurerFRegionDetectionText = "INSURER F :";
        public const string insurerFRegionDetectionText_b = "INSURER F:";
        public const string PhoneActNoRegionDetectionText = "(A/C, NO, EXT):";
        public const string FaxActNoRegionDetectionText = "(A/C, NO):";
        public const string PhoneActNoRegionDetectionText_b = "(A/C. NO. EXT):"; // sujit
        public const string FaxActNoRegionDetectionText_b = "(A/C. NO.):"; // sujit

        public const string EmailRegionDetectionText = "ADDRESS:";
        public float contactEmailWidth;
        public float phoneWidth;
        public float faxWidth;
        public float insurerAWidth;
        public float insurerAnaicWidth;
        public float rowHeight;
        public Rectangle rectContact;
        public Rectangle rectPhone;
        public Rectangle rectEmail;
        public Rectangle rectFax;

        public Rectangle rectInsurerA;
        public Rectangle rectInsurerB;
        public Rectangle rectInsurerC;
        public Rectangle rectInsurerD;
        public Rectangle rectInsurerE;
        public Rectangle rectInsurerF;

        public Rectangle rectInsurerNaicA;
        public Rectangle rectInsurerNaicB;
        public Rectangle rectInsurerNaicC;
        public Rectangle rectInsurerNaicD;
        public Rectangle rectInsurerNaicE;
        public Rectangle rectInsurerNaicF;

        public string contact = "";
        public string phone = "";
        public string fax = "";
        public string email = "";
        public string insurerA = "";
        public string insurerB = "";
        public string insurerC = "";
        public string insurerD = "";
        public string insurerE = "";
        public string insurerF = "";

        public string insurerAnaic = "";
        public string insurerBnaic = "";
        public string insurerCnaic = "";
        public string insurerDnaic = "";
        public string insurerEnaic = "";
        public string insurerFnaic = "";
        #endregion

        public InsurerAtoFandNaic_ContactDetails(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            contactEmailWidth =  aForm.constantNRulesObj.tableWidth / 2f;
            phoneWidth = aForm.PhoneWidthRatio * aForm.constantNRulesObj.tableWidth; 
            faxWidth = aForm.FaxWidthRatio * aForm.constantNRulesObj.tableWidth; ;
            insurerAWidth = aForm.InsurerANameWidthRatio * aForm.constantNRulesObj.tableWidth; 
            insurerAnaicWidth = aForm.InsurerANameNaicWidthRatio * aForm.constantNRulesObj.tableWidth;                // insurerAnaicXstartFromEnd = 65;
            FindCoordinatesOfText();
        }

        public void ReadContactPhoneEmailFax()
        {

            string contactRegionDetectionText = "CONTACT";
            string InsurerRegionDetectionText = "INSURER(S) AFFORDING COVERAGE";


            PdfWordCoordinates pdfWcContact;
            PdfWordCoordinates pdfWcInsurer;

          
            pdfWcContact = new UtilityFn().FindPdfWordCordinatesOfGivenTextEquals
               (aForm.liLinesSorted, contactRegionDetectionText);

            pdfWcInsurer = new UtilityFn().FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, InsurerRegionDetectionText);

          Rectangle  RectCol8 = new Rectangle(pdfWcContact.RectOuter.Left, pdfWcInsurer.RectOuter.Top, aForm.constantNRulesObj.pdfFileWidth, pdfWcContact.RectOuter.Top);
            // RectCol8 = new Rectangle(pdfWcmmDDyyyy.RectOuter.Right, pdfWcdescOfOperations.RectOuter.Top, 500, pdfWcmmDDyyyy.RectOuter.Bottom);
            var result = new UtilityFn().ExtractTextFromRegionForCol8(reader, RectCol8, aForm, OcrTypes.SingleBlock);


            float llx, lly, urx, ury;
            llx = 0f;
            lly = 0f;
            urx = 0f;
            ury = 0f;
            // contact name
            foreach (var v in result)
            {
                if (v.Word.Trim() == "CONTACT")
                {
                    llx = v.RectOuter.Right;
                    ury = v.RectOuter.Top;
                }

                if (v.Word.Trim() == "PHONE")
                {
                    lly = v.RectOuter.Top;
                }
            }

            urx = aForm.constantNRulesObj.pdfFileWidth;

            Rectangle  rectContact = new Rectangle(llx, lly, urx, ury);
            string[] resultOutput = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            aForm.ContactName = resultOutput[0];
            contact = resultOutput[0];

            foreach (var v in result)
            {
                if (v.Word.Trim() == "(A/C, No, Ext):")
                {
                    lly = v.RectOuter.Bottom;
                    llx = v.RectOuter.Right;
                }

                if (v.Word.Trim() == "FAX")
                {
                    ury = v.RectOuter.Top;
                    urx = v.RectOuter.Left;
                }
            }

            Rectangle rectPhone = new Rectangle(llx, lly, urx, ury);
             resultOutput = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
          aForm.ContactPhone = resultOutput[0];
            phone = resultOutput[0];
            foreach (var v in result)
            {
                if (v.Word.Trim() == "FAX")
                {
                    ury = v.RectOuter.Top;
                    
                }

                if (v.Word.Trim() == "(A/C, No):")
                {
                    llx = v.RectOuter.Right;
                    lly = v.RectOuter.Bottom;
                }
            }

            urx = aForm.constantNRulesObj.pdfFileWidth;
            // phone no
            Rectangle rectFax = new Rectangle(llx, lly, urx, ury);
            resultOutput = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            aForm.ContactFax = resultOutput[0];
            fax = resultOutput[0];

            foreach (var v in result)
            {
                if (v.Word.Trim() == "E-MAIL")
                {
                    ury = v.RectOuter.Top;

                }

                if (v.Word.Trim() == "ADDRESS:")
                {
                    llx = v.RectOuter.Right;
                    lly = v.RectOuter.Bottom;
                }

               
            }

            urx = aForm.constantNRulesObj.pdfFileWidth;
            // phone no
            Rectangle rectEmail = new Rectangle(llx, lly, urx, ury);
            resultOutput = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            aForm.ContactEmail = resultOutput[0];
            email = resultOutput[0];

        }
        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            ReadContactPhoneEmailFax();

            PdfWordCoordinates contact = null;
            PdfWordCoordinates insurerF = null;
            PdfWordCoordinates phoneActNo = null;
            PdfWordCoordinates faxActNo = null;
            PdfWordCoordinates email = null;

            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            contact = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, contactRegionDetectionText);

            insurerF = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, insurerFRegionDetectionText);

            if (insurerF == null) // sujit
            {
                insurerF = new UtilityFn().
                   FindPdfWordCordinatesOfGivenTextEquals
                   (aForm.liLinesSorted, insurerFRegionDetectionText_b);
            }

            phoneActNo = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, PhoneActNoRegionDetectionText);

            if (phoneActNo == null)
            {
                phoneActNo = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, PhoneActNoRegionDetectionText_b);
            }

            faxActNo = new UtilityFn().
                 FindPdfWordCordinatesOfGivenTextEquals
                 (aForm.liLinesSorted, FaxActNoRegionDetectionText);

            if (faxActNo == null)
            {
                faxActNo = new UtilityFn().
                 FindPdfWordCordinatesOfGivenTextEquals
                 (aForm.liLinesSorted, FaxActNoRegionDetectionText_b);
            }

            email = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, EmailRegionDetectionText);

            float top = contact.RectInner.Top + aForm.constantNRulesObj.smallMoveTop;
            float bottom = insurerF.RectOuter.Bottom - aForm.constantNRulesObj.smallMoveBottom;

            float noOfRows = 10f;
            float sizeOfEachRow = (top - bottom) / noOfRows;

            float llx, lly, urx, ury = 0f;

            float llxNaic, urxNaic = 0f;
            llxNaic = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth -2;
            urxNaic = llxNaic + insurerAnaicWidth + 2 ;

            // contact
            llx = contact.RectOuter.Right + aForm.constantNRulesObj.smallMoveRight;
            lly = contact.RectOuter.Top - sizeOfEachRow;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;
            ury = contact.RectOuter.Top;

            rectContact = new Rectangle(llx, lly, urx, ury);
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
           // this.contact = result[0];
            //aForm.ContactName = this.contact;
            //aForm.acrdFrmForOcrObj.ContactName = result[1];
            //aForm.acrdFrmForType3Obj.ContactName = result[2];

            //phone
            llx = phoneActNo.RectOuter.Right + aForm.constantNRulesObj.smallMoveRight;
            lly = lly - sizeOfEachRow;
            lly = phoneActNo.RectOuter.Bottom; // - sizeOfEachRow;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;
            ury = ury - sizeOfEachRow;
            ury = phoneActNo.RectOuter.Top + sizeOfEachRow;

            rectPhone = new Rectangle(llx, lly, faxActNo.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, faxActNo.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft, ury, aForm);
           // phone = result[0];
            //aForm.ContactPhone = phone;
            //aForm.acrdFrmForOcrObj.ContactPhone = result[1];
            //aForm.acrdFrmForType3Obj.ContactPhone = result[2];

            // fax
            llx = faxActNo.RectOuter.Right + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;

            rectFax = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            //fax = result[0];
            //aForm.ContactFax = fax;
            //aForm.acrdFrmForOcrObj.ContactFax = result[1];
            //aForm.acrdFrmForType3Obj.ContactFax = result[2];

            // Email
            llx = email.RectOuter.Right + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth; ;

            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;
            rectEmail = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            //this.email = result[0];
            //aForm.ContactEmail = this.email;
            //aForm.acrdFrmForOcrObj.ContactEmail = result[1];
            //aForm.acrdFrmForType3Obj.ContactEmail = result[2];

            // one blank row, skip it
            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;


            // sujit Re-caliberate here
            // take INSURED as Top, number of rows as 6 and BOTTOM as it is

            string insuredRegionDetectionText = "INSURER A :";
            string insuredRegionDetectionText_b = "INSURER A:";

            PdfWordCoordinates insured = new UtilityFn().
               FindPdfWordCordinatesOfGivenTextEquals
               (aForm.liLinesSorted, insuredRegionDetectionText);

            if (insured == null)
                insured = new UtilityFn().
              FindPdfWordCordinatesOfGivenTextEquals
              (aForm.liLinesSorted, insuredRegionDetectionText_b);
            // Insurer A
            // llx = insurerF.rectOuter.Right + aForm.constantNRulesObj.smallMoveRight;
            llx = insured.RectOuter.Left - aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth;


           // .RectInner.Top + aForm.constantNRulesObj.smallMoveTop;
           // float bottom = insurerF.RectOuter.Bottom - aForm.constantNRulesObj.smallMoveBottom;



            sizeOfEachRow = ((insured.RectInner.Top - aForm.constantNRulesObj.smallMoveTop)  - bottom) / 6;


            lly = insured.RectOuter.Top - sizeOfEachRow;
            ury = insured.RectOuter.Top;


            //lly = lly - sizeOfEachRow;
            //ury = ury - sizeOfEachRow;

            rectInsurerA = new Rectangle(llx, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx -aForm.constantNRulesObj.smallMoveLeft, ury, aForm);
            insurerA = result[0];
            insurerA = insurerA.Replace("INSURER A :", "").Replace("INSURER A:", "").Trim();
            aForm.InsurerAName = insurerA.Trim();
            aForm.acrdFrmForOcrObj.InsurerAName = result[1];
            aForm.acrdFrmForType3Obj.InsurerAName = result[2];
            // Insurer A NAIC
            llx = urx + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;

            //rectInsurerNaicA = new Rectangle(llx, lly, urx, ury);
            rectInsurerNaicA = new Rectangle(llxNaic, lly, urxNaic, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llxNaic, lly, urxNaic, ury, aForm);
            insurerAnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]); //Regex.Replace(result[0], "[^0-9.]", "");

            if((insurerA.Trim() != "") && (insurerAnaic.Trim() == ""))
            {
                rectInsurerNaicA = new Rectangle((aForm.constantNRulesObj.pdfFileWidth / 2f), lly, urxNaic, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectInsurerNaicA.Left, lly, urxNaic, ury, aForm);
                insurerAnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]);  //Regex.Replace(result[0], "[^0-9.]", "");

            }

            aForm.InsurerANaic = insurerAnaic;
            aForm.acrdFrmForOcrObj.InsurerANaic = result[1];
            aForm.acrdFrmForType3Obj.InsurerANaic = result[2];

            // Insurer B
            llx = insured.RectOuter.Left - aForm.constantNRulesObj.smallMoveRight;// (aForm.constantNRulesObj.pdfFileWidth / 2f) - aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth ;

            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;

            rectInsurerB = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            insurerB = result[0];
            insurerB = insurerB.Replace("INSURER B :", "").Replace("INSURER B:", "").Trim();
            aForm.InsurerBName = insurerB.Trim();
            aForm.acrdFrmForOcrObj.InsurerBName = result[1];
            aForm.acrdFrmForType3Obj.InsurerBName = result[2];

            // Insurer B NAIC
            llx = urx + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;

            //rectInsurerNaicB = new Rectangle(llx, lly, urx, ury);
            rectInsurerNaicB = new Rectangle(llxNaic, lly, urxNaic, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llxNaic, lly, urxNaic, ury, aForm);
            insurerBnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]);  //Regex.Replace(result[0], "[^0-9.]", ""); //result[0];

            if ((insurerB.Trim() != "") && (insurerBnaic.Trim() == ""))
            {
                rectInsurerNaicB = new Rectangle((aForm.constantNRulesObj.pdfFileWidth / 2f), lly, urxNaic, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectInsurerNaicB.Left, lly, urxNaic, ury, aForm);
                insurerBnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]); //Regex.Replace(result[0], "[^0-9.]", "");

            }

            aForm.InsurerBNaic = insurerBnaic;
            aForm.acrdFrmForOcrObj.InsurerBNaic = result[1];
            aForm.acrdFrmForType3Obj.InsurerBNaic = result[2];

            // Insurer C
            llx = insured.RectOuter.Left - aForm.constantNRulesObj.smallMoveRight;// (aForm.constantNRulesObj.pdfFileWidth / 2f) - aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth;

            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;

            rectInsurerC = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            insurerC = result[0];
            insurerC = insurerC.Replace("INSURER C :", "").Replace("INSURER C:", "").Trim();
            aForm.InsurerCName = insurerC.Trim();
            aForm.acrdFrmForOcrObj.InsurerCName = result[1];
            aForm.acrdFrmForType3Obj.InsurerCName = result[2];

            // Insurer C NAIC
            llx = urx + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;

            rectInsurerNaicC = new Rectangle(llxNaic, lly, urxNaic, ury);
            //rectInsurerNaicC = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llxNaic, lly, urxNaic, ury, aForm);
            insurerCnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]); // result[0];

            if ((insurerC.Trim() != "") && (insurerCnaic.Trim() == ""))
            {
                rectInsurerNaicC = new Rectangle((aForm.constantNRulesObj.pdfFileWidth / 2f), lly, urxNaic, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectInsurerNaicC.Left, lly, urxNaic, ury, aForm);
                insurerCnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]);  //Regex.Replace(result[0], "[^0-9.]", "");

            }

            aForm.InsurerCNaic = insurerCnaic;
            aForm.acrdFrmForOcrObj.InsurerCNaic = result[1];
            aForm.acrdFrmForType3Obj.InsurerCNaic = result[2];

            // Insurer D
            llx = insured.RectOuter.Left - aForm.constantNRulesObj.smallMoveRight;// (aForm.constantNRulesObj.pdfFileWidth / 2f) - aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth;

            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;

            rectInsurerD = new Rectangle(llx, lly, urx, ury);
           

            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            insurerD = result[0];

            insurerD = insurerD.Replace("INSURER D :", "").Replace("INSURER D:", "").Trim();
            aForm.InsurerDName = insurerD.Trim();
            aForm.acrdFrmForOcrObj.InsurerDName = result[1];
            aForm.acrdFrmForType3Obj.InsurerDName = result[2];

            // Insurer D NAIC
            llx = urx + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;

            rectInsurerNaicD = new Rectangle(llxNaic, lly, urxNaic, ury);
            //rectInsurerNaicD = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llxNaic, lly, urxNaic, ury, aForm);
            insurerDnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]); // Regex.Replace(result[0], "[^0-9.]", "");  //result[0];

            if ((insurerD.Trim() != "") && (insurerDnaic.Trim() == ""))
            {
                rectInsurerNaicD = new Rectangle((aForm.constantNRulesObj.pdfFileWidth / 2f), lly, urxNaic, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectInsurerNaicD.Left, lly, urxNaic, ury, aForm);
                insurerDnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]);  //Regex.Replace(result[0], "[^0-9.]", "");

            }

            aForm.InsurerDNaic = insurerDnaic;
            aForm.acrdFrmForOcrObj.InsurerDNaic = result[1];
            aForm.acrdFrmForType3Obj.InsurerDNaic = result[2];

            // Insurer E
            llx = insured.RectOuter.Left - aForm.constantNRulesObj.smallMoveRight;// (aForm.constantNRulesObj.pdfFileWidth / 2f) - aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth;

            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;

            rectInsurerE = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            insurerE = result[0];
            insurerE = insurerE.Replace("INSURER E :", "").Replace("INSURER E:", "").Replace("INSURER D :", "").Replace("INSURER D:", "").Trim();
            aForm.InsurerEName = insurerE.Trim();
            aForm.acrdFrmForOcrObj.InsurerEName = result[1];
            aForm.acrdFrmForType3Obj.InsurerEName = result[2];

            // Insurer E NAIC
            llx = urx + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth+  insurerAnaicWidth;

            rectInsurerNaicE = new Rectangle(llxNaic, lly, urxNaic, ury);
            //rectInsurerNaicE = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llxNaic, lly, urxNaic, ury, aForm);
            insurerEnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]);// Regex.Replace(result[0], "[^0-9.]", "");  //result[0];

            if ((insurerE.Trim() != "") && (insurerAnaic.Trim() == ""))
            {
                rectInsurerNaicE = new Rectangle((aForm.constantNRulesObj.pdfFileWidth / 2f), lly, urxNaic, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectInsurerNaicE.Left, lly, urxNaic, ury, aForm);
                insurerEnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]); //Regex.Replace(result[0], "[^0-9.]", "");

            }
            aForm.InsurerENaic = insurerEnaic;
            aForm.acrdFrmForOcrObj.InsurerENaic = result[1];
            aForm.acrdFrmForType3Obj.InsurerENaic = result[2];

            // Insurer F
            llx = insured.RectOuter.Left - aForm.constantNRulesObj.smallMoveRight;// (aForm.constantNRulesObj.pdfFileWidth / 2f) - aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth;

            lly = lly - sizeOfEachRow;
            ury = ury - sizeOfEachRow;

            rectInsurerF = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llx, lly, urx, ury, aForm);
            this.insurerF = result[0];
            this.insurerF = this.insurerF.Replace("INSURER F :", "").Replace("INSURER F:", "").Trim();
            aForm.InsurerFName = this.insurerF.Trim();
            aForm.acrdFrmForOcrObj.InsurerFName = result[1];
            aForm.acrdFrmForType3Obj.InsurerFName = result[2];

            // Insurer F NAIC
            llx = urx + aForm.constantNRulesObj.smallMoveRight;
            urx = (aForm.constantNRulesObj.pdfFileWidth / 2f) + insurerAWidth + insurerAnaicWidth;

            rectInsurerNaicF = new Rectangle(llxNaic, lly, urxNaic, ury);
            //rectInsurerNaicE = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, llxNaic, lly, urxNaic, ury, aForm);
            insurerFnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]); // Regex.Replace(result[0], "[^0-9.]", "");  //result[0];

            if ((insurerE.Trim() != "") && (insurerAnaic.Trim() == ""))
            {
                rectInsurerNaicF = new Rectangle((aForm.constantNRulesObj.pdfFileWidth / 2f), lly, urxNaic, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectInsurerNaicF.Left, lly, urxNaic, ury, aForm);
                insurerFnaic = new UtilityFn().RemoveDigitsFromStartOnly(result[0]);  //Regex.Replace(result[0], "[^0-9.]", "");

            }


            aForm.InsurerFNaic = insurerFnaic;
            aForm.acrdFrmForOcrObj.InsurerFNaic = result[1];
            aForm.acrdFrmForType3Obj.InsurerFNaic = result[2];

            return liProducer;
        }

        //public string getInsuredTextFromRegion(Rectangle rect)
        //{
        //    string result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
        //    return result;
           
        //}
    }
}



