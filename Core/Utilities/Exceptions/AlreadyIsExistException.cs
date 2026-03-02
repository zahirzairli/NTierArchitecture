namespace Core.Utilities.Exceptions;

public class AlreadyIsExistException:Exception
{
    public AlreadyIsExistException(string message) : base(message) { }
    public AlreadyIsExistException() : base("Entity already exist!") { }
}
