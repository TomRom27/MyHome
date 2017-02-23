using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace MyHome.Models
{
    public class MvcProfile : Profile
    {
        public override string ProfileName
        {
            get { return "MvcProfile"; }
        }

        [Obsolete]
        protected override void Configure()
        {
            CreateMap<DataModel.Readout, Models.ReadoutViewModel>()
                .ForMember(dest => dest.DeviceId, m => m.MapFrom(src => src.Device.DeviceId))
                .ForMember(dest => dest.AddressName, m => m.MapFrom(src => src.Device.DeviceAddress.FriendlyName))
                .ForMember(dest => dest.AddressId, m => m.MapFrom(src => src.Device.DeviceAddress.AddressId))
                .ForMember(dest => dest.ReadoutType, m => m.MapFrom(src => src.Device.SensorType))
                ;

            CreateMap<DataModel.Address, Models.AddressViewModel>()
                ;

            CreateMap<Models.AddressViewModel, DataModel.Address>()
                ;

            CreateMap<DataModel.Device, Models.DeviceViewModel>()
                .ForMember(dest => dest.IsOn, m => m.MapFrom(src => src.ActionState == DataModel.ActionStateEnum.On))
                .ForMember(dest => dest.AddressId, m => m.MapFrom(src => src.DeviceAddress.AddressId))
                .ForMember(dest => dest.Last, m => m.MapFrom(src => src.Readouts.LastOrDefault()))
                ;

            CreateMap<Models.DeviceViewModel, DataModel.Device>()
                .ForMember(dest => dest.ActionState, m => m.MapFrom(src =>  Convert.ToInt32( src.IsOn )))
                ;
        }
    }
}