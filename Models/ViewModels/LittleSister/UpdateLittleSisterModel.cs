using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels.LittleSister
{
    public class UpdateLittleSisterModel
    {
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
            public IList<SelectListItem> Infosbig { get; set; }

        }
    }

