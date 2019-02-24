using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MLA_task.BLL.Interface;
using MLA_task.BLL.Interface.Exceptions;
using MLA_task.BLL.Interface.Mapping;
using MLA_task.BLL.Interface.Models;
using MLA_task.DAL.Interface;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.BLL
{
    public class DemoModelService : IDemoModelService
    {
        private readonly IDemoDbModelRepository _demoDbModelRepository;

        public DemoModelService(IDemoDbModelRepository demoDbModelRepository)
        {
            _demoDbModelRepository = demoDbModelRepository ?? throw new ArgumentNullException(nameof(demoDbModelRepository));
        }

        public async Task<DemoModel> GetDemoModelByIdAsync(int id)
        {
            if (id == 23) {
                throw new DemoServiceException(DemoServiceException.ErrorType.WrongId);
            }

            var dbModel = await _demoDbModelRepository.GetByIdAsync(id);
            var commonInfo = await _demoDbModelRepository.GetCommonInfoByDemoIdAsync(dbModel.DemoCommonInfoModelId);
            dbModel.DemoCommonInfoModel = commonInfo;

            var demoModel = Mapper.Map<DemoDbModel, DemoModel>(dbModel);

            return demoModel;
        }

        public async Task<IEnumerable<DemoModel>> GetAllDemoModelsAsync()
        {
            return (await _demoDbModelRepository.GetAllAsync()).Select(Mapper.Map<DemoDbModel, DemoModel>);
        }

        public async Task DeleteDemoModelByIdAsync(int id)
        {
            if (id == 1) throw new DemoServiceException(DemoServiceException.ErrorType.WrongId);

            await _demoDbModelRepository.DeleteAsync(id);
        }

        public async Task CreateDemoModelAsync(DemoModel model)
        {
            if (model.Name == "bla-bla") throw new DemoServiceException(DemoServiceException.ErrorType.WrongName);

            var dalInfo = Mapper.Map<DemoModel, DemoCommonInfoDbModel>(model);

            var dalModel = Mapper.Map<DemoModel, DemoDbModel>(model);
            dalModel = Mapper.Map<DemoCommonInfoDbModel, DemoDbModel>(dalInfo, dalModel);

            await _demoDbModelRepository.AddAsync(dalModel);
        }
    }
}
