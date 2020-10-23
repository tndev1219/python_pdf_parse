using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccordCL.BCL
{
    public class Producer
    {
        #region Variable Declaration
        AccordForm aForm;
        PdfReader reader;

        public const string producerRegionDetectionText = "PRODUCER";
        public const string insuredRegionDetectionText = "INSURED";
        public const string contactRegionDetectionText = "CONTACT";

        public Rectangle rectProducer;
        public string producerText = "";
        #endregion

        public Producer(AccordForm aForm, PdfReader reader)
        {
            this.aForm = aForm;
            this.reader = reader;
            findLocationOfProducerText();
        }

        public List<PdfWordCoordinates> findLocationOfProducerText()
        {
            PdfWordCoordinates producer = null;
            PdfWordCoordinates insured = null;
            PdfWordCoordinates contact = null;
            List<PdfWordCoordinates> liProducer = new List<PdfWordCoordinates>();

            producer = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, producerRegionDetectionText);

            //if ((producer.XStartCoordinate > 0) && (producer.XEndCoordinate > 0) && (producer.YStartCoordinate > 0) && (producer.YEndCoordinate > 0))
            //{
            //    aForm.ProducerFoundAsText = true;
            //}

            insured = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, insuredRegionDetectionText);

            contact = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (aForm.liLinesSorted, contactRegionDetectionText);

            rectProducer = new Rectangle(producer.XStartCoordinate - aForm.constantNRulesObj.smallMoveLeft , insured.RectOuter.Top + aForm.constantNRulesObj.smallMoveTop, (aForm.constantNRulesObj.pdfFileWidth /2f)  - aForm.constantNRulesObj.smallMoveLeft, producer.RectOuter.Top - aForm.constantNRulesObj.smallMoveBottom);
            getProducerTextFromRegion(rectProducer);
            return liProducer;
        }

     
        public void getProducerTextFromRegion(Rectangle rect)
        {
            string[] result = new UtilityFn().ExtractTextFromRegion(reader, aForm.constantNRulesObj.tableStartXcoordinate - 2.0f, rect.Bottom, rect.Right, rect.Top, aForm);
            result[0] = result[0].Trim();
            StringBuilder sb = new StringBuilder();
            string r = "";
            foreach (string str in result[0].Split('\n'))
            {
                r = str.Trim().Replace("PRODUCER", "");
                if (r != "PRODUCER")
                    if(r != "")
                        sb.AppendLine(r);
            }
            aForm.Producer = sb.ToString();
            aForm.acrdFrmForOcrObj.Producer = result[1];
            aForm.acrdFrmForType3Obj.Producer = result[2];

            if (aForm.Producer.Trim() != "")
            {
                aForm.ProducerFoundAsText = true;
            }


            //DoOcr(rect);
            //return result;
        }
    }   
}       


