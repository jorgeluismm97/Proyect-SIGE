namespace SiGe
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdministrator { get; set; }
    }
}

