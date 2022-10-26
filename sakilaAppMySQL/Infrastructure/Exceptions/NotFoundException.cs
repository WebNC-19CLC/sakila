namespace sakilaAppMySQL.Infrastructure.Exceptions
{
  public class NotFoundException : BusinessException
  {
    public NotFoundException(Exception exception = null) : base("NotFound", new { Type = "Entity" }, exception)
    {
    }

    public NotFoundException(string message, Exception exception = null) : base(message, exception)
    {
    }

    public NotFoundException(object identifier, Exception exception = null) : base("NotFoundItem", new { Identifier = identifier, Type = "Entity" }, exception)
    {
    }

    public NotFoundException(string message, object info, Exception exception = null) : base(message, info, exception)
    {
    }
  }

  public class NotFoundException<T> : NotFoundException
  {
    public NotFoundException(string message, Exception exception = null) : base(message, exception)
    {
    }

    public NotFoundException(Exception exception = null) : base("NotFound", new { Type = typeof(T).Name }, exception)
    {
    }

    public NotFoundException(object identifier, Exception exception = null) : base("NotFoundItem", new { Identifier = identifier, Type = typeof(T).Name }, exception)
    {
    }

    public NotFoundException(string message, object info, Exception inner = null) : base(message, info, inner)
    {
    }
  }
}
