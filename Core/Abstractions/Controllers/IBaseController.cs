
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.Abstractions.Controllers
{
    public interface IBaseController<TEntityDTO> where TEntityDTO : BaseEntityDTO
    {
      //  Task<ActionResult<TEntityDTO>> Get(int id);

        Task<ActionResult<List<TEntityDTO>>> Get();

        Task<ActionResult<PaginationResult<TEntityDTO>>> GetPage(int page, int pageSize);

        Task<ActionResult<TEntityDTO>> Add(TEntityDTO obj);

        Task<ActionResult<TEntityDTO>> Update(TEntityDTO obj);

        Task<ActionResult<TEntityDTO>> Delete(int id);
    }
}
