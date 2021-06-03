using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class SerieViewModel
    {
        public int DocumentTypeBranchOfficeSerieId { get; set; }
        public List<DocumentTypeModel> DocumentTypes { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentType { get; set; }
        public List<BranchOfficeModel> BranchOffices { get; set; }
        public int BranchOfficeId { get; set; }
        public string BranchOffice { get; set; }
        public string Serie { get; set; }
        public bool IsElectronic { get; set; }
    }
}
