using System;
using System.Collections.Generic;
using System.Text;

namespace SiGe
{
    public class MainCommandText : IMainCommandText
    {
        // IdentityDocumentType

        public string AddIdentityDocumentType => "Usp_Mai_I_Mai_IdentityDocumentType";
        public string UpdateIdentityDocumentType => "Usp_Mai_U_Mai_IdentityDocumentType";
        public string GetIdentityDocumentTypeById => "Usp_Mai_S_Mai_IdentityDocumentType_Get_By_IdentityDocumentTypeId";
        public string GetAllIdentityDocumentType => "Usp_Mai_S_Mai_IdentityDocumentType";

        // Person
        public string AddPerson => "Usp_Mai_I_Mai_Person";
        public string UpdatePerson => "Usp_Mai_U_Mai_Person";
        public string GetPersonById => "Usp_Mai_S_Mai_Person_Get_By_PersonId";
        public string GetAllPerson => "Usp_Mai_S_Mai_Person";
        public string GetPersonByIdentityDocumentTypeIdIdentityDocumentNumber => "Usp_Mai_S_Mai_Person_Get_By_IdentityDocumentTypeId_IdentityDocumentNumber";
        public string GetPersonByCompanyId => "Usp_Mai_S_Mai_Person_Get_By_CompanyId";
        public string GetPersonWithOutUserByCompanyId => "Usp_Mai_S_Mai_Person_WithOut_User_Get_By_CompanyId";

        // Company
        public string AddCompany => "Usp_Mai_I_Mai_Company";
        public string UpdateCompany => "Usp_Mai_U_Mai_Company";
        public string GetCompanyById => "Usp_Mai_S_Mai_Company_Get_By_CompanyId";
        public string GetAllCompany => "Usp_Mai_S_Mai_Company";
        public string GetCompanyByIdentityDocumentNumber => "Usp_Mai_S_Mai_Company_Get_By_IdentityDocumentNumber";
        public string GetCompanyByUserId => "Usp_Mai_S_Mai_Company_Get_By_UserId";
        public string GetCompanyByUserNameIdentityDocumentNumber => "Usp_Mai_S_Mai_Company_Get_By_UserName_IdentityDocumentNumber";

        // PersonCompany
        public string AddPersonCompany => "Usp_Mai_I_Mai_PersonCompany";
        public string UpdatePersonCompany => "Usp_Mai_U_Mai_PersonCompany";
        public string GetPersonCompanyById => "Usp_Mai_S_Mai_PersonCompany_Get_By_PersonCompanyId";
        public string GetAllPersonCompany => "Usp_Mai_S_Mai_PersonCompany";

        // UserCompany

        public string AddUserCompany => "Usp_Mai_I_Mai_UserCompany";
        public string UpdateUserCompany => "Usp_Mai_U_Mai_UserCompany";
        public string GetUserCompanyById => "Usp_Mai_S_Mai_UserCompany_Get_By_UserCompanyId";
        public string GetAllUserCompany => "Usp_Mai_S_Mai_UserCompany";


        // CompanyCertificate
        public string AddCompanyCertificate => "Usp_Mai_I_Mai_CompanyCertificate";
        public string UpdateCompanyCertificate => "Usp_Mai_U_Mai_CompanyCertificate";
        public string GetCompanyCertificateById => "Usp_Mai_S_Mai_CompanyCertificate_Get_By_CompanyCertificateId";
        public string GetAllCompanyCertificate => "Usp_Mai_S_Mai_CompanyCertificate";

        // Mai_CompanyCredential

        public string AddCompanyCredential => "Usp_Mai_I_Mai_CompanyCredential";
        public string UpdateCompanyCredential => "Usp_Mai_U_Mai_CompanyCredential";
        public string GetCompanyCredentialById => "Usp_Mai_S_Mai_CompanyCredential_Get_By_CompanyCredentialId";
        public string GetAllCompanyCredential => "Usp_Mai_S_Mai_CompanyCredential";

        // Mai_BranchOffice

        public string AddBranchOffice => "Usp_Mai_I_Mai_BranchOffice";
        public string UpdateBranchOffice => "Usp_Mai_U_Mai_BranchOffice";
        public string GetBranchOfficeById => "Usp_Mai_S_Mai_BranchOffice_Get_By_BranchOfficeId";
        public string GetAllBranchOfficeCard => "Usp_Mai_S_Mai_BranchOffice";
        public string GetBranchOfficeByCompanyId => "Usp_Mai_S_Mai_BranchOffice_Get_By_CompanyId";
        public string GetBranchOfficeByCode => "Usp_Mai_S_Mai_BranchOffice_Get_By_Code";

        // Mai_CustomerProvider

        public string AddCustomerProvider => "Usp_Mai_I_Mai_CustomerProvider";
        public string UpdateCustomerProvider => "Usp_Mai_U_Mai_CustomerProvider";
        public string GetCustomerProviderById => "Usp_Mai_S_Mai_CustomerProvider_Get_By_CustomerProviderId";
        public string GetAllCustomerProvider => "Usp_Mai_S_Mai_CustomerProvider";
        public string GetCustomerProviderByIdentityDocumentNumber => "Usp_Mai_S_Mai_CustomerProvider_Get_By_IdentityDocumentNumber";

        // Mai_MethodPayment

        public string AddMethodPayment => "Usp_Mai_I_Mai_MethodPayment";
        public string UpdateMethodPayment => "Usp_Mai_U_Mai_MethodPayment";
        public string GetMethodPaymentById => "Usp_Mai_S_Mai_MethodPayment_Get_By_MethodPaymentId";
        public string GetAllMethodPayment => "Usp_Mai_S_Mai_MethodPayment";

    }
}
