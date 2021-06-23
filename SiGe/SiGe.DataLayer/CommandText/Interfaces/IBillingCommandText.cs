namespace SiGe
{
    public interface IBillingCommandText
    {
        // Document

        string AddDocument { get; }
        string UpdateDocument { get; }
        string GetDocumentById { get; }
        string GetAllDocument { get; }
        string GetDocumentByCompanyId { get; }
        string GetDocumentSearchView { get;  }
        string GetDocumentByDocumentTypeIdSerieNumber { get; }
        string GetDocumentNewNumber { get; }

        // Bil_DocumentDetail

        string AddDocumentDetail { get; }
        string UpdateDocumentDetail { get; }
        string GetDocumentDetailById { get; }
        string GetAllDocumentDetail { get; }
        string GetDocumentByDocumentId { get; }

        // Bil_DocumentElectronic

        string AddDocumentElectronic { get; }
        string UpdateDocumentElectronic { get; }
        string GetDocumentElectronicById { get; }
        string GetAllDocumentElectronic { get; }

        // Bil_DocumentTypeBranchOfficeSerie

        string AddDocumentTypeBranchOfficeSerie { get; }
        string UpdateDocumentTypeBranchOfficeSerie { get; }
        string GetDocumentTypeBranchOfficeSerieById { get; }
        string GetAllDocumentTypeBranchOfficeSerie { get; }
        string GetDocumentTypeBranchOfficeSerieByCompanyId { get; }
        string GetDocumentTypeBranchOfficeSerieByCompanyIdDocumentTypeId { get;  }
        string GetDocumentTypeBranchOfficeSerieMainView { get;  }
        string GetDocumentTypeBranchOfficeSerieByDocumentTypeIdBranchOfficeIdSerie { get; }
        string GetDocumentTypeBranchOfficeSerieByDocumentTypeIdBranchOfficeId { get; }

        // Bil_DocumentType

        string AddDocumentType { get; }
        string UpdateDocumentType { get; }
        string GetDocumentTypeById { get; }
        string GetAllDocumentType { get; }
        string GetDocumentTypeEmitReceive { get; }
        string GetDocumentTypeEmit { get; }
        string GetDocumenttypeReceive { get;  }

        // Bil_DocumentTypeSetting 

        string AddDocumentTypeSetting { get; }
        string UpdateDocumentTypeSetting { get; }
        string GetDocumentTypeSettingById { get; }
        string GetAllDocumentTypeSetting { get; }

        // Bil_VoidedDocumentsDetail

        string AddVoidedDocumentsDetail { get; }
        string UpdateVoidedDocumentsDetail { get; }
        string GetVoidedDocumentsDetailById { get; }
        string GetAllVoidedDocumentsDetail { get; }

        // Bil_VoidedDocuments

        string AddVoidedDocuments { get; }
        string UpdateVoidedDocuments { get; }
        string GetVoidedDocumentsById { get; }
        string GetAllVoidedDocuments { get; }
        string GetVoidedDocumentsCorrelativeNumber { get; }
    }
}
