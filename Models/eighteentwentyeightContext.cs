using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _18TWENTY8.Models
{
    public class EighteentwentyeightContext :DbContext

{
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public EighteentwentyeightContext(DbContextOptions<EighteentwentyeightContext> options)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
             : base(options)
        {
        }
        public object ApplicationUser { get; internal set; }
        public DbSet<Graduates> Graduates { get; set; }
        public DbSet<BursaryType> BursaryType { get; set; }
        public DbSet<Time> Time { get; set; }
        public DbSet<EMessage> EMessage { get; set; }
        public DbSet<ConfidentialityStatement> ConfidentialityStatement { get; set; }
        
        public DbSet<VolunteerAcademic> VolunteerAcademic { get; set; }
        public DbSet<Volunteerdetail> Volunteerdetail { get; set; }
        public DbSet<Committees> Committees { get; set; }
        public DbSet<CommitteesStorage> CommitteesStorage { get; set; }
        public DbSet<Daysofweek> Daysofweek { get; set; }
        public DbSet<Daysofweekstorage> Daysofweekstorage { get; set; }
        public DbSet<Programmes> Programmes { get; set; }
        public DbSet<ProgrammesStorage> ProgrammesStorage { get; set; }
        
        public DbSet<BursaryApplicationStatusApp> BursaryApplicationStatusApp { get; set; }
        public DbSet<BursaryApplicationCandidate> BursaryApplicationCandidate { get; set; }
        

        public DbSet<FinancialSupport> FinancialSupport { get; set; }
        
                    public DbSet<Messaging> Messaging { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatus { get; set; }
        public DbSet<InformationofStorageLittle> InformationofStorageLittle { get; set; }
        public DbSet<LittleSisterAcademic> LittleSisterAcademic { get; set; }
        public DbSet<ProfileStatus> ProfileStatus { get; set; }
        public DbSet<RecognitionLetters> RecognitionLetters { get; set; }
        public DbSet<SisterAssignment> SisterAssignment { get; set; }
        public DbSet<AssignSisterStatus> AssignSisterStatus { get; set; }
        
        public DbSet<AssignApprove> AssignApprove { get; set; }
        public DbSet<PressPublicity> PressPublicity { get; set; }
        public DbSet<PressPubType> PressPubType { get; set; }
        public DbSet<BigSisterDetail> BigSisterDetail { get; set; }
        public DbSet<LittleSisterDetail> LittleSisterDetail { get; set; }
        public DbSet<OptionalBool> OptionalBool { get; set; }
        public DbSet<InteractionLevel> InteractionLevel { get; set; }
        public DbSet<InformationInterest> InformationInterest { get; set; }

        public DbSet<AdditionalSupportStorageBig> AdditionalSupportStorageBig { get; set; }
        public DbSet<InformationofStorageBig> InformationofStorageBig { get; set; }
        public DbSet<AdditionalSupportBig> AdditionalSupportBig { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<Workshops> Workshops { get; set; }
        public DbSet<WorkshopsupportDocType> WorkshopsupportDocType { get; set; }
        public DbSet<BigSisterAcademic> BigSisterAcademic { get; set; }
        public DbSet<BursaryApplication> BursaryApplication { get; set; }
        public DbSet<BursaryApplicationRequest> BursaryApplicationRequest { get; set; }
                public DbSet<BursaryStatus> BursaryStatus { get; set; }




        public DbSet<_18TWENTY8.Models.ApplicationRole> ApplicationRole { get; set; }
}
}
