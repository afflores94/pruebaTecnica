namespace PruebaTecnica_ArielFlores.Models.DTO
{
    public class ApiResponse<T>
    {
        public ApiResponseHeader Header { get; set; }
        public T Body { get; set; }

        public ApiResponse(string codigo, string mensaje, T body = default)
        {
            Header = new ApiResponseHeader
            {
                Codigo = codigo,
                Mensaje = mensaje
            };
            Body = body;
        }
    }

    public class ApiResponseHeader
    {
        public string Codigo { get; set; }
        public string Mensaje { get; set; }
    }
}
