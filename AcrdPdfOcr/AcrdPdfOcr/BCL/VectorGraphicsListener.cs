using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    class VectorGraphicsListener : IExtRenderListener
    {
        public void ModifyPath(PathConstructionRenderInfo renderInfo)
        {
            if (renderInfo.Operation == PathConstructionRenderInfo.RECT)
            {
                float x = renderInfo.SegmentData[0];
                float y = renderInfo.SegmentData[1];
                float w = renderInfo.SegmentData[2];
                float h = renderInfo.SegmentData[3];
                Vector a = new Vector(x, y, 1).Cross(renderInfo.Ctm);
                Vector b = new Vector(x + w, y, 1).Cross(renderInfo.Ctm);
                Vector c = new Vector(x + w, y + h, 1).Cross(renderInfo.Ctm);
                Vector d = new Vector(x, y + h, 1).Cross(renderInfo.Ctm);

                Console.Out.WriteLine("Rectangle at ({0}, {1}) with size ({2}, {3})", x, y, w, h);
                Console.Out.WriteLine("--> at ({0}, {1}) ({2}, {3}) ({4}, {5}) ({6}, {7})", a[Vector.I1], a[Vector.I2], b[Vector.I1], b[Vector.I2], c[Vector.I1], c[Vector.I2], d[Vector.I1], d[Vector.I2]);
            }
            else
            {
                switch (renderInfo.Operation)
                {
                    case PathConstructionRenderInfo.MOVETO:
                        Console.Out.Write("Move to");
                        break;
                    case PathConstructionRenderInfo.LINETO:
                        Console.Out.Write("Line to");
                        break;
                    case PathConstructionRenderInfo.CLOSE:
                        Console.Out.WriteLine("Close");
                        return;
                    default:
                        Console.Out.Write("Curve along");
                        break;
                }
                List<Vector> points = new List<Vector>();
                for (int i = 0; i < renderInfo.SegmentData.Count - 1; i += 2)
                {
                    float x = renderInfo.SegmentData[i];
                    float y = renderInfo.SegmentData[i + 1];
                    Console.Out.Write(" ({0}, {1})", x, y);
                    Vector a = new Vector(x, y, 1).Cross(renderInfo.Ctm);
                    points.Add(a);
                }
                Console.Out.WriteLine();
                Console.Out.Write("--> at ");
                foreach (Vector point in points)
                {
                    Console.Out.Write(" ({0}, {1})", point[Vector.I1], point[Vector.I2]);
                }
                Console.Out.WriteLine();
            }
        }

        public void ClipPath(int rule)
        {
            Console.Out.WriteLine("Clip");
        }

        public iTextSharp.text.pdf.parser.Path RenderPath(PathPaintingRenderInfo renderInfo)
        {
            switch (renderInfo.Operation)
            {
                case PathPaintingRenderInfo.FILL:
                    Console.Out.WriteLine("Fill");
                    break;
                case PathPaintingRenderInfo.STROKE:
                    Console.Out.WriteLine("Stroke");
                    break;
                case PathPaintingRenderInfo.STROKE + PathPaintingRenderInfo.FILL:
                    Console.Out.WriteLine("Stroke and fill");
                    break;
                case PathPaintingRenderInfo.NO_OP:
                    Console.Out.WriteLine("Drop");
                    break;
            }
            return null;
        }

        public void BeginTextBlock() { }
        public void EndTextBlock() { }
        public void RenderImage(ImageRenderInfo renderInfo) { }
        public void RenderText(TextRenderInfo renderInfo) { }
    }
}
