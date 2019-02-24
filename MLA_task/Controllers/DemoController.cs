using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MLA_task.BLL.Interface;
using MLA_task.BLL.Interface.Exceptions;
using MLA_task.BLL.Interface.Models;
using NLog;

namespace MLA_task.Controllers
{
    public class DemoController : ApiController
    {
        private readonly ILogger _logger;
        private readonly IDemoModelService _demoModelService;

        public DemoController(ILogger logger, IDemoModelService demoModelService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _demoModelService = demoModelService ?? throw new ArgumentNullException(nameof(demoModelService));
        }

        // GET (all)
        public async Task<IHttpActionResult> Get()
        {
            var models = await _demoModelService.GetAllDemoModelsAsync();

            return Ok(models.Select(model => new
            {
                model.Id,
                model.Name,
                Info = model.CommonInfo,
                model.Created,
                model.Modified
            }));
        }

        // GET (by id)
        public async Task<IHttpActionResult> Get(int id)
        {
            _logger.Info($"receiving item with id {id}");

            try
            {
                var model = await _demoModelService.GetDemoModelByIdAsync(id);

                _logger.Info($"item with id {id} has been received.");

                return Ok(model);
            }
            catch (DemoServiceException ex)
            {
                if (ex.Error == DemoServiceException.ErrorType.WrongId) 
                {
                    _logger.Info(ex, $"Wrong ID {id} has been requested");
                    return this.BadRequest("Wrong ID");
                }

                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Server error occured while trying to get item with id {id}");
                return this.InternalServerError(ex);
            }
        }

        // POST (create item)
        public async Task<IHttpActionResult> Post([FromBody]DemoModel model)
        {
            _logger.Info($"adding model with name {model.Name}");

            try
            {
                await _demoModelService.CreateDemoModelAsync(model);

                return Ok(model);
            }
            catch (DemoServiceException ex)
            {
                if (ex.Error == DemoServiceException.ErrorType.WrongName)
                {
                    _logger.Info($"Wrong model name {model.Name} detected");
                    return this.BadRequest("Wrong name");
                }

                throw ex;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Server error occured while trying to add item with name {model.Name}");
                return this.InternalServerError();
            }
        }

        // DELETE (by id)
        public async Task<IHttpActionResult> Delete(int id)
        {
            _logger.Info($"deleting item with id {id}");

            try
            {
                await _demoModelService.DeleteDemoModelByIdAsync(id);

                _logger.Info($"item with id {id} has been deleted.");

                return Ok();
            }
            catch (DemoServiceException ex)
            {
                if (ex.Error == DemoServiceException.ErrorType.WrongId)
                {
                    _logger.Info(ex, $"Wrong ID {id} has been requested");
                    return this.BadRequest("Wrong ID");
                }

                throw ex;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Server error occured while trying to get item with id {id}");
                return this.InternalServerError(ex);
            }
        }
    }
}