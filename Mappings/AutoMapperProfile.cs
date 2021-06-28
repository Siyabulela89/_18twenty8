using _18TWENTY8.Models;
using _18TWENTY8.Models.ViewModels.BigSister;
using _18TWENTY8.Models.ViewModels.LittleSister;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _18TWENTY8.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BigSisterDetail, BigSisterDetailViewModel>();
            CreateMap<BigSisterAcademic, BigSisterAcademicViewModel>();
            CreateMap<LittleSisterDetail, LittleSisterDetailViewModel>();
            CreateMap<LittleSisterAcademic, LittleSisterAcademicViewModel>();
            CreateMap<InformationInterest, SisterInformationOfInterestViewModel>();
            CreateMap<UpdateLittleSisterModel, LittleSisterDetail>();
            CreateMap<UpdateBigSisterModel, BigSisterDetail>();
        }
    }
}
