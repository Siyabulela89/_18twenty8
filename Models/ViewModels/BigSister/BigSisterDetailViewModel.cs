using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels.BigSister
{
    public class BigSisterDetailViewModel
    {
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

    }
}
