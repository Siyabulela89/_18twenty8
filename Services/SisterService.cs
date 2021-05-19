using _18TWENTY8.Models;
using _18TWENTY8.Models.ViewModels;
using _18TWENTY8.Models.ViewModels.BigSister;
using _18TWENTY8.Models.ViewModels.LittleSister;
using AutoMapper;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Services
{
    public class SisterService
    {
        private readonly EighteentwentyeightContext _context;
        private readonly IMapper _mapper;

        public SisterService(EighteentwentyeightContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<bool> UpdateProfileStatus(ActionProfileViewModel model)
        {
            return model.Role.Equals("Big Sister (Mentor)", StringComparison.OrdinalIgnoreCase)
                ? await UpdateBigSisterProfileStatus(model)
                : await UpdateLittleSisterProfileStatus(model);
        }

        public async Task<bool> UpdateSisterAssignStatus(SisterAssignmentActionViewModel model)
        {
            var sisterAssign = await _context.SisterAssignment.FirstOrDefaultAsync(s => s.SisAssID == model.SisterAssignId);

            if (sisterAssign == null)
                throw new Exception($"Sister assignment record not found: AssignmentId - {model.SisterAssignId}");

            var status = model.Action.Equals("Approved", StringComparison.OrdinalIgnoreCase) ? 3 : 2; //TODO: Confirm status

            if (model.Role.Equals("Little Sister (Mentee)", StringComparison.OrdinalIgnoreCase))
            {
                sisterAssign.LittleApproveID = status;
                if (sisterAssign.BigApproveID == 3 && status==3)
                    sisterAssign.AssignSisterStatusID = 2;
                
            }
            else if (model.Role.Equals("Big Sister (Mentor)", StringComparison.OrdinalIgnoreCase))
            {
                sisterAssign.BigApproveID = status;
                if (sisterAssign.LittleApproveID ==3 && status == 3)
                    sisterAssign.AssignSisterStatusID = 2;
            }
    
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AssignBigSisterToLittleSister(AssignSisterViewModel model)
        {
            var sisterAssign = await _context.SisterAssignment.FirstOrDefaultAsync(s => s.LittleSisterID.Equals(model.LittleSisterUserId) && string.IsNullOrEmpty(s.BigSisterID));

            if (sisterAssign == null)
                throw new Exception($"Cannot find Assignment record for Little Sister - {model.LittleSisterUserId}");

            sisterAssign.BigSisterID = model.BigSisterUserId;
            sisterAssign.BigApproveID = 1;
            sisterAssign.LittleApproveID = 1;

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
        public async Task<GetSisterAssignmentViewModel> GetSisterAssignment(string littleSisterUserId)
        {
            var mentee = await GetLittleSisterDetail(littleSisterUserId);
            var mentors = await GetApprovedBigSisters();

            return new GetSisterAssignmentViewModel
            {
                Mentee = _mapper.Map<LittleSisterDetailViewModel>(mentee),
                Mentors = _mapper.Map<List<BigSisterDetailViewModel>>(mentors)
            };

        }
        public async Task<LittleSisterProfileViewModel> GetLittleSisterProfile(string userId)
        {
            var profileInfo = await GetLittleSisterDetail(userId);

            if (profileInfo == null)
                throw new Exception($"Little Sister not found: UserId - {userId}");

            var academicInfo = await GetLittleSisterAcademicInfo(profileInfo.LittleSisterDetailID);
            var interests = await GetLittleSisterInformationOfInterest(profileInfo.LittleSisterDetailID);

            var littleProfile = new LittleSisterProfileViewModel
            {
                Profile = _mapper.Map<LittleSisterDetailViewModel>(profileInfo),
                AcademicRecords = _mapper.Map<List<LittleSisterAcademicViewModel>>(academicInfo),
                InformationOfInterest = _mapper.Map<List<SisterInformationOfInterestViewModel>>(interests)
            };

            var profileStatus = await GetProfileStatus(profileInfo.ProfileStatusID);
            var Interaction = await GetInteractionLevel(profileInfo.Interactionlevelmeet);
            var Interactiondigi = await GetInteractionLevel(profileInfo.InteractionlevelDigCom);
            var sisterAssignment = await GetLittleSisterAssignment(userId);
            var assignStatus = await GetSisterAssignmentStatus(sisterAssignment.AssignSisterStatusID);
            var assignStatusApp = await GetSisterAssignmentStatus(sisterAssignment.LittleApproveID);
            var Menteed = await GetyesnoOption(profileInfo.EverbeenamenteeQ);
            var Convicted = await GetyesnoOption(profileInfo.ArrestedConvictedQ);

            var assignedBigSister = !string.IsNullOrEmpty(sisterAssignment.BigSisterID) ? await GetBigSisterDetail(sisterAssignment.BigSisterID) : null;
            if (assignedBigSister != null)
            {
                littleProfile.Profile.AssignedSister = $"{assignedBigSister.Name} {assignedBigSister.Surname}";
                littleProfile.Profile.AssignedSisterId = assignedBigSister.UserID;
             
               
            }
            littleProfile.Profile.AssignedSisterStatus = assignStatus.description;
            littleProfile.Profile.SisterAssignId = sisterAssignment.SisAssID;
            littleProfile.Profile.Interactionlevelmeetd = Interaction.Description;
            littleProfile.Profile.InteractionlevelDigComd = Interactiondigi.Description;
            littleProfile.Profile.ProfileStatus = profileStatus.Description;
            littleProfile.Profile.SisterAssignStatus = assignStatus.description;
            littleProfile.Profile.LittleApproveID = sisterAssignment.LittleApproveID;
            littleProfile.Profile.BigApproveID = sisterAssignment.BigApproveID;
            littleProfile.Profile.SisterStatus = assignStatus.description;
            littleProfile.Profile.EverBeenAMenteed = Menteed.Description;
  
    
        littleProfile.Profile.PreviouslyArrestedOrConvictedd = Convicted.Description;


            return littleProfile;

        }
        private async Task<bool> UpdateBigSisterProfileStatus(ActionProfileViewModel model)
        {
            var bigSisterDetail = await GetBigSisterDetail(model.UserId);

            if (bigSisterDetail == null)
                throw new Exception($"Big Sister Profile not found: UserId: {model.UserId}");

            var profileStatus = await GetProfileStatus(model.Role, model.Action);

            if (profileStatus == null)
                throw new Exception($"Big Sister Profile Status not found: Status: {model.Action}");

            bigSisterDetail.ProfileStatusID = profileStatus.ProfileStatusID;
            bigSisterDetail.profilestatusreason = model.Reason;

            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<bool> UpdateLittleSisterProfileStatus(ActionProfileViewModel model)
        {
            var littleSisterDetail = await GetLittleSisterDetail(model.UserId);

            if (littleSisterDetail == null)
                throw new Exception($"Little Sister Profile not found: UserId: {model.UserId}");

            var profileStatus = await GetProfileStatus(model.Role, model.Action);

            if (profileStatus == null)
                throw new Exception($"Little Sister Profile Status not found: Status: {model.Action}");

            littleSisterDetail.ProfileStatusID = profileStatus.ProfileStatusID;
            littleSisterDetail.profilestatusreason = model.Reason;

            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<ProfileStatus> GetProfileStatus(string role, string action)
        {
            return await _context
                .ProfileStatus
                .FirstOrDefaultAsync(p => p.Role.Equals(role, StringComparison.OrdinalIgnoreCase)
                && p.Description.Equals(action, StringComparison.OrdinalIgnoreCase));
        }
        private async Task<BigSisterDetail> GetBigSisterDetail(string userId)
        {
            return await _context.BigSisterDetail.FirstOrDefaultAsync(s => s.UserID == userId);
        }
        private async Task<List<BigSisterDetail>> GetApprovedBigSisters()
        {
            return await _context.BigSisterDetail.Where(s => s.ProfileStatusID == 6).ToListAsync();
        }
        private async Task<ProfileStatus> GetProfileStatus(int profileStatusId)
        {
            return await _context.ProfileStatus.FirstOrDefaultAsync(ps => ps.ProfileStatusID == profileStatusId);
        }
        private async Task<OptionalBool> GetyesnoOption(int OptYN)

        {
            int? yesn = Convert.ToInt32(OptYN);
            return await _context.OptionalBool.FirstOrDefaultAsync(OP => OP.YesNoID == yesn);
        }
        private async Task<InteractionLevel> GetInteractionLevel(int InteractionID)
        {
            return await _context.InteractionLevel.FirstOrDefaultAsync(il => il.InteractionLevelID == InteractionID);
        }
        private async Task<SisterAssignment> GetLittleSisterAssignment(string littleSisterId)
        {
            return await _context.SisterAssignment.FirstOrDefaultAsync(sa => sa.LittleSisterID.Equals(littleSisterId, StringComparison.OrdinalIgnoreCase));
        }
        private async Task<AssignSisterStatus> GetSisterAssignmentStatus(int assignSisterStatusID)
        {
            return await _context.AssignSisterStatus.FirstOrDefaultAsync(s => s.AssignSisterStatusID == assignSisterStatusID);
        }
        private async Task<List<BigSisterAcademic>> GetBigSisterAcademicInfo(int bigSisterId)
        {
            return await _context.BigSisterAcademic.Where(s => s.BigSisterUserID == bigSisterId).ToListAsync();
        }
        private async Task<LittleSisterDetail> GetLittleSisterDetail(string userId)
        {
            return await _context.LittleSisterDetail.FirstOrDefaultAsync(s => s.UserID == userId);
        }
        private async Task<List<LittleSisterAcademic>> GetLittleSisterAcademicInfo(int littleSisterId)
        {
            return await _context.LittleSisterAcademic.Where(s => s.LittleSisterUserID == littleSisterId).ToListAsync();
        }
        private async Task<List<InformationInterest>> GetLittleSisterInformationOfInterest(int littleSisterId)
        {
            var interestIds = await _context.InformationofStorageLittle.Where(s => s.UserID == littleSisterId).Select(i => i.InformationofInterestID).ToListAsync();

            if (interestIds.Count > 0)
                return await _context.InformationInterest.Where(i => interestIds.Contains(i.InformationofInterestID)).ToListAsync();

            return new List<InformationInterest>();
        }
    }
}
