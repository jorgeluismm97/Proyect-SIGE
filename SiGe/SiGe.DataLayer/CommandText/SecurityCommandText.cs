
namespace SiGe
{
    public class SecurityCommandText : ISecurityCommandText
    {
        // User

        public string AddUser => "Usp_Sec_I_Sec_User";
        public string UpdateUser => "Usp_Sec_U_Sec_User";
        public string GetUserById => "Usp_Sec_S_Sec_User_Get_By_UserId";
        public string GetAllUser => "Usp_Sec_S_Sec_User";
        public string ValidateUser => "Usp_Sec_S_Sec_User_Validate";
        public string GetUserByCompanyId => "Usp_Sec_S_Sec_User_Get_By_CompanyId";

        // Sec_UserBranchOffice

        public string AddUserBranchOffice => "Usp_Sec_I_Sec_UserBranchOffice";
        public string UpdateUserBranchOffice => "Usp_Sec_U_Sec_UserBranchOffice";
        public string GetUserBranchOfficeById => "Usp_Sec_S_Sec_UserBranchOffice_Get_By_UserBranchOfficeId";
        public string GetAllUserBranchOffice => "Usp_Sec_S_Sec_UserBranchOffice";

        // Sec_Session

        public string AddSession => "Usp_Sec_I_Sec_Session";
        public string UpdateSession => "Usp_Sec_U_Sec_Session";
        public string GetSessionById => "Usp_Sec_S_Sec_Session_Get_By_SessionId";
        public string GetAllSession => "Usp_Sec_S_Sec_Session";

    }
}
