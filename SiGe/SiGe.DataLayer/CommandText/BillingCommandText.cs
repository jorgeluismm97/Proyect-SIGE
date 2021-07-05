namespace SiGe
{
    public class BillingCommandText : IBillingCommandText
    {
        // Document

        public string AddDocument => "Usp_Bil_I_Bil_Document";
        public string UpdateDocument => "Usp_Bil_U_Bil_Document";
        public string GetDocumentById => "Usp_Bil_S_Bil_Document_Get_By_DocumentId";
        public string GetAllDocument => "Usp_Bil_S_Bil_Document";
        public string GetDocumentByCompanyId => "Usp_Bil_S_Bil_Document_Get_By_CompanyId";
        public string GetDocumentSearchView => "Usp_Bil_S_Bil_Document_Get_Search_View_Document";
        public string GetDocumentByDocumentTypeIdSerieNumber => "Usp_Bil_S_Bil_Document_Get_By_DocumentTypeId_Serie_Number";
        public string GetDocumentNewNumber => "Usp_Bil_S_Bil_Document_Get_New_Number_By_CompanyId_DocumentTypeId_Serie";
        public string GetDocumentByCompanyIdDate => "Usp_Bil_S_Bil_Document_Get_By_CompanyId_Date";
        public string GetMethodPaymentByCompanyIdDate => "Usp_Bil_S_Bil_Document_Get_MethodPayment_By_CompanyId_Date";

        // Bil_DocumentDetail

        public string AddDocumentDetail => "Usp_Bil_I_Bil_DocumentDetail";
        public string UpdateDocumentDetail => "Usp_Bil_U_Bil_DocumentDetail";
        public string GetDocumentDetailById => "Usp_Bil_S_Bil_DocumentDetail_Get_By_DocumentDetailId";
        public string GetAllDocumentDetail => "Usp_Bil_S_Bil_DocumentDetail";
        public string GetDocumentDetailProductByDocumentId => "Usp_Bil_S_Bil_DocumentDetail_Product_Get_By_DocumentId";
        public string GetDocumentDetailByDocumentId => "Usp_Bil_S_Bil_DocumentDetail_Get_By_DocumentId";

        // Bil_DocumentElectronic

        public string AddDocumentElectronic => "Usp_Bil_I_Bil_DocumentElectronic";
        public string UpdateDocumentElectronic => "";
        public string GetDocumentElectronicById => "";
        public string GetAllDocumentElectronic => "";
        public string GetDocumentElectronicByDocumentId => "Usp_Bil_S_Bil_DocumentElectronic_Get_By_DocumentId";

        // Bil_DocumentTypeBranchOfficeSerie

        public string AddDocumentTypeBranchOfficeSerie => "Usp_Bil_I_Bil_DocumentTypeBranchOfficeSerie";
        public string UpdateDocumentTypeBranchOfficeSerie => "Usp_Bil_U_Bil_DocumentTypeBranchOfficeSerie";
        public string GetDocumentTypeBranchOfficeSerieById => "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie_Get_By_DocumentTyeBranchOfficeSerieId";
        public string GetAllDocumentTypeBranchOfficeSerie => "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie";
        public string GetDocumentTypeBranchOfficeSerieByCompanyId=> "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie_Get_By_CompanyId";
        public string GetDocumentTypeBranchOfficeSerieByCompanyIdDocumentTypeId => "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie_Get_By_CompanyId_DocumentTypeId";
        public string GetDocumentTypeBranchOfficeSerieMainView => "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie_ShortView";
        public string GetDocumentTypeBranchOfficeSerieByDocumentTypeIdBranchOfficeIdSerie => "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie_By_DocumentTypeIdBranchOfficeIdSerie";
        public string GetDocumentTypeBranchOfficeSerieByDocumentTypeIdBranchOfficeId => "Usp_Bil_S_Bil_DocumentTypeBranchOfficeSerie_Get_By_DocumentTypeId_BranchOfficeId";

        // Bil_DocumentType

        public string AddDocumentType => "Usp_Bil_I_Bil_DocumentType";
        public string UpdateDocumentType => "Usp_Bil_U_Bil_DocumentType";
        public string GetDocumentTypeById => "Usp_Bil_S_Bil_DocumentType_Get_By_DocumentTypeId";
        public string GetAllDocumentType => "Usp_Bil_S_Bil_DocumentType";
        public string GetDocumentTypeEmitReceive => "Usp_Bil_S_Bil_DocumentType_EmitReceive";
        public string GetDocumentTypeEmit => "Usp_Bil_S_Bil_DocumentType_Emit";
        public string GetDocumenttypeReceive => "Usp_Bil_S_Bil_DocumentType_Receive";

        // Bil_DocumentTypeSetting 

        public string AddDocumentTypeSetting => "Usp_Bil_I_Bil_DocumentTypeSetting";
        public string UpdateDocumentTypeSetting => "Usp_Bil_U_Bil_DocumentTypeSetting";
        public string GetDocumentTypeSettingById => "Usp_Bil_S_Bil_DocumentTypeSetting_Get_By_DocumentTypeSettingId";
        public string GetAllDocumentTypeSetting => "Usp_Bil_S_Bil_DocumentTypeSetting";

        // Bil_VoidedDocumentsDetail

        public string AddVoidedDocumentsDetail => "Usp_Bil_I_Bil_VoidedDocumentsDetail";
        public string UpdateVoidedDocumentsDetail => "Usp_Bil_U_Bil_VoidedDocumentsDetail";
        public string GetVoidedDocumentsDetailById => "Usp_Bil_S_Bil_VoidedDocumentsDetail_Get_By_VoidedDocumentsDetailId";
        public string GetAllVoidedDocumentsDetail => "Usp_Bil_S_Bil_VoidedDocumentsDetail";

        // Bil_VoidedDocuments

        public string AddVoidedDocuments => "Usp_Bil_I_Bil_VoidedDocuments";
        public string UpdateVoidedDocuments => "Usp_Bil_U_Bil_VoidedDocuments";
        public string GetVoidedDocumentsById => "Usp_Bil_S_Bil_VoidedDocuments_Get_By_VoidedDocumentsId";
        public string GetAllVoidedDocuments => "Usp_Bil_S_Bil_VoidedDocuments";
        public string GetVoidedDocumentsCorrelativeNumber => "Usp_Bil_S_Bil_VoidedDocuments_Get_New_Number_By_Correlative";

    }
}
