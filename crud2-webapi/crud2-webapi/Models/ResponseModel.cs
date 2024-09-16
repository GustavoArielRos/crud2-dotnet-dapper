using System.Data.SqlTypes;

namespace crud2_webapi.Models
{
    public class ResponseModel<T>
    {
        public T? Dados { get; set; }

        public string Mensagem { get; set; } = string.Empty;

        public bool Status { get; set; } = true;
    }
}
