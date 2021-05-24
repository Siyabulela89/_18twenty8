using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels.BursaryApplicant
{
    public class BursaryApplicantviewm
    {

     
        public List<BursaryApplicationCandidate> BursCand { get; set; }
        public FinancialSupport FinancialsS { get; set; }
        public List<BursaryStatus> AppstatusBursary { get; set; }
        public ApplicationStatus ApplicationStatusProfile { get; set; }
        public List<BursaryApplication> Bursarylist { get; set; }
    }
}
