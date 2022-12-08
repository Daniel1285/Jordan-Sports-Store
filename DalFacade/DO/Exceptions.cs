

namespace DO;

   


[Serializable]
public class AlreadyExistException : Exception
{
  public AlreadyExistException(string str) : base(str) { }
}

[Serializable]
public class NotExistException : Exception
{
	public NotExistException() : base($"Not found object to delete") { }
	public NotExistException(string message) : base(message) { }
	public NotExistException(string message, Exception inner) : base(message, inner) { }
	protected NotExistException(
	  System.Runtime.Serialization.SerializationInfo info,
	  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}




