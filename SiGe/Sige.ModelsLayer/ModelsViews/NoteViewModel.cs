using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class NoteViewModel
    {
        public int NoteId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Note { get; set; }
        public string NoteType { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
