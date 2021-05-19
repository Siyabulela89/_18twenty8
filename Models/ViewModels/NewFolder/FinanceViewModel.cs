using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels.NewFolder
{
   
        public class FinanceApproveview
        {
          
           public string Appstatus { get; set; }
           
        public FinancialSupport finsupport { get; set; }
        public List<ApplicationStatus> Application { get; set; }

        }
        public class FinanceApproveStore
        {
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
    }

