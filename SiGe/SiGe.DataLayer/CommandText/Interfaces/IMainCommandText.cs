namespace SiGe
{
    public interface IMainCommandText
    {
        // IdentityDocumentType

        string AddIdentityDocumentType { get; }
        string UpdateIdentityDocumentType { get; }
        string GetIdentityDocumentTypeById { get; }
        string GetAllIdentityDocumentType { get; }

        // Person

        string AddPerson { get; }
        string UpdatePerson { get; }
        string GetPersonById { get; }
        string GetAllPerson { get; }
        string GetPersonByIdentityDocumentTypeIdIdentityDocumentNumber { get; }
        string GetPersonByCompanyId { get; }
        string GetPersonWithOutUserByCompanyId { get; }

        // Company

        string AddCompany { get; }
        string UpdateCompany { get; }
        string GetCompanyById { get; }
        string GetAllCompany { get; }
        string GetCompanyByIdentityDocumentNumber { get; }
        string GetCompanyByUserId { get; }
        string GetCompanyByUserNameIdentityDocumentNumber { get; }

        // Mai_PersonCompany

        string AddPersonCompany{ get; }
        string UpdatePersonCompany { get; }
        string GetPersonCompanyById { get; }
        string GetAllPersonCompany { get; }

        // Mai_CompanyCertificate

        string AddCompanyCertificate { get; }
        string UpdateCompanyCertificate { get; }
        string GetCompanyCertificateById { get; }
        string GetAllCompanyCertificate{ get; }
        string GetCompanyCertificateByCompanyId { get; }

        // Mai_CompanyCredential

        string AddCompanyCredential { get; }
        string UpdateCompanyCredential { get; }
        string GetCompanyCredentialById { get; }
        string GetAllCompanyCredential { get; }
        string GetCompanyCredentialByCompanyId { get; }

        // Mai_BranchOffice

        string AddBranchOffice { get; }
        string UpdateBranchOffice { get; }
        string GetBranchOfficeById { get; }
        string GetAllBranchOfficeCard { get; }
        string GetBranchOfficeByCompanyId { get; }
        string GetBranchOfficeByCode { get; }

        // Mai_Customer

        string AddCustomer { get; }
        string UpdateCustomer { get; }
        string GetCustomerById { get; }
        string GetAllCustomer { get; }
        string GetCustomerByIdentityDocumentNumber { get;  }
        string GetCustomerByCompanyId { get; }

        // Mai_MethodPayment

        string AddMethodPayment { get; }
        string UpdateMethodPayment { get; }
        string GetMethodPaymentById { get; }
        string GetAllMethodPayment { get; }

        // UserCompany

        string AddUserCompany { get; }
        string UpdateUserCompany { get; }
        string GetUserCompanyById { get; }
        string GetAllUserCompany { get; }
    }
}
