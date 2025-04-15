using PruebaTecnica_ArielFlores.Models.DTO;

namespace PruebaTecnica_ArielFlores.Utilidades
{
    public class ApiResponseHelper
    {
        public static ApiResponse<T> Success<T>(string mensaje)
        {
            return new ApiResponse<T>("00", mensaje);
        }

        public static ApiResponse<T> Success<T>(T body)
        {
            return new ApiResponse<T>("00", "Éxito", body);
        }

        public static ApiResponse<T> NotFound<T>(string mensaje)
        {
            return new ApiResponse<T>("01", mensaje, default);
        }

        public static ApiResponse<T> BadRequest<T>(string mensaje)
        {
            return new ApiResponse<T>("02", mensaje, default);
        }

        public static ApiResponse<T> Error<T>(T body = default)
        {
            return new ApiResponse<T>("99", "Excepción", body);
        }
    }
}
