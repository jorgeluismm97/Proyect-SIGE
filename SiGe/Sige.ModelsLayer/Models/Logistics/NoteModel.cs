using System;

namespace SiGe
{
    public class NoteModel : BaseModel
    {
        public int NoteId { get; set; }
        public int NoteTypeId { get; set; }
        public int BranchOfficeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime IssueDate { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
    }
}
