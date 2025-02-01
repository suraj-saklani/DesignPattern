namespace workingProject
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(T result, string message = "")
        {
            Success = true;
            Result = result;
            Message = message;
            Errors = null;
        }

        public ApiResponse(string errorMessage)
        {
            Success = false;
            Message = errorMessage;
            Errors = new List<string> { errorMessage };
            Result = default;
        }

        public ApiResponse(List<string> errors)
        {
            Success = false;
            Errors = errors;
            Message = "One or more errors occurred.";
            Result = default;
        }
    }
}
