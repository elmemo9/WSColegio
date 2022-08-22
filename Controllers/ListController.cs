using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;

namespace WSColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly IListService _listService;
        public ListController (IListService listService)
        {
            _listService = listService;
        }
        [HttpGet("{table}")]
        public IActionResult Get(string table)
        {
            switch (table)
            {
                case "alumno": return GetAlumnos();
                case "grado": return GetGrados();
                case "profesor": return GetProfesores();
                default: return BadRequest("No se encuentra la lista solicitada");
            }
            
        }
        private IActionResult GetAlumnos()
        {
            Response response = _listService.GetAlumnos();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        private IActionResult GetGrados()
        {
            Response response = _listService.GetGrados();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        private IActionResult GetProfesores()
        {
            Response response = _listService.GetProfesores();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }

    }
}
