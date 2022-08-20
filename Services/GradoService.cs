using WSColegio.Models;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;
namespace WSColegio.Services
{
    public class GradoService : IGradoService
    {
        private readonly IConfiguration _configuration;
        public GradoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Response Add(GradoRequest gradoRequest)
        {
            
            Response response = new();
            if (!new DBContext(_configuration).ProfesorExists(gradoRequest.ProfesorId))
            {
                response.Message = "El alumno no existe";
                return response;
            }
            Grado grado = new()
            {
                Nombre = gradoRequest.Nombre,
                ProfesorId = gradoRequest.ProfesorId
            };
            int result = new DBContext(_configuration).AddGrado(grado);
            if (result > 0)
            {
                grado.Id = result;
                response.Success = 1;
                response.Message = "El grado se insertó correctamente";
                response.Data = grado;
            }
            else
            {
                response.Message = "No se ha podido insertar";
            }
            return response;
        }

        public Response Delete(int id)
        {
            Response response = new();
            if (!new DBContext(_configuration).GradoExists(id))
            {
                response.Message = "El grado no existe";
                return response;
            }
            int result = new DBContext(_configuration).DeleteGrado(id);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El grado se eliminó correctamente";
            }
            else
            {
                response.Message = "No se ha podido eliminar";
            }
            return response;
        }

        public Response Get()
        {
            Response response = new();

            List<Grado>? grados = new DBContext(_configuration).GetGrados(null);
            if (grados == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", grados.Count);
                response.Data = grados;
            }
            return response;
        }

        public Response Update(GradoRequest gradoRequest)
        {
            Response response = new();
            if (!new DBContext(_configuration).ProfesorExists(gradoRequest.ProfesorId))
            {
                response.Message = "El alumno no existe";
                return response;
            }
            if (!new DBContext(_configuration).GradoExists(gradoRequest.Id))
            {
                response.Message = "El grado no existe";
                return response;
            }
            Grado grado = new()
            {
                Id = gradoRequest.Id,
                Nombre = gradoRequest.Nombre,
                ProfesorId = gradoRequest.ProfesorId
            };
            int result = new DBContext(_configuration).UpdateGrado(grado);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El grado se actualizó correctamente";
                response.Data = grado;
            }
            else
            {
                response.Message = "No se ha podido actualizar";
            }
            return response;
        }
    }
}
