using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class RectAreaTextExtractionStrategy : ITextExtractionStrategy
    {
        #region Variable Declaration

        public List<byte[]> Images = new List<byte[]>();
        public List<string> ImageNames = new List<string>();

        iTextSharp.text.Rectangle rect;
      public  List<PdfWordCoordinates> liPdfCoordinates = new List<PdfWordCoordinates>();
        public List<PdfWordCoordinates> liLines;
        public List<PdfWordCoordinates> liLinesSorted;
        AccordForm aForm;
        bool AuthRepImageCheckRequired = false;
        bool imageAttached = false;
        int noOfImagesFound = 0;
        #endregion

        public StringBuilder Text { get; set; }

        public RectAreaTextExtractionStrategy(iTextSharp.text.Rectangle rect, AccordForm aForm, bool AuthRepImageCheckRequired = false)
        {
            this.rect = rect;
            this.aForm = aForm;
            liLines = new List<PdfWordCoordinates>();
            liLinesSorted = new List<PdfWordCoordinates>();
            Text = new StringBuilder();
            this.AuthRepImageCheckRequired = AuthRepImageCheckRequired;
        }
        
        public void BeginTextBlock()
        {
            // Text.Append("<");
        }
        public void EndTextBlock()
        {
            //Text.AppendLine(">");
            Text.AppendLine();
        }

      
        public string GetResultantText()
        {
            SortWordCoordinatesByY();
            StringBuilder b = new StringBuilder();
            foreach (var v in liLinesSorted)
            {
                b.AppendLine(v.Word);
            }

            if (AuthRepImageCheckRequired)
                if (imageAttached && (noOfImagesFound >= 1))
                {
                    aForm.isImageAttached = true;
                }

            int count = 0;
            count = b.ToString().Split(',').Count();
            if(count == 3)
            {
                if(b.ToString().Contains(",000,000"))
                {
                    return b.ToString().Trim();
                }
                else if (b.ToString().Contains(",000,00"))
                {
                    string result = b.ToString().Replace(",000,00", ",000,000");
                    return result.Trim();
                }
                else
                    return b.ToString().Trim();

            }
            else
                return b.ToString().Trim();
        }

       
        public  void RenderText(TextRenderInfo renderInfo)
        {

            bool boolHighPercentOverlap = false;
            LineSegment segment1 = renderInfo.GetBaseline();
            Vector start1 = segment1.GetStartPoint();
            Vector end1 = segment1.GetEndPoint();

            LineSegment segmentAscent1 = renderInfo.GetAscentLine();
            Vector startAscent1 = segmentAscent1.GetStartPoint();
            Vector endAscent1 = segmentAscent1.GetEndPoint();

            LineSegment segmentDescent1 = renderInfo.GetDescentLine();
            Vector startDescent1 = segmentDescent1.GetStartPoint();
            Vector endDescent1 = segmentDescent1.GetEndPoint();

            int trueCount = 0;
            bool baseLine = false;

            if ((startAscent1[Vector.I2] >= rect.Bottom) && (startAscent1[Vector.I2] <= rect.Top))
                trueCount++;

            if (((startDescent1[Vector.I2] >= rect.Bottom) && (startDescent1[Vector.I2] <= rect.Top)))
                trueCount++;

            if (((start1[Vector.I2] >= rect.Bottom) && (start1[Vector.I2] <= rect.Top))) // baseLine
            {
                baseLine = true;
            }

            float regionA1Len, regionA4Len;

            
            regionA4Len = end1[Vector.I1] - rect.Left;
            regionA1Len = end1[Vector.I1] - start1[Vector.I1] - regionA4Len;
            float percentageOverlap = 0;
            percentageOverlap = (regionA4Len / (regionA1Len + regionA4Len)) * 100f;

           if ((percentageOverlap > 90) && (percentageOverlap <120))
            {
                if(renderInfo.GetText().Length > 3)
                {
                    boolHighPercentOverlap = true;
                }
                
            }
            if (((start1[Vector.I1] >= rect.Left) && (end1[Vector.I1] <= rect.Right)) || (boolHighPercentOverlap))
            {
                if(baseLine && trueCount >=1)
                {
                    PdfWordCoordinates word = new PdfWordCoordinates();
                    Text.Append("<");
                    Text.Append(renderInfo.GetText());
                    word.Word = renderInfo.GetText();

                    LineSegment segment = renderInfo.GetBaseline();
                    Vector start = segment.GetStartPoint();
                    Text.Append("| xStart=");
                    Text.Append(start[Vector.I1]);
                    word.XStartCoordinate = start[Vector.I1];

                    Text.Append("; yStart=");
                    Text.Append(start[Vector.I2]);
                    word.YStartCoordinate = start[Vector.I2];
                    Text.Append(">");
                    word.SerialNo = liPdfCoordinates.Count + 1;

                    Vector end = segment.GetEndPoint();
                    Text.Append("| xEnd=");
                    Text.Append(end[Vector.I1]);
                    word.XEndCoordinate = end[Vector.I1];

                    Text.Append("; yEnd=");
                    Text.Append(end[Vector.I2]);
                    word.YEndCoordinate = end[Vector.I2];
                    Text.Append(">");

                    var bottomLeft = renderInfo.GetDescentLine().GetStartPoint();
                    var topRight = renderInfo.GetAscentLine().GetEndPoint();
                    var rect = new iTextSharp.text.Rectangle(
                                                        bottomLeft[Vector.I1],
                                                        bottomLeft[Vector.I2],
                                                        topRight[Vector.I1],
                                                        topRight[Vector.I2]
                                                        );

                    word.RectInner = rect;

                    bottomLeft = renderInfo.GetBaseline().GetStartPoint();
                    topRight = renderInfo.GetAscentLine().GetEndPoint();
                    rect = new iTextSharp.text.Rectangle(
                                                       bottomLeft[Vector.I1],
                                                       bottomLeft[Vector.I2],
                                                       topRight[Vector.I1],
                                                       topRight[Vector.I2]
                                                       );
                    word.CharacterSpacing = renderInfo.GetSingleSpaceWidth();
                    word.VectorStart = start;
                    word.VectorEnd = end;
                    word.RectOuter = rect;

                    PdfWordCoordinates res = liPdfCoordinates.Find(w => (w.XStartCoordinate == word.XStartCoordinate) && (w.YStartCoordinate == word.YStartCoordinate) && (w.Word == word.Word) && (w.XEndCoordinate == word.XEndCoordinate) && (w.YEndCoordinate == word.YEndCoordinate));
                    if (res == null)
                    {
                        liPdfCoordinates.Add(word);
                    }
                }
            }
            else
            {
                if ((((start1[Vector.I1] <= rect.Left) && ((end1[Vector.I1] <= rect.Right) && (end1[Vector.I1] >= rect.Left)) && ((end1[Vector.I1] - rect.Left) > 5f)))
                    ||
                        (((start1[Vector.I1] >= rect.Left) && ((end1[Vector.I1] >= rect.Left)))))
                {
                    if (liPdfCoordinates.Count != 0)
                        return;
                    //System.Diagnostics.Debug.WriteLine(renderInfo.GetText() + " > " + liPdfCoordinates.Count);
                    //return;

                    int count = renderInfo.GetCharacterRenderInfos().Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i <= count - 1; i++)
                    {
                        float start_ = renderInfo.GetCharacterRenderInfos()[i].GetBaseline().GetStartPoint()[Vector.I1];
                        float end_ = renderInfo.GetCharacterRenderInfos()[i].GetBaseline().GetEndPoint()[Vector.I1];
                        if (start_ < rect.Left || end_ > rect.Right)
                        {

                        }
                        else
                        {
                            sb.Append(renderInfo.GetCharacterRenderInfos()[i].GetText());
                        }

                    }
                    if (sb.Length > 0)
                    {
                        liPdfCoordinates.Clear();
                        PdfWordCoordinates word = new PdfWordCoordinates();
                        word.Word = sb.ToString();
                        liPdfCoordinates.Add(word);
                    }
                    else
                    {
                       // System.Diagnostics.Debug.WriteLine(renderInfo.GetText());
                    }
                }
            }
           
            
        }

        public  void RenderImage(ImageRenderInfo renderInfo)
        {

            if (AuthRepImageCheckRequired)
            {
                var ctm = renderInfo.GetImageCTM();
                var ctmWidth = ctm[iTextSharp.text.pdf.parser.Matrix.I11];
                var ctmHeight = ctm[iTextSharp.text.pdf.parser.Matrix.I22];
                var xLocation = ctm[iTextSharp.text.pdf.parser.Matrix.I31];
                var yLocation = ctm[iTextSharp.text.pdf.parser.Matrix.I32];

                //if((( xLocation > rect.Left) && (xLocation < rect.Right)) && ((xLocation > rect.Left) && (xLocation < rect.Right)) && (ctmWidth > 0 && ctmHeight > 0))
                if (((xLocation > rect.Left) && (xLocation < rect.Right)) && ((xLocation > rect.Left) && (xLocation < rect.Right)) && (ctmWidth != 0 && ctmHeight != 0))
                {
                        imageAttached = true;
                        noOfImagesFound++;
                    }
            }
        }

        public void SortWordCoordinatesByY()
        {
            liPdfCoordinates = liPdfCoordinates.OrderByDescending(o => o.YStartCoordinate).ThenBy(o => o.XStartCoordinate).ToList();
            float currentLineY = 0;
            float lastX = 0;
            Vector vectorLastX = null;
            string currentLineStr = "";
            PdfWordCoordinates sorted = null;
            int count = 0;
            foreach (var v in liPdfCoordinates)
            {
                if (v.Word == "")
                    continue;

                count++;
                if (currentLineY == 0)
                {
                    sorted = new PdfWordCoordinates();
                    if (lastX != 0)
                    {
                        if (v.XStartCoordinate - lastX > aForm.constantNRulesObj.characterXMargin)
                        {
                            v.LiWords.Add(v.Word);
                            sorted.LiPdfCordinates.Add(v);
                            currentLineStr += " >> " + v.Word; // same line, new word
                        }
                        else
                        {
                            currentLineStr += v.Word;
                            sorted.LiWords.Add(v.Word);
                            v.LiWords.Add(v.Word);
                            sorted.LiPdfCordinates.Add(v);
                        }
                    }
                    else
                    {
                        sorted.XStartCoordinate = v.XStartCoordinate;
                        sorted.YStartCoordinate = v.YStartCoordinate;
                        sorted.XEndCoordinate = v.XStartCoordinate;
                        sorted.YEndCoordinate = v.YStartCoordinate;
                        sorted.SerialNo = liLinesSorted.Count + 1;
                        sorted.LiWords.Add(v.Word);
                        sorted.LiPdfCordinates.Add(v);
                        v.LiWords.Add(v.Word);
                        currentLineStr += v.Word;
                    }

                    sorted.XEndCoordinate = v.XStartCoordinate;
                    sorted.YEndCoordinate = v.YStartCoordinate;
                    currentLineY = v.YStartCoordinate;
                    //lastX = (this.constantNRulesObj.characterXMargin * v.word.Length) + v.xStartCoordinate;
                    lastX = v.XEndCoordinate;
                    vectorLastX = v.VectorEnd;
                    continue;
                }

                if (v.YStartCoordinate == currentLineY)
                {
                    if (lastX != 0)
                    {
                        if (v.XStartCoordinate - lastX > aForm.constantNRulesObj.characterXMargin)
                        {
                            // new sentence
                            liLines.Add(v);
                            sorted.Word = currentLineStr;
                            sorted.SerialNo = liLinesSorted.Count + 1;
                            sorted.LengthOfWord = aForm.constantNRulesObj.characterXMargin * sorted.Word.Length;
                            sorted.NoOfCharsInWord = sorted.Word.Length;

                            sorted.RectInner = new iTextSharp.text.Rectangle(sorted.LiPdfCordinates[0].RectInner.Left, sorted.LiPdfCordinates[0].RectInner.Bottom, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectInner.Right, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectInner.Top);
                            sorted.RectOuter = new iTextSharp.text.Rectangle(sorted.LiPdfCordinates[0].RectOuter.Left, sorted.LiPdfCordinates[0].RectOuter.Bottom, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectOuter.Right, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectOuter.Top);

                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            sorted.Word = regex.Replace(sorted.Word, " ");

                            liLinesSorted.Add(sorted);

                            sorted = new PdfWordCoordinates();
                            sorted.XStartCoordinate = v.XStartCoordinate;
                            sorted.YStartCoordinate = v.YStartCoordinate;
                            sorted.XEndCoordinate = v.XStartCoordinate;
                            sorted.YEndCoordinate = v.YStartCoordinate;
                            sorted.SerialNo = liLinesSorted.Count + 1;
                            sorted.LiWords.Add(v.Word);
                            sorted.LiPdfCordinates.Add(v);
                            v.LiWords.Add(v.Word);

                            currentLineStr = v.Word;
                        }
                        else
                        {
                            float spacing = vectorLastX.Subtract(v.VectorStart).Length;
                            if (spacing > v.CharacterSpacing / 2f)
                            //if (v.xStartCoordinate - lastX > this.constantNRulesObj.addSpaceIfMargin)
                            {
                                sorted.LiWords.Add(v.Word);
                                sorted.LiPdfCordinates.Add(v);
                                if (v.Word.StartsWith(" "))
                                {
                                    currentLineStr += v.Word;
                                }
                                else
                                {
                                    currentLineStr += " " + v.Word;
                                }

                            }
                            else
                            {
                                sorted.LiWords.Add(v.Word);
                                sorted.LiPdfCordinates.Add(v);
                                currentLineStr += v.Word;
                            }
                        }
                    }
                    else
                    {
                        currentLineStr += v.Word;
                    }
                }
                else
                {
                    if (currentLineStr != "")
                    {
                        // new line 
                        liLines.Add(v);
                        sorted.Word = currentLineStr;
                        sorted.SerialNo = liLinesSorted.Count + 1;
                        sorted.LengthOfWord = aForm.constantNRulesObj.characterXMargin * sorted.Word.Length;
                        sorted.NoOfCharsInWord = sorted.Word.Length;

                        sorted.RectInner = new iTextSharp.text.Rectangle(sorted.LiPdfCordinates[0].RectInner.Left, sorted.LiPdfCordinates[0].RectInner.Bottom, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectInner.Right, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectInner.Top);
                        sorted.RectOuter = new iTextSharp.text.Rectangle(sorted.LiPdfCordinates[0].RectOuter.Left, sorted.LiPdfCordinates[0].RectOuter.Bottom, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectOuter.Right, sorted.LiPdfCordinates[sorted.LiPdfCordinates.Count - 1].RectOuter.Top);

                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);
                        sorted.Word = regex.Replace(sorted.Word, " ");

                        liLinesSorted.Add(sorted);

                        sorted = new PdfWordCoordinates();
                        sorted.XStartCoordinate = v.XStartCoordinate;
                        sorted.YStartCoordinate = v.YStartCoordinate;
                        sorted.XEndCoordinate = v.XStartCoordinate;
                        sorted.YEndCoordinate = v.YStartCoordinate;
                        sorted.SerialNo = liLinesSorted.Count + 1;
                        sorted.LiWords.Add(v.Word);
                        sorted.LiPdfCordinates.Add(v);
                        v.LiWords.Add(v.Word);

                        currentLineStr = v.Word;
                    }
                }
                sorted.XEndCoordinate = v.XStartCoordinate;
                sorted.YEndCoordinate = v.YStartCoordinate;

                currentLineY = v.YStartCoordinate;
                lastX = v.XEndCoordinate;
                vectorLastX = v.VectorEnd;
                //lastX = (this.constantNRulesObj.characterXMargin * v.word.Length) + v.xStartCoordinate;
            }

            if (currentLineStr != "")
            {
                sorted = new PdfWordCoordinates();
                sorted.Word = currentLineStr;
                sorted.SerialNo = liLinesSorted.Count + 1;
                sorted.LengthOfWord = aForm.constantNRulesObj.characterXMargin * sorted.Word.Length;
                sorted.NoOfCharsInWord = sorted.Word.Length;

                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                sorted.Word = regex.Replace(sorted.Word, " ");
                liLinesSorted.Add(sorted);

            }

            //Console.WriteLine("**********************************");
            //foreach (var v1 in liLinesSorted)
            //{
            //    Console.WriteLine(v1.xStartCoordinate.ToString("000.00000") + " , " + v1.yStartCoordinate.ToString("000.00000") + " > " + v1.word);
            //}
        }
    }
}
