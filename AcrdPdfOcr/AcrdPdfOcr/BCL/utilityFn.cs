
using AcrdPdfOcr.BCL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class UtilityFn
    {
        public string[] ExtractTextFromRegion(PdfReader reader, float lowerLeftX, float lowerLeftY, float upperRightX, float upperRightY, AccordForm aForm, bool AuthRepImageCheckRequired = false)
        {
           // PdfReader reader = null;
            try
            {
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);
              //  reader = new PdfReader(pdfFilePath);
                RenderFilter[] filter = { new RegionTextRenderFilter(rect) };
                ITextExtractionStrategy strategy;
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= 1; i++)
                {
                  //  VectorGraphicsListener listener = new VectorGraphicsListener();

                   // PdfReaderContentParser parser2 = new PdfReaderContentParser(reader);
                  //  parser2.ProcessContent(1, listener);

                    // strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                    strategy = new FilteredTextRenderListener(new RectAreaTextExtractionStrategy(rect, aForm, AuthRepImageCheckRequired), filter);
                    sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, staticConst.CurrentPdfPageNo, strategy));
                    PdfReaderContentParser parser = new PdfReaderContentParser(reader);
                    //strategy.
                    //iTextSharp.text.pdf.parser.Vector v = 
                }
                string type3Pdf = ReadType3Pdf(rect, aForm);

                string[] output = new string[3];
                output[0] = sb.ToString().Trim();
               // output[0] = "";
                output[1] = "";
                output[2] = type3Pdf.Trim();

                return output;
            }
            finally 
            {
              //  reader.Close();
              //  reader.Dispose();
            }
        }

        public string[] ExtractTextFromRegion(PdfReader reader, Rectangle rectPara, AccordForm aForm, OcrTypes type )
        {

          //  PdfReader reader = null;
            try
            {
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(rectPara.Left, rectPara.Bottom, rectPara.Right, rectPara.Top);
              //  reader = new PdfReader(pdfFilePath);
                RenderFilter[] filter = { new RegionTextRenderFilter(rect) };
                ITextExtractionStrategy strategy;
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= 1; i++)
                {
                    //strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                    strategy = new FilteredTextRenderListener(new RectAreaTextExtractionStrategy(rect, aForm), filter);
                    sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, staticConst.CurrentPdfPageNo, strategy));
                }
                //Console.WriteLine(sb.ToString());
                string type3Pdf = ReadType3Pdf(rect, aForm);
                string[] output = new string[3];
                output[0] = sb.ToString().Trim();
                //output[0] = "";
                output[1] = "";
                output[2] = type3Pdf.Trim();

                return output;

            }
            finally
            {
              //  reader.Close();
              //  reader.Dispose();
            }

           
        }


        public List<PdfWordCoordinates> ExtractTextFromRegionForCol8(PdfReader reader, Rectangle rectPara, AccordForm aForm, OcrTypes type)
        {

            //  PdfReader reader = null;
            try
            {
                List<PdfWordCoordinates> liResult = new List<PdfWordCoordinates>();

                foreach (var v in aForm.liLinesSorted)
                {
                    if ((v.RectOuter.Left >= rectPara.Left) && (v.RectOuter.Top <= rectPara.Top) && (v.RectOuter.Bottom >= rectPara.Bottom))
                    {
                        liResult.Add(v);
                    }
                    // rectpara
                    
                   
                }
                return liResult;
             //  return aForm.liLinesSorted;

                RectAreaTextExtractionStrategy rctTxtExtStrategy = null;
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(rectPara.Left, rectPara.Bottom, rectPara.Right, rectPara.Top);
                //  reader = new PdfReader(pdfFilePath);
                RegionTextRenderFilter rgnTextRender = new RegionTextRenderFilter(rect);
                RenderFilter[] filter = {  rgnTextRender};
                ITextExtractionStrategy strategy;
                
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= 1; i++)
                {
                    //strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                    rctTxtExtStrategy = new RectAreaTextExtractionStrategy(rect, aForm);
                    strategy = new FilteredTextRenderListener(rctTxtExtStrategy, filter);
                    sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, staticConst.CurrentPdfPageNo, strategy));
                }
                //Console.WriteLine(sb.ToString());
                string type3Pdf = ReadType3Pdf(rect, aForm);
                string[] output = new string[3];
                output[0] = sb.ToString().Trim();
                //output[0] = "";
                output[1] = "";
                output[2] = type3Pdf.Trim();

                return rctTxtExtStrategy.liPdfCoordinates;
               // return output;

            }
            finally
            {
                //  reader.Close();
                //  reader.Dispose();
            }


        }


        public bool compareType3PdfHtWd(float v1, float v2, float lenOrWid)
        {
            if (v1 == v2)
                return false;

           // float toleranceValue = 0.5f;

            //if (lenOrWid < 20f)
            //    ma = 0.05f;

            float margin = (10f / 100f) * lenOrWid;

            if(v1 > v2)
            {
                if (v1 - v2 <= margin)
                    return true;
                else
                    return false;
            }
            else
            {
                if (v2 - v1 <= margin)
                    return true;
                else
                    return false;
            }
        }
        public string ReadType3Pdf(Rectangle rect, AccordForm aForm)
        {
            float toleranceValue = 0.5f;
            string matchVal = "";
            int matchCount = 0;
            StringBuilder sb = new StringBuilder();
           foreach (var v in aForm.liPdfType3Obj)
            {

                float width = rect.Width;
                float ht = rect.Height;

                float leftPdf = rect.Left;
                float rightPdf = rect.Right;
                float topPdf = rect.Top;
                float bottomPdf = rect.Bottom;

                float leftType3 = v.rect.Left;
                float rightType3 = v.rect.Right;
                float topType3 = v.rect.Top;
                float bottomType3 = v.rect.Bottom;

                //if (rightPdf - leftPdf < 20f)
                //    toleranceValue = 0.05f;

                int successCount = 0;

                if (leftPdf <= leftType3)
                    successCount++;
                else if (compareType3PdfHtWd(leftPdf, leftType3, width))
                    successCount++;


                if (rightPdf >= rightType3)
                    successCount++;
                else if (compareType3PdfHtWd(rightPdf, rightType3, width))
                    successCount++;

                if (topPdf >= topType3)
                    successCount++;
                else if (compareType3PdfHtWd(topPdf, topType3, ht))
                    successCount++;

                if (bottomPdf <= bottomType3)
                    successCount++;
                else if (compareType3PdfHtWd(bottomPdf, bottomType3, ht))
                    successCount++;




             //   if ((leftPdf <= leftType3) && (rightPdf >= rightType3) && (topPdf >= topType3) && (bottomPdf <= bottomType3))
                //    if ((leftPdf <= leftType3 + toleranceValue) && (rightPdf >= rightType3 - toleranceValue) && (topPdf >= topType3 - toleranceValue) && (bottomPdf <= bottomType3 + toleranceValue))
               // if ((compareType3PdfHtWd(leftPdf , leftType3,width )) && (compareType3PdfHtWd(rightPdf , rightType3 , width)) && (compareType3PdfHtWd(topPdf, topType3, ht)) && (compareType3PdfHtWd(bottomPdf ,bottomType3, ht)))
               if(successCount == 4)
                {
                    matchCount++;
                    matchVal = v.Value;
                    sb.Append(matchVal + " ");
                }
              //  else

            }

            matchVal = sb.ToString().Trim();
           
            return matchVal;
        }

     
        public string[] ExtractTextFromRegionValue(PdfReader reader, Rectangle rectPara, AccordForm aForm, OcrTypes type)
        {
          //  PdfReader reader = null;
            try
            {
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(rectPara.Left, rectPara.Bottom, rectPara.Right, rectPara.Top);
              //  reader = new PdfReader(pdfFilePath);
                RenderFilter[] filter = { new RegionTextRenderFilter(rect) };
                ITextExtractionStrategy strategy;
                StringBuilder sb = new StringBuilder();
                for (int i = 1; i <= 1; i++)
                {

                    //  strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                    strategy = new FilteredTextRenderListener(new RectAreaTextExtractionStrategy(rect, aForm), filter);
                    sb.AppendLine(PdfTextExtractor.GetTextFromPage(reader, staticConst.CurrentPdfPageNo, strategy));
                }
                Console.WriteLine(sb.ToString());
                string result = "";
                result = sb.ToString().Trim().Replace("$", "");
                result = "$ " + result;

               
                string type3Pdf = ReadType3Pdf(rect, aForm);

                string[] output = new string[3];
                output[0] = result;
                //output[0] = "";
                output[1] = "";
                output[2] = type3Pdf.Trim();
                return output;

            }
            finally
            {
             //   reader.Close();
              //  reader.Dispose();
            }
        }

        public void InitializePageDimensions(PdfReader reader, AccordForm aForm)
        {
           // PdfReader reader = null;
            try
            {
                //reader = new PdfReader(System.IO.File.ReadAllBytes(pdfFilePath));
                Rectangle mediabox = reader.GetPageSize(1);
                aForm.constantNRulesObj.pdfFileHeight = mediabox.Height;
                aForm.constantNRulesObj.pdfFileWidth = mediabox.Width;

                int rotation = reader.GetPageRotation(1);
                Rectangle pagesize = reader.GetPageSizeWithRotation(1);
                Rectangle cropbox = reader.GetCropBox(1);

                aForm.constantNRulesObj.tiffImageExtension = "jpg";
            }
            finally
            {
               // reader.Close();
               // reader.Dispose();
            }
        }

        public string RuleForCol1InsrLtr(string input)
        {
            return input;
            string output = Regex.Replace(input, "[^abcdef]", string.Empty, RegexOptions.IgnoreCase);
            return output;
        }

        public string RuleForCol3AddlInsd(string val)
        {
            return val;
            string output = Regex.Replace(val, "[^x ]", string.Empty, RegexOptions.IgnoreCase);
            if (output.Length > 1)
                output = output.Substring(0, 1);
            return output;
        }

        public string RuleForCol4SubrWvd(string val)
        {
            return val;
            string output = Regex.Replace(val, "[^x ]", string.Empty, RegexOptions.IgnoreCase);

            if (output.Length > 1)
                output = output.Substring(1, 1);

            return output;
        }

        public string RuleForCol3AddlInsdRow4(string val)
        {
            return val;
            StringBuilder sb = new StringBuilder();
            foreach (string str in val.Split('\n'))
            {

            }
            return sb.ToString();
        }

        public PdfWordCoordinates FindPdfWordCordinatesOfGivenTextContains(List<PdfWordCoordinates> liLinesSorted, string textToDetect, float vertPos = 0, float horPos = 0, float vertTolerance = 0, float horTolerance = 0)
        {
            PdfWordCoordinates pdfWordCordinate = null;
            foreach (var v in liLinesSorted)
            {
                if (v.Word.ToUpper().Contains(textToDetect.ToUpper()))
                {
                    pdfWordCordinate = v;
                    break;
                }
            }

            return pdfWordCordinate;
        }

        public PdfWordCoordinates FindPdfWordCordinatesOfGivenTextEquals(List<PdfWordCoordinates> liLinesSorted, string textToDetect, float vertPos = 0, float horPos = 0, float vertTolerance = 0, float horTolerance = 0)
        {
            PdfWordCoordinates pdfWordCordinate = null;
            System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());

            foreach (var v in liLinesSorted)
            {
                System.Diagnostics.Debug.WriteLine(v.Word);
                if (v.Word.ToUpper().Trim() == textToDetect.ToUpper().Trim())
                {
                    pdfWordCordinate = v;
                    break;
                }
            }

            if (textToDetect == "(MM/DD/YYYY) (MM/DD/YYYY)")
            {
                if (pdfWordCordinate == null)
                {
                    //"INSR WVD"

                    bool activate = false;
                    foreach (var v in liLinesSorted)
                    {
                        System.Diagnostics.Debug.WriteLine(v.Word);
                        if (v.Word.ToUpper().Trim() == "INSR WVD")
                        {
                            activate = true;
                        }
                        if (activate)
                        {
                            if (v.Word.ToUpper().Trim() == "(MM/DD/YYYY)".ToUpper().Trim())
                            {
                                pdfWordCordinate = v;
                                break;
                            }
                        }

                    }
                }
            }
          

            return pdfWordCordinate;
        }


        public float DistanceBetweenTwoPoints(float first, float second)
        {
            if (first == second)
                return 0;
            else if (first > second)
                return first - second;
            else
                return second - first;

        }
        public string FindNearestWord(List<PdfWordCoordinates> liPdfWc, PdfWordCoordinates wordToFind, float AllowedDistance)
        {
            AllowedDistance = 2f;
            PdfWordCoordinates pdfTemp = null;
            float currentDiff = 10000f;

            foreach (var v in liPdfWc)
            {
                if (pdfTemp != null)
                {
                  if ( DistanceBetweenTwoPoints(v.YStartCoordinate, wordToFind.YStartCoordinate) <  currentDiff)
                    {
                        pdfTemp = v;
                        currentDiff = DistanceBetweenTwoPoints(v.YStartCoordinate, wordToFind.YStartCoordinate);
                    }
                }
                else
                {
                    pdfTemp = v;
                    currentDiff = DistanceBetweenTwoPoints(v.YStartCoordinate, wordToFind.YStartCoordinate);
                }
            }

            //if ((currentDiff > AllowedDistance - 2f) && (currentDiff < AllowedDistance + 2))
            //    return pdfTemp.Word;
            //else
            //    return "";
            if (currentDiff <= AllowedDistance)
                return pdfTemp.Word;
            else
            {
                foreach (var v in liPdfWc)
                {
                   if (((wordToFind.RectOuter.Bottom >=   (v.RectOuter.Bottom )) && (wordToFind.RectOuter.Bottom <= (v.RectOuter.Top + 1))) || ((wordToFind.RectOuter.Top >= (v.RectOuter.Bottom - 1)) && (wordToFind.RectOuter.Top <= (v.RectOuter.Top + 1))))
                    {
                        return v.Word;
                    }
                }
            }
                return "";
          
        }

        public string RemoveDigitsFromStartOnly(string input)
        {
           
            int cutUpto = 0;
            foreach (char chr in input)
            {
                if (char.IsDigit(chr))
                {
                    break;
                } else
                {
                    cutUpto++;
                }
                   


            }

            string result = input.Substring(cutUpto, input.Length - cutUpto);
            return result;
        }
        
    }
}
