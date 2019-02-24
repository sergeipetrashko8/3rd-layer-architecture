using System.Collections.Generic;
using System.Threading.Tasks;
using MLA_task.DAL.Interface.Entities;

namespace MLA_task.DAL.Interface
{
    public interface IDemoDbModelRepository
    {
        Task<DemoDbModel> GetByIdAsync(int id);

        Task<DemoCommonInfoDbModel> GetCommonInfoByDemoIdAsync(int demoDbModelId);

        Task<IEnumerable<DemoDbModel>> GetAllAsync();

        Task AddAsync(DemoDbModel model);

        Task DeleteAsync(int id);
    }
}
