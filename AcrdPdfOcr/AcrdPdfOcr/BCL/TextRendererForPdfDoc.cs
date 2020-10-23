using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class TextRendererForPdfDoc : IRenderListener
    {
        #region Variable Declaration
        public StringBuilder Text { get; set; }
        AccordForm aForm;
        #endregion

        public TextRendererForPdfDoc(AccordForm aForm)
        {
            Text = new StringBuilder();
            this.aForm = aForm;
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

        public void RenderImage(ImageRenderInfo renderInfo)
        {
         //   Console.WriteLine(renderInfo.GetImage().GetDrawingImage().Height);
         //   Console.WriteLine(renderInfo.GetImage().GetDrawingImage().Width);
            Console.WriteLine("***********");
        }
       
        public void RenderText(TextRenderInfo renderInfo )
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
            word.SerialNo = aForm.liPdfCoordinates.Count + 1;

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

            PdfWordCoordinates res = aForm.liPdfCoordinates.Find(w => (w.XStartCoordinate == word.XStartCoordinate) && (w.YStartCoordinate == word.YStartCoordinate) && (w.Word == word.Word) && (w.XEndCoordinate == word.XEndCoordinate) && (w.YEndCoordinate == word.YEndCoordinate));
            if (res == null)
            {
                aForm.liPdfCoordinates.Add(word);
            }
        }
    }
}
