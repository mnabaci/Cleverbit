namespace Cleverbit.Dto.Response
{
    public class ResponseDto<T> : ResponseDto
    {
        public T Data { get; private set; }

        public static ResponseDto<T> Success(T data)
        {
            return Success(data, string.Empty);
        }

        public static ResponseDto<T> Success(T data, string message)
        {
            return new ResponseDto<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }
    }
}
