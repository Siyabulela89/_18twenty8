using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Models.ViewModels
{
    public class EditRoleViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Role Name is required")]

        public string RoleName { get; set; }
        public List<String> Users { get; set; }
    }
}
