using AutoMapper;
using MLA_task.BLL.Interface.Models;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.BLL.Interface.Mapping
{
    public class DemoMapperProfile : Profile 
    {
        public DemoMapperProfile()
        {
            this.DemoMap();
        }

        private void DemoMap()
        {
            /*
             * DemoModel -> DemoDbModel(except DemoCommonInfoModel and DemoCommonInfoModelId)
             * then 
             * DemoCommonInfoDbModel -> DemoDbModel(add DemoCommonInfoModel and DemoCommonInfoModelId)
             */

            CreateMap<DemoModel, DemoDbModel>()
                .ForMember(dbModel => dbModel.DemoCommonInfoModelId, model => model.Ignore())
                .ForMember(dbModel => dbModel.DemoCommonInfoModel, model => model.Ignore());

            CreateMap<DemoCommonInfoDbModel, DemoDbModel>()
                //.ForMember(dbModel => dbModel.DemoCommonInfoModelId, opt => opt.Ignore()/*MapFrom(dbInfo => dbInfo.Id)*/)
                .ForMember(dbModel => dbModel.DemoCommonInfoModel, opt => opt.MapFrom(dbInfo => dbInfo))
                .ForAllOtherMembers(opt => opt.Ignore());

            /*
             * DemoModel -> DemoCommonInfoDbModel
             */

            CreateMap<DemoModel, DemoCommonInfoDbModel>()
                .ForMember(infoDbModel => infoDbModel.CommonInfo, model => model.MapFrom(modelMember => modelMember.CommonInfo))
                .ForAllOtherMembers(model => model.Ignore());

            /*
             * DemoDbModel -> DemoModel
             */

            CreateMap<DemoDbModel, DemoModel>()
                .ForMember(model => model.CommonInfo, opt => opt.MapFrom(dbModel => dbModel.DemoCommonInfoModel.CommonInfo));
        }
    }
}