namespace Cleverbit.Dto.Response
{
    public class ResponseDto
    {
        public bool IsSuccess { get; protected set; }
        public string Message { get; protected set; }

        public static ResponseDto Success()
        {
            return Success(string.Empty);
        }

        public static ResponseDto Success(string message)
        {
            return new ResponseDto
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static ResponseDto Error()
        {
            return Error(string.Empty);
        }

        public static ResponseDto Error(string message)
        {
            return new ResponseDto
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
