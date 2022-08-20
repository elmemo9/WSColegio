using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;

namespace WSColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;
        public ProfesorController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }
        [HttpPost]
        public IActionResult Add(ProfesorRequest profesorRequest)
        {
            Response response = _profesorService.Add(profesorRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = _profesorService.Delete(id);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Get()
        {
            Response response = _profesorService.Get();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpPatch]
        public IActionResult Update(ProfesorRequest profesorRequest)
        {
            Response response = _profesorService.Update(profesorRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
    }
}
