using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels
{
    public class SisterMessagingViewModel
    {
        public string SendUserId { get; set; }
        public string ReceiveUserID { get; set; }
         public string RoleID { get; set; }
        public string Message { get; set; }
        public LittleSisterDetail LittleSisterProfile { get; set; }

        public BigSisterDetail BigSisterProfile { get; set; }
        public List<Messaging> Messages { get; set; }
    }
    public class SisterCreateMessageViewModel
    {
        public string SendUserId { get; set; }
        public string ReceiveUserID { get; set; }
        public string Message { get; set; }
        public string RoleID { get; set; }
    }
}
