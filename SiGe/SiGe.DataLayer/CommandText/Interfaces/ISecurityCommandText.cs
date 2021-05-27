namespace SiGe
{
    public interface ISecurityCommandText
    {
        // User
        string AddUser { get; }
        string UpdateUser { get; }
        string GetUserById { get; }
        string GetAllUser { get; }
        string ValidateUser { get; }
        string GetUserByCompanyId { get;  }

        // Sec_UserBranchOffice

        string AddUserBranchOffice { get; }
        string UpdateUserBranchOffice { get; }
        string GetUserBranchOfficeById { get; }
        string GetAllUserBranchOffice { get; }

        // Sec_Session

        string AddSession { get; }
        string UpdateSession { get; }
        string GetSessionById { get; }
        string GetAllSession { get; }
    }
}
