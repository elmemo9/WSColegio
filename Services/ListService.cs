using WSColegio.Models;
using WSColegio.Responses;
using WSColegio.Responses.Models;
using WSColegio.Services.Interfaces;

namespace WSColegio.Services
{
    public class ListService : IListService
    {
        private readonly IConfiguration _configuration;
        public ListService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response GetAlumnos()
        {
            Response response = new();

            List<ItemList>? list = new DBContext(_configuration).AlumnosList();
            if (list == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", list.Count);
                response.Data = list;
            }
            return response;
        }

        public Response GetGrados()
        {
            Response response = new();

            List<ItemList>? list = new DBContext(_configuration).GradosList();
            if (list == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", list.Count);
                response.Data = list;
            }
            return response;
        }

        public Response GetProfesores()
        {
            Response response = new();

            List<ItemList>? list = new DBContext(_configuration).ProfesoresList();
            if (list == null)
            {
                response.Message = "Ha ocurrido un error";
            }
            else
            {
                response.Success = 1;
                response.Message = string.Format("Se han encontrado {0} resultado(s)", list.Count);
                response.Data = list;
            }
            return response;
        }
    }
}
