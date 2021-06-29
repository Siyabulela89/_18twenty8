using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _18TWENTY8.Models
{
    public class EMessage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int mesid { get; set; }
        public string to { get; set; }
        public string From { get; set; }
        public string Fromemail { get; set; }
        public string Contact { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string content { get; set; }
        public bool IsHtml { get; set; }
        public string np { get; set; }
        public string nt { get; set; }
    }
    public class ConfidentialityStatement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfStatementID { get; set; }
        public string userId { get; set; }
        public bool Accepted { get; set; }

    }
    public class Loggedinbefore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoggedinBID { get; set; }
        public string userId { get; set; }

    }
        public class Volunteerdetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VolunteerID { get; set; }
        public string Fullnames { get; set; }
        public DateTime DOB { get; set; }
        public string email { get; set; }
        public string cellnumber { get; set; }
        public string Postalcode { get; set; }
        public string Postaladdressline1 { get; set; }
        public string Postaladdressline2 { get; set; }
        public int Province { get; set; }
        public string home_businesscontact { get; set; }
        public int timetocall { get; set; }
        public string Occupation { get; set; }
        public string Employer { get; set; }
        public int Hoursweekforprogramme { get; set; }
        public string Otherintprogrammes { get; set; }
        public string previousexperienceinotherorgasvolunteer { get; set; }
        public string describewhyyouofferedtovolunteer { get; set; }
        public string goaltoachieveinvolunteer { get; set; }
        public string Othercommittees { get; set; }
        public string describehobbies { get; set; }
        public string IDurl { get; set; }
        public string CVurl { get; set; }
        public string imgurl { get; set; }
        public bool indemnity { get; set; }
        public bool confidentiality { get; set; }
        public DateTime DateCreated { get; set; }

        [NotMapped]
        public IList<SelectListItem> programmelist { get; set; }
        [NotMapped]
        public IList<SelectListItem> Commitees { get; set; }
        [NotMapped]
        public IList<SelectListItem> Daysofweek { get; set; }

    }
    public class Committees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitteeTypeID { get; set; }
        public string Description { get; set; }
    }
    public class Time
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TimeID { get; set; }
        public string Description { get; set; }
    }
    public class CommitteesStorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitteeStorageID { get; set; }
        public int VolunteerID { get; set; }
        public int CommitteeTypeID { get; set; }
    }
    public class Daysofweek
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DayID { get; set; }
        public string Description { get; set; }
    }
    public class Daysofweekstorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DayStorageID { get; set; }
        public int VolunteerID { get; set; }
        public int DayID { get; set; }
    }
    public class Programmes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgrammeID { get; set; }
        public string Description { get; set; }
    }
    public class ProgrammesStorage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProgrammeStorageID { get; set; }
        public int VolunteerID { get; set; }
        public int ProgrammeID { get; set; }
    }


    public class BursaryType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BursaryTypeID { get; set; }
        public string Description { get; set; }
    }
        public class Messaging
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public string SendUserId { get; set; }
        public string ReceiveUserID { get; set; }
        public DateTime DateSent { get; set; }
        public int MsRead { get; set; }

 
    }
    public class BursaryApplicationCandidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BACID { get; set; }
        public string UserID { get; set; }
        public int BursaryID { get; set; }
        public int Status { get; set; }
        public string ReceiveUserID { get; set; }
        public DateTime DateApplied { get; set; }
      


    }
    public class BursaryApplicationStatusApp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BASAID { get; set; }
        public string Description { get; set; }
        }
    public class PressPublicity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PressPubID { get; set; }
        public int PressPubTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Pathurl { get; set; }
        public string SiteUrl { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class FinancialSupport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinancialSupportID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string IdNr_Passport { get; set; }
        public string Email { get; set; }
        public string CellphoneNr { get; set; }
        public string PersonalBio { get; set; }
        public DateTime DOB { get; set; }
        public string UserID { get; set; }
        public string CertifiedID { get; set; }
        public string Imgurl { get; set; }
        public string CVurl { get; set; }
        public string SiteUrl { get; set; }
        public string Academictranscript { get; set; }
        public string Proofofregoistrationurl { get; set; }
        public string LatestStatementfees { get; set; }
        public string VideoURl { get; set; }
        public int ApplicationStatusID { get; set; }
        public String VerifCode { get; set; }
        public String VerifCodeComp { get; set; }
        public String verifiedRegistration { get; set; }
        public string ApplicationReason { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class SisterAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SisAssID { get; set; }
        public string LittleSisterID { get; set; }
        public string BigSisterID { get; set; }
        public int BigApproveID { get; set; }

        public int LittleApproveID { get; set; }
         public int AssignSisterStatusID { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class BursaryApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BursaryID { get; set; }
        public int BursaryTypeID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ApplicationStartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ApplicationEndDate { get; set; }
        public string Description { get; set; }
        public string externallink { get; set; }
        public string QualifyingCriteria { get; set; }
        

        public DateTime DateCreated { get; set; }

    }

    public class BursaryApplicationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BARID { get; set; }
        public string UserIDApp { get; set; }
        public int BursaryStatus { get; set; }


        public string Comments { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class BursaryStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BursaryStatusID { get; set; }
        public string Description { get; set; }
      

        public DateTime DateCreated { get; set; }

    }
    public class AssignSisterStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignSisterStatusID { get; set; }
        public string description { get; set; }
        public DateTime DateCreated { get; set; }

    }
    public class ApplicationStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationStatusID { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated { get; set; }

    }
    public class AssignApprove
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssAppID { get; set; }
        public string description { get; set; }
       public DateTime DateCreated { get; set; }

    }

    public class RecognitionLetters
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecognitionLetterID { get; set; }
        public String RecognitionLetterName { get; set; }
        public String popupmodalid { get; set; }
        public String recogurl { get; set; }
        public String logourl { get; set; }
        public int order { get; set; }




        public DateTime DateCreated { get; set; }

    }

    public class PressPubType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PressPubTypeID { get; set; }
        public string Description { get; set; }
       
        public DateTime DateCreated { get; set; }

    }

    public class Workshops
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkshopID { get; set; }
        public string WorkshopTitle { get; set; }
        public string WorkshopContent { get; set; }
       
        public string fileurl { get; set; }
        public int WorkshopFileTypeID { get; set; }
        public int fileorder { get; set; }

        public DateTime DateCreated { get; set; }

    }

    public class WorkshopsupportDocType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkshopFileTypeID { get; set; }
        public string FileTypeDescription { get; set; }
       
        public DateTime DateCreated { get; set; }

    }

    public class Graduates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GraduateID { get; set; }
        public string GraduateName { get; set; }
        public string GraduatenameandQual { get; set; }
        public string GraduateStoryname { get; set; }
        public string pdfstoryurl { get; set; }
        public string Videourl { get; set; }
        public string Modalpopupid { get; set; }
        public string imageurl { get; set; }
        public string Shortstory { get; set; }
        public int order { get; set; }

        public DateTime DateCreated { get; set; }

    }

    public class BigSisterDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BigSisterDetailID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string IDPassport { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateofBirth { get; set; }
        public String email { get; set; }
        public String Phonenumber { get; set; }
        public String AddressStreet { get; set; }
        public String AddressStreetlinetwo { get; set; }
        public int Province { get; set; }
        public string PostalCode { get; set; }
        public String EmergencyContactNameone { get; set; }
        public String EmergencyContactNumberone { get; set; }

        public String EmergencyContactNametwo { get; set; }
        public String EmergencyContactNumbertwo { get; set; }
        public String howdidyouhearabout { get; set; }
        public String BackgroundQ { get; set; }
        public int EverbeenamentorQ { get; set; }
    
        public String DetailsOnEverbeenMentorQ { get; set; }
        public int ArrestedConvictedQ { get; set; }

        public string DetailsArrestedConvictedQ { get; set; }
     
        public int Interactionlevelmeet { get; set; }
        public string Interactionlevelmeetother { get; set; }
        public int InteractionlevelDigCom { get; set; }
        public string InteractionlevelDigComother { get; set; }
        public String prefferedstudentdetails { get; set; }
        public String youngerselfQ { get; set; }
       
        public String Expectationsonlittlesister { get; set; }
        public String Imageurl { get; set; }
        public bool ConfirmMentordurationQ { get; set; }
        public string CVUrl { get; set; }
        public string CertifiedID { get; set; }
        public int ProfileStatusID { get; set; }
        public String VerifCode { get; set; }
        public String VerifCodeComp { get; set; }
        public String HobbiesOther { get; set; }
        public String Additionasupportother { get; set; }
        public String verifiedRegistration { get; set; }
        public string profilestatusreason { get; set; }
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public IList<SelectListItem> Addsupbig { get; set; }
        [NotMapped]
        public IList<SelectListItem> Infosbig { get; set; }

    }

    public class BigSisterAcademic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BigSisterAcademicID { get; set; }
        public int BigSisterUserID { get; set; }
        public string QualificationDocname { get; set; }
        public string Qualificationurl { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class VolunteerAcademic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VolunteerAcademicID { get; set; }
        public int VolunteerID { get; set; }
        public string QualificationDocname { get; set; }
        public string Qualificationurl { get; set; }
        public int order { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class LittleSisterAcademic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LittleSisterAcademicID { get; set; }
        public int LittleSisterUserID { get; set; }
        public string QualificationDocname { get; set; }
        public string Qualificationurl { get; set; }

        public DateTime DateCreated { get; set; }

    }


    public class AdditionalSupportBig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdditionalSupportBigID { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class Province
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceID { get; set; }
        public string Provincename { get; set; }

        public DateTime DateCreated { get; set; }

    }

    public class AdditionalSupportStorageBig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdditionalSupportStorID { get; set; }
        public int AdditionalSupportBigID { get; set; }
        public int UserID { get; set; }


        public DateTime DateCreated { get; set; }

    }
 

    public class InformationofStorageBig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InformationofinterestStorageID { get; set; }
        public int InformationofInterestID { get; set; }

        public int UserID { get; set; }


        public DateTime DateCreated { get; set; }

    }

    public class InformationofStorageLittle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InformationofinterestStorageID { get; set; }
        public int InformationofInterestID { get; set; }

        public int UserID { get; set; }


        public DateTime DateCreated { get; set; }

    }

    public class LittleSisterDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LittleSisterDetailID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string IDPassport { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateofBirth { get; set; }
        public String email { get; set; }
        public String Phonenumber { get; set; }
        public String AddressStreet { get; set; }
        public String AddressStreetlinetwo { get; set; }
        public string CertifiedID { get; set; }
        public int Province { get; set; }
        public string PostalCode { get; set; }
        public String AlternateContact { get; set; }
        public String howdidyouhearaboutQ { get; set; }
        public String CurrentStudyQ { get; set; }
        public String BackgroundQ { get; set; }
        public int EverbeenamenteeQ { get; set; }
        public string Interactionlevelmeetother { get; set; }
        public string InteractionlevelDigComother { get; set; }

        public String EmergencyContactNameone { get; set; }
        public String otherhobbies { get; set; }
        public String EmergencyContactNumberone { get; set; }

        public String EmergencyContactNametwo { get; set; }
        public String EmergencyContactNumbertwo { get; set; }
        public String DetailsOnEverbeenMenteeQ { get; set; }
        public int ArrestedConvictedQ { get; set; }
        public int ProfileStatusID { get; set; }
        public string DetailsArrestedConvictedQ { get; set; }
        public int InformationofInterest { get; set; }
        public int Interactionlevelmeet { get; set; }
        public int InteractionlevelDigCom { get; set; }
        public String prefferedMenteedetails { get; set; }
        public String CVurl { get; set; }
        public String Imageurl { get; set; }
        public String VerifCode { get; set; }
        public String VerifCodeComp { get; set; }
        public String verifiedRegistration { get; set; }
        public String Expectationsonlittlesister { get; set; }
        public bool ConfirmMenteedurationQ { get; set; }
        public string profilestatusreason { get; set; }
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public IList<SelectListItem> Infosbig { get; set; }

    }


    public class ProfileStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileStatusID { get; set; }
        public string Role { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

    }

    public class OptionalBool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YesNoID { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class InteractionLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InteractionLevelID { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

    }
    public class InformationInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InformationofInterestID { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

    }



}
