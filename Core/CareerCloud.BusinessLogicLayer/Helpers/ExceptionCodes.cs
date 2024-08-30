namespace CareerCloud.BusinessLogicLayer;

internal static class ExceptionCodes
{
    #region ApplicantSkill
    internal const int ApplicantSkill_StartMonth = 101;
    internal const int ApplicantSkill_EndMonth = 102;
    internal const int ApplicantSkill_StartYear = 103;
    internal const int ApplicantSkill_EndYear = 104;
    #endregion

    #region ApplicantWorkHistory
    internal const int ApplicantWorkHistory_CompanyName = 105;
    #endregion

    #region CompanyDescription
    internal const int CompanyDescription_CompanyName = 106;
    internal const int CompanyDescription_CompanyDescription = 107;
    #endregion

    #region ApplicantEducation
    internal const int ApplicantEducation_Major = 107;
    internal const int ApplicantEducation_StartDate = 108;
    internal const int ApplicantEducation_CompletionDate = 109;
    #endregion

    #region ApplicantJobApplication
    internal const int ApplicantJobApplication_ApplicationDate = 110;
    #endregion

    #region ApplicantProfile
    internal const int ApplicantProfile_CurrentSalary = 111;
    internal const int ApplicantProfile_CurrentRate = 112;
    #endregion

    #region ApplicantResume
    internal const int ApplicantResume_Resume = 113;
    #endregion

    #region CompanyJobEducation
    internal const int CompanyJobEducation_Major = 200;
    internal const int CompanyJobEducation_Importance = 201;
    #endregion

    #region CompanyJobDescription
    internal const int CompanyJobDescription_JobName = 300;
    internal const int CompanyJobDescription_JobDescriptions = 301;
    #endregion

    #region CompanyJobSkill
    internal const int CompanyJobSkill_Importance = 400;
    #endregion

    #region CompanyLocation
    internal const int CompanyLocation_CountryCode = 500;
    internal const int CompanyLocation_Province = 501;
    internal const int CompanyLocation_Street = 502;
    internal const int CompanyLocation_City = 503;
    internal const int CompanyLocation_PostalCode = 504;
    #endregion

    #region CompanyProfile
    internal const int CompanyProfile_CompanyWebsite = 600;
    internal const int CompanyProfile_ContactPhone = 601;
    #endregion

    #region SecurityLogin
    internal const int SecurityLogin_PasswordLength = 700;
    internal const int SecurityLogin_PasswordPattern = 701;
    internal const int SecurityLogin_PhoneNumberEmpty = 702;
    internal const int SecurityLogin_PhoneNumberPattern = 703;
    internal const int SecurityLogin_EmailAddress = 704;
    internal const int SecurityLogin_FullName = 705;
    #endregion

    #region SecurityRole
    internal const int SecurityRole_Role = 800;
    #endregion

    #region SystemCountryCode
    internal const int SystemCountryCode_Code = 900;
    internal const int SystemCountryCode_Name = 901;
    #endregion

    #region SystemLanguageCode
    internal const int SystemLanguageCode_LanguageID = 1000;
    internal const int SystemLanguageCode_Name = 1001;
    internal const int SystemLanguageCode_NativeName = 1002;
    #endregion
}