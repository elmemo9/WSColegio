using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;

namespace WSColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradoAlumnoController : ControllerBase
    {
        private readonly IGradoAlumnoService _gradoAlumnoService;
        public GradoAlumnoController(IGradoAlumnoService gradoAlumnoService)
        {
            _gradoAlumnoService = gradoAlumnoService;
        }
        [HttpPost]
        public IActionResult Add(GradoAlumnoRequest gradoAlumnoRequest)
        {
            Response response = _gradoAlumnoService.Add(gradoAlumnoRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = _gradoAlumnoService.Delete(id);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Get()
        {
            Response response = _gradoAlumnoService.Get();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpPatch]
        public IActionResult Update(GradoAlumnoRequest gradoAlumnoRequest)
        {
            Response response = _gradoAlumnoService.Update(gradoAlumnoRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
    }
}
