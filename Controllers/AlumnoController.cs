using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;


namespace WSColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly IAlumnoService _alumnoService;
        public AlumnoController(IAlumnoService alumnoService)
        {
            _alumnoService = alumnoService;
        }
        [HttpPost]
        public IActionResult Add(AlumnoRequest alumnoRequest)
        {
            Response response = _alumnoService.Add(alumnoRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = _alumnoService.Delete(id);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Get()
        {
            Response response = _alumnoService.Get();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpPatch]
        public IActionResult Update(AlumnoRequest alumnoRequest)
        {
            Response response = _alumnoService.Update(alumnoRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
    }
}
