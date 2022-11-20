

namespace DO
{
    public class NotExistException : Exception
    {
        public NotExistException(string str) : base(str) { }
    }

    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string str) : base(str) { }
    }



}
