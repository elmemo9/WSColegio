using WSColegio.Models;
using WSColegio.Requests;
using WSColegio.Responses;
using WSColegio.Services.Interfaces;
namespace WSColegio.Services
{
    public class GradoAlumnoService : IGradoAlumnoService
    {
        private readonly IConfiguration _configuration;
        public GradoAlumnoService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public Response Add(GradoAlumnoRequest gradoAlumnoRequest)
        {
            Response response = new();
            string message = Validations(gradoAlumnoRequest);
            if(message != string.Empty)
            {
                response.Message = message;
                return response;
            }


            GradoAlumno gradoAlumno = new()
            {
                AlumnoId = gradoAlumnoRequest.AlumnoId,
                GradoId = gradoAlumnoRequest.GradoId,
                Seccion = gradoAlumnoRequest.Seccion
            };
            int result = new DBContext(_configuration).AddGradoAlumno(gradoAlumno);
            if (result > 0)
            {
                gradoAlumno.Id = result;
                response.Success = 1;
                response.Message = "El GradoAlumno se insertó correctamente";
                response.Data = gradoAlumno;
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
            if (!new DBContext(_configuration).GradoAlumnoExists(id))
            {
                response.Message = "El GradoAlumno no existe";
                return response;
            }
            int result = new DBContext(_configuration).DeleteGradoAlumno(id);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El GradoAlumno se eliminó correctamente";
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

            List<GradoAlumno>? gradoAlumnos = new DBContext(_configuration).GetGradoAlumnos(null);
            if (gradoAlumnos == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", gradoAlumnos.Count);
                response.Data = gradoAlumnos;
            }
            return response;
        }

        public Response Update(GradoAlumnoRequest gradoAlumnoRequest)
        {
            Response response = new();
            string message = Validations(gradoAlumnoRequest);
            if (message != string.Empty)
            {
                response.Message = message;
                return response;
            }
            if (!new DBContext(_configuration).GradoAlumnoExists(gradoAlumnoRequest.Id))
            {
                response.Message = "El GradoAlumno no existe";
                return response;
            }
            GradoAlumno gradoAlumno = new()
            {
                Id = gradoAlumnoRequest.Id,
                AlumnoId = gradoAlumnoRequest.AlumnoId,
                GradoId = gradoAlumnoRequest.GradoId,
                Seccion = gradoAlumnoRequest.Seccion
            };
            int result = new DBContext(_configuration).UpdateGradoAlumno(gradoAlumno);
            if (result > 0)
            {
                response.Success = 1;
                response.Message = "El GradoAlumno se actualizó correctamente";
                response.Data = gradoAlumno;
            }
            else
            {
                response.Message = "No se ha podido actualizar";
            }
            return response;
        }
        public string Validations(GradoAlumnoRequest gradoAlumnoRequest)
        {
            string message = string.Empty;
            if (!new DBContext(_configuration).AlumnoExists(gradoAlumnoRequest.AlumnoId))
            {
                message = "El alumno no existe";
            }
            if (!new DBContext(_configuration).GradoExists(gradoAlumnoRequest.GradoId))
            {
                message = "El grado no existe";
            }
            List<GradoAlumno>? gradoAlumnos = new DBContext(_configuration).GetGradoAlumnos(new List<Common.Filter>()
            {
                new Common.Filter(){ Column = "AlumnoId", Value = gradoAlumnoRequest.AlumnoId.ToString()},
                new Common.Filter(){ Column = "GradoId", Value = gradoAlumnoRequest.GradoId.ToString()},
            });
            if (gradoAlumnos != null && gradoAlumnos.Count > 0)
            {
                message = "El alumno ya está en el grado";

            }
            return message;
        }
    }
}
