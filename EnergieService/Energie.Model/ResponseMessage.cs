namespace Energie.Model
{
    public class ResponseMessage
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        //private ResponseMessage(int id, bool isSuccess, string message)
        //{
        //    Id = id;
        //    IsSuccess = isSuccess;
        //    Message = message;
        //}
        //public static ResponseMessage ResponseMessages(int id, bool isSuccess, string message)
        //{
        //    return new(id, isSuccess, message);
        //}
    }

    public class ApiResponse
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        private ApiResponse(int id, bool isSuccess, string message)
        {
            Id = id;
            IsSuccess = isSuccess;
            Message = message;
        }
        public static ApiResponse ResponseMessages(int id, bool isSuccess, string message)
        {
            return new(id, isSuccess, message);
        }
    }

}
