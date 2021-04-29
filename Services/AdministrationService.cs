using _18TWENTY8.Models;
using _18TWENTY8.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Services
{
    public class AdministrationService
    {
        private readonly EighteentwentyeightContext _context;

        public AdministrationService(EighteentwentyeightContext context)
        {
            this._context = context;
        }

        public async Task<bool> UpdateProfileStatus(ActionProfileViewModel model)
        {
            var bigSisterProfile = _context.BigSisterDetail.FirstOrDefault(s => s.UserID == model.UserId);

            if (bigSisterProfile == null)
                throw new Exception($"Big Sister Profile not found: UserId: {model.UserId}");

            var profileStatus = _context
                .ProfileStatus
                .FirstOrDefault(p => p.Role.Equals("Big Sister (Mentor)", StringComparison.OrdinalIgnoreCase)
                && p.Description.Equals(model.Action, StringComparison.OrdinalIgnoreCase));

            if (profileStatus == null)
                throw new Exception($"Big Sister Profile Status not found: Status: {model.Action}");


            bigSisterProfile.ProfileStatusID = profileStatus.ProfileStatusID;

            return await _context.SaveChangesAsync() > 0;


        }
    }
}
