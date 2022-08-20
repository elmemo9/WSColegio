using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;

namespace WSColegio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradoController : ControllerBase
    {
        private readonly IGradoService _gradoService;
        public GradoController(IGradoService gradoService)
        {
            _gradoService = gradoService;
        }
        [HttpPost]
        public IActionResult Add(GradoRequest gradoRequest)
        {
            Response response = _gradoService.Add(gradoRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response response = _gradoService.Delete(id);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult Get()
        {
            Response response = _gradoService.Get();
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
        [HttpPatch]
        public IActionResult Update(GradoRequest gradoRequest)
        {
            Response response = _gradoService.Update(gradoRequest);
            if (response.Success == 0) return BadRequest(response.Message);
            return Ok(response);
        }
    }
}
