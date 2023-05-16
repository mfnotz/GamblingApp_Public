using Core.Abstractions.Controllers;
using Core.Abstractions.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using ILogger = Serilog.ILogger;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController<TEntityModel, TEntityDTO> : ControllerBase, IBaseController<TEntityDTO> where TEntityModel : BaseEntityModel where TEntityDTO : BaseEntityDTO
    {
        internal IBaseService<TEntityModel, TEntityDTO> _genericService { get; set; }
        internal ILogger _logger { get; set; }
        internal IConfiguration _configuration { get; set; }

        public BaseController(IBaseService<TEntityModel, TEntityDTO> genericService, IConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            _genericService = genericService;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async virtual Task<ActionResult<TEntityDTO>> Get(int id)
        {
            var obj = await _genericService.Get(id);

            if (obj == null)
                return NotFound();

            return obj;
        }

        [HttpGet]
        public async virtual Task<ActionResult<List<TEntityDTO>>> Get()
        {
            var objs = await _genericService.Get();

            if (objs.Count() == 0)
                return NotFound();

            return objs;
        }

        [HttpGet("{page}/{pageSize}")]
        public async virtual Task<ActionResult<PaginationResult<TEntityDTO>>> GetPage(int page, int pageSize)
        {
            var obj = await _genericService.GetPage(page, pageSize);

            if (obj.TotalItems == 0)
                return NotFound();

            return obj;
        }

        [HttpPost]
        public async virtual Task<ActionResult<TEntityDTO>> Add([FromBody] TEntityDTO obj)
        {
            var added = await _genericService.Add(obj);

            if (added == null)
                return BadRequest("There was an issue handling your request, please contact your system administrator");

            return CreatedAtAction("Get", new { id = obj.Id }, obj);
        }

        [HttpPut]
        public async virtual Task<ActionResult<TEntityDTO>> Update([FromBody] TEntityDTO obj)
        {

            var updated = await _genericService.Update(obj);

            if (updated == null)
                return BadRequest("There was an issue handling your request, please contact your system administrator");

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async virtual Task<ActionResult<TEntityDTO>> Delete(int id)
        {
            var deleted = await _genericService.Delete(id);

            if (deleted == null)
                return BadRequest("There was an issue handling your request, please contact your system administrator");

            return deleted;
        }


        protected string GetWindowsUsername()
        {
            WindowsPrincipal wp = new WindowsPrincipal(WindowsIdentity.GetCurrent());

            var userName = wp.Identity.Name;

            return userName.Contains(@"\") ? userName.Split('\\')[1] : userName;
        }

    }
}
