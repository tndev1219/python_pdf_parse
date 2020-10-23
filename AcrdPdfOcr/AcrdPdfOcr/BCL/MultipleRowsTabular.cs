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
    public class MultipleRowsTabular
    {

        #region Variable Declaration
        AccordForm aForm;
        public float row1Height;
        public float row2Height;
        public float row3Height;
        public float row4Height;
        public float row5Height;

        public float col1Width;
        public float col2Width;
        public float col3Width;
        public float col4Width;
        public float col5Width;
        public float col6Width;
        public float col7Width;
        public float col8_1Width;
        public float col8_2Width;

        public int totalRowsInRow1 = 7;
        public int totalRowsInRow2 = 5;
        public int totalRowsInRow3 = 3;
        public int totalRowsInRow4 = 4;
        public int totalRowsInRow5 = 7;

        PdfReader reader;
        public const string mmDDyyyyRegionDetectionText = "(MM/DD/YYYY) (MM/DD/YYYY)";
        public const string insrRegionDetectionText = "INSR";
        public const string descOfOperationsRegionDetectionText =   "DESCRIPTION OF OPERATIONS / LOCATIONS / VEHICLES  (Attach ACORD 101, Additional Remarks Schedule, if more space is required)";
        public const string descOfOperationsRegionDetectionText_b = "DESCRIPTION OF OPERATIONS / LOCATIONS / VEHICLES (ACORD 101, Additional Remarks Schedule, may be attached if more space is required)";
        public const string descOfOperationsRegionDetectionText_c = "DESCRIPTION OF OPERATIONS / LOCATIONS / VEHICLES (Attach ACORD 101, Additional Remarks Schedule, if more space is required)";
        public const string allRightsReservedRegionDetectionText = "THIS IS TO CERTIFY THAT THE POLICIES OF INSURANCE LISTED BELOW HAVE BEEN ISSUED TO THE INSURED NAMED ABOVE FOR THE POLICY PERIOD";

        #endregion

        #region ROW 1 variables
        public Rectangle rectRow1Col1Ltr; // LTR
        public Rectangle rectRow1Col2; // not implemented so far
        public Rectangle rectRow1Col3Insd; // INSD
        public Rectangle rectRow1Col4Wvd; // WVD
        public Rectangle rectRow1Col5PolicyNo; // Policy No
        public Rectangle rectRow1Col6PolicyEff; // Policy EFF
        public Rectangle rectRow1Col7PolicyExp; // Policy Exp

        public Rectangle rectRow1Col8LimitCol1_row1; // Limit col1, row 1
        public Rectangle rectRow1Col8LimitCol1_row2; // Limit col1, row 2
        public Rectangle rectRow1Col8LimitCol1_row3; // Limit col1, row 3
        public Rectangle rectRow1Col8LimitCol1_row4; // Limit col1, row 4
        public Rectangle rectRow1Col8LimitCol1_row5; // Limit col1, row 5
        public Rectangle rectRow1Col8LimitCol1_row6; // Limit col1, row 6
        public Rectangle rectRow1Col8LimitCol1_row7; // Limit col1, row 7

        public Rectangle rectRow1Col8LimitCol2_row1; // Limit col2, row 1
        public Rectangle rectRow1Col8LimitCol2_row2; // Limit col2, row 2
        public Rectangle rectRow1Col8LimitCol2_row3; // Limit col2, row 3
        public Rectangle rectRow1Col8LimitCol2_row4; // Limit col2, row 4
        public Rectangle rectRow1Col8LimitCol2_row5; // Limit col2, row 5
        public Rectangle rectRow1Col8LimitCol2_row6; // Limit col2, row 6
        public Rectangle rectRow1Col8LimitCol2_row7; // Limit col2, row 7
        #endregion

        #region ROW 2 variables
        public Rectangle rectRow2Col1Ltr; // LTR
        public Rectangle rectRow2Col2; // not implemented so far
        public Rectangle rectRow2Col3Insd; // INSD
        public Rectangle rectRow2Col4Wvd; // WVD
        public Rectangle rectRow2Col5PolicyNo; // Policy No
        public Rectangle rectRow2Col6PolicyEff; // Policy EFF
        public Rectangle rectRow2Col7PolicyExp; // Policy Exp

        public Rectangle rectRow2Col8LimitCol1_row1; // Limit col1, row 1
        public Rectangle rectRow2Col8LimitCol1_row2; // Limit col1, row 2
        public Rectangle rectRow2Col8LimitCol1_row3; // Limit col1, row 3
        public Rectangle rectRow2Col8LimitCol1_row4; // Limit col1, row 4
        public Rectangle rectRow2Col8LimitCol1_row5; // Limit col1, row 5

        public Rectangle rectRow2Col8LimitCol2_row1; // Limit col2, row 1
        public Rectangle rectRow2Col8LimitCol2_row2; // Limit col2, row 2
        public Rectangle rectRow2Col8LimitCol2_row3; // Limit col2, row 3
        public Rectangle rectRow2Col8LimitCol2_row4; // Limit col2, row 4
        public Rectangle rectRow2Col8LimitCol2_row5; // Limit col2, row 5
        #endregion

        #region ROW 3 variables
        public Rectangle rectRow3Col1Ltr; // LTR
        public Rectangle rectRow3Col2; // not implemented so far
        public Rectangle rectRow3Col3Insd; // INSD
        public Rectangle rectRow3Col4Wvd; // WVD
        public Rectangle rectRow3Col5PolicyNo; // Policy No
        public Rectangle rectRow3Col6PolicyEff; // Policy EFF
        public Rectangle rectRow3Col7PolicyExp; // Policy Exp

        public Rectangle rectRow3Col8LimitCol1_row1; // Limit col1, row 1
        public Rectangle rectRow3Col8LimitCol1_row2; // Limit col1, row 2
        public Rectangle rectRow3Col8LimitCol1_row3; // Limit col1, row 3

        public Rectangle rectRow3Col8LimitCol2_row1; // Limit col2, row 1
        public Rectangle rectRow3Col8LimitCol2_row2; // Limit col2, row 2
        public Rectangle rectRow3Col8LimitCol2_row3; // Limit col2, row 3
        #endregion

        #region ROW 4 variables
        public Rectangle rectRow4Col1Ltr; // LTR
        public Rectangle rectRow4Col2; // not implemented so far
        public Rectangle rectRow4Col3Insd; // INSD
        public Rectangle rectRow4Col4Wvd; // WVD
        public Rectangle rectRow4Col5PolicyNo; // Policy No
        public Rectangle rectRow4Col6PolicyEff; // Policy EFF
        public Rectangle rectRow4Col7PolicyExp; // Policy Exp

        public Rectangle rectRow4Col8LimitCol1_row1_1; // Limit col1, row 1
        public Rectangle rectRow4Col8LimitCol1_row1_2; // Limit col1, row 1
        public Rectangle rectRow4Col8LimitCol1_row1_3; // Limit col1, row 1
        public Rectangle rectRow4Col8LimitCol1_row1_4; // Limit col1, row 1
        public Rectangle rectRow4Col8LimitCol1_row2; // Limit col1, row 1
        public Rectangle rectRow4Col8LimitCol1_row3; // Limit col1, row 2
        public Rectangle rectRow4Col8LimitCol1_row4; // Limit col1, row 3

        public Rectangle rectRow4Col8LimitCol2_row1; // Limit col2, row 1
        public Rectangle rectRow4Col8LimitCol2_row2; // Limit col2, row 1
        public Rectangle rectRow4Col8LimitCol2_row3; // Limit col2, row 2
        public Rectangle rectRow4Col8LimitCol2_row4; // Limit col2, row 3

        #endregion

        #region ROW 5 variables
        public Rectangle rectRow5Col1Ltr; // LTR
        public Rectangle rectRow5Col2; // not implemented so far
        public Rectangle rectRow5Col3Insd; // INSD
        public Rectangle rectRow5Col4Wvd; // WVD
        public Rectangle rectRow5Col5PolicyNo; // Policy No
        public Rectangle rectRow5Col6PolicyEff; // Policy EFF
        public Rectangle rectRow5Col7PolicyExp; // Policy Exp

        public Rectangle rectRow5Col8LimitCol1_row1; // Limit col1, row 1

        public Rectangle rectRow5Col8LimitCol2_row1; // Limit col2, row 1
        #endregion

        public MultipleRowsTabular(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
        
            row1Height = aForm.tableRow1Ratio * aForm.constantNRulesObj.tableHeight;
            row2Height = aForm.tableRow2Ratio * aForm.constantNRulesObj.tableHeight;
            row3Height = aForm.tableRow3Ratio * aForm.constantNRulesObj.tableHeight;
            row4Height = aForm.tableRow4Ratio * aForm.constantNRulesObj.tableHeight;
            row5Height = aForm.tableRow5Ratio * aForm.constantNRulesObj.tableHeight;

            col1Width = aForm.tableCol1Ratio * aForm.constantNRulesObj.tableWidth;
            col2Width = aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            col3Width = aForm.tableCol3Ratio * aForm.constantNRulesObj.tableWidth;
            col4Width = aForm.tableCol4Ratio * aForm.constantNRulesObj.tableWidth;
            col5Width = aForm.tableCol5Ratio * aForm.constantNRulesObj.tableWidth;
            col6Width = aForm.tableCol6Ratio * aForm.constantNRulesObj.tableWidth;
            col7Width = aForm.tableCol7Ratio * aForm.constantNRulesObj.tableWidth;
            col8_1Width = aForm.tableCol8ARatio * aForm.constantNRulesObj.tableWidth;
            col8_2Width = aForm.tableCol8BRatio* aForm.constantNRulesObj.tableWidth;
        }

        public List<PdfWordCoordinates> FindCoordinatesOfText()
        {
            RetrieveCol8Data();
            PdfWordCoordinates pdfWcmmDDyyyy = null;
            PdfWordCoordinates pdfWcinsr = null;
            PdfWordCoordinates pdfWcdescOfOperations = null;

            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            pdfWcmmDDyyyy = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, mmDDyyyyRegionDetectionText);

            pdfWcinsr = new UtilityFn().
                   FindPdfWordCordinatesOfGivenTextEquals
                   (aForm.liLinesSorted, insrRegionDetectionText);

            pdfWcdescOfOperations = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, descOfOperationsRegionDetectionText);

            if (pdfWcdescOfOperations == null)
            {
                pdfWcdescOfOperations = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, descOfOperationsRegionDetectionText_b);

            }

            float col1llx, col1urx, col3llx, col3urx, col4llx, col4urx = 0f;

            string col1RegionDetectionText = "LTR";
            PdfWordCoordinates pdfCol1 = null;

            pdfCol1 = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, col1RegionDetectionText);



            string col3N4RegionDetectionText = "ADDL SUBR";
            PdfWordCoordinates pdfCol3N4 = null;

            pdfCol3N4 = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, col3N4RegionDetectionText);

            col1llx = aForm.constantNRulesObj.tableStartXcoordinate;
            col1urx = col1llx + col1Width;

            col3llx = pdfCol3N4.RectOuter.Left;
            col3urx = col3llx + col3Width;
            col4llx = col3urx;
            col4urx = pdfCol3N4.RectOuter.Right;

            col3llx = col3llx - aForm.constantNRulesObj.smallMoveLeft;
            col3urx = col3urx + aForm.constantNRulesObj.smallMoveRight;

            col4llx = col4llx - aForm.constantNRulesObj.smallMoveLeft;
            col4urx = col4urx + aForm.constantNRulesObj.smallMoveRight;
            RetrieveRow1Data(pdfWcmmDDyyyy, pdfWcinsr, col1llx, col1urx, col3llx, col3urx, col4llx, col4urx);
            RetrieveRow2Data(pdfWcmmDDyyyy, pdfWcinsr, col1llx, col1urx, col3llx, col3urx, col4llx, col4urx);
            RetrieveRow3Data(pdfWcmmDDyyyy, pdfWcinsr, col1llx, col1urx, col3llx, col3urx, col4llx, col4urx);
            RetrieveRow4Data(pdfWcmmDDyyyy, pdfWcinsr, col1llx, col1urx, col3llx, col3urx, col4llx, col4urx);
            RetrieveRow5Data(pdfWcmmDDyyyy, pdfWcinsr, col1llx, col1urx, col3llx, col3urx, col4llx, col4urx);

            if (!aForm.xFoundAsText)
            {
                foreach (var v in aForm.col2KeyValRow1Obj)
                {
                    if (v.valVal.Trim() != "")
                    {
                        aForm.xFoundAsText = true;
                        break;
                    }
                }
            }

            if (!aForm.xFoundAsText)
            {
                foreach (var v in aForm.col2KeyValRow2Obj)
                {
                    if (v.valVal.Trim() != "")
                    {
                        aForm.xFoundAsText = true;
                        break;
                    }
                }
            }

            if (!aForm.xFoundAsText)
            {
                foreach (var v in aForm.col2KeyValRow3Obj)
                {
                    if (v.valVal.Trim() != "")
                    {
                        aForm.xFoundAsText = true;
                        break;
                    }
                }
            }

            return liProducer;
        }

        private void RetrieveRow1Column2Data(float llx, float lly, float urx, float ury, float sizeOfEachRow, PdfReader reader)
        {
            string keyVal = "";
            string valVal = "";

            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";


            float llxInitial, llyInitial, urxInitial, uryInitial;
            llxInitial = llx;
            llyInitial = lly;
            uryInitial = ury;
            urxInitial = urx;
            Rectangle row1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, ury - sizeOfEachRow, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, row1, aForm, OcrTypes.SingleWord);

            if ((result[0].ToUpper().Contains("COMMERCIAL GENERAL LIABILITY"))) // sujit
            {
                Rectangle rectRow1Col1, rectRow1Col2 = null;
                Rectangle rectRow2Col1, rectRow2Col2, rectRow2Col3, rectRow2Col4, rectRow2Col5 = null;
                Rectangle rectRow3Col1, rectRow3Col2 = null;
                Rectangle rectRow4Col1, rectRow4Col2 = null;
                Rectangle rectRow5Col1 = null;
                Rectangle rectRow6Col1, rectRow6Col2, rectRow6Col3, rectRow6Col4, rectRow6Col5, rectRow6Col6 = null;
                Rectangle rectRow7Col1, rectRow7Col2 = null;
                float row1Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row1Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row2Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row2Col2width = 0.10390625f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row2Col3width = 0.358167614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row2Col4width = 0.102556818f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row2Col5width = 0.334161932f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row3Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row4Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row4Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row5Col1width = 1f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row6Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row6Col2width = 0.205042614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row6Col3width = 0.102556818f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row6Col4width = 0.203764205f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row6Col5width = 0.102556818f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row6Col6width = 0.284872159f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row7Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row7Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                urx = llx + row1Col1width;
                lly = ury - sizeOfEachRow;
                rectRow1Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
                 result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                llx = urx;
                urx = llx + row1Col2width;
                rectRow1Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col2, aForm, OcrTypes.SingleLine);
                keyVal = "COMMERCIAL GENERAL LIABILITY";
                keyValOcr = keyVal; // result[1];
                keyValType3 = keyVal;

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }
                
                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 2, col 1
                urx = llx + row2Col1width;
                rectRow2Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
                result  =  new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                // row 2, col 2
                llx = urx;
                urx = llx + row2Col2width;
                rectRow2Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
                //valVal += "," +  new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col2, aForm);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col2, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 2, col 3
                llx = urx;
                urx = llx + row2Col3width;
                rectRow2Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col3, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyVal = "CLAIMS-MADE";
                keyValOcr = result[1];
                keyValOcr = keyVal;
                keyValType3 = keyVal;

                if (keyVal != "")
                {
                    valVal = valVal.Trim();
                    if (valVal == ",")
                        valVal = "";

                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row 2 col 4
                llx = urx;
                urx = llx + row2Col4width;
                rectRow2Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col4, aForm,OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];
                // row 2 col 5
                llx = urx;
                urx = llx + row2Col5width;
                rectRow2Col5 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col5, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyVal = "OCCUR";
                keyValOcr = result[1];
                keyValOcr = keyVal;
                keyValType3 = keyVal;


                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 3, col 1
                urx = llx + row3Col1width;
                rectRow3Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 3, col 2
                llx = urx;
                urx = llx + row3Col2width;
                rectRow3Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
           result =     new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col2, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 4, col 1
                urx = llx + row4Col1width;
                rectRow4Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 4, col 2
                llx = urx;
                urx = llx + row4Col2width;
                rectRow4Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result =    new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col2, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 5, col 1
                urx = llx + row5Col1width;
                rectRow5Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col1, aForm, OcrTypes.SingleLine);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                valVal = "";
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 6, col 1
                urx = llx + row6Col1width;
                rectRow6Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 6, col 2
                llx = urx;
                urx = llx + row6Col2width;
                rectRow6Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col2, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "POLICY";
                keyValOcr = keyVal;

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row 6, col 3
                llx = urx;
                urx = llx + row6Col3width;
                rectRow6Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col3, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 6 col 4
                llx = urx;
                urx = llx + row6Col4width;
                rectRow6Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col4, aForm, OcrTypes.SingleBlock);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "PROJECT";
                keyValOcr = keyVal;
                keyValType3 = keyVal;

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row 6 col 5
                llx = urx;
                urx = llx + row6Col5width;
                rectRow6Col5 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col5, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 6 col 6
                llx = urx;
                urx = llx + row6Col6width;
                rectRow6Col6 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col6, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "LOC";
                keyValOcr = keyVal;
                keyValType3 = keyVal;
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                    // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 7, col 1
                urx = llx + row7Col1width;
                rectRow7Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 7, col 2
                llx = urx;
                urx = llx + row7Col2width;
                rectRow7Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col2, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];
                keyVal = "OTHER:";
                keyValOcr = keyVal;
                keyValType3 = keyVal;

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }
            }
            else
            {
                Rectangle rectRow1Col1 = null;
                Rectangle rectRow2Col1, rectRow2Col2 = null;
                Rectangle rectRow3Col1, rectRow3Col2, rectRow3Col3, rectRow3Col4, rectRow3Col5 = null;
                Rectangle rectRow4Col1, rectRow4Col2 = null;
                Rectangle rectRow5Col1, rectRow5Col2 = null;
                Rectangle rectRow6Col1 = null;
                Rectangle rectRow7Col1, rectRow7Col2, rectRow7Col3, rectRow7Col4, rectRow7Col5, rectRow7Col6  = null;

             //   float widthMultiplicationFactor = 1; // sujit
                float row1Col1width = 1f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth; 
               
                float row2Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row2Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth; 
              
                float row3Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col2width = 0.10390625f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col3width = 0.358167614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col4width = 0.102556818f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col5width = 0.334161932f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                
                float row4Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row4Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row5Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row5Col2width = 0.898792614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row6Col1width = 1f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth; ; ;
              
                float row7Col1width = 0.101207386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row7Col2width = 0.205042614f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row7Col3width = 0.102556818f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row7Col4width = 0.203764205f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row7Col5width = 0.102556818f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row7Col6width = 0.284872159f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                
                urx = llx + row1Col1width;
                lly = ury - sizeOfEachRow;
                rectRow1Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col1, aForm, OcrTypes.SingleLine);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                valVal = "";
                valValOcr = "";
                valValType3 = "";

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                    // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 2, col 1
                urx = llx + row2Col1width;
                rectRow2Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 2, col 2
                llx = urx;
                urx = llx + row2Col2width;
                rectRow2Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col2, aForm, OcrTypes.SingleLine);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "COMMERCIAL GENERAL LIABILITY";
                keyValOcr = keyVal;
                keyValType3 = keyVal;

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                    // row change
                    ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 3, col 1
                urx = llx + row3Col1width;
                rectRow3Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 3, col 2
                llx = urx;
                urx = llx + row3Col2width;
                rectRow3Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
                //valVal += "," +  new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col2, aForm);
                result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col2, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 3, col 3
                llx = urx;
                urx = llx + row3Col3width;
                rectRow3Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col3, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "CLAIMS-MADE";
                keyValOcr = keyVal;
                keyValType3 = keyVal;

                if (keyVal != "")
                {
                    valVal = valVal.Trim();
                    if (valVal == ",")
                        valVal = "";

                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row 3, col 4
                llx = urx;
                urx = llx + row3Col4width;
                rectRow3Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col4, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 3, col 5
                llx = urx;
                urx = llx + row3Col5width;
                rectRow3Col5 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col5, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "OCCUR";
                keyValOcr = keyVal;
                keyValType3 = keyVal;
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 4, col 1
                urx = llx + row4Col1width;
                rectRow4Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];


                // row 4, col 2
                llx = urx;
                urx = llx + row4Col2width;
                rectRow4Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col2, aForm, OcrTypes.SingleLine);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 5, col 1
                urx = llx + row5Col1width;
                rectRow5Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 5, col 2
                urx = llx + row5Col2width;
                rectRow5Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col2, aForm, OcrTypes.SingleLine);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 6, col 1
                urx = llx + row6Col1width;
                rectRow6Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow6Col1, aForm, OcrTypes.SingleLine);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                valVal = "";
                valValOcr = "";
                valValType3 = "";
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }
                
                // row change
                ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 7, col 1
                urx = llx + row7Col1width;
                rectRow7Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col1, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 7, col 2
                llx = urx;
                urx = llx + row7Col2width;
                rectRow7Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col2, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "POLICY";
                keyValOcr = keyVal;
                keyValType3 = keyVal;
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }
                
                // row 7, col 3
                llx = urx;
                urx = llx + row7Col3width;
                rectRow7Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =    new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col3, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 7, col 4
                llx = urx;
                urx = llx + row7Col4width;
                rectRow7Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col4, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "PROJECT";
                keyValOcr = keyVal;
                keyValType3 = keyVal;
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

                // row 7, col 5
                llx = urx;
                urx = llx + row7Col5width;
                rectRow7Col5 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col5, aForm, OcrTypes.SingleChar);
                valVal = result[0];
                valValOcr = result[1];
                valValType3 = result[2];

                // row 7, col 6
                llx = urx;
                urx = llx + row7Col6width;
                rectRow7Col6 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow7Col6, aForm, OcrTypes.SingleWord);
                keyVal = result[0];
                keyValOcr = result[1];
                keyValType3 = result[2];

                keyVal = "LOC";
                keyValOcr = keyVal;
                keyValType3 = keyVal;
                if (keyVal != "")
                {
                    aForm.col2KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                    aForm.acrdFrmForOcrObj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                    aForm.acrdFrmForType3Obj.col2KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
                }

            }

        }

        private void RetrieveRow2Column2Data(float llx, float lly, float urx, float ury, float sizeOfEachRow, PdfReader reader)
        {
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";


            string[] result;

            float llxInitial, llyInitial, urxInitial, uryInitial;
            llxInitial = llx;
            llyInitial = lly;
            uryInitial = ury;
            urxInitial = urx;
           
                Rectangle rectRow1Col1 = null;
                Rectangle rectRow2Col1, rectRow2Col2 = null;
                Rectangle rectRow3Col1, rectRow3Col2, rectRow3Col3, rectRow3Col4 = null;
                Rectangle rectRow4Col1, rectRow4Col2, rectRow4Col3, rectRow4Col4 = null;
                Rectangle rectRow5Col1, rectRow5Col2, rectRow5Col3, rectRow5Col4 = null;
            
                float row1Col1width = 1f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

                float row2Col1width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row2Col2width = 0.897088068f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
              
                float row3Col1width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col2width = 0.382528409f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col3width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row3Col4width = 0.411647727f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            
                float row4Col1width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row4Col2width = 0.382528409f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row4Col3width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row4Col4width = 0.411647727f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                                                     
                float row5Col1width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row5Col2width = 0.382528409f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row5Col3width = 0.102911932f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
                float row5Col4width = 0.411647727f *  aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;


            urx = llx + row1Col1width;
                lly = ury - sizeOfEachRow;
                rectRow1Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
              result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col1, aForm, OcrTypes.SingleLine);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            valVal = "";
            valValOcr = "";
            valValType3 = "";

            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row change
            ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 2, col 1
                urx = llx + row2Col1width;
                rectRow2Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

                // row 2, col 2
                llx = urx;
                urx = llx + row2Col2width;
                rectRow2Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
               result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col2, aForm, OcrTypes.SingleLine);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "ANY AUTO";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row change 3
            ury = lly;
                lly = ury - sizeOfEachRow;
                llx = llxInitial;

                // row 3, col 1
                urx = llx + row3Col1width;
                rectRow3Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

                // row 3, col 2
                llx = urx;
                urx = llx + row3Col2width;
                rectRow3Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result =    new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "OWNED AUTOS ONLY";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row 3, col 3
            llx = urx;
                urx = llx + row3Col3width;
                rectRow3Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
             result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

                // row 3, col 4
                llx = urx;
                urx = llx + row3Col4width;
                rectRow3Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result =    new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "SCHEDULED AUTOS";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row change 4
            ury = lly;
            lly = ury - sizeOfEachRow;
            llx = llxInitial;

            // row 4, col 1
            urx = llx + row4Col1width;
            rectRow4Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];
            // row 4, col 2
            llx = urx;
            urx = llx + row4Col2width;
            rectRow4Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "HIRED AUTOS ONLY";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row 4, col 3
            llx = urx;
            urx = llx + row4Col3width;
            rectRow4Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];


            // row 4, col 4
            llx = urx;
            urx = llx + row3Col4width;
            rectRow4Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "NON-OWNED AUTOS ONLY";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row change 5
            ury = lly;
            lly = ury - sizeOfEachRow;
            llx = llxInitial;

            // row 5, col 1
            urx = llx + row5Col1width;
            rectRow5Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 5, col 2
            llx = urx;
            urx = llx + row5Col2width;
            rectRow5Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row 5, col 3
            llx = urx;
            urx = llx + row5Col3width;
            rectRow5Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 5, col 4
            llx = urx;
            urx = llx + row5Col4width;
            rectRow5Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            if (keyVal != "")
            {
                aForm.col2KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

        }

        private void RetrieveRow3Column2Data(float llx, float lly, float urx, float ury, float sizeOfEachRow, PdfReader reader)
        {
            // string result = "";
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";


            string[] result;

            float llxInitial, llyInitial, urxInitial, uryInitial;
            llxInitial = llx;
            llyInitial = lly;
            uryInitial = ury;
            urxInitial = urx;

            Rectangle rectRow1Col1, rectRow1Col2, rectRow1Col3, rectRow1Col4 = null;
            Rectangle rectRow2Col1, rectRow2Col2, rectRow2Col3, rectRow2Col4 = null;
            Rectangle rectRow3Col1, rectRow3Col2, rectRow3Col3, rectRow3Col4 = null;
            Rectangle rectRow4Col1, rectRow4Col2, rectRow4Col3, rectRow4Col4 = null;
            
            float row1Col1width = 0.102911932f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row1Col2width = 0.45859375f  * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row1Col3width = 0.102911932f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row1Col4width = 0.335582386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;

            float row2Col1width = 0.102911932f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row2Col2width = 0.45859375f  * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row2Col3width = 0.102911932f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row2Col4width = 0.335582386f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            
            float row3Col1width = 0.101640514f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row3Col2width = 0.155135521f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row3Col3width = 0.10299572f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;
            float row3Col4width = 0.640228245f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth;


            urx = llx + row1Col1width;
            lly = ury - sizeOfEachRow;
            rectRow1Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 1, col 2
            llx = urx;
            urx = llx + row1Col2width;
            rectRow1Col2= new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];
            keyVal = "UMBRELLA LIAB";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            if (keyVal != "")
            {
                aForm.col2KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

                // row 1, col 3
                llx = urx;
            urx = llx + row1Col3width;
            rectRow1Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 1, col 4
            llx = urx;
            urx = llx + row1Col4width;
            rectRow1Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "OCCUR";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));

            }

            // row change
            ury = lly;
            lly = ury - sizeOfEachRow;
            llx = llxInitial;

            // row 2, col 1
            urx = llx + row2Col1width;
            rectRow2Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 2, col 2
            llx = urx;
            urx = llx + row2Col2width;
            rectRow2Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "EXCESS LIAB";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            if (keyVal != "")
            {
                aForm.col2KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row 2, col 3
            llx = urx;
            urx = llx + row2Col3width;
            rectRow2Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 2, col 4
            llx = urx;
            urx = llx + row2Col4width;
            rectRow2Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "CLAIMS-MADE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            if (keyVal != "")
            {
                aForm.col2KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

                // row change 3
                ury = lly;
            lly = ury - sizeOfEachRow;
            llx = llxInitial;

            // row 3, col 1
            urx = llx + row3Col1width;
            rectRow3Col1 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 3, col 2
            llx = urx;
            urx = llx + row3Col2width;
            rectRow3Col2 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "DED";
            keyValOcr = keyVal;
            keyValType3 = keyVal;
            if (keyVal != "")
            {
                aForm.col2KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            // row 3, col 3
            llx = urx;
            urx = llx + row3Col3width;
            rectRow3Col3 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            // row 3, col 4
            llx = urx;
            urx = llx + row3Col4width;
            rectRow3Col4 = new Rectangle(llx + aForm.constantNRulesObj.smallMoveRight, lly, urx - aForm.constantNRulesObj.smallMoveLeft, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "RETENTION";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            if (keyVal != "")
            {
                aForm.col2KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }


        }

        private void RetrieveRow4Column2Data(float llx, float lly, float urx, float ury, float sizeOfEachRow, PdfReader reader)
        {
            // string result = "";
            string keyVal = "";
            string valVal = "";
            string[] result;
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";

            float llxInitial, llyInitial, urxInitial, uryInitial;
            llxInitial = llx;
            llyInitial = lly;
            uryInitial = ury;
            urxInitial = urx;

            float widthCol1 = 0.872443182f * aForm.tableCol2Ratio * aForm.constantNRulesObj.tableWidth; 
            float heightCol2fromTop  = 0.361870954f * aForm.tableRow4Ratio * aForm.constantNRulesObj.tableHeight; 

            Rectangle rectCol1, rectCol2A, rectCol2B = null;

            urx = llx + widthCol1;
            rectCol1 = new Rectangle(llx - 0.2f, lly, urx, ury);
            result  = new UtilityFn().ExtractTextFromRegion(reader, rectCol1, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[0];

            keyVal = "ROW4COL2_TEXT_WORKERS_COMPENSATION";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            llx = urx;
            urx = llx + ( urxInitial - llx);
            lly = ury - heightCol2fromTop;
            rectCol2A = new Rectangle(llx, lly, urx, ury);
            new UtilityFn().ExtractTextFromRegion(reader, rectCol2A, aForm, OcrTypes.SingleBlock);
            
            ury = lly;
            lly = llyInitial;
            rectCol2B = new Rectangle(llx, lly, urx, ury);
         result =   new UtilityFn().ExtractTextFromRegion(reader, rectCol2B, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                aForm.col2KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                aForm.acrdFrmForOcrObj.col2KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                aForm.acrdFrmForType3Obj.col2KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            }


       
        private void RetrieveRow5Column2Data(float llx, float lly, float urx, float ury, float sizeOfEachRow, PdfReader reader)
        {
            string[] result ;

            Rectangle rectCol1 = null;
            rectCol1 = new Rectangle(llx - 0.2f, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectCol1, aForm, OcrTypes.SingleBlock);
            aForm.col2Row5 = result[0];
            aForm.acrdFrmForOcrObj.col2Row5 = result[1];
            aForm.acrdFrmForType3Obj.col2Row5 = result[2];

        }

        private void RetrieveRow1Data(PdfWordCoordinates pdfWcmmDDyyyy, PdfWordCoordinates pdfWcinsr, float col1llx, float col1urx, float col3llx, float col3urx, float col4llx, float col4urx )
        {
            string[] result ;
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";

            float bottom = pdfWcmmDDyyyy.RectInner.Bottom - aForm.constantNRulesObj.smallMoveBottom;
            int rowNum = 1;
            bottom = aForm.constantNRulesObj.tableStartYcoordinate - (0.5f * rowNum) ;
            float sizeOfEachRowForCol8 = (row1Height) / 7f;

            float llx, lly, urx, ury, lly_sub1, ury_sub1, llx_sub1, urx_sub1, lly_sub2, ury_sub2, llx_sub2, urx_sub2 = 0f;

            //llx = left;
            llx = col1llx;
            lly = bottom - row1Height;
            urx = llx + col1Width;
            ury = bottom;
            //  rectRow1Col1Ltr = new Rectangle(col1llx, lly, col1urx, ury);
            rectRow1Col1Ltr = new Rectangle(col1llx, lly, col1urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col1Ltr, aForm, OcrTypes.SingleChar);
            aForm.PolicyInsrLtrRow1 = new UtilityFn().RuleForCol1InsrLtr(result[0]);
            aForm.acrdFrmForOcrObj.PolicyInsrLtrRow1 = new UtilityFn().RuleForCol1InsrLtr(result[1]);
            aForm.acrdFrmForType3Obj.PolicyInsrLtrRow1 = new UtilityFn().RuleForCol1InsrLtr(result[2]);

            // rectRow1Col2 // work in progress
            llx = urx;
            urx = llx + col2Width;

            RetrieveRow1Column2Data(llx, lly, urx, ury, sizeOfEachRowForCol8, reader);
            //

            llx = urx;
            urx = llx + col3Width;
            rectRow1Col3Insd = new Rectangle(col3llx, lly, col3urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col3Insd, aForm, OcrTypes.SingleChar);
            aForm.PolicyAddlInsrRow1 = new UtilityFn().RuleForCol3AddlInsd(result[0]);
            aForm.acrdFrmForOcrObj.PolicyAddlInsrRow1 = new UtilityFn().RuleForCol3AddlInsd(result[1]);
            aForm.acrdFrmForType3Obj.PolicyAddlInsrRow1 = new UtilityFn().RuleForCol3AddlInsd(result[2]);

            llx = urx;
            urx = llx + col4Width;
            rectRow1Col4Wvd = new Rectangle(col4llx, lly, col4urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col4Wvd, aForm, OcrTypes.SingleChar);
            aForm.PolicySubrWvdRow1 = new UtilityFn().RuleForCol4SubrWvd(result[0]);
            aForm.acrdFrmForOcrObj.PolicySubrWvdRow1 = new UtilityFn().RuleForCol4SubrWvd(result[1]);
            aForm.acrdFrmForType3Obj.PolicySubrWvdRow1 = new UtilityFn().RuleForCol4SubrWvd(result[2]);


            llx = urx;
            urx = llx + col5Width;
            rectRow1Col5PolicyNo = new Rectangle(llx - 0.5f, lly, urx , ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col5PolicyNo, aForm, OcrTypes.SingleBlock);

            aForm.PolicyNoRow1 = result[0].Trim();
            aForm.acrdFrmForOcrObj.PolicyNoRow1 = result[1].Trim();
            aForm.acrdFrmForType3Obj.PolicyNoRow1 = result[2].Trim();

            llx = urx;
            urx = llx + col6Width;
            rectRow1Col6PolicyEff = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col6PolicyEff, aForm, OcrTypes.SingleBlock);
            aForm.PolicEffRow1 = result[0];
            aForm.acrdFrmForOcrObj.PolicEffRow1 = result[1];
            aForm.acrdFrmForType3Obj.PolicEffRow1 = result[2];

            llx = urx;
            urx = llx + col7Width;
            rectRow1Col7PolicyExp = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col7PolicyExp, aForm, OcrTypes.SingleBlock);
            aForm.PolicyExpRow1 = result[0];
            aForm.acrdFrmForOcrObj.PolicyExpRow1 = result[1];
            aForm.acrdFrmForType3Obj.PolicyExpRow1 = result[2];

            llx_sub1 = urx;
            lly_sub1 = bottom - sizeOfEachRowForCol8;
            urx_sub1 = llx_sub1 + col8_1Width;
            ury_sub1 = bottom;
            rectRow1Col8LimitCol1_row1 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row1, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "EACH OCCURRENCE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            llx_sub2 = urx_sub1;
            lly_sub2 = lly_sub1;
            urx_sub2 = llx_sub2 + col8_2Width;
            ury_sub2 = ury_sub1;
            rectRow1Col8LimitCol2_row1 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row1, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //Form.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
              //  aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
               // aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }
                
            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow1Col8LimitCol1_row2 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "DAMAGE TO RENTED PREMISES(Ea occurrence)";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow1Col8LimitCol2_row2 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row2, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow1Col8LimitCol1_row3 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row3, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "MED EXP (Any one person)";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow1Col8LimitCol2_row3 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
         result =   new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row3, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));

            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow1Col8LimitCol1_row4 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "PERSONAL & ADV INJURY";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow1Col8LimitCol2_row4 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
          result =  new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row4, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];
            if (keyVal != "")
            {
                //aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow1Col8LimitCol1_row5 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row5, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "GENERAL AGGREGATE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow1Col8LimitCol2_row5 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row5, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow1Col8LimitCol1_row6 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row6, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "PRODUCTS - COMP/OP AGG";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow1Col8LimitCol2_row6 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row6, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
             //   aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow1Col8LimitCol1_row7 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow1Col8LimitCol1_row7, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow1Col8LimitCol2_row7 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result =  new UtilityFn().ExtractTextFromRegionValue(reader, rectRow1Col8LimitCol2_row7, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow1Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }
        }

        private void RetrieveRow2Data(PdfWordCoordinates pdfWcmmDDyyyy, PdfWordCoordinates pdfWcinsr, float col1llx, float col1urx, float col3llx, float col3urx, float col4llx, float col4urx)
        {
            string[] result ;
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";


            float bottom = pdfWcmmDDyyyy.RectInner.Bottom - aForm.constantNRulesObj.smallMoveBottom - row1Height;

            int rowNum = 2;
            bottom = aForm.constantNRulesObj.tableStartYcoordinate - row1Height - (0.5f * rowNum);

            float sizeOfEachRowForCol8 = (row2Height) / 5f;
            float llx, lly, urx, ury, lly_sub1, ury_sub1, llx_sub1, urx_sub1, lly_sub2, ury_sub2, llx_sub2, urx_sub2 = 0f;

            llx = col1llx;
            lly = bottom - row2Height;
            urx = llx + col1Width;
            ury = bottom;
            rectRow2Col1Ltr = new Rectangle(col1llx, lly, col1urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col1Ltr, aForm, OcrTypes.SingleChar);
            aForm.PolicyInsrLtrRow2 = result[0];
            aForm.acrdFrmForOcrObj.PolicyInsrLtrRow2 = result[1];
            aForm.acrdFrmForType3Obj.PolicyInsrLtrRow2 = result[2];

            // rectRow1Col2 // work in progress
            llx = urx;
            urx = llx + col2Width;

            RetrieveRow2Column2Data(llx, lly, urx, ury, sizeOfEachRowForCol8, reader);
            //
            
            llx = urx;
            urx = llx + col3Width;
            rectRow2Col3Insd = new Rectangle(col3llx, lly, col3urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col3Insd, aForm, OcrTypes.SingleChar);
            aForm.PolicyAddlInsrRow2 = result[0];
            aForm.acrdFrmForOcrObj.PolicyAddlInsrRow2 = result[1];
            aForm.acrdFrmForType3Obj.PolicyAddlInsrRow2 = result[2];

            llx = urx;
            urx = llx + col4Width;
            rectRow2Col4Wvd = new Rectangle(col4llx, lly, col4urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col4Wvd, aForm, OcrTypes.SingleChar);
            aForm.PolicySubrWvdRow2 = result[0];
            aForm.acrdFrmForOcrObj.PolicySubrWvdRow2 = result[1];
            aForm.acrdFrmForType3Obj.PolicySubrWvdRow2 = result[2];

            llx = urx;
            urx = llx + col5Width;
            rectRow2Col5PolicyNo = new Rectangle(llx - 0.5f, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col5PolicyNo, aForm, OcrTypes.SingleBlock);

            //result = result.Trim();
            aForm.PolicyNoRow2 = result[0].Trim();
            aForm.acrdFrmForOcrObj.PolicyNoRow2 = result[1].Trim();
            aForm.acrdFrmForType3Obj.PolicyNoRow2 = result[2].Trim();

            llx = urx;
            urx = llx + col6Width;
            rectRow2Col6PolicyEff = new Rectangle(llx, lly, urx, ury);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col6PolicyEff, aForm, OcrTypes.SingleBlock);
            aForm.PolicEffRow2 = result[0];
            aForm.acrdFrmForOcrObj.PolicEffRow2 = result[1];
            aForm.acrdFrmForType3Obj.PolicEffRow2 = result[2];

            llx = urx;
            urx = llx + col7Width;
            rectRow2Col7PolicyExp = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col7PolicyExp, aForm, OcrTypes.SingleBlock);
            aForm.PolicyExpRow2 = result[0];
            aForm.acrdFrmForOcrObj.PolicyExpRow2 = result[1];
            aForm.acrdFrmForType3Obj.PolicyExpRow2 = result[2];

            llx_sub1 = urx;
            lly_sub1 = bottom - sizeOfEachRowForCol8;
            urx_sub1 = llx_sub1 + col8_1Width;
            ury_sub1 = bottom;
            rectRow2Col8LimitCol1_row1 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col8LimitCol1_row1, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];
            keyVal = "COMBINED SINGLE LIMIT (Ea accident)";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            llx_sub2 = urx_sub1;
            lly_sub2 = lly_sub1;
            urx_sub2 = llx_sub2 + col8_2Width;
            ury_sub2 = ury_sub1;
            rectRow2Col8LimitCol2_row1 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow2Col8LimitCol2_row1, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow2Col8LimitCol1_row2 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col8LimitCol1_row2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];
            keyVal = "BODILY INJURY (Per person)";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow2Col8LimitCol2_row2 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow2Col8LimitCol2_row2, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ///
            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow2Col8LimitCol1_row3 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col8LimitCol1_row3, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "BODILY INJURY (Per accident)";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow2Col8LimitCol2_row3 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow2Col8LimitCol2_row3, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ///
            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow2Col8LimitCol1_row4 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col8LimitCol1_row4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];
            keyVal = "PROPERTY DAMAGE (Per accident)";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow2Col8LimitCol2_row4 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result =  new UtilityFn().ExtractTextFromRegionValue(reader, rectRow2Col8LimitCol2_row4, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ///
            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow2Col8LimitCol1_row5 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow2Col8LimitCol1_row5, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow2Col8LimitCol2_row5 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
          result =  new UtilityFn().ExtractTextFromRegionValue(reader, rectRow2Col8LimitCol2_row5, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow2Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow2Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }
        }

        private void RetrieveRow3Data(PdfWordCoordinates pdfWcmmDDyyyy, PdfWordCoordinates pdfWcinsr, float col1llx, float col1urx, float col3llx, float col3urx, float col4llx, float col4urx)
        {
            string[] result ;
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";

            float bottom = pdfWcmmDDyyyy.RectInner.Bottom - aForm.constantNRulesObj.smallMoveBottom - row1Height - row2Height;

            int rowNum = 3;
            bottom = aForm.constantNRulesObj.tableStartYcoordinate - row1Height - row2Height - (0.5f * rowNum);


            float sizeOfEachRowForCol8 = (row3Height) / 3f;

            float llx, lly, urx, ury, lly_sub1, ury_sub1, llx_sub1, urx_sub1, lly_sub2, ury_sub2, llx_sub2, urx_sub2 = 0f;

            llx = col1llx;
            lly = bottom - row3Height;
            urx = llx + col1Width;
            ury = bottom;
            rectRow3Col1Ltr = new Rectangle(col1llx, lly, col1urx, ury);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col1Ltr, aForm, OcrTypes.SingleChar);
            aForm.PolicyInsrLtrRow3 = result[0];
            aForm.acrdFrmForOcrObj.PolicyInsrLtrRow3 = result[1];
            aForm.acrdFrmForType3Obj.PolicyInsrLtrRow3 = result[2];

            // rectRow1Col2 
            llx = urx;
            urx = llx + col2Width;
            RetrieveRow3Column2Data(llx, lly, urx, ury, sizeOfEachRowForCol8, reader);

            //
            llx = urx;
            urx = llx + col3Width;
            rectRow3Col3Insd = new Rectangle(col3llx, lly, col3urx, ury);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col3Insd, aForm, OcrTypes.SingleChar);
            aForm.PolicyAddlInsrRow3 = result[0];
            aForm.acrdFrmForOcrObj.PolicyAddlInsrRow3 = result[1];
            aForm.acrdFrmForType3Obj.PolicyAddlInsrRow3 = result[2];

            llx = urx;
            urx = llx + col4Width;
            rectRow3Col4Wvd = new Rectangle(col4llx, lly, col4urx, ury);
            result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col4Wvd, aForm, OcrTypes.SingleChar);
            aForm.PolicySubrWvdRow3 = result[0];
            aForm.acrdFrmForOcrObj.PolicySubrWvdRow3 = result[1];
            aForm.acrdFrmForType3Obj.PolicySubrWvdRow3 = result[2];

            llx = urx;
            urx = llx + col5Width;
            rectRow3Col5PolicyNo = new Rectangle(llx - 0.5f, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col5PolicyNo, aForm, OcrTypes.SingleBlock);

            //result = result.Trim();
            aForm.PolicyNoRow3 = result[0].Trim();
            aForm.acrdFrmForOcrObj.PolicyNoRow3 = result[1].Trim();
            aForm.acrdFrmForType3Obj.PolicyNoRow3 = result[2].Trim();

            llx = urx;
            urx = llx + col6Width;
            rectRow3Col6PolicyEff = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col6PolicyEff, aForm, OcrTypes.SingleBlock);
            aForm.PolicEffRow3 = result[0];
            aForm.acrdFrmForOcrObj.PolicEffRow3 = result[1];
            aForm.acrdFrmForType3Obj.PolicEffRow3 = result[2];

            llx = urx;
            urx = llx + col7Width;
            rectRow3Col7PolicyExp = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col7PolicyExp, aForm, OcrTypes.SingleBlock);
            aForm.PolicyExpRow3 = result[0];
            aForm.acrdFrmForOcrObj.PolicyExpRow3 = result[1];
            aForm.acrdFrmForType3Obj.PolicyExpRow3 = result[2];

            llx_sub1 = urx;
            lly_sub1 = bottom - sizeOfEachRowForCol8;
            urx_sub1 = llx_sub1 + col8_1Width;
            ury_sub1 = bottom;
            rectRow3Col8LimitCol1_row1 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col8LimitCol1_row1, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "EACH OCCURRENCE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            llx_sub2 = urx_sub1;
            lly_sub2 = lly_sub1;
            urx_sub2 = llx_sub2 + col8_2Width;
            ury_sub2 = ury_sub1;
            rectRow3Col8LimitCol2_row1 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow3Col8LimitCol2_row1, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow3Col8LimitCol1_row2 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col8LimitCol1_row2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "AGGREGATE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow3Col8LimitCol2_row2 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow3Col8LimitCol2_row2, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

                ///
                ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow3Col8LimitCol1_row3 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col8LimitCol1_row3, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow3Col8LimitCol2_row3 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow3Col8LimitCol2_row3, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow3Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow3Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow3Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

        }

        private void RetrieveRow4Data(PdfWordCoordinates pdfWcmmDDyyyy, PdfWordCoordinates pdfWcinsr, float col1llx, float col1urx, float col3llx, float col3urx, float col4llx, float col4urx)
        {
            string[] result ;
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";

            string keyValType3 = "";
            string valValType3 = "";

            float bottom = pdfWcmmDDyyyy.RectInner.Bottom - aForm.constantNRulesObj.smallMoveBottom - row1Height - row2Height - row3Height;
            int rowNum = 4;
            bottom = aForm.constantNRulesObj.tableStartYcoordinate - row1Height - row2Height - row3Height - (0.5f * rowNum);


            float sizeOfEachRowForCol8 = (row4Height) / 4f;
            float llx, lly, urx, ury, lly_sub1, ury_sub1, llx_sub1, urx_sub1, lly_sub2, ury_sub2, llx_sub2, urx_sub2 = 0f;

            llx = col1llx;
            lly = bottom - row4Height;
            urx = llx + col1Width;
            ury = bottom;
            rectRow4Col1Ltr = new Rectangle(col1llx, lly, col1urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col1Ltr, aForm, OcrTypes.SingleChar);
            aForm.PolicyInsrLtrRow4 = result[0];
            aForm.acrdFrmForOcrObj.PolicyInsrLtrRow4 = result[1];
            aForm.acrdFrmForType3Obj.PolicyInsrLtrRow4 = result[2];

            // rectRow1Col2 
            llx = urx;
            urx = llx + col2Width;
            RetrieveRow4Column2Data(llx, lly, urx, ury, sizeOfEachRowForCol8, reader);

            //

            llx = urx;
            urx = llx + col3Width;
            rectRow4Col3Insd = new Rectangle(col3llx, lly, col3urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col3Insd, aForm, OcrTypes.SingleCharOrWord);
            aForm.PolicyAddlInsrRow4 = result[0];
            aForm.acrdFrmForOcrObj.PolicyAddlInsrRow4 = result[1];
            aForm.acrdFrmForType3Obj.PolicyAddlInsrRow4 = result[0];

            llx = urx;
            urx = llx + col4Width;
            rectRow4Col4Wvd = new Rectangle(col4llx, lly, col4urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col4Wvd, aForm, OcrTypes.SingleChar);
            aForm.PolicySubrWvdRow4 = result[0];
            aForm.acrdFrmForOcrObj.PolicySubrWvdRow4 = result[1];
            aForm.acrdFrmForType3Obj.PolicySubrWvdRow4 = result[2];

            llx = urx;
            urx = llx + col5Width;
            rectRow4Col5PolicyNo = new Rectangle(llx - 0.5f, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col5PolicyNo, aForm, OcrTypes.SingleBlock);

            //result = result.Trim();
            aForm.PolicyNoRow4 = result[0].Trim();
            aForm.acrdFrmForOcrObj.PolicyNoRow4 = result[1].Trim();
            aForm.acrdFrmForType3Obj.PolicyNoRow4 = result[2].Trim();

            llx = urx;
            urx = llx + col6Width;
            rectRow4Col6PolicyEff = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col6PolicyEff, aForm, OcrTypes.SingleBlock);
            aForm.PolicEffRow4 = result[0];
            aForm.acrdFrmForOcrObj.PolicEffRow4 = result[1];
            aForm.acrdFrmForType3Obj.PolicEffRow4 = result[2];

            llx = urx;
            urx = llx + col7Width;
            rectRow4Col7PolicyExp = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col7PolicyExp, aForm, OcrTypes.SingleBlock);
            aForm.PolicyExpRow4 = result[0];
            aForm.acrdFrmForOcrObj.PolicyExpRow4 = result[1];
            aForm.acrdFrmForType3Obj.PolicyExpRow4 = result[2];


            float widthCol1 = 0.160857016f * aForm.tableCol8ARatio *aForm.constantNRulesObj.tableWidth;
            float widthCol2 = 0.398645648f * aForm.tableCol8ARatio * aForm.constantNRulesObj.tableWidth;
            float widthCol3 = 0.160857016f * aForm.tableCol8ARatio * aForm.constantNRulesObj.tableWidth;
            float widthCol4 = 0.27964032f * aForm.tableCol8ARatio * aForm.constantNRulesObj.tableWidth;
            float llx_sub1_1, lly_sub1_1, ury_sub1_1, urx_sub1_1 = 0;
          
            llx_sub1 = urx;
            llx_sub1_1 = llx_sub1;
            lly_sub1 = bottom - sizeOfEachRowForCol8;
            lly_sub1_1 = lly_sub1;
            urx_sub1 = llx_sub1 + col8_1Width;
            urx_sub1_1 = llx_sub1_1 + widthCol1;
            ury_sub1 = bottom;
            ury_sub1_1 = ury_sub1;

            rectRow4Col8LimitCol1_row1_1 = new Rectangle(llx_sub1_1, lly_sub1_1, urx_sub1_1, ury_sub1_1);
            result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row1_1, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            llx_sub1_1 = urx_sub1_1;
            urx_sub1_1 = llx_sub1_1 + widthCol2;
            rectRow4Col8LimitCol1_row1_2 = new Rectangle(llx_sub1_1, lly_sub1_1 - aForm.constantNRulesObj.specialCaseAdjustment, urx_sub1_1, ury_sub1_1);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row1_2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

          keyVal = "PER STATUTE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            if (!aForm.xFoundAsText)
            {
                    if (valVal.Trim() != "")
                        aForm.xFoundAsText = true;
            }

            if (keyVal != "")
            {
                //aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            llx_sub1_1 = urx_sub1_1;
            urx_sub1_1 = llx_sub1_1 + widthCol3;
            rectRow4Col8LimitCol1_row1_3 = new Rectangle(llx_sub1_1, lly_sub1_1, urx_sub1_1, ury_sub1_1);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row1_3, aForm, OcrTypes.SingleChar);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            llx_sub1_1 = urx_sub1_1;
            urx_sub1_1 = llx_sub1_1 + widthCol4;
            rectRow4Col8LimitCol1_row1_4 = new Rectangle(llx_sub1_1, lly_sub1_1 - aForm.constantNRulesObj.specialCaseAdjustment, urx_sub1_1, ury_sub1_1);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row1_4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];
            keyVal = "OTHER";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            if (!aForm.xFoundAsText)
            {
                if (valVal.Trim() != "")
                    aForm.xFoundAsText = true;
            }


            if (keyVal != "")
            {
                //aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            llx_sub2 = urx_sub1;
            lly_sub2 = lly_sub1;
            urx_sub2 = llx_sub2 + col8_2Width;
            ury_sub2 = ury_sub1;
            rectRow4Col8LimitCol2_row1 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
           result = new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol2_row1, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            valVal = "";
            valValOcr = "";
            valValType3 = "";

            if (keyVal != "")
            {
                //aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow4Col8LimitCol1_row2 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1 - aForm.constantNRulesObj.specialCaseAdjustment);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row2, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "E.L. EACH ACCIDENT";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow4Col8LimitCol2_row2 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol2_row2, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ///
            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow4Col8LimitCol1_row3 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row3, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "E.L. DISEASE - EA EMPLOYEE";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow4Col8LimitCol2_row3 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegionValue(reader, rectRow4Col8LimitCol2_row3, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

            ///
            ury_sub1 = lly_sub1;
            lly_sub1 = ury_sub1 - sizeOfEachRowForCol8;
            rectRow4Col8LimitCol1_row4 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
          result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow4Col8LimitCol1_row4, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            keyVal = "E.L. DISEASE - POLICY LIMIT";
            keyValOcr = keyVal;
            keyValType3 = keyVal;

            ury_sub2 = ury_sub1;
            lly_sub2 = lly_sub1;
            rectRow4Col8LimitCol2_row4 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
         result =   new UtilityFn().ExtractTextFromRegionValue(reader, rectRow4Col8LimitCol2_row4, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "")
            {
                //aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow4Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }
        }

        private void RetrieveRow5Data(PdfWordCoordinates pdfWcmmDDyyyy, PdfWordCoordinates pdfWcinsr, float col1llx, float col1urx, float col3llx, float col3urx, float col4llx, float col4urx)
        {
            string[] result;
            string keyVal = "";
            string valVal = "";
            string keyValOcr = "";
            string valValOcr = "";
            
            string keyValType3 = "";
            string valValType3 = "";

            string descOfOperationsRegionDetectionText = "DESCRIPTION OF OPERATIONS / LOCATIONS / VEHICLES";
            PdfWordCoordinates pdfWcdescOfOperations = null;

            foreach (var v in aForm.liLinesSorted)
            {
                if (v.Word.ToUpper().Contains(descOfOperationsRegionDetectionText.ToUpper()))
                {
                    pdfWcdescOfOperations = v;
                    break;
                }
            }

            float bottom = pdfWcdescOfOperations.RectOuter.Top + aForm.constantNRulesObj.smallMoveBottom;
            float sizeOfEachRowForCol8 = (row5Height) / 1f;
            float llx, lly, urx, ury, lly_sub1, ury_sub1, llx_sub1, urx_sub1, lly_sub2, ury_sub2, llx_sub2, urx_sub2 = 0f;

            llx = col1llx;
            lly = bottom ;
            urx = llx + col1Width;
            ury = bottom + row5Height;
            rectRow5Col1Ltr = new Rectangle(col1llx, lly, col1urx, ury);
            
           result =  new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col1Ltr, aForm, OcrTypes.SingleChar);
            aForm.PolicyInsrLtrRow5 = result[0];
            aForm.acrdFrmForOcrObj.PolicyInsrLtrRow5 = result[1];
            aForm.acrdFrmForType3Obj.PolicyInsrLtrRow5 = result[2];

            llx = urx;
            urx = llx + col2Width;
            RetrieveRow5Column2Data(llx, lly, urx, ury, sizeOfEachRowForCol8, reader);

            llx = urx;
            urx = llx + col3Width;
            rectRow5Col3Insd = new Rectangle(col3llx, lly, col3urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col3Insd, aForm, OcrTypes.SingleChar);
            aForm.PolicyAddlInsrRow5 = result[0];
            aForm.acrdFrmForOcrObj.PolicyAddlInsrRow5 = result[1];
            aForm.acrdFrmForType3Obj.PolicyAddlInsrRow5 = result[2];

            llx = urx;
            urx = llx + col4Width;
            rectRow5Col4Wvd = new Rectangle(col4llx, lly, col4urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col4Wvd, aForm, OcrTypes.SingleChar);
            aForm.PolicySubrWvdRow5 = result[0];
            aForm.acrdFrmForOcrObj.PolicySubrWvdRow5 = result[1];
            aForm.acrdFrmForType3Obj.PolicySubrWvdRow5 = result[2];

            llx = urx;
            urx = llx + col5Width;
            rectRow5Col5PolicyNo = new Rectangle(llx - 0.5f, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col5PolicyNo, aForm, OcrTypes.SingleBlock);

            //result = result.Trim();
        
            aForm.PolicyNoRow5 = result[0].Trim();
            aForm.acrdFrmForOcrObj.PolicyNoRow5 = result[1].Trim();
            aForm.acrdFrmForType3Obj.PolicyNoRow5 = result[2].Trim();

            llx = urx;
            urx = llx + col6Width;
            rectRow5Col6PolicyEff = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col6PolicyEff, aForm, OcrTypes.SingleBlock);
            aForm.PolicEffRow5 = result[0];
            aForm.acrdFrmForOcrObj.PolicEffRow5 = result[1];
            aForm.acrdFrmForType3Obj.PolicEffRow5 = result[2];

            llx = urx;
            urx = llx + col7Width;
            rectRow5Col7PolicyExp = new Rectangle(llx, lly, urx, ury);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col7PolicyExp, aForm, OcrTypes.SingleBlock);
            aForm.PolicyExpRow5 = result[0];
            aForm.acrdFrmForOcrObj.PolicyExpRow5 = result[1];
            aForm.acrdFrmForType3Obj.PolicyExpRow5 = result[2];

            llx_sub1 = urx;
            lly_sub1 = bottom;
            urx_sub1 = llx_sub1 + col8_1Width;
            ury_sub1 = bottom + row5Height;
            rectRow5Col8LimitCol1_row1 = new Rectangle(llx_sub1, lly_sub1, urx_sub1, ury_sub1);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col8LimitCol1_row1, aForm, OcrTypes.SingleBlock);
            keyVal = result[0];
            keyValOcr = result[1];
            keyValType3 = result[2];

            llx_sub2 = urx_sub1;
            lly_sub2 = lly_sub1;
            urx_sub2 = llx_sub2 + col8_2Width;
            ury_sub2 = ury_sub1;
            rectRow5Col8LimitCol2_row1 = new Rectangle(llx_sub2, lly_sub2, urx_sub2, ury_sub2);
            result = new UtilityFn().ExtractTextFromRegion(reader, rectRow5Col8LimitCol2_row1, aForm, OcrTypes.SingleBlock);
            valVal = result[0];
            valValOcr = result[1];
            valValType3 = result[2];

            if (keyVal != "" || valVal != "")
            {
                //aForm.col8KeyValRow5Obj.Add(new Col8KeyVal(keyVal, valVal));
                //aForm.acrdFrmForOcrObj.col8KeyValRow5Obj.Add(new Col8KeyVal(keyValOcr, valValOcr));
                //aForm.acrdFrmForType3Obj.col8KeyValRow5Obj.Add(new Col8KeyVal(keyValType3, valValType3));
            }

        }

        private void RetrieveCol8Data()
        {
            List<Col8New> liRowA = new List<Col8New>();
            List<Col8New> liRowB = new List<Col8New>();
            List<Col8New> liRowC = new List<Col8New>();
            List<Col8New> liRowD = new List<Col8New>();
            List<Col8New> liRowE = new List<Col8New>();

          
            float Row2Top = 0;
            float Row3Top = 0;
            float Row4Top = 0;
            float Row4Bottom = 0;

            string Row2TopDetectionText = "COMBINED SINGLE LIMIT";
            string Row3TopDetectionText = "AGGREGATE";
            string Row4TopDetectionText = "OTH-";
            string Row4BottomDetectionText = "E.L. DISEASE - POLICY LIMIT";

            PdfWordCoordinates pdfWcRow2Top ;
            PdfWordCoordinates pdfWcRow3Top;
            PdfWordCoordinates pdfWcRow4Top;
            PdfWordCoordinates pdfWcRow4Bottom;

            pdfWcRow2Top = new UtilityFn().
               FindPdfWordCordinatesOfGivenTextEquals
               (aForm.liLinesSorted, Row2TopDetectionText);
            Row2Top = pdfWcRow2Top.RectOuter.Top;

            pdfWcRow3Top = new UtilityFn().
             FindPdfWordCordinatesOfGivenTextEquals
             (aForm.liLinesSorted, Row3TopDetectionText);
            Row3Top = pdfWcRow3Top.RectOuter.Top;

            pdfWcRow4Top = new UtilityFn().
             FindPdfWordCordinatesOfGivenTextEquals
             (aForm.liLinesSorted, Row4TopDetectionText);
            Row4Top = pdfWcRow4Top.RectOuter.Top;

            pdfWcRow4Bottom = new UtilityFn().
             FindPdfWordCordinatesOfGivenTextEquals
             (aForm.liLinesSorted, Row4BottomDetectionText);
            Row4Bottom = pdfWcRow4Bottom.RectOuter.Bottom;

            PdfWordCoordinates pdfWcmmDDyyyy = null;
            PdfWordCoordinates pdfWcdescOfOperations = null;
            PdfWordCoordinates pdfAllRightsReserved = null;

            Rectangle RectCol8;

            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            pdfWcmmDDyyyy = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, mmDDyyyyRegionDetectionText);

           
            pdfWcdescOfOperations = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, descOfOperationsRegionDetectionText);

            if (pdfWcdescOfOperations == null)
            {
                pdfWcdescOfOperations = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, descOfOperationsRegionDetectionText_b);

            }

            if (pdfWcdescOfOperations == null)
            {
                pdfWcdescOfOperations = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, descOfOperationsRegionDetectionText_c);

            }

            pdfAllRightsReserved = new UtilityFn().
               FindPdfWordCordinatesOfGivenTextEquals
               (aForm.liLinesSorted, allRightsReservedRegionDetectionText);

              RectCol8 = new Rectangle(pdfWcmmDDyyyy.RectOuter.Right, pdfWcdescOfOperations.RectOuter.Top, aForm.constantNRulesObj.pdfFileWidth, pdfWcmmDDyyyy.RectOuter.Bottom);
           // RectCol8 = new Rectangle(pdfWcmmDDyyyy.RectOuter.Right, pdfWcdescOfOperations.RectOuter.Top, 500, pdfWcmmDDyyyy.RectOuter.Bottom);
            var result = new UtilityFn().ExtractTextFromRegionForCol8(reader, RectCol8, aForm, OcrTypes.SingleBlock);

            var resultTemp = result;

            PdfWordCoordinates PdfWcOth = null;
            PdfWordCoordinates PdfWcstatute = null;
            foreach (var v in result)
            {
                //if (v.Word == "EACH OCCURRENCE")
                //{

                //}

                if (v.Word == "STATUTE" || v.Word == "TORY LIMITS")
                {
                    PdfWcstatute = v;
                }

                if (v.Word == "OTH-")
                {
                    PdfWcOth = v;
                }



            }

            if (PdfWcOth != null && PdfWcstatute != null)
            {
                Rectangle rectStatute = new Rectangle(pdfWcmmDDyyyy.RectOuter.Right, PdfWcstatute.RectOuter.Bottom, PdfWcstatute.RectOuter.Left, PdfWcOth.RectOuter.Top);
               string[] resultT = new UtilityFn().ExtractTextFromRegion(reader, rectStatute, aForm, OcrTypes.SingleChar);

                liRowD.Add(new Col8New()
                {
                    LeftVal = "PER STATUTE",
                    RightVal = resultT[0]
                });
                Rectangle rectOth = new Rectangle(PdfWcstatute.RectOuter.Right, PdfWcstatute.RectOuter.Bottom, PdfWcOth.RectOuter.Left, PdfWcOth.RectOuter.Top);
                resultT = new UtilityFn().ExtractTextFromRegion(reader, rectOth, aForm, OcrTypes.SingleChar);

                liRowD.Add(new Col8New()
                {
                    LeftVal = "OTHER",
                    RightVal = resultT[0]
                });
            }
         
            

            List<PdfWordCoordinates> liPdfStart = new List<PdfWordCoordinates>();
            List<PdfWordCoordinates> liPdfLast = new List<PdfWordCoordinates>();

            float FirstX = 0f;
            float LastX = 0f;

            System.Diagnostics.Debug.WriteLine("All");
            foreach (var v in result)
            {
                System.Diagnostics.Debug.WriteLine(v.Word + " > " + v.YStartCoordinate + " > " + v.YEndCoordinate + " > " + v.XStartCoordinate + " > " + v.XEndCoordinate + " > " + v.RectInner.Left + " > " + v.RectInner.Right);
            }
                foreach (var v in result)
            {
                if (FirstX == 0)
                    FirstX = v.XStartCoordinate;
                else
                {
                    if (v.XStartCoordinate <= FirstX)
                    {
                        DateTime temp;
                        if (DateTime.TryParse(v.Word.Trim(), out temp))
                        {
                            // Yay :)
                        }
                        else
                        {
                            FirstX = v.XStartCoordinate;
                            // Aww.. :(
                        }
                       // FirstX = v.XStartCoordinate;
                    }
                }

                


            }

            foreach (var v in result)
            {
                if (v.Word.Trim() == "$")
                {
                    LastX = v.RectOuter.Right;
                }

            }

            if (LastX == 0)
            {
                foreach (var v in result)
                {
                    if (v.Word.Trim().StartsWith("$"))
                    {
                        if ((LastX == 0) ||(v.RectOuter.Left <= LastX))
                            LastX = v.RectOuter.Left;
                    }

                }
            }

                foreach (var vv in result)
            {
                
                if ( (vv.XStartCoordinate > FirstX - 10f) && vv.XStartCoordinate < LastX + 10f)
                {
                    if (vv.Word.Trim() != "$")
                        liPdfStart.Add(vv);
                }

                if (vv.XStartCoordinate > (LastX - 10f) )
                {
                    if (vv.Word.Trim() != "$")
                    liPdfLast.Add(vv);
                }


            }
            int i = 0;
            System.Diagnostics.Debug.WriteLine("Start");

            //float AllowedDistance = liPdfStart[0].YStartCoordinate - liPdfStart[1].YStartCoordinate;
            foreach (var v in liPdfStart)
            {
                i++;
                System.Diagnostics.Debug.WriteLine(i.ToString() + " > " + v.Word);
            }

            System.Diagnostics.Debug.WriteLine("End");
            i = 0;
            foreach (var v in liPdfLast)
            {
                i++;
                System.Diagnostics.Debug.WriteLine(i.ToString() + " > " + v.Word);
            }

            List<Col8New> liTemp = new List<Col8New>();

            string[] strArrayIgnore = new string[]
            {
                "PREMISES",
                "Ea accident"
            };

            string[] strArrayIgnoreFullWord = new string[]
          {
                "PER",
                "OTH-",
                "ER",
                "STATUTE",
                "X",
                "(Per accident)"

          };

           liPdfStart.OrderByDescending(c => c.YStartCoordinate);

            float AllowedDistance = liPdfStart[liPdfStart.FindIndex(c => c.Word == "PERSONAL & ADV INJURY")].YStartCoordinate - liPdfStart[liPdfStart.FindIndex(c => c.Word == "GENERAL AGGREGATE")].YStartCoordinate;
            foreach (var v in liPdfStart)
            {
                bool ignore = false;
               foreach (string str in strArrayIgnore)
                {
                    if (v.Word.ToUpper().Contains(str.ToUpper()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                    continue;

                foreach (string str in strArrayIgnoreFullWord)
                {
                    if (v.Word.ToUpper().Equals(str.ToUpper()))
                    {
                        ignore = true;
                        break;
                    }
                }

                if (ignore)
                    continue;

                liTemp.Add(new Col8New() {
                    LeftVal = v.Word,
                    RightVal =  new UtilityFn().FindNearestWord(liPdfLast, v, AllowedDistance),
                    LeftValStartY = v.RectOuter.Bottom,
                    LeftValEndY = v.RectOuter.Top

                });
            }

            System.Diagnostics.Debug.WriteLine("Final values");
            foreach (var v in liTemp)
            {
                System.Diagnostics.Debug.WriteLine(v.LeftVal + " > " + v.RightVal);

            }
            List<Col8New> liTempTemp = new List<Col8New>();
            
            foreach (var v in liTemp)
            {
                var isNumeric = int.TryParse(v.LeftVal.Trim().Replace("$", "").Replace(",", ""), out int n);
                
                if (!isNumeric)
                {
                    if (v.LeftVal.Trim().Replace("$", "").Replace(",", "").Length > 1)
                    {
                        isNumeric = int.TryParse(v.LeftVal.Trim().Replace("$", "").Replace(",", "").Substring(0, 1), out int n1);
                }
                }
               
                if (!isNumeric)
                    liTempTemp.Add(v);
            }

            liTempTemp.OrderByDescending(c => c.LeftValStartY);

            // for (int j = liTempTemp.Count - 1; j > 0;  j--)
            for (int j = 0; j < liTempTemp.Count; j++)
            {
                if (((liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "EACH OCCURRENCE") && (liTempTemp[j].LeftValStartY > Row2Top))||
                    (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "DAMAGE TO RENTED") ||
                    (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "MED EXP (Any one person)") ||
                    (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "PERSONAL & ADV INJURY") ||
                    (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "GENERAL AGGREGATE") ||
                    (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "PRODUCTS - COMP/OP AGG"))
                {
                    liRowA.Add(liTempTemp[j]);
                }
                else

                if ((liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "COMBINED SINGLE LIMIT") ||
                   (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "BODILY INJURY (Per person)") ||
                   (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "BODILY INJURY (Per accident)") ||
                   (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "PROPERTY DAMAGE") 
                  )
                {
                    liRowB.Add(liTempTemp[j]);
                }
                else

                if (((liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "EACH OCCURRENCE") && (liTempTemp[j].LeftValStartY < Row2Top)) ||
                   (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "AGGREGATE") 
                  )
                {
                    liRowC.Add(liTempTemp[j]);
                }
                else
                if ((liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "E.L. EACH ACCIDENT") ||
                   (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "E.L. DISEASE - EA EMPLOYEE") ||
                    (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "E.L. DISEASE -EA EMPLOYEE") ||
                   (liTempTemp[j].LeftVal.Trim().Trim('$').Trim() == "E.L. DISEASE - POLICY LIMIT") 
                   )
                {
                    liRowD.Add(liTempTemp[j]);
                }
                else 
                    if ((liTempTemp[j].LeftValEndY > Row2Top))
                {
                    liRowA.Add(liTempTemp[j]);
                } else
                    if ((liTempTemp[j].LeftValEndY > Row3Top) && (liTempTemp[j].LeftValEndY < Row2Top))
                {
                    liRowB.Add(liTempTemp[j]);
                } else
                    if ((liTempTemp[j].LeftValEndY > Row4Top) && (liTempTemp[j].LeftValEndY < Row3Top))
                {
                    liRowC.Add(liTempTemp[j]);
                } else
                     if ((liTempTemp[j].LeftValEndY < Row4Bottom) )
                {
                    liRowE.Add(liTempTemp[j]);
                }




            }

            foreach(var v in liRowA)
            {
                aForm.col8KeyValRow1Obj.Add(new Col8KeyVal(v.LeftVal.Trim().Trim('$'), v.RightVal));

                System.Diagnostics.Debug.WriteLine("Row A" + v.LeftVal + " > " + v.RightVal);
            }

            foreach (var v in liRowB)
            {
                aForm.col8KeyValRow2Obj.Add(new Col8KeyVal(v.LeftVal.Trim().Trim('$'), v.RightVal));
                System.Diagnostics.Debug.WriteLine("Row B" + v.LeftVal + " > " + v.RightVal);
            }

            foreach (var v in liRowC)
            {
                aForm.col8KeyValRow3Obj.Add(new Col8KeyVal(v.LeftVal.Trim().Trim('$'), v.RightVal));
                System.Diagnostics.Debug.WriteLine("Row C" + v.LeftVal + " > " + v.RightVal);
            }

            foreach (var v in liRowD)
            {
                aForm.col8KeyValRow4Obj.Add(new Col8KeyVal(v.LeftVal.Trim().Trim('$'), v.RightVal));
                System.Diagnostics.Debug.WriteLine("Row D" + v.LeftVal + " > " + v.RightVal);
            }

            foreach (var v in liRowE)
            {
                aForm.col8KeyValRow5Obj.Add(new Col8KeyVal(v.LeftVal.Trim().Trim('$'), v.RightVal));
                System.Diagnostics.Debug.WriteLine("Row E" + v.LeftVal + " > " + v.RightVal);
            }
        }

        //public string GetInsuredTextFromRegion(Rectangle rect)
        //{
        //    string result = new UtilityFn().ExtractTextFromRegion(reader, rect.Left, rect.Bottom, rect.Right, rect.Top, aForm);
        //    return result;
        //}
    }

    public class Col8
    {
        public string LeftVal { get; set; }
        public string RightVal { get; set; }
    }

    public class Col8New
    {
        public string LeftVal { get; set; }
        public string RightVal { get; set; }

        public float LeftValStartY { get; set; }
        public float LeftValEndY { get; set; }
        public float RighttValStartY { get; set; }
        public float RightValEndY { get; set; }

    }
}



