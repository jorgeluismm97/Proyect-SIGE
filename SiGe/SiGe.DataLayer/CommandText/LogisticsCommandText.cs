namespace SiGe
{
    public class LogisticsCommandText : ILogisticsCommandText
    {
        //  Log_NoteDetailDocumentDetail

        public string AddNoteDetailDocumentDetail => "Usp_Log_I_Log_NoteDetailDocumentDetail";
        public string UpdateNoteDetailDocumentDetail => "";
        public string GetNoteDetailDocumentDetailById => "";
        public string GetAllNoteDetailDocumentDetail => "";

        // Log_NoteDetail

        public string AddNoteDetail => "Usp_Log_I_Log_NoteDetail";
        public string UpdateNoteDetail => "Usp_Log_U_Log_NoteDetail";
        public string GetNoteDetailById => "Usp_Log_S_Log_NoteDetail_Get_By_NoteDetailId";
        public string GetAllNoteDetail => "Usp_Log_S_Log_NoteDetail";
        public string GetNoteDetailProductByNoteId => "Usp_Log_S_Log_NoteDetail_Product_Get_By_NoteId";
        public string GetNoteDetailByNoteId => "Usp_Log_S_Log_NoteDetail_Get_By_NoteId";

        // Log_Note

        public string AddNote => "Usp_Log_I_Log_Note";
        public string UpdateNote => "Usp_Log_U_Log_Note";
        public string GetNoteById => "Usp_Log_S_Log_Note_Get_By_NoteId";
        public string GetAllNote => "Usp_Log_S_Log_Note";
        public string GetNoteByCompanyIdActionType => "Usp_Log_S_Log_Note_Get_By_CompanyId_ActionType";
        public string GetNoteSearchView => "Usp_Log_S_Log_Note_Get_Search_View";
        public string GetNoteNewNumber => "Usp_Log_S_Log_Note_Get_New_Number_By_ActionType";
        public string GetNoteKardexSimple => "Usp_Log_S_Log_Note_Get_Kardex_Simple";
        public string GetNoteBalanceKardexSimple => "Usp_Log_S_Log_Note_Get_Balance_Kardex_Simple";
        public string GetNoteKardexVaued => "Usp_Log_S_Log_Note_Get_Kardex_Valued";
        public string GetNoteBalanceKardexValued => "Usp_Log_S_Log_Note_Get_Balance_Kardex_Valued";
        public string ValidateQuantityByProductIdBranchOfficeId => "Usp_Log_S_Log_Note_Get_By_ProductId_BranchOfficeId";


        // Log_NoteType

        public string AddNoteType => "Usp_Log_I_Log_NoteType";
        public string UpdateNoteType => "Usp_Log_U_Log_NoteType";
        public string GetNoteTypeById => "Usp_Log_S_Log_NoteType_Get_By_NoteTypeId";
        public string GetAllNoteType => "Usp_Log_S_Log_NoteType";
        public string GetNoteTypeByActionType => "Usp_Log_S_Log_NoteType_Get_Selection_View_By_ActionType";
    }
}
