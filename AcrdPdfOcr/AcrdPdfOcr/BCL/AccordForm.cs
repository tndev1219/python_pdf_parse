using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using iTextSharp.text;
using Newtonsoft.Json;
using AcrdPdfOcr.BCL;

namespace AccordCL.BCL
{
    public class AccordForm
    {
        #region Variable Declaration

        public string LastApiUpdateDate = "26 Nov 2019 7 am"; 

        [JsonIgnore]
        public AccordForm acrdFrmForOcrObj;

        [JsonIgnore]
        public AccordForm acrdFrmForType3Obj;

        [JsonIgnore]
        public List<PdfWordCoordinates> liPdfCoordinates;

        [JsonIgnore]
        public List<PdfWordCoordinates> liLines;

        [JsonIgnore]
        public List<PdfWordCoordinates> liLinesSorted;

        [JsonIgnore]
        public string pdfFilePath;

        [JsonIgnore]
        public ConstantNRules constantNRulesObj;

        [JsonIgnore]
        public List<Object> liPassedObjects;
        public string CertificateDate { get; set; }
        public string Producer { get; set; }
        public string Insured { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public string InsurerAName { get; set; }
        public string InsurerBName { get; set; }
        public string InsurerCName { get; set; }
        public string InsurerDName { get; set; }
        public string InsurerEName { get; set; }
        public string InsurerFName { get; set; }
        public string InsurerANaic { get; set; }
        public string InsurerBNaic { get; set; }
        public string InsurerCNaic { get; set; }
        public string InsurerDNaic { get; set; }
        public string InsurerENaic { get; set; }
        public string InsurerFNaic { get; set; }

        public string CertificateNo { get; set; }
        public string RevisionNo { get; set; }

        public string PolicyNoRow1 { get; set; }
        public string PolicyNoRow2 { get; set; }
        public string PolicyNoRow3 { get; set; }
        public string PolicyNoRow4 { get; set; }
        public string PolicyNoRow5 { get; set; }

        public string PolicEffRow1 { get; set; }
        public string PolicEffRow2 { get; set; }
        public string PolicEffRow3 { get; set; }
        public string PolicEffRow4 { get; set; }
        public string PolicEffRow5 { get; set; }

        public string PolicyExpRow1 { get; set; }
        public string PolicyExpRow2 { get; set; }
        public string PolicyExpRow3 { get; set; }
        public string PolicyExpRow4 { get; set; }
        public string PolicyExpRow5 { get; set; }

        public string PolicyInsrLtrRow1 { get; set; }
        public string PolicyInsrLtrRow2 { get; set; }
        public string PolicyInsrLtrRow3 { get; set; }
        public string PolicyInsrLtrRow4 { get; set; }
        public string PolicyInsrLtrRow5 { get; set; }

        public string PolicyAddlInsrRow1 { get; set; }
        public string PolicyAddlInsrRow2 { get; set; }
        public string PolicyAddlInsrRow3 { get; set; }
        public string PolicyAddlInsrRow4 { get; set; }
        public string PolicyAddlInsrRow5 { get; set; }

        public string PolicySubrWvdRow1 { get; set; }
        public string PolicySubrWvdRow2 { get; set; }
        public string PolicySubrWvdRow3 { get; set; }
        public string PolicySubrWvdRow4 { get; set; }
        public string PolicySubrWvdRow5 { get; set; }

        [JsonIgnore]
        public bool isImageAttached { get; set; }

        //  public List<AccordRow> liAccordRow = new List<AccordRow>();
        // public List<AccordColumn> liAccordColumn = new List<AccordColumn>();

        [JsonIgnore]
        public string col2Row5 { get; set; }

        public string DescriptionOfOperations { get; set; }

        public string CertificateHolder { get; set; }
        public string AuthRep { get; set; }

        [JsonIgnore]
        public string tessDataPath { get; set; }

        [JsonIgnore]
        public PdfType accordFrmType { get; set; }

        [JsonIgnore]
        public bool isPdfAccordFrm { get; set; }

        [JsonIgnore]
        public int accordFormPageNo { get; set; }

        [JsonIgnore]
        public string accordFormReorderedUrl { get; set; }


        [JsonIgnore]
        public bool ThisCertIsIssuesFoundAsText { get; set; }

        [JsonIgnore]
        public bool ProducerFoundAsText { get; set; }

        [JsonIgnore]
        public bool xFoundAsText { get; set; }

        
        public List<Col8KeyVal> col8KeyValRow1Obj = new List<Col8KeyVal>();
    
        public List<Col8KeyVal> col8KeyValRow2Obj = new List<Col8KeyVal>();

       
        public List<Col8KeyVal> col8KeyValRow3Obj = new List<Col8KeyVal>();

        public List<Col8KeyVal> col8KeyValRow4Obj = new List<Col8KeyVal>();
        public List<Col8KeyVal> col8KeyValRow5Obj = new List<Col8KeyVal>();

        [JsonIgnore]
        public List<Col8KeyVal> col2KeyValRow1Obj = new List<Col8KeyVal>();

        [JsonIgnore]
        public List<Col8KeyVal> col2KeyValRow2Obj = new List<Col8KeyVal>();

        [JsonIgnore]
        public List<Col8KeyVal> col2KeyValRow3Obj = new List<Col8KeyVal>();

        [JsonIgnore]
        public List<Col8KeyVal> col2KeyValRow4Obj = new List<Col8KeyVal>();


        [JsonIgnore]
        public int TotalNoOfLinesInAllPagesOfPdf { get; set; }

        [JsonIgnore]
        public int NoOfTextPagesInPdf { get; set; }

        [JsonIgnore]
        public string PhysicalPathOfOrderedPdf { get; set; }

        [JsonIgnore]
        public int TotalNoOfPagesInPdf { get; set; }

        [JsonIgnore]
        public int accordFormFoundOnPage { get; set; }

        [JsonIgnore]
        public bool fullImagePdf { get; set; }

        [JsonIgnore]
        public bool accordForm25Found { get; set; }



        //public List<Col8KeyVal> col8KeyValRow1ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col8KeyValRow2ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col8KeyValRow3ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col8KeyValRow4ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col8KeyValRow5ObjOcr = new List<Col8KeyVal>();

        //public List<Col8KeyVal> col2KeyValRow1ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col2KeyValRow2ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col2KeyValRow3ObjOcr = new List<Col8KeyVal>();
        //public List<Col8KeyVal> col2KeyValRow4ObjOcr = new List<Col8KeyVal>();

        [JsonIgnore]
        public float tableCol1Ratio = 0.031365314f;

        [JsonIgnore]
        public float tableCol2Ratio = 0.243542435f;

        [JsonIgnore]
        public float tableCol3Ratio = 0.03101476f;

        [JsonIgnore]
        public float tableCol4Ratio = 0.031531365f;

        [JsonIgnore]
        public float tableCol5Ratio = 0.205793358f;

        [JsonIgnore]
        public float tableCol6Ratio = 0.081586716f;

        [JsonIgnore]
        public float tableCol7Ratio = 0.08095941f;

        [JsonIgnore]
        public float tableCol8ARatio = 0.156365314f;

        [JsonIgnore]
        public float tableCol8BRatio = 0.137841328f;

        [JsonIgnore]
        public float tableCheckboxWidthRatio = 0.0257857142857143f;

        [JsonIgnore]
        public float tableRow1Ratio = 0.318933312f;

        [JsonIgnore]
        public float tableRow2Ratio = 0.228042286f;

        [JsonIgnore]
        public float tableRow3Ratio = 0.135601304f;

        [JsonIgnore]
        public float tableRow4Ratio = 0.181821795f;

        [JsonIgnore]
        public float tableRow5Ratio = 0.135601304f;

        [JsonIgnore]
        public float PhoneWidthRatio = 0.305313653f;

        [JsonIgnore]
        public float FaxWidthRatio = 0.194059041f;

        [JsonIgnore]
        public float InsurerANameWidthRatio = 0.406051661f;

        [JsonIgnore]
        public float InsurerANameNaicWidthRatio = 0.093948339f;

        [JsonIgnore]
        public FileInfo FiPdfTiffImage;

        [JsonIgnore]
        public FileInfo FiPdfTiffImageBmp;

        [JsonIgnore]
        public FileInfo FiPdfTiffImagePng;

        [JsonIgnore]
        public FileInfo FiPdfTiffImageFullLineJpg;

        [JsonIgnore]

        public List<pdfType3> liPdfType3Obj;

        #endregion

        public AccordForm()
        {
            this.accordFrmType = PdfType.notDetected;
            this.liPassedObjects = new List<object>();
            this.liPdfType3Obj = new List<pdfType3>();

        }
            public AccordForm(string pdfFilePath)
        {
            staticConst.CurrentPdfPageNo = 1;
            FileInfo fi = new FileInfo(pdfFilePath);
          string  pdfFilePathT =  fi.DirectoryName + "\\" + System.IO.Path.GetFileNameWithoutExtension(pdfFilePath) + "_" + DateTime.Now.Ticks.ToString() + ".pdf";
            System.IO.File.Copy(pdfFilePath, pdfFilePathT);
            pdfFilePath = pdfFilePathT;
            this.liPdfType3Obj = new List<pdfType3>();

            this.accordFrmType = PdfType.notDetected;
            this.liPassedObjects = new List<object>();
         
            acrdFrmForOcrObj = new AccordForm();
            acrdFrmForType3Obj = new AccordForm();

            this.pdfFilePath = pdfFilePath;
            liPdfCoordinates = new List<PdfWordCoordinates>();
            liLines = new List<PdfWordCoordinates>();
            liLinesSorted = new List<PdfWordCoordinates>();
            constantNRulesObj = new ConstantNRules();
            this.tessDataPath = tessDataPath;
            PdfReader reader = null;
            try
            {
                
                //string pdfTemplate = "my.pdf";
                PdfReader pdfReader1 = new PdfReader(pdfFilePath);
                AcroFields fields = pdfReader1.AcroFields;
                int count = fields.Fields.Count;
                System.Diagnostics.Debug.WriteLine(count);

                pdfType3 pdfType3Obj;
                foreach(var v in fields.Fields.Keys)
                {
                string ke = v;
                string val = fields.GetField(v);
                var pos = fields.GetFieldPositions(v)[0].position;
                    pdfType3Obj = new pdfType3
                    {
                        Key = ke,
                        Value = val,
                        rect = pos
                    };
                    liPdfType3Obj.Add(pdfType3Obj);
                }
                int count1 = fields.Fields.Keys.Count;
                System.Diagnostics.Debug.WriteLine(count1);
                int count2 = fields.Fields.Count;
                System.Diagnostics.Debug.WriteLine(count2);

           

                reader = new PdfReader(pdfFilePath);

            new UtilityFn().InitializePageDimensions(reader, this);

                while ( staticConst.CurrentPdfPageNo <= reader.NumberOfPages)
                {
                    liLines.Clear();
                    liLinesSorted.Clear();
                    liPdfCoordinates.Clear();
                    LoadCoordinatesofAccordForm();
                    SortWordCoordinatesByY();
                    if (liLinesSorted.Where( c=> c.Word.Contains("CERTIFICATE OF LIABILITY INSURANCE")).Any())
                    {
                       
                        break;
                    }
                    else
                    {
                        liPdfCoordinates.Clear();
                        staticConst.CurrentPdfPageNo++;
                    }
                    
                    
                }
               
                
           

            string col1RegionDetectionText = "LTR";
           // col1RegionDetectionText = "INSR";
            PdfWordCoordinates pdfCol1 = null;

            pdfCol1 = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (liLinesSorted, col1RegionDetectionText);

            
            string descOfOperationsRegionDetectionText = "DESCRIPTION OF OPERATIONS / LOCATIONS / VEHICLES";
            PdfWordCoordinates pdfWcdescOfOperations = null;

            pdfWcdescOfOperations = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextContains
                (liLinesSorted, descOfOperationsRegionDetectionText);

 
            string insrRegionDetectionText = "INSR";
            PdfWordCoordinates pdfInsr = null;

            pdfInsr = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (liLinesSorted, insrRegionDetectionText);

            
            string col3N4RegionDetectionText = "ADDL SUBR";
            PdfWordCoordinates pdfAddlSubr = null;

            pdfAddlSubr = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (liLinesSorted, col3N4RegionDetectionText);

                if (pdfAddlSubr == null)
                {
                    pdfAddlSubr = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (liLinesSorted, "ADDTL");
                }

            float difference = pdfAddlSubr.XStartCoordinate - pdfInsr.XStartCoordinate;
            float multiplicationFactor = 3.658536544f;
            float tableWidth = difference * multiplicationFactor;
            float xStart = (constantNRulesObj.pdfFileWidth - tableWidth) / 2f;
            float xEnd = xStart + tableWidth;

            float differenceH = pdfCol1.RectOuter.Bottom - pdfWcdescOfOperations.RectOuter.Top;
            float multiplicationFactorH = 0.98296098f;
            float tableHeight = differenceH * multiplicationFactorH;
            float yStart = pdfCol1.RectOuter.Bottom - 0.5f;
            float yEnd = yStart + tableHeight;

            constantNRulesObj.tableStartXcoordinate = xStart;
            constantNRulesObj.tableEndXcoordinate = xEnd;
            constantNRulesObj.tableWidth = tableWidth;

            constantNRulesObj.tableStartYcoordinate = yStart;
            constantNRulesObj.tableEndYcoordinate = yEnd;
            constantNRulesObj.tableHeight = tableHeight;

            string coveragesRegionDetectionText = "COVERAGES";
            PdfWordCoordinates coverages = null;

            coverages = new UtilityFn().
                FindPdfWordCordinatesOfGivenTextEquals
                (liLinesSorted, coveragesRegionDetectionText);
                
               
                List<PdfWordCoordinates> liPr = new Producer(this, reader).findLocationOfProducerText();
                List<PdfWordCoordinates> liIn = new Insured(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liAtoF = new InsurerAtoFandNaic_ContactDetails(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liDesc = new Description(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liCertificateHolder = new CertificateHolder(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liAuthRep = new AuthRep(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liDateRegion = new AccordDate(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liCertificateNo = new CertificateNo(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liRevisionNo = new RevisionNo(this, reader).FindCoordinatesOfText();
                List<PdfWordCoordinates> liMultipleTable = new MultipleRowsTabular(this, reader).FindCoordinatesOfText();
            }
            catch (Exception ex)
            {

            }
            finally 
            {
                reader.Close();
                reader.Dispose();
            }


           
        }

        public AccordForm(string pdfFilePath, bool detectAcrdFrm)
        {
            this.accordFrmType = PdfType.notDetected;
            this.liPassedObjects = new List<object>();
           
            
            acrdFrmForOcrObj = new AccordForm();
            acrdFrmForType3Obj = new AccordForm();

            this.pdfFilePath = pdfFilePath;
            liPdfCoordinates = new List<PdfWordCoordinates>();
            liLines = new List<PdfWordCoordinates>();
            liLinesSorted = new List<PdfWordCoordinates>();
            constantNRulesObj = new ConstantNRules();
            this.tessDataPath = tessDataPath;
            PdfReader reader = null;
            try
            {
               


                reader = new PdfReader(pdfFilePath);

                new UtilityFn().InitializePageDimensions(reader, this);

              

                LoadCoordinatesofAccordForm();
                SortWordCoordinatesByY();

                if (liLinesSorted.Count == 0)
                {
                    this.isPdfAccordFrm = true;
                    return ;
                }

               
                //string coveragesRegionDetectionText = "COVERAGES";
                string thisCertIsIssuedRegionDetectionText = "THIS CERTIFICATE IS ISSUED AS A MATTER OF INFORMATION ONLY AND CONFERS NO RIGHTS UPON THE CERTIFICATE HOLDER. THIS"; // find above this

        PdfWordCoordinates coverages = null;

                coverages = new UtilityFn().
                    FindPdfWordCordinatesOfGivenTextEquals
                    (liLinesSorted, thisCertIsIssuedRegionDetectionText);

                if (coverages == null)
                {
                    this.isPdfAccordFrm = false;
                    return;
                }

                if ((coverages.XStartCoordinate > 0) && (coverages.XEndCoordinate > 0) && (coverages.YStartCoordinate > 0) && (coverages.YEndCoordinate > 0))
                {
                    this.isPdfAccordFrm = true;
                }
                
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }



        }
        public AccordForm(string pdfFilePath, bool detectAcrdFrm, bool checkInsidePages)
        {
            this.accordFrmType = PdfType.notDetected;
            this.liPassedObjects = new List<object>();
          

            acrdFrmForOcrObj = new AccordForm();
            acrdFrmForType3Obj = new AccordForm();

            this.pdfFilePath = pdfFilePath;
            liPdfCoordinates = new List<PdfWordCoordinates>();
            liLines = new List<PdfWordCoordinates>();
            liLinesSorted = new List<PdfWordCoordinates>();
            constantNRulesObj = new ConstantNRules();
            this.tessDataPath = tessDataPath;
            PdfReader reader = null;
            try
            {



                reader = new PdfReader(pdfFilePath);

                new UtilityFn().InitializePageDimensions(reader, this);


                int lineCount = 0;
                int pageNo = LoadCoordinatesofAccordFormRecursively(ref lineCount);

               // SortWordCoordinatesByY();

                if (lineCount == 0)
                {
                    this.isPdfAccordFrm = true;
                    return;
                }

                if( accordFormPageNo == 0)
                {
                    this.isPdfAccordFrm = false;
                    return;
                } else
                {
                    if(accordFormPageNo == 1)
                    {
                        this.isPdfAccordFrm = true;
                        //accordFormReorderedUrl = pdfFilePath;
                        return;
                    }
                    else
                    {
                        var inputFile = pdfFilePath;
                        var output = System.IO.Path.GetDirectoryName(pdfFilePath) + "\\reorderedPdf\\" +  System.IO.Path.GetFileNameWithoutExtension(pdfFilePath) + "_" + DateTime.Now.Ticks + ".pdf";

                        if (File.Exists(output))
                            File.Delete(output);
                        //Bind a reader to our input file
                        var readerP = new PdfReader(inputFile);

                        //Create our output file, nothing special here
                        using (FileStream fs = new FileStream(output, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            using (Document doc = new Document(readerP.GetPageSizeWithRotation(1)))
                            {
                                //Use a PdfCopy to duplicate each page
                                using (PdfCopy copy = new PdfCopy(doc, fs))
                                {
                                    doc.Open();
                                    copy.SetLinearPageMode();
                                    for (int i = 1; i <= readerP.NumberOfPages; i++)
                                    {
                                        copy.AddPage(copy.GetImportedPage(readerP, i));
                                    }
                                    //Reorder pages
                                    int[] order = new int[readerP.NumberOfPages];
                                    order[0] = accordFormPageNo;
                                    for (int i = 1; i<= readerP.NumberOfPages; i++)
                                    {
                                        if(i < accordFormPageNo)
                                        {
                                            order[i] = i;
                                        }
                                        else if(i == accordFormPageNo)
                                        {

                                        } else
                                        {
                                            order[i-1] = i  ;
                                        }
                                    }
                                    
                                    


                                    copy.ReorderPages(order);
                                    doc.Close();
                                    PhysicalPathOfOrderedPdf = output;
    }
                            }
                        }

                        accordFormReorderedUrl = @"http://rhaccorddns.eastus.cloudapp.azure.com/detectAcrd/pdfUpload/reorderedPdf/" + System.IO.Path.GetFileName(output);

                        //accordFormReorderedUrl = output;


                        // here we need to move accord form to top
                    }
                }

            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }



        }

        public void LoadCoordinatesofAccordForm()
        {
            PdfReader reader = null;
            PdfReader pdfReader = null;
            try
            {
                reader = new PdfReader(pdfFilePath);
                pdfReader = new PdfReader(pdfFilePath);

                TextRendererForPdfDoc listener = new TextRendererForPdfDoc(this);
                //SimpleTextExtractionStrategy listener = new SimpleTextExtractionStrategy();

                PdfContentStreamProcessor processor = new PdfContentStreamProcessor(listener);
                PdfDictionary pageDic = reader.GetPageN(staticConst.CurrentPdfPageNo);

                PdfDictionary resourcesDic = pageDic.GetAsDict(PdfName.RESOURCES);
                processor.ProcessContent(ContentByteUtils.GetContentBytesForPage(reader, staticConst.CurrentPdfPageNo), resourcesDic);
                listener.Text.ToString();  // commented for now, uncomment it later : sujit
            }

            finally
            {
                reader.Close();
                reader.Dispose();
                pdfReader.Close();
                pdfReader.Dispose();
            }
        }

        public int LoadCoordinatesofAccordFormRecursively(ref int lineCount)
        {
            PdfReader reader = null;
            PdfReader pdfReader = null;
            try
            {
                reader = new PdfReader(pdfFilePath);
                pdfReader = new PdfReader(pdfFilePath);

                int accordFormPageNum = 0;
                TotalNoOfPagesInPdf = reader.NumberOfPages;
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    liLinesSorted.Clear();
                    TextRendererForPdfDoc listener = new TextRendererForPdfDoc(this);
                    //SimpleTextExtractionStrategy listener = new SimpleTextExtractionStrategy();

                    PdfContentStreamProcessor processor = new PdfContentStreamProcessor(listener);
                    PdfDictionary pageDic = reader.GetPageN(i);

                    PdfDictionary resourcesDic = pageDic.GetAsDict(PdfName.RESOURCES);
                    processor.ProcessContent(ContentByteUtils.GetContentBytesForPage(reader, i), resourcesDic);
                    listener.Text.ToString();

                    SortWordCoordinatesByY();
                    lineCount += liLinesSorted.Count();

                    if (liLinesSorted.Count > 0)
                        NoOfTextPagesInPdf++;

                    TotalNoOfLinesInAllPagesOfPdf = lineCount;

                    //string coveragesRegionDetectionText = "COVERAGES";
                    string thisCertIsIssuedRegionDetectionText = "THIS CERTIFICATE IS ISSUED AS A MATTER OF INFORMATION ONLY AND CONFERS NO RIGHTS UPON THE CERTIFICATE HOLDER. THIS"; // find above this
                    thisCertIsIssuedRegionDetectionText = "CERTIFICATE OF LIABILITY INSURANCE";
                    PdfWordCoordinates coverages = null;

                    coverages = new UtilityFn().
                        FindPdfWordCordinatesOfGivenTextEquals
                        (liLinesSorted, thisCertIsIssuedRegionDetectionText);

                    if (coverages != null)
                    {
                        if ((coverages.XStartCoordinate > 0) && (coverages.XEndCoordinate > 0) && (coverages.YStartCoordinate > 0) && (coverages.YEndCoordinate > 0))
                        {
                            this.isPdfAccordFrm = true;
                            accordFormPageNum = i;
                            accordForm25Found = true;

                            break;
                        }
                    }

                 //   if ((coverages.XStartCoordinate > 0) && (coverages.XEndCoordinate > 0) && (coverages.YStartCoordinate > 0) && (coverages.YEndCoordinate > 0))
                 //   {
                    //    this.isPdfAccordFrm = true;
                   // }


                }
                this.accordFormPageNo = accordFormPageNum;
                this.accordFormFoundOnPage = accordFormPageNum;

                if (TotalNoOfLinesInAllPagesOfPdf == 0)
                    fullImagePdf = true;

                return accordFormPageNum;
                 // commented for now, uncomment it later : sujit
            }

            finally
            {
                reader.Close();
                reader.Dispose();
                pdfReader.Close();
                pdfReader.Dispose();
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
                        if (v.XStartCoordinate - lastX > this.constantNRulesObj.characterXMargin)
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
                    lastX = v.XEndCoordinate;
                    vectorLastX = v.VectorEnd;
                    continue;          
                }

                if (v.YStartCoordinate == currentLineY)
                {
                    if (lastX != 0)
                    {
                        if (v.XStartCoordinate - lastX >  this.constantNRulesObj.characterXMargin)
                        {
                            // new sentence
                            liLines.Add(v);
                            sorted.Word = currentLineStr;
                            sorted.SerialNo = liLinesSorted.Count + 1;
                            sorted.LengthOfWord = this.constantNRulesObj.characterXMargin * sorted.Word.Length;
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
                            // same sentence continue
                            // if (v.xStartCoordinate - lastX > this.constantNRulesObj.addSpaceIfMargin)
                        
                            float spacing = vectorLastX.Subtract(v.VectorStart).Length;
                            if (spacing > v.CharacterSpacing / 2f)
                                //if (v.xStartCoordinate - lastX > this.constantNRulesObj.addSpaceIfMargin)
                            {
                             
                                sorted.LiWords.Add(v.Word);
                                sorted.LiPdfCordinates.Add(v);
                                if(v.Word.StartsWith(" "))
                                {
                                    currentLineStr +=  v.Word;
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
                        sorted.LengthOfWord = this.constantNRulesObj.characterXMargin * sorted.Word.Length;
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

                    }

                }
                sorted.XEndCoordinate = v.XStartCoordinate;
                sorted.YEndCoordinate = v.YStartCoordinate;

                currentLineY = v.YStartCoordinate;
                lastX = v.XEndCoordinate;
                vectorLastX = v.VectorEnd;
           }

            //foreach (var v1 in liLinesSorted)
            //{
            //    Console.WriteLine(v1.xStartCoordinate.ToString("000.00000") + " , " + v1.yStartCoordinate.ToString("000.00000") + " > " + v1.word);
            //}
        }

    }

    
}
