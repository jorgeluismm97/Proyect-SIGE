namespace SiGe
{
    public class DocumentTypeBranchOfficeSerieModel : BaseModel
    {
        public int DocumentTypeBranchOfficeSerieId { get; set; }
        public int DocumentTypeId { get; set; }
        public int BranchOfficeId { get; set; }
        public string Serie { get; set; }
        public bool IsElectronic { get; set; }
    }
}
