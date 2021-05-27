using System;

namespace SiGe
{
    public class BaseModel
    {
        public BaseModel()
        {
            CreatorUser = "";
            UpdaterUser = "";
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Status = true;
            Removed = false;
        }
        public string CreatorUser { get; set; }
        public string UpdaterUser { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }
        public bool Removed { get; set; }
    }
}
