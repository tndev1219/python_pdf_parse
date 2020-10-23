using AccordCL.BCL;
//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ProcessAcrdFrm
{
    public partial class ProcessAccordFrmNew : System.Web.UI.Page
    {
        //public  List<pdfWordCoordinates> liPdfCoordinates = new List<pdfWordCoordinates>();
        //public  List<pdfWordCoordinates> liLines = new List<pdfWordCoordinates>();
        //public  List<pdfWordCoordinates> liLinesSorted = new List<pdfWordCoordinates>();

       static string pdfFileNameUploaded = "";

        //List<pdfWordCoordinatesSorted> liLinesXsorted = new List<pdfWordCoordinatesSorted>();

       

        protected void btnProcessOCR_Click(object sender, EventArgs e)
        {
            if(pdfFileNameUploaded == "")
            {
                lblOcrStatus.Text = "Error : Kindly upload file before clicking on Process OCR";
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();

            try
            {
                string pdfFilePath = Server.MapPath("~/pdfUpload/" + pdfFileNameUploaded);
                
                //string gsDataPath = Server.MapPath("~/gs/gs9.15/bin");

                //string tessDataPath = Server.MapPath("~/data/");
                //tessDataPath = Server.MapPath("~/data/tessdata");

                //string savePath = Server.MapPath("~/pdfUpload/" + pdfFileNameUploaded);


                AccordForm acrdFrm = new AccordForm(pdfFilePath);

                //JsonConvert.SerializeObject(myReturnData)
                printAccordFormData(acrdFrm);

                sw.Stop();


             //   //Source file to read from
             ////   string sourceFile = "c:\\Hello.pdf";

             //   //Bind a reader to our PDF
             //   PdfReader reader = new PdfReader(pdfFilePath);

             //   //Create our buffer for previous token values. For Java users, List<string> is a generic list, probably most similar to an ArrayList
             //   List<string> buf = new List<string>();

             //   //Get the raw bytes for the page
             //   byte[] pageBytes = reader.GetPageContent(1);

             //   RandomAccessFileOrArray r = new RandomAccessFileOrArray(pageBytes);
             //   //Get the raw tokens from the bytes
             //   PRTokeniser tokeniser = new PRTokeniser(r);

             //   //Create some variables to set later
             //   PRTokeniser.TokType tokenType;
             //   string tokenValue;

             //   //Loop through each token
             //   while (tokeniser.NextToken())
             //   {
             //       //Get the types and value
             //       tokenType = tokeniser.TokenType;
             //       tokenValue = tokeniser.StringValue;
             //       //If the type is a numeric type
             //       if (tokenType == PRTokeniser.TokType.NUMBER)
             //       {
             //           //Store it in our buffer for later user
             //           buf.Add(tokenValue);
             //           //Otherwise we only care about raw commands which are categorized as "OTHER"
             //       }
             //       else if (tokenType == PRTokeniser.TokType.OTHER)
             //       {
             //           //Look for a rectangle token
             //           if (tokenValue == "re")
             //           {
             //               //Sanity check, make sure we have enough items in the buffer
             //               if (buf.Count < 4) throw new Exception("Not enough elements in buffer for a rectangle");
             //               //Read and convert the values
             //               float x = float.Parse(buf[buf.Count - 4]);
             //               float y = float.Parse(buf[buf.Count - 3]);
             //               float w = float.Parse(buf[buf.Count - 2]);
             //               float h = float.Parse(buf[buf.Count - 1]);
             //               //..do something with them here
             //           }
             //       
             //   }
                lblOcrStatus.Text = pdfFileNameUploaded + " > Processed >  Success. Total time taken : " + (sw.ElapsedMilliseconds / 1000f).ToString() + " seconds.";
            }
            catch (Exception ex)
            {
                lblOcrStatus.Text = "Fail : " + ex.Message + " > " + ex.StackTrace;
                //throw;
            }
           
        }

        private void getTypeOfAccordFrm(AccordForm frm)
        {
            if (frm.ThisCertIsIssuesFoundAsText)
            {
                if (!frm.ProducerFoundAsText)
                {
                    // allVector
                    frm.accordFrmType = PdfType.allVectorText;
                    
                    //Response.Write("ALL VECTOR PDF");
                }
                else
                {
                    if ((frm.ProducerFoundAsText) && (!frm.xFoundAsText))
                    {
                        frm.accordFrmType = PdfType.textButXasVectorText;
                        // xAsVector and other as text
                      //  Response.Write("X AS VECTOR, OTHER AS TEXT");
                    }
                    else
                        if (frm.ProducerFoundAsText && frm.xFoundAsText)
                    {
                        // all text
                        frm.accordFrmType = PdfType.pureText;
                      //  Response.Write("ALL TEXT PDF");
                    }
                }
            }
            else
            {
                frm.accordFrmType = PdfType.allImage;
              //  Response.Write("ALL IMAGE PDF");
                // full scanned image 
                // or it might be non accord form
            }

        }
        private bool getOcrOutpur(AccordForm frm, bool XorData)
        {
            if (frm.ThisCertIsIssuesFoundAsText)
            {
                if (!frm.ProducerFoundAsText)
                    {
                    // allVector
                    return true;
                    }
                else
                {
                    if ((frm.ProducerFoundAsText) && (!frm.xFoundAsText))
                    {
                        // xAsVector and other as text
                        if (XorData)
                            return true;
                        else
                            return false;
                    } else
                        if (frm.ProducerFoundAsText && frm.xFoundAsText)
                    {
                        // all text
                        return false;
                    }
                    
                }

            }
            else
            {

                return true;
                // full scanned image 
                // or it might be non accord form
            }

            return false;
        }
        private void printAccordFormData(AccordForm frm)
        {
            // frm.xFoundAsText = false;
            //  frm.ProducerFoundAsText = false;
            //  frm.ThisCertIsIssuesFoundAsText = true;

          Response.Write(JsonConvert.SerializeObject(frm));

            getTypeOfAccordFrm(frm);
                // accord form 

                // pureText
                // xAsVectorPureText
                // allVector
               

            if(!getOcrOutpur(frm,false))
            {
                txtbxCertDate.Text = frm.CertificateDate;
            }
            else
            {
                if(frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxCertDate.Text = frm.acrdFrmForType3Obj.CertificateDate;
                }
                else
                txtbxCertDate.Text = frm.acrdFrmForOcrObj.CertificateDate;
            }


            if (!getOcrOutpur(frm, false))
            {
                txtbxAuthRep.Text = frm.AuthRep;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxAuthRep.Text = frm.acrdFrmForType3Obj.AuthRep;
                }
                else
                    txtbxAuthRep.Text = frm.acrdFrmForOcrObj.AuthRep;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxImageAttached.Text = frm.isImageAttached.ToString();
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxImageAttached.Text = frm.acrdFrmForType3Obj.isImageAttached.ToString();
                }
                else
                    txtbxImageAttached.Text = frm.acrdFrmForOcrObj.isImageAttached.ToString();
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxCertificateHolder.Text = frm.CertificateHolder;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxCertificateHolder.Text = frm.acrdFrmForType3Obj.CertificateHolder;
                }
                else
                    txtbxCertificateHolder.Text = frm.acrdFrmForOcrObj.CertificateHolder;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxDescription.Text = frm.DescriptionOfOperations;

            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxDescription.Text = frm.acrdFrmForType3Obj.DescriptionOfOperations;
                }
                else
                    txtbxDescription.Text = frm.acrdFrmForOcrObj.DescriptionOfOperations;

            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxEmail.Text = frm.ContactEmail;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxEmail.Text = frm.acrdFrmForType3Obj.ContactEmail;
                }
                else
                    txtbxEmail.Text = frm.acrdFrmForOcrObj.ContactEmail;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxFax.Text = frm.ContactFax;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxFax.Text = frm.acrdFrmForType3Obj.ContactFax;
                }
                else
                    txtbxFax.Text = frm.acrdFrmForOcrObj.ContactFax;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsured.Text = frm.Insured;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsured.Text = frm.acrdFrmForType3Obj.Insured;
                }
                else
                    txtbxInsured.Text = frm.acrdFrmForOcrObj.Insured;
            }


            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerA.Text = frm.InsurerAName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerA.Text = frm.acrdFrmForType3Obj.InsurerAName;
                }
                else
                    txtbxInsurerA.Text = frm.acrdFrmForOcrObj.InsurerAName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerANaic.Text = frm.InsurerANaic;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerANaic.Text = frm.acrdFrmForType3Obj.InsurerANaic;
                }
                else
                    txtbxInsurerANaic.Text = frm.acrdFrmForOcrObj.InsurerANaic;
            }


            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerB.Text = frm.InsurerBName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerB.Text = frm.acrdFrmForType3Obj.InsurerBName;
                }
                else
                    txtbxInsurerB.Text = frm.acrdFrmForOcrObj.InsurerBName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerBNaic.Text = frm.InsurerBNaic;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerBNaic.Text = frm.acrdFrmForType3Obj.InsurerBNaic;
                }
                else
                    txtbxInsurerBNaic.Text = frm.acrdFrmForOcrObj.InsurerBNaic;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerC.Text = frm.InsurerCName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerC.Text = frm.acrdFrmForType3Obj.InsurerCName;
                }
                else
                    txtbxInsurerC.Text = frm.acrdFrmForOcrObj.InsurerCName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerCNaic.Text = frm.InsurerCNaic;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerCNaic.Text = frm.acrdFrmForType3Obj.InsurerCNaic;
                }
                else
                    txtbxInsurerCNaic.Text = frm.acrdFrmForOcrObj.InsurerCNaic;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerD.Text = frm.InsurerDName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerD.Text = frm.acrdFrmForType3Obj.InsurerDName;
                }
                else
                    txtbxInsurerD.Text = frm.acrdFrmForOcrObj.InsurerDName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerDNaic.Text = frm.InsurerDNaic;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerDNaic.Text = frm.acrdFrmForType3Obj.InsurerDNaic;
                }
                else
                    txtbxInsurerDNaic.Text = frm.acrdFrmForOcrObj.InsurerDNaic;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerE.Text = frm.InsurerEName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerE.Text = frm.acrdFrmForType3Obj.InsurerEName;
                }
                else
                    txtbxInsurerE.Text = frm.acrdFrmForOcrObj.InsurerEName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerENaic.Text = frm.InsurerENaic;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerENaic.Text = frm.acrdFrmForType3Obj.InsurerENaic;
                }
                else
                    txtbxInsurerENaic.Text = frm.acrdFrmForOcrObj.InsurerENaic;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerF.Text = frm.InsurerFName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerF.Text = frm.acrdFrmForType3Obj.InsurerFName;
                }
                else
                    txtbxInsurerF.Text = frm.acrdFrmForOcrObj.InsurerFName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsurerFNaic.Text = frm.InsurerFNaic;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsurerFNaic.Text = frm.acrdFrmForType3Obj.InsurerFNaic;
                }
                else
                    txtbxInsurerFNaic.Text = frm.acrdFrmForOcrObj.InsurerFNaic;
            }

            if(!getOcrOutpur(frm,false))
            {
                txtbxName.Text = frm.ContactName;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxName.Text = frm.acrdFrmForType3Obj.ContactName;
                }
                else
                    txtbxName.Text = frm.acrdFrmForOcrObj.ContactName;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPhone.Text = frm.ContactPhone;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPhone.Text = frm.acrdFrmForType3Obj.ContactPhone;
                }
                else
                    txtbxPhone.Text = frm.acrdFrmForOcrObj.ContactPhone;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxProducer.Text = frm.Producer;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxProducer.Text = frm.acrdFrmForType3Obj.Producer;
                }
                else
                    txtbxProducer.Text = frm.acrdFrmForOcrObj.Producer;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxCertificateNo.Text = frm.CertificateNo;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxCertificateNo.Text = frm.acrdFrmForType3Obj.CertificateNo;
                }
                else
                    txtbxCertificateNo.Text = frm.acrdFrmForOcrObj.CertificateNo;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxRevisionNo.Text = frm.RevisionNo;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxRevisionNo.Text = frm.acrdFrmForType3Obj.RevisionNo;
                }
                else
                    txtbxRevisionNo.Text = frm.acrdFrmForOcrObj.RevisionNo;
            }


            if (!getOcrOutpur(frm, false))
            {
                txtbxInsrLtrCol1Row1.Text = frm.PolicyInsrLtrRow1;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsrLtrCol1Row1.Text = frm.acrdFrmForType3Obj.PolicyInsrLtrRow1;
                }
                else
                    txtbxInsrLtrCol1Row1.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow1;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxAddlInsdCol3Row1.Text = frm.PolicyAddlInsrRow1;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxAddlInsdCol3Row1.Text = frm.acrdFrmForType3Obj.PolicyAddlInsrRow1;
                }
                else
                    txtbxAddlInsdCol3Row1.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow1;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxSubrWvdCol4Row1.Text = frm.PolicySubrWvdRow1;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxSubrWvdCol4Row1.Text = frm.acrdFrmForType3Obj.PolicySubrWvdRow1;
                }
                else
                    txtbxSubrWvdCol4Row1.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow1;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyNoCol5Row1.Text = frm.PolicyNoRow1;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyNoCol5Row1.Text = frm.acrdFrmForType3Obj.PolicyNoRow1;
                }
                else
                    txtbxPolicyNoCol5Row1.Text = frm.acrdFrmForOcrObj.PolicyNoRow1;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyEffCol6Row1.Text = frm.PolicEffRow1;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyEffCol6Row1.Text = frm.acrdFrmForType3Obj.PolicEffRow1;
                }
                else
                    txtbxPolicyEffCol6Row1.Text = frm.acrdFrmForOcrObj.PolicEffRow1;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyExpCol7Row1.Text = frm.PolicyExpRow1;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyExpCol7Row1.Text = frm.acrdFrmForType3Obj.PolicyExpRow1;
                }
                else
                    txtbxPolicyExpCol7Row1.Text = frm.acrdFrmForOcrObj.PolicyExpRow1;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsrLtrCol1Row2.Text = frm.PolicyInsrLtrRow2;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsrLtrCol1Row2.Text = frm.acrdFrmForType3Obj.PolicyInsrLtrRow2;
                }
                else
                    txtbxInsrLtrCol1Row2.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow2;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxAddlInsdCol3Row2.Text = frm.PolicyAddlInsrRow2;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxAddlInsdCol3Row2.Text = frm.acrdFrmForType3Obj.PolicyAddlInsrRow2;
                }
                else
                    txtbxAddlInsdCol3Row2.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow2;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxSubrWvdCol4Row2.Text = frm.PolicySubrWvdRow2;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxSubrWvdCol4Row2.Text = frm.acrdFrmForType3Obj.PolicySubrWvdRow2;
                }
                else
                    txtbxSubrWvdCol4Row2.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow2;
            }


            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyNoCol5Row2.Text = frm.PolicyNoRow2;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyNoCol5Row2.Text = frm.acrdFrmForType3Obj.PolicyNoRow2;
                }
                else
                    txtbxPolicyNoCol5Row2.Text = frm.acrdFrmForOcrObj.PolicyNoRow2;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyEffCol6Row2.Text = frm.PolicEffRow2;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyEffCol6Row2.Text = frm.acrdFrmForType3Obj.PolicEffRow2;
                }
                else
                    txtbxPolicyEffCol6Row2.Text = frm.acrdFrmForOcrObj.PolicEffRow2;
            }


            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyExpCol7Row2.Text = frm.PolicyExpRow2;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyExpCol7Row2.Text = frm.acrdFrmForType3Obj.PolicyExpRow2;
                }
                else
                    txtbxPolicyExpCol7Row2.Text = frm.acrdFrmForOcrObj.PolicyExpRow2;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsrLtrCol1Row3.Text = frm.PolicyInsrLtrRow3;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsrLtrCol1Row3.Text = frm.acrdFrmForType3Obj.PolicyInsrLtrRow3;
                }
                else
                    txtbxInsrLtrCol1Row3.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow3;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxAddlInsdCol3Row3.Text = frm.PolicyAddlInsrRow3;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxAddlInsdCol3Row3.Text = frm.acrdFrmForType3Obj.PolicyAddlInsrRow3;
                }
                else
                    txtbxAddlInsdCol3Row3.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow3;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxSubrWvdCol4Row3.Text = frm.PolicySubrWvdRow3;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxSubrWvdCol4Row3.Text = frm.acrdFrmForType3Obj.PolicySubrWvdRow3;
                }
                else
                    txtbxSubrWvdCol4Row3.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow3;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyNoCol5Row3.Text = frm.PolicyNoRow3;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyNoCol5Row3.Text = frm.acrdFrmForType3Obj.PolicyNoRow3;
                }
                else
                    txtbxPolicyNoCol5Row3.Text = frm.acrdFrmForOcrObj.PolicyNoRow3;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyEffCol6Row3.Text = frm.PolicEffRow3;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyEffCol6Row3.Text = frm.acrdFrmForType3Obj.PolicEffRow3;
                }
                else
                    txtbxPolicyEffCol6Row3.Text = frm.acrdFrmForOcrObj.PolicEffRow3;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyExpCol7Row3.Text = frm.PolicyExpRow3;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyExpCol7Row3.Text = frm.acrdFrmForType3Obj.PolicyExpRow3;
                }
                else
                    txtbxPolicyExpCol7Row3.Text = frm.acrdFrmForOcrObj.PolicyExpRow3;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsrLtrCol1Row4.Text = frm.PolicyInsrLtrRow4;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsrLtrCol1Row4.Text = frm.acrdFrmForType3Obj.PolicyInsrLtrRow4;
                }
                else
                    txtbxInsrLtrCol1Row4.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow4;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxAddlInsdCol3Row4.Text = frm.PolicyAddlInsrRow4;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxAddlInsdCol3Row4.Text = frm.acrdFrmForType3Obj.PolicyAddlInsrRow4;
                }
                else
                    txtbxAddlInsdCol3Row4.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow4;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxSubrWvdCol4Row4.Text = frm.PolicySubrWvdRow4;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxSubrWvdCol4Row4.Text = frm.acrdFrmForType3Obj.PolicySubrWvdRow4;
                }
                else
                    txtbxSubrWvdCol4Row4.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow4;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyNoCol5Row4.Text = frm.PolicyNoRow4;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyNoCol5Row4.Text = frm.acrdFrmForType3Obj.PolicyNoRow4;
                }
                else
                    txtbxPolicyNoCol5Row4.Text = frm.acrdFrmForOcrObj.PolicyNoRow4;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyEffCol6Row4.Text = frm.PolicEffRow4;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyEffCol6Row4.Text = frm.acrdFrmForType3Obj.PolicEffRow4;
                }
                else
                    txtbxPolicyEffCol6Row4.Text = frm.acrdFrmForOcrObj.PolicEffRow4;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyExpCol7Row4.Text = frm.PolicyExpRow4;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyExpCol7Row4.Text = frm.acrdFrmForType3Obj.PolicyExpRow4;
                }
                else
                    txtbxPolicyExpCol7Row4.Text = frm.acrdFrmForOcrObj.PolicyExpRow4;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxInsrLtrCol1Row5.Text = frm.PolicyInsrLtrRow5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxInsrLtrCol1Row5.Text = frm.acrdFrmForType3Obj.PolicyInsrLtrRow5;
                }
                else
                    txtbxInsrLtrCol1Row5.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow5;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxAddlInsdCol3Row5.Text = frm.PolicyAddlInsrRow5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxAddlInsdCol3Row5.Text = frm.acrdFrmForType3Obj.PolicyAddlInsrRow5;
                }
                else
                    txtbxAddlInsdCol3Row5.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow5;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxSubrWvdCol4Row5.Text = frm.PolicySubrWvdRow5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxSubrWvdCol4Row5.Text = frm.acrdFrmForType3Obj.PolicySubrWvdRow5;
                }
                else
                    txtbxSubrWvdCol4Row5.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow5;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyNoCol5Row5.Text = frm.PolicyNoRow5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyNoCol5Row5.Text = frm.acrdFrmForType3Obj.PolicyNoRow5;
                }
                else
                    txtbxPolicyNoCol5Row5.Text = frm.acrdFrmForOcrObj.PolicyNoRow5;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyEffCol6Row5.Text = frm.PolicEffRow5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyEffCol6Row5.Text = frm.acrdFrmForType3Obj.PolicEffRow5;
                }
                else
                    txtbxPolicyEffCol6Row5.Text = frm.acrdFrmForOcrObj.PolicEffRow5;
            }

            if (!getOcrOutpur(frm, false))
            {
                txtbxPolicyExpCol7Row5.Text = frm.PolicyExpRow5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    txtbxPolicyExpCol7Row5.Text = frm.acrdFrmForType3Obj.PolicyExpRow5;
                }
                else
                    txtbxPolicyExpCol7Row5.Text = frm.acrdFrmForOcrObj.PolicyExpRow5;
            }



            #region update OCR data
            //txtbxCertDateOcr.Text = frm.acrdFrmForOcrObj.CertificateDate;
            //txtbxAuthRepOcr.Text = frm.acrdFrmForOcrObj.AuthRep;
            //txtbxImageAttachedOcr.Text = frm.acrdFrmForOcrObj.isImageAttached.ToString();
            //txtbxCertificateHolderOcr.Text = frm.acrdFrmForOcrObj.CertificateHolder;
            //txtbxDescriptionOcr.Text = frm.acrdFrmForOcrObj.DescriptionOfOperations;
            //txtbxEmailOcr.Text = frm.acrdFrmForOcrObj.ContactEmail;
            //txtbxFaxOcr.Text = frm.acrdFrmForOcrObj.ContactFax;
            //txtbxInsuredOcr.Text = frm.acrdFrmForOcrObj.Insured;

            //txtbxInsurerAOcr.Text = frm.acrdFrmForOcrObj.InsurerAName;
            //txtbxInsurerANaicOcr.Text = frm.acrdFrmForOcrObj.InsurerANaic;

            //txtbxInsurerBOcr.Text = frm.acrdFrmForOcrObj.InsurerBName;
            //txtbxInsurerBNaicOcr.Text = frm.acrdFrmForOcrObj.InsurerBNaic;

            //txtbxInsurerCOcr.Text = frm.acrdFrmForOcrObj.InsurerCName;
            //txtbxInsurerCNaicOcr.Text = frm.acrdFrmForOcrObj.InsurerCNaic;

            //txtbxInsurerDOcr.Text = frm.acrdFrmForOcrObj.InsurerDName;
            //txtbxInsurerDNaicOcr.Text = frm.acrdFrmForOcrObj.InsurerDNaic;

            //txtbxInsurerEOcr.Text = frm.acrdFrmForOcrObj.InsurerEName;
            //txtbxInsurerENaicOcr.Text = frm.acrdFrmForOcrObj.InsurerENaic;

            //txtbxInsurerFOcr.Text = frm.acrdFrmForOcrObj.InsurerFName;
            //txtbxInsurerFNaicOcr.Text = frm.acrdFrmForOcrObj.InsurerFNaic;

            //txtbxNameOcr.Text = frm.acrdFrmForOcrObj.ContactName;
            //txtbxPhoneOcr.Text = frm.acrdFrmForOcrObj.ContactPhone;
            //txtbxProducerOcr.Text = frm.acrdFrmForOcrObj.Producer;

            //txtbxCertificateNoOcr.Text = frm.acrdFrmForOcrObj.CertificateNo;
            //txtbxRevisionNoOcr.Text = frm.acrdFrmForOcrObj.RevisionNo;


            //txtbxInsrLtrCol1Row1Ocr.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow1;
            //txtbxAddlInsdCol3Row1Ocr.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow1;
            //txtbxSubrWvdCol4Row1Ocr.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow1;
            //txtbxPolicyNoCol5Row1Ocr.Text = frm.acrdFrmForOcrObj.PolicyNoRow1;
            //txtbxPolicyEffCol6Row1Ocr.Text = frm.acrdFrmForOcrObj.PolicEffRow1;
            //txtbxPolicyExpCol7Row1Ocr.Text = frm.acrdFrmForOcrObj.PolicyExpRow1;

            //txtbxInsrLtrCol1Row2Ocr.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow2;
            //txtbxAddlInsdCol3Row2Ocr.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow2;
            //txtbxSubrWvdCol4Row2Ocr.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow2;
            //txtbxPolicyNoCol5Row2Ocr.Text = frm.acrdFrmForOcrObj.PolicyNoRow2;
            //txtbxPolicyEffCol6Row2Ocr.Text = frm.acrdFrmForOcrObj.PolicEffRow2;
            //txtbxPolicyExpCol7Row2Ocr.Text = frm.acrdFrmForOcrObj.PolicyExpRow2;

            //txtbxInsrLtrCol1Row3Ocr.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow3;
            //txtbxAddlInsdCol3Row3Ocr.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow3;
            //txtbxSubrWvdCol4Row3Ocr.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow3;
            //txtbxPolicyNoCol5Row3Ocr.Text = frm.acrdFrmForOcrObj.PolicyNoRow3;
            //txtbxPolicyEffCol6Row3Ocr.Text = frm.acrdFrmForOcrObj.PolicEffRow3;
            //txtbxPolicyExpCol7Row3Ocr.Text = frm.acrdFrmForOcrObj.PolicyExpRow3;

            //txtbxInsrLtrCol1Row4Ocr.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow4;
            //txtbxAddlInsdCol3Row4Ocr.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow4;
            //txtbxSubrWvdCol4Row4Ocr.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow4;
            //txtbxPolicyNoCol5Row4Ocr.Text = frm.acrdFrmForOcrObj.PolicyNoRow4;
            //txtbxPolicyEffCol6Row4Ocr.Text = frm.acrdFrmForOcrObj.PolicEffRow4;
            //txtbxPolicyExpCol7Row4Ocr.Text = frm.acrdFrmForOcrObj.PolicyExpRow4;

            //txtbxInsrLtrCol1Row5Ocr.Text = frm.acrdFrmForOcrObj.PolicyInsrLtrRow5;
            //txtbxAddlInsdCol3Row5Ocr.Text = frm.acrdFrmForOcrObj.PolicyAddlInsrRow5;
            //txtbxSubrWvdCol4Row5Ocr.Text = frm.acrdFrmForOcrObj.PolicySubrWvdRow5;
            //txtbxPolicyNoCol5Row5Ocr.Text = frm.acrdFrmForOcrObj.PolicyNoRow5;
            //txtbxPolicyEffCol6Row5Ocr.Text = frm.acrdFrmForOcrObj.PolicEffRow5;
            //txtbxPolicyExpCol7Row5Ocr.Text = frm.acrdFrmForOcrObj.PolicyExpRow5;

            #endregion
           // return;
            int count = 0;
           
                foreach (var v in frm.col2KeyValRow1Obj)
                {
                    count++;

                    Panel pp = new Panel();
                    Label lb = new Label();
                    Label lblGap1 = new Label();
                    Label lblGap2 = new Label();

                    lblGap1.Text = "";
                    lblGap2.Text = "";


                    TextBox tbKey = new TextBox();
                    TextBox tbVal = new TextBox();

                    tbKey.Width = 200;
                    tbKey.Height = 100;
                    tbVal.Width = 200;
                    tbVal.Height = 100;

                    lblGap1.Width = 10;
                    lblGap2.Width = 10;
                    tbKey.TextMode = TextBoxMode.MultiLine;
                    tbVal.TextMode = TextBoxMode.MultiLine;


                    lb.Text = count.ToString();
                    tbKey.Text = v.keyVal;

                if (!getOcrOutpur(frm, true))
                {
                    // normal output
                    tbVal.Text = v.valVal;
                }
                else
                {
                    // ocr output
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col2KeyValRow1Obj[count - 1].valVal;
                    }
                    else
                        tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow1Obj[count - 1].valVal;

                }

                //   if (v.valVal.Trim() != "")
                //tbVal.Text = v.valVal;
                    //else
                    //    tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow1Obj[count - 1].valVal;

                    pp.Controls.Add(lb);
                    pp.Controls.Add(lblGap1);
                    pp.Controls.Add(tbKey);
                    pp.Controls.Add(lblGap2);
                    pp.Controls.Add(tbVal);

                    pnlCol2Row1.Controls.Add(pp);
                }
          
                
            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col2KeyValRow1Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";


            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;


            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol2Row1Ocr.Controls.Add(pp);
            //}


            count = 0;

          
                foreach (var v in frm.col2KeyValRow2Obj)
                {
                    count++;

                    Panel pp = new Panel();
                    Label lb = new Label();
                    Label lblGap1 = new Label();
                    Label lblGap2 = new Label();

                    lblGap1.Text = "";
                    lblGap2.Text = "";

                    TextBox tbKey = new TextBox();

                    TextBox tbVal = new TextBox();

                    tbKey.Width = 200;
                    tbKey.Height = 100;
                    tbVal.Width = 200;
                    tbVal.Height = 100;

                    lblGap1.Width = 10;
                    lblGap2.Width = 10;
                    tbKey.TextMode = TextBoxMode.MultiLine;
                    tbVal.TextMode = TextBoxMode.MultiLine;

                    lb.Text = count.ToString();
                    tbKey.Text = v.keyVal;
                // tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, true))
                {
                    // normal output
                    tbVal.Text = v.valVal;
                }
                else
                {
                    // ocr output
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col2KeyValRow2Obj[count - 1].valVal;
                    }
                    else
                        tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow2Obj[count - 1].valVal;

                }

                // if (v.valVal.Trim() != "")
                //tbVal.Text = v.valVal;
                    //else
                    //    tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow2Obj[count - 1].valVal;


                    pp.Controls.Add(lb);
                    pp.Controls.Add(lblGap1);
                    pp.Controls.Add(tbKey);
                    pp.Controls.Add(lblGap2);
                    pp.Controls.Add(tbVal);

                    pnlCol2Row2.Controls.Add(pp);
                }

          

            
            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col2KeyValRow2Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();

            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;

            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol2Row2Ocr.Controls.Add(pp);
            //}

            count = 0;

           
                foreach (var v in frm.col2KeyValRow3Obj)
                {
                    count++;

                    Panel pp = new Panel();
                    Label lb = new Label();
                    Label lblGap1 = new Label();
                    Label lblGap2 = new Label();

                    lblGap1.Text = "";
                    lblGap2.Text = "";

                    TextBox tbKey = new TextBox();
                    TextBox tbVal = new TextBox();

                    tbKey.Width = 200;
                    tbKey.Height = 100;
                    tbVal.Width = 200;
                    tbVal.Height = 100;

                    lblGap1.Width = 10;
                    lblGap2.Width = 10;
                    tbKey.TextMode = TextBoxMode.MultiLine;
                    tbVal.TextMode = TextBoxMode.MultiLine;


                    lb.Text = count.ToString();
                    tbKey.Text = v.keyVal;
                //tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, true))
                {
                    // normal output
                    tbVal.Text = v.valVal;
                }
                else
                {
                    // ocr output
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col2KeyValRow3Obj[count - 1].valVal;
                    }
                    else
                        tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow3Obj[count - 1].valVal;
                }

                // if (v.valVal.Trim() != "")
                //tbVal.Text = v.valVal;
                  //  else
                    //    tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow3Obj[count - 1].valVal;




                    pp.Controls.Add(lb);
                    pp.Controls.Add(lblGap1);
                    pp.Controls.Add(tbKey);
                    pp.Controls.Add(lblGap2);
                    pp.Controls.Add(tbVal);

                    pnlCol2Row3.Controls.Add(pp);
                }

          

           

            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col2KeyValRow3Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;


            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol2Row3Ocr.Controls.Add(pp);
            //}

            count = 0;

          
                foreach (var v in frm.col2KeyValRow4Obj)
                {
                    count++;

                    Panel pp = new Panel();
                    Label lb = new Label();
                    Label lblGap1 = new Label();
                    Label lblGap2 = new Label();

                    lblGap1.Text = "";
                    lblGap2.Text = "";

                    TextBox tbKey = new TextBox();
                    TextBox tbVal = new TextBox();

                    tbKey.Width = 600;
                    tbKey.Height = 100;
                    tbVal.Width = 600;
                    tbVal.Height = 100;

                    lblGap1.Width = 10;
                    lblGap2.Width = 10;
                    tbKey.TextMode = TextBoxMode.MultiLine;
                    tbVal.TextMode = TextBoxMode.MultiLine;


                    lb.Text = count.ToString();
                    tbKey.Text = v.keyVal;
                // tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, true))
                {
                    // normal output
                    tbVal.Text = v.valVal;
                }
                else
                {
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col2KeyValRow4Obj[count - 1].valVal;
                    }
                    else
                        // ocr output
                        tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow4Obj[count - 1].valVal;
                }

                // if (v.valVal.Trim() != "")
                //tbVal.Text = v.valVal;
                   // else
                    //    tbVal.Text = frm.acrdFrmForOcrObj.col2KeyValRow4Obj[count - 1].valVal;


                    pp.Controls.Add(lb);
                    pp.Controls.Add(lblGap1);
                    pp.Controls.Add(tbKey);
                    pp.Controls.Add(lblGap2);
                    pp.Controls.Add(tbVal);

                    pnlCol2Row4.Controls.Add(pp);
                }
          

            

            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col2KeyValRow4Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 600;
            //    tbKey.Height = 100;
            //    tbVal.Width = 600;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;


            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol2Row4Ocr.Controls.Add(pp);
            //}

            //frm.col2Row5

            TextBox tbTemp = new TextBox();
            tbTemp.Width = 600;
            tbTemp.Height = 100;
            tbTemp.TextMode = TextBoxMode.MultiLine;

            //tbTemp.Text = frm.col2Row5;

            if (!getOcrOutpur(frm, true))
            {
                tbTemp.Text = frm.col2Row5;
            }
            else
            {
                if (frm.accordFrmType == PdfType.allVectorText)
                {
                    tbTemp.Text = frm.acrdFrmForType3Obj.col2Row5;
                }
                else
                    tbTemp.Text = frm.acrdFrmForOcrObj.col2Row5;
            }

            //if (frm.col2Row5 != "")
            //{
            //    tbTemp.Text = frm.col2Row5;
            //}
            //else
            //{
            //    tbTemp.Text = frm.acrdFrmForOcrObj.col2Row5;
            //}

            pnlCol2Row5.Controls.Add(tbTemp);

            //tbTemp = new TextBox();
            //tbTemp.Width = 600;
            //tbTemp.Height = 100;
            //tbTemp.TextMode = TextBoxMode.MultiLine;
            //tbTemp.Text = frm.acrdFrmForOcrObj.col2Row5;
            //pnlCol2Row5Ocr.Controls.Add(tbTemp);


            count = 0;
            foreach (var v in frm.col8KeyValRow1Obj)
            {
                count++;

                Panel pp = new Panel();
                Label lb = new Label();
                Label lblGap1 = new Label();
                Label lblGap2 = new Label();

                lblGap1.Text = "";
                lblGap2.Text = "";

                TextBox tbKey = new TextBox();
                TextBox tbVal = new TextBox();

                tbKey.Width = 200;
                tbKey.Height = 100;
                tbVal.Width = 200;
                tbVal.Height = 100;

                lblGap1.Width = 10;
                lblGap2.Width = 10;
                tbKey.TextMode = TextBoxMode.MultiLine;
                tbVal.TextMode = TextBoxMode.MultiLine;

                lb.Text = count.ToString();
                tbKey.Text = v.keyVal;
                //tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, false))
                {
                    // normal output
                    tbVal.Text = v.valVal;

                }
                else
                {
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col8KeyValRow1Obj[count - 1].valVal;
                    }
                    else
                        // ocr output
                        tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow1Obj[count - 1].valVal;

                }

                //if (v.valVal.Trim() != "")
                //    tbVal.Text = v.valVal;
                //else
                //    tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow1Obj[count - 1].valVal;


                pp.Controls.Add(lb);
                pp.Controls.Add(lblGap1);
                pp.Controls.Add(tbKey);
                pp.Controls.Add(lblGap2);
                pp.Controls.Add(tbVal);

                pnlCol8Row1.Controls.Add(pp);
            }


            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col8KeyValRow1Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;

            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol8Row1Ocr.Controls.Add(pp);
            //}


            count = 0;
            foreach (var v in frm.col8KeyValRow2Obj)
            {
                count++;

                Panel pp = new Panel();
                Label lb = new Label();
                Label lblGap1 = new Label();
                Label lblGap2 = new Label();

                lblGap1.Text = "";
                lblGap2.Text = "";

                TextBox tbKey = new TextBox();
                TextBox tbVal = new TextBox();

                tbKey.Width = 200;
                tbKey.Height = 100;
                tbVal.Width = 200;
                tbVal.Height = 100;

                lblGap1.Width = 10;
                lblGap2.Width = 10;
                tbKey.TextMode = TextBoxMode.MultiLine;
                tbVal.TextMode = TextBoxMode.MultiLine;

                lb.Text = count.ToString();
                tbKey.Text = v.keyVal;
                //tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, false))
                {
                    // normal output
                    tbVal.Text = v.valVal;

                }
                else
                {
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col8KeyValRow2Obj[count - 1].valVal;
                    }
                    else
                        // ocr output
                        tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow2Obj[count - 1].valVal;
                }

                //if (v.valVal.Trim() != "")
                //    tbVal.Text = v.valVal;
                //else
                //    tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow2Obj[count - 1].valVal;


                pp.Controls.Add(lb);
                pp.Controls.Add(lblGap1);
                pp.Controls.Add(tbKey);
                pp.Controls.Add(lblGap2);
                pp.Controls.Add(tbVal);

                pnlCol8Row2.Controls.Add(pp);
            }

            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col8KeyValRow2Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;

            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol8Row2Ocr.Controls.Add(pp);
            //}

            count = 0;
            foreach (var v in frm.col8KeyValRow3Obj)
            {
                count++;

                Panel pp = new Panel();
                Label lb = new Label();
                Label lblGap1 = new Label();
                Label lblGap2 = new Label();

                lblGap1.Text = "";
                lblGap2.Text = "";

                TextBox tbKey = new TextBox();
                TextBox tbVal = new TextBox();

                tbKey.Width = 200;
                tbKey.Height = 100;
                tbVal.Width = 200;
                tbVal.Height = 100;

                lblGap1.Width = 10;
                lblGap2.Width = 10;
                tbKey.TextMode = TextBoxMode.MultiLine;
                tbVal.TextMode = TextBoxMode.MultiLine;

                lb.Text = count.ToString();
                tbKey.Text = v.keyVal;
                // tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, false))
                {
                    // normal output
                    tbVal.Text = v.valVal;
                }
                else
                {
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col8KeyValRow3Obj[count - 1].valVal;
                    }
                    else
                        // ocr output
                        tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow3Obj[count - 1].valVal;

                }

                //if (v.valVal.Trim() != "")
                //    tbVal.Text = v.valVal;
                //else
                //    tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow3Obj[count - 1].valVal;


                pp.Controls.Add(lb);
                pp.Controls.Add(lblGap1);
                pp.Controls.Add(tbKey);
                pp.Controls.Add(lblGap2);
                pp.Controls.Add(tbVal);

                pnlCol8Row3.Controls.Add(pp);
            }

            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col8KeyValRow3Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;

            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol8Row3Ocr.Controls.Add(pp);
            //}

            count = 0;
            foreach (var v in frm.col8KeyValRow4Obj)
            {
                count++;

                Panel pp = new Panel();
                Label lb = new Label();
                Label lblGap1 = new Label();
                Label lblGap2 = new Label();

                lblGap1.Text = "";
                lblGap2.Text = "";

                TextBox tbKey = new TextBox();
                TextBox tbVal = new TextBox();

                tbKey.Width = 200;
                tbKey.Height = 100;
                tbVal.Width = 200;
                tbVal.Height = 100;

                lblGap1.Width = 10;
                lblGap2.Width = 10;
                tbKey.TextMode = TextBoxMode.MultiLine;
                tbVal.TextMode = TextBoxMode.MultiLine;


                lb.Text = count.ToString();
                tbKey.Text = v.keyVal;
                //tbVal.Text = v.valVal;

                if((v.keyVal.ToUpper() == "PER STATUTE") || (v.keyVal.ToUpper() == "OTHER"))
                {
                    if (!getOcrOutpur(frm, true))
                    {
                        // normal output
                        tbVal.Text = v.valVal;
                    }
                    else
                    {
                        if (frm.accordFrmType == PdfType.allVectorText)
                        {
                            tbVal.Text = frm.acrdFrmForType3Obj.col8KeyValRow4Obj[count - 1].valVal;
                        }
                        else
                            // ocr output
                            // sujit commented now tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow4Obj[count - 1].valVal;
                                tbVal.Text = "";
                    }
                }
                else
                {
                    if (!getOcrOutpur(frm, false))
                    {
                        // normal output
                        tbVal.Text = v.valVal;
                    }
                    else
                    {
                        if (frm.accordFrmType == PdfType.allVectorText)
                        {
                            tbVal.Text = frm.acrdFrmForType3Obj.col8KeyValRow4Obj[count - 1].valVal;
                        }
                        else
                            // ocr output
                            tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow4Obj[count - 1].valVal;
                    }
                }
                //if (v.valVal.Trim() != "")
                //    tbVal.Text = v.valVal;
                //else
                //    tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow4Obj[count - 1].valVal;


                pp.Controls.Add(lb);
                pp.Controls.Add(lblGap1);
                pp.Controls.Add(tbKey);
                pp.Controls.Add(lblGap2);
                pp.Controls.Add(tbVal);

                pnlCol8Row4.Controls.Add(pp);
            }

            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col8KeyValRow4Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;


            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol8Row4Ocr.Controls.Add(pp);
            //}

            count = 0;
            foreach (var v in frm.col8KeyValRow5Obj)
            {
                count++;

                Panel pp = new Panel();
                Label lb = new Label();
                Label lblGap1 = new Label();
                Label lblGap2 = new Label();

                lblGap1.Text = "";
                lblGap2.Text = "";

                TextBox tbKey = new TextBox();
                TextBox tbVal = new TextBox();

                tbKey.Width = 200;
                tbKey.Height = 100;
                tbVal.Width = 200;
                tbVal.Height = 100;

                lblGap1.Width = 10;
                lblGap2.Width = 10;
                tbKey.TextMode = TextBoxMode.MultiLine;
                tbVal.TextMode = TextBoxMode.MultiLine;


                lb.Text = count.ToString();
                tbKey.Text = v.keyVal;
                // tbVal.Text = v.valVal;

                if (!getOcrOutpur(frm, false))
                {
                    // normal output
                    tbVal.Text = v.valVal;
                }
                else
                {
                    if (frm.accordFrmType == PdfType.allVectorText)
                    {
                        tbVal.Text = frm.acrdFrmForType3Obj.col8KeyValRow5Obj[count - 1].valVal;
                    }
                    else
                        // ocr output
                        tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow5Obj[count - 1].valVal;
                }

                //if (v.valVal.Trim() != "")
                //    tbVal.Text = v.valVal;
                //else
                //    tbVal.Text = frm.acrdFrmForOcrObj.col8KeyValRow5Obj[count - 1].valVal;


                pp.Controls.Add(lb);
                pp.Controls.Add(lblGap1);
                pp.Controls.Add(tbKey);
                pp.Controls.Add(lblGap2);
                pp.Controls.Add(tbVal);

                pnlCol8Row5.Controls.Add(pp);
            }

            //count = 0;
            //foreach (var v in frm.acrdFrmForOcrObj.col8KeyValRow5Obj)
            //{
            //    count++;

            //    Panel pp = new Panel();
            //    Label lb = new Label();
            //    Label lblGap1 = new Label();
            //    Label lblGap2 = new Label();

            //    lblGap1.Text = "";
            //    lblGap2.Text = "";

            //    TextBox tbKey = new TextBox();
            //    TextBox tbVal = new TextBox();

            //    tbKey.Width = 200;
            //    tbKey.Height = 100;
            //    tbVal.Width = 200;
            //    tbVal.Height = 100;

            //    lblGap1.Width = 10;
            //    lblGap2.Width = 10;
            //    tbKey.TextMode = TextBoxMode.MultiLine;
            //    tbVal.TextMode = TextBoxMode.MultiLine;


            //    lb.Text = count.ToString();
            //    tbKey.Text = v.keyVal;
            //    tbVal.Text = v.valVal;

            //    pp.Controls.Add(lb);
            //    pp.Controls.Add(lblGap1);
            //    pp.Controls.Add(tbKey);
            //    pp.Controls.Add(lblGap2);
            //    pp.Controls.Add(tbVal);

            //    pnlCol8Row5Ocr.Controls.Add(pp);
            //}

            txtbxTypeOfAcrdFrm.Text = frm.accordFrmType.ToString();
            return;

            Response.Write("</br>");
            Response.Write("---- START ----");
            Response.Write(frm.pdfFilePath);
            Response.Write("</br>");

            Response.Write("Certificate Date > " + frm.CertificateDate);
            Response.Write("</br>");
            Response.Write("----");
            Response.Write("Producer > " + frm.Producer);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Insured > " + frm.Insured);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Contact Name > " + frm.ContactName);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Phone > " + frm.ContactPhone);
            Response.Write("----");
            Response.Write("Fax > " + frm.ContactFax);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Email > " + frm.ContactEmail);
            Response.Write("----");
            Response.Write("</br>");

            Response.Write("Insure A to F details :");
            Response.Write("</br>");
            Response.Write("A > " + frm.InsurerAName + " > " + frm.InsurerANaic);
            Response.Write("B > " + frm.InsurerBName + " > " + frm.InsurerBNaic);
            Response.Write("C > " + frm.InsurerCName + " > " + frm.InsurerCNaic);
            Response.Write("D > " + frm.InsurerDName + " > " + frm.InsurerDNaic);
            Response.Write("E > " + frm.InsurerEName + " > " + frm.InsurerENaic);
            Response.Write("F > " + frm.InsurerFName + " > " + frm.InsurerFNaic);
            Response.Write("----");
            Response.Write("</br>");

            Response.Write("Certificate No > " + frm.CertificateNo);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Revision No > " + frm.RevisionNo);
            Response.Write("----");
            Response.Write("</br>");


            Response.Write("Desc of Operations > " + frm.DescriptionOfOperations);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Certificate Holder > " + frm.CertificateHolder);
            Response.Write("----");
            Response.Write("</br>");
            Response.Write("Auth Rep > " + frm.AuthRep);
            Response.Write("----");
            Response.Write("</br>");

            Response.Write("INSR LTR Details Row 1 to 5");
            Response.Write("</br>");
            Response.Write("Row 1 > " + frm.PolicyInsrLtrRow1);
            Response.Write("Row 2 > " + frm.PolicyInsrLtrRow2);
            Response.Write("Row 3 > " + frm.PolicyInsrLtrRow3);
            Response.Write("Row 4 > " + frm.PolicyInsrLtrRow4);
            Response.Write("Row 5 > " + frm.PolicyInsrLtrRow5);

            Response.Write("----");
            Response.Write("</br>");

            Response.Write("ADDL INSD Details Row 1 to 5");
            Response.Write("</br>");
            Response.Write("Row 1 > " + frm.PolicyAddlInsrRow1);
            Response.Write("Row 2 > " + frm.PolicyAddlInsrRow2);
            Response.Write("Row 3 > " + frm.PolicyAddlInsrRow3);
            Response.Write("Row 4 > " + frm.PolicyAddlInsrRow4);
            Response.Write("Row 5 > " + frm.PolicyAddlInsrRow5);

            Response.Write("----");
            Response.Write("</br>");
            Response.Write("SUBR WVD Details Row 1 to 5");
            Response.Write("</br>");
            Response.Write("Row 1 > " + frm.PolicySubrWvdRow1);
            Response.Write("Row 2 > " + frm.PolicySubrWvdRow2);
            Response.Write("Row 3 > " + frm.PolicySubrWvdRow3);
            Response.Write("Row 4 > " + frm.PolicySubrWvdRow4);
            Response.Write("Row 5 > " + frm.PolicySubrWvdRow5);

            Response.Write("----");
            Response.Write("</br>");
            Response.Write("POLICY NO Details Row 1 to 5");
            Response.Write("</br>");
            Response.Write("Row 1 > " + frm.PolicyNoRow1);
            Response.Write("Row 2 > " + frm.PolicyNoRow2);
            Response.Write("Row 3 > " + frm.PolicyNoRow3);
            Response.Write("Row 4 > " + frm.PolicyNoRow4);
            Response.Write("Row 5 > " + frm.PolicyNoRow5);

            Response.Write("----");
            Response.Write("</br>");
            Response.Write("POLICY EFF Details Row 1 to 5");
            Response.Write("Row 1 > " + frm.PolicEffRow1);
            Response.Write("Row 2 > " + frm.PolicEffRow2);
            Response.Write("Row 3 > " + frm.PolicEffRow3);
            Response.Write("Row 4 > " + frm.PolicEffRow4);
            Response.Write("Row 5 > " + frm.PolicEffRow5);

            Response.Write("----");
            Response.Write("POLICY EXP Details Row 1 to 5");
            Response.Write("Row 1 > " + frm.PolicyExpRow1);
            Response.Write("Row 2 > " + frm.PolicyExpRow2);
            Response.Write("Row 3 > " + frm.PolicyExpRow3);
            Response.Write("Row 4 > " + frm.PolicyExpRow4);
            Response.Write("Row 5 > " + frm.PolicyExpRow5);

            Response.Write("----");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col8KeyValRow1Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("----");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col8KeyValRow2Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("----");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col8KeyValRow3Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("----");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col8KeyValRow4Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("----");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col8KeyValRow5Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("---- ROW 1, COLUMN 2");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col2KeyValRow1Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("---- ROW 2, COLUMN 2");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col2KeyValRow2Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("---- ROW 3, COLUMN 2");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col2KeyValRow3Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }

            Response.Write("---- ROW 4, COLUMN 2");
            Response.Write("</br>");
            count = 0;
            foreach (var v in frm.col2KeyValRow4Obj)
            {
                count++;
                Response.Write(count.ToString() + "] " + v.keyVal + " > " + v.valVal);
            }
            Response.Write("---- ROW 5, COLUMN 2");
            Response.Write("</br>");
            Response.Write(frm.col2Row5);
        }

        protected void btnUploadPdf_Click(object sender, EventArgs e)
        {
            if (FileUpload1.FileName == "")
            {
                lblFileUploadStatus.Text = "Error : kindly select file to upload.";
                return;
            }
           
            if (FileUpload1.PostedFile != null) 
            {
                //string savePath = Server.MapPath("~/pdfUpload/" + "1.pdf" + "");
                
                pdfFileNameUploaded = FileUpload1.FileName;
                string savePath = Server.MapPath("~/pdfUpload/" +  pdfFileNameUploaded );

                try
                {

                    if (File.Exists(savePath))
                        File.Delete(savePath);

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    FileUpload1.PostedFile.SaveAs(savePath);
                    sw.Stop();
                    lblFileUploadStatus.Text = pdfFileNameUploaded + " > Success : File  upload successful.";
                }
                catch (Exception ex)
                {
                    lblFileUploadStatus.Text = "Error : " + ex.Message;
                }
            }


        }
    }
}