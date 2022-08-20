using System.ComponentModel.DataAnnotations;

namespace WSColegio.Requests.Validations
{
    public class Genero : ValidationAttribute
    {
        private readonly string[] _listValues;
        public Genero()
        {
            _listValues = new string[]
            {
                "Femenino",
                "Masculino",
                "Prefiero no decirlo",
                "Otro"
            } ;
        }
        public override bool IsValid(object? value)
        {
            if(value == null) return false;
            var text = (string)value;
            foreach (var item in _listValues)
            {
                if (text == item) return true;
            }
            return false;
        }
    }
}
