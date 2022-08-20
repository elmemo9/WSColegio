using WSColegio.Models;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;

namespace WSColegio.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IConfiguration _configuration;
        public AlumnoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Response Add(AlumnoRequest alumnoRequest)
        {
            Response response = new();
            Alumno alumno = new()
            {
                Nombre = alumnoRequest.Nombre,
                Apellidos = alumnoRequest.Apellidos,
                Genero = alumnoRequest.Genero,
                FechaNacimiento = alumnoRequest.FechaNacimiento
            };
            int result = new DBContext(_configuration).AddAlumno(alumno);
            if (result > 0)
            {
                alumno.Id = result;
                response.Success = 1;
                response.Message = "El alumno se insertó correctamente";
                response.Data = alumno;
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
            if (!new DBContext(_configuration).AlumnoExists(id))
            {
                response.Message = "El alumno no existe";
                return response;
            }
            int result = new DBContext(_configuration).DeleteAlumno(id);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El alumno se eliminó correctamente";
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

            List <Alumno>? alumnos = new DBContext(_configuration).GetAlumnos(null);
            if (alumnos == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", alumnos.Count);
                response.Data = alumnos;
            }
            return response;
        }

        public Response Update(AlumnoRequest alumnoRequest)
        {
            Response response = new();
            if (!new DBContext(_configuration).AlumnoExists(alumnoRequest.Id))
            {
                response.Message = "El alumno no existe";
                return response;
            }
            Alumno alumno = new()
            {
                Id = alumnoRequest.Id,
                Nombre = alumnoRequest.Nombre,
                Apellidos = alumnoRequest.Apellidos,
                Genero = alumnoRequest.Genero,
                FechaNacimiento = alumnoRequest.FechaNacimiento
            };
            int result = new DBContext(_configuration).UpdateAlumno(alumno);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El alumno se actualizó correctamente";
                response.Data = alumno;
            }
            else
            {
                response.Message = "No se ha podido actualizar";
            }
            return response;
        }
    }
}
