using _18TWENTY8.Models;
using _18TWENTY8.Models.ViewModels;
using _18TWENTY8.Models.ViewModels.BigSister;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Services
{
    public class AdministrationService
    {
        private readonly EighteentwentyeightContext _context;
        private readonly IMapper _mapper;

        public AdministrationService(EighteentwentyeightContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<bool> UpdateProfileStatus(ActionProfileViewModel model)
        {
            var bigSisterDetail = await GetBigSisterDetail(model.UserId);

            if (bigSisterDetail == null)
                throw new Exception($"Big Sister Profile not found: UserId: {model.UserId}");

            var profileStatus = _context
                .ProfileStatus
                .FirstOrDefault(p => p.Role.Equals("Big Sister (Mentor)", StringComparison.OrdinalIgnoreCase)
                && p.Description.Equals(model.Action, StringComparison.OrdinalIgnoreCase));

            if (profileStatus == null)
                throw new Exception($"Big Sister Profile Status not found: Status: {model.Action}");


            bigSisterDetail.ProfileStatusID = profileStatus.ProfileStatusID;

            return await _context.SaveChangesAsync() > 0;


        }

        public async Task<BigSisterProfileViewModel> GetBigSisterProfile(string userId)
        {
            var profileInfo = await GetBigSisterDetail(userId);

            if (profileInfo == null)
                throw new Exception($"Big Sister not found: UserId - {userId}");

            var academicInfo = await GetBigSisterAcademicInfo(profileInfo.BigSisterDetailID);

            return new BigSisterProfileViewModel
            {
                Profile = _mapper.Map<BigSisterDetailViewModel>(profileInfo),
                AcademicRecords = _mapper.Map<List<BigSisterAcademicViewModel>>(academicInfo)
            };
        }

        private async Task<BigSisterDetail> GetBigSisterDetail(string userId)
        {
            return await _context.BigSisterDetail.FirstOrDefaultAsync(s => s.UserID == userId);
        }

        private async Task<List<BigSisterAcademic>> GetBigSisterAcademicInfo(int bigSisterId)
        {
            return await _context.BigSisterAcademic.Where(s => s.BigSisterUserID == bigSisterId).ToListAsync();
        }
    }
}
