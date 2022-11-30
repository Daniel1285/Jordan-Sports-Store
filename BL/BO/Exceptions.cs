

namespace BO;

[Serializable]
public class NotExistException : Exception
{
    public NotExistException(string ex) : base() { }

}

[Serializable]
public class AlreadyExistException : Exception
{
    public AlreadyExistException(string ex) : base(ex) { }
}

[Serializable]
public class IdSmallThanZeroException : Exception
{
    public IdSmallThanZeroException(string ex) : base(ex) { }
}
public class NameIsEmptyException : Exception
{
    public NameIsEmptyException(string ex) : base(ex) { }
}
public class PriceSmallThanZeroException : Exception
{
    public PriceSmallThanZeroException(string ex) : base(ex) { }
}

public class InStokeSmallThanZeroException : Exception
{
    public InStokeSmallThanZeroException(string ex) : base(ex) { }
}
public class CanNotDeleteProductException : Exception
{
    public CanNotDeleteProductException(string ex) : base(ex) { }
}