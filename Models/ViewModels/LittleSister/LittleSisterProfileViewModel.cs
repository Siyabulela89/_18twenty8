using _18TWENTY8.Models.ViewModels.LittleSister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels.LittleSister
{
    public class LittleSisterProfileViewModel
    {
        public LittleSisterProfileViewModel()
        {
            AcademicRecords = new List<LittleSisterAcademicViewModel>();
            InformationOfInterest = new List<SisterInformationOfInterestViewModel>();
        }
        public LittleSisterDetailViewModel Profile { get; set; }
        public List<LittleSisterAcademicViewModel> AcademicRecords { get; set; }
        public List<SisterInformationOfInterestViewModel> InformationOfInterest { get; set; }
        
    }
}
