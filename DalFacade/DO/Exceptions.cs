

namespace DO;

[Serializable]

public class NotExistException : Exception
{
    public NotExistException(string str) : base(str) {}
        
}


[Serializable]
public class AlreadyExistException : Exception
{
  public AlreadyExistException(string str) : base(str) { }
}




