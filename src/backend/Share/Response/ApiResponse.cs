namespace Share.Response;

public class ApiResponse
{
    public int Code { get; set; } = 200;
    
    public string Message { get; set; }
    
    public string UserMessage { get; set; }

    public ApiResponse() { }

    protected ApiResponse(int code, string message = null, string userMessage = null)
    {
        Code = code;
        Message = message;
        UserMessage = userMessage;
    }
    
    public static ApiResponse Ok(string message = null, string userMessage = null) => new (200, message, userMessage);
    
    public static ApiResponse BadRequest(string message = null, string userMessage = null) => new (400, message, userMessage);
    
    public static ApiResponse Unauthorized(string message= null, string userMessage = null) => new (401, message, userMessage); 
    
}

public class ApiResponse<T> : ApiResponse
{
    public T Result { get; set; }

    public ApiResponse(T result)
    {
        Result = result;
    }

    private ApiResponse(int code, string message = null, string userMessage = null) : base(code, message, userMessage)
    {
    }
    
    public static ApiResponse<T> Ok(string message = null, string userMessage = null) => new (200, message, userMessage);
    
    public static ApiResponse<T> Ok(T result) => new (200) { Result = result };
    
    public static ApiResponse<T> BadRequest(string message, string userMessage = null) => new (400, message, userMessage);
    
    public static ApiResponse<T> Unauthorized(string message, string userMessage = null) => new (401, message, userMessage); 
    
}