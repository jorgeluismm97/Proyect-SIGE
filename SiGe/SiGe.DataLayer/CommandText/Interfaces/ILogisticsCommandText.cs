namespace SiGe
{
    public interface ILogisticsCommandText
    {
        //  Log_NoteDetailDocumentDetail

        string AddNoteDetailDocumentDetail { get; }
        string UpdateNoteDetailDocumentDetail { get; }
        string GetNoteDetailDocumentDetailById { get; }
        string GetAllNoteDetailDocumentDetail { get; }

        // Log_NoteDetail

        string AddNoteDetail { get; }
        string UpdateNoteDetail { get; }
        string GetNoteDetailById { get; }
        string GetAllNoteDetail { get; }
        string GetNoteDetailProductByNoteId { get; }
        string GetNoteDetailByNoteId { get; }

        // Log_Note

        string AddNote { get; }
        string UpdateNote { get; }
        string GetNoteById { get; }
        string GetAllNote { get; }
        string GetNoteByCompanyIdActionType { get;  }
        string GetNoteSearchView { get;  }
        string GetNoteNewNumber { get; }
        string GetNoteKardexSimple { get; }
        string GetNoteBalanceKardexSimple { get; }
        string GetNoteKardexVaued { get; }
        string GetNoteBalanceKardexValued { get;  }
        string ValidateQuantityByProductIdBranchOfficeId { get; }

        // Log_NoteType

        string AddNoteType { get; }
        string UpdateNoteType { get; }
        string GetNoteTypeById { get; }
        string GetAllNoteType { get; }
        string GetNoteTypeByActionType { get; }

    }
}
