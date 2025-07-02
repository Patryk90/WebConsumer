namespace WebConsumer.Exceptions;

public class ErrorCodeException(ErrorCode errorCode, string message) : Exception(message)
{
    public ErrorCode ErrorCode { get; set; } = errorCode;
}