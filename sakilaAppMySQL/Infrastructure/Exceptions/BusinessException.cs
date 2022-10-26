namespace sakilaAppMySQL.Infrastructure.Exceptions
{
  public class BusinessException : Exception
  {
    public object? Info { get; set; } = null;

    public BusinessException(string message, Exception exception = null) : base(message, exception)
    {
    }

    public BusinessException(string message, object info, Exception exception = null) : base(message, exception)
    {
      Info = info;
    }
  }
}
