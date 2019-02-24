using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MLA_task.DAL.Interface;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.Repositories
{
    public class DemoDbModelRepository : IDemoDbModelRepository
    {
        private readonly DbContext _context;

        public DemoDbModelRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DemoDbModel> GetByIdAsync(int id)
        {
            var model = await _context.Set<DemoDbModel>().SingleAsync(item => item.Id == id);

            return model;
        }

        public async Task<DemoCommonInfoDbModel> GetCommonInfoByDemoIdAsync(int demoDbModelId)
        {
            var demoModel = await _context.Set<DemoDbModel>().SingleAsync(item => item.Id == demoDbModelId);

            var commonInfo = await _context.Set<DemoCommonInfoDbModel>().SingleAsync(item => item.Id == demoModel.DemoCommonInfoModelId);

            return commonInfo;
        }

        public async Task<IEnumerable<DemoDbModel>> GetAllAsync()
        {
            return await _context.Set<DemoDbModel>()
                                 .Include(model => model.DemoCommonInfoModel)
                                 .ToArrayAsync();
        }

        public async Task AddAsync(DemoDbModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            _context.Set<DemoDbModel>().Add(model);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemForDelete = await _context.Set<DemoDbModel>().SingleAsync(item => item.Id == id);

            _context.Set<DemoDbModel>().Remove(itemForDelete);

            await _context.SaveChangesAsync();
        }
    }
}