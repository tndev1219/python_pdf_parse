using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;

namespace AccordCL.BCL
{
    public class ConstantNRules
    {
        #region Variable Declaration
        public float pdfFileWidth;
        public float pdfFileHeight;
        public float smallMoveLeft = 0.4f;
        public float smallMoveRight = 0.4f;
        public float smallMoveTop = 0.4f;
        public float smallMoveBottom = 0.4f;
        public float specialCaseAdjustment = 0.8f;
        public float characterXMargin = 7f;
        public float addSpaceIfMargin = 1.6f;
        public float tableStartYcoordinate;
        public float tableStartXcoordinate;
        public float tableEndYcoordinate;
        public float tableEndXcoordinate;
        public float tableWidth;
        public float tableHeight;

        public float tiffImageFileWidth;
        public float tiffImageFileHeight;
        public string tiffImageDirectory;
        public string tiffImageOnlyNameWithoutExtension;
        public string tiffImageExtension;
        public float pdfToImageRatio;
        #endregion

    }

    public enum OcrTypes
    {
        SingleChar, SingleLine, SingleBlock,
        SingleWord, SingleCharOrWord

    }

    public enum PdfType
    {
        notDetected, pureText, textButXasVectorText, allVectorText, allImage
    }
    
}
