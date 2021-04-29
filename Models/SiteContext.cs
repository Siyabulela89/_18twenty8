using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _18TWENTY8.Models
{
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
    public class AssignSisterStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignSisterStatusID { get; set; }
        public string description { get; set; }
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
        public int ConfirmMentordurationQ { get; set; }
        public string CVUrl { get; set; }
        public string CertifiedID { get; set; }
        public int ProfileStatusID { get; set; }
        public String VerifCode { get; set; }
        public String VerifCodeComp { get; set; }
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
        public int ConfirmMenteedurationQ { get; set; }
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
