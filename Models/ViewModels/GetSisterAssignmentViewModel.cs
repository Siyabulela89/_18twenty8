using _18TWENTY8.Models.ViewModels.BigSister;
using _18TWENTY8.Models.ViewModels.LittleSister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels
{
    public class GetSisterAssignmentViewModel
    {
        public LittleSisterDetailViewModel Mentee { get; set; }
        public List<BigSisterDetailViewModel> Mentors { get; set; }
    }
}
