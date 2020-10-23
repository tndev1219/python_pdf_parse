<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProcessAccordFrmNew.aspx.cs" Inherits="ProcessAcrdFrm.ProcessAccordFrmNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" Width="484px" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnUploadPdf" runat="server" OnClick="btnUploadPdf_Click" Text="Upload PDF file" />
            <br />
            <asp:Label ID="lblFileUploadStatus" runat="server" ForeColor="Blue"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnProcessOCR" runat="server" OnClick="btnProcessOCR_Click" Text="Process OCR" />
            <br />
            <asp:Label ID="lblOcrStatus" runat="server" ForeColor="Blue"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
                <asp:Table ID="Table1" runat="server" Height="117px" Width="460px">
                     
                     <asp:TableRow>
                        <asp:TableCell >Accord Form Type</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox Visible="false" ID="txtbxTypeOfAcrdFrm" runat="server" Width ="800px"  ></asp:TextBox>  </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell >Certificate Date</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxCertDate" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell >Producer</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxProducer" TextMode="MultiLine" runat="server" Width ="800px" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insured</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxInsured" TextMode="MultiLine" runat="server" Width ="800px" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Contact Name</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxName" runat="server" Width ="800px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Phone</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxPhone" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Fax</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxFax" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Email Address</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxEmail" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insurer A</asp:TableCell>
                        <asp:TableCell>
                           Name <asp:TextBox ID="txtbxInsurerA" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                        <asp:TableCell>
                            NAIC <asp:TextBox ID="txtbxInsurerANaic" runat="server" Width ="100px" ></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insurer B</asp:TableCell>
                        <asp:TableCell>
                           Name <asp:TextBox ID="txtbxInsurerB"  runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                        <asp:TableCell>
                            NAIC <asp:TextBox ID="txtbxInsurerBNaic" runat="server" Width ="100px" ></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insurer C</asp:TableCell>
                        <asp:TableCell>
                           Name <asp:TextBox ID="txtbxInsurerC"  runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                        <asp:TableCell>
                            NAIC <asp:TextBox ID="txtbxInsurerCNaic"  runat="server" Width ="100px" ></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insurer D</asp:TableCell>
                        <asp:TableCell>
                           Name <asp:TextBox ID="txtbxInsurerD" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                        <asp:TableCell>
                            NAIC <asp:TextBox ID="txtbxInsurerDNaic" runat="server" Width ="100px" ></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insurer E</asp:TableCell>
                        <asp:TableCell>
                           Name <asp:TextBox ID="txtbxInsurerE" runat="server" Width ="800px"></asp:TextBox> </asp:TableCell>
                        <asp:TableCell>
                            NAIC <asp:TextBox ID="txtbxInsurerENaic" runat="server" Width ="100px" ></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Insurer F</asp:TableCell>
                        <asp:TableCell>
                           Name <asp:TextBox ID="txtbxInsurerF" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                        <asp:TableCell>
                            NAIC <asp:TextBox ID="txtbxInsurerFNaic" runat="server" Width ="100px" ></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>

                     <asp:TableRow>
                        <asp:TableCell>Description of Operations</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxDescription" TextMode="MultiLine" runat="server" Width ="800px" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                     <asp:TableRow>
                        <asp:TableCell>Certificate Holder</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxCertificateHolder" TextMode="MultiLine" runat="server" Width ="800px" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                     <asp:TableRow>
                        <asp:TableCell>Authorized Rep</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxAuthRep" EnableViewState="false" ViewStateMode="Disabled" TextMode="MultiLine" runat="server" Width ="800px" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>Image Attached</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxImageAttached" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Certificate No</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxCertificateNo" runat="server" Width ="800px" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>Revision No</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxRevisionNo" runat="server" Width ="800" ></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>
                     </asp:Table>

                 <asp:Table ID="Table3" runat="server" Height="117px" Width="460px">
                    <asp:TableRow>
                        <asp:TableCell>Row 1</asp:TableCell>
                        <asp:TableCell>Col 1 LTR
                            <asp:TextBox ID="txtbxInsrLtrCol1Row1" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>Col 3 INSD
                            <asp:TextBox ID="txtbxAddlInsdCol3Row1" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>Col 4 WVD
                           <asp:TextBox ID="txtbxSubrWvdCol4Row1" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>Col 5 Policy No
                           <asp:TextBox ID="txtbxPolicyNoCol5Row1" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>Col 6 Policy Eff
                           <asp:TextBox ID="txtbxPolicyEffCol6Row1" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>Col 7 Policy Exp
                            <asp:TextBox ID="txtbxPolicyExpCol7Row1" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>

                    </asp:TableRow>
                    
                    
                        <asp:TableRow>
                        <asp:TableCell>Row 2</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxInsrLtrCol1Row2" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxAddlInsdCol3Row2" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxSubrWvdCol4Row2" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyNoCol5Row2" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyEffCol6Row2" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyExpCol7Row2" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                        <asp:TableRow>
                        <asp:TableCell>Row 3</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxInsrLtrCol1Row3" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxAddlInsdCol3Row3" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxSubrWvdCol4Row3" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyNoCol5Row3" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxPolicyEffCol6Row3" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyExpCol7Row3" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>

                                                <asp:TableRow>
                        <asp:TableCell>Row 4</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxInsrLtrCol1Row4" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxAddlInsdCol3Row4" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxSubrWvdCol4Row4" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxPolicyNoCol5Row4" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxPolicyEffCol6Row4" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyExpCol7Row4" runat="server" Width ="200px" TextMode="MultiLine" Height="100px"></asp:TextBox> </asp:TableCell>
                    </asp:TableRow>


                                                <asp:TableRow>
                        <asp:TableCell>Row 5</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="txtbxInsrLtrCol1Row5" TextMode="MultiLine" Height="100px" runat="server" Width ="200px" ></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxAddlInsdCol3Row5" TextMode="MultiLine" Height="100px" runat="server" Width ="200px" ></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxSubrWvdCol4Row5" TextMode="MultiLine" Height="100px" runat="server" Width ="200px" ></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                           <asp:TextBox ID="txtbxPolicyNoCol5Row5" TextMode="MultiLine" Height="100px" runat="server" Width ="200px" ></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyEffCol6Row5" TextMode="MultiLine" Height="100px" runat="server" Width ="200px" ></asp:TextBox> </asp:TableCell>
                         <asp:TableCell>
                            <asp:TextBox ID="txtbxPolicyExpCol7Row5" TextMode="MultiLine" Height="100px" runat="server" Width ="200px" ></asp:TextBox> </asp:TableCell>
                        
                    </asp:TableRow>


                        


                </asp:Table>

                <br />
                <br />
                <br />
                <br />
                <br />

                COLUMN 2 (Type of Insureance) ROW 1
                <asp:Panel ID="pnlCol2Row1" runat="server">
        </asp:Panel>
                
                <br />
                <br />
                
                COLUMN 2 (Type of Insureance) ROW 2
                    <asp:Panel ID="pnlCol2Row2" runat="server">
        </asp:Panel>

                <br />
                <br />

                COLUMN 2 (Type of Insureance) ROW 3
                    <asp:Panel ID="pnlCol2Row3" runat="server">
        </asp:Panel>

                <br />
                <br />

                COLUMN 2 (Type of Insureance) ROW 4
                    <asp:Panel ID="pnlCol2Row4" runat="server">
        </asp:Panel>
                <br />
                <br />
                COLUMN 2 (Type of Insureance) ROW 5
                    <asp:Panel ID="pnlCol2Row5" runat="server">
        </asp:Panel>

                <br />
                <br />

                COLUMN 8 (Limits) ROW 1
                    <asp:Panel ID="pnlCol8Row1" runat="server">
        </asp:Panel>
                <br />
                <br />
                COLUMN 8 (Limits) ROW 2
                     <asp:Panel ID="pnlCol8Row2" runat="server">
        </asp:Panel>
                <br />
                <br />
                <br />
                <br />
                COLUMN 8 (Limits) ROW 3
                    <asp:Panel ID="pnlCol8Row3" runat="server">
        </asp:Panel>
                <br />
                <br />
                COLUMN 8 (Limits) ROW 4
                    <asp:Panel ID="pnlCol8Row4" runat="server">
        </asp:Panel>
                <br />
                <br />
                COLUMN 8 (Limits) ROW 5
                    <asp:Panel ID="pnlCol8Row5" runat="server">
        </asp:Panel>
                    
            </asp:Panel>

        </div>
        <asp:Panel ID="Panel2" runat="server">
            <br />
            <br />
            <strong>************&nbsp; End of Accord Form ************&nbsp; </strong>
        </asp:Panel>
    </form>
</body>
</html>
