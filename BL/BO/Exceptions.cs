

namespace BO;

[Serializable]
public class NotExistException : Exception
{
    public NotExistException(){}
    public NotExistException(string message) : base(message){}
    public NotExistException(string message, Exception inner) : base(message, inner){}
    protected NotExistException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
public class AlreadyExistException : Exception
{
    public AlreadyExistException() { }
    public AlreadyExistException(string message) : base(message) { }
    public AlreadyExistException(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class IdSmallThanZeroException : Exception
{
    public IdSmallThanZeroException() { }
    public IdSmallThanZeroException(string message) : base(message) { }
    public IdSmallThanZeroException(string message, Exception inner) : base(message, inner) { }
    protected IdSmallThanZeroException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NameIsEmptyException : Exception
{
    public NameIsEmptyException() { }
    public NameIsEmptyException(string message) : base(message) { }
    public NameIsEmptyException(string message, Exception inner) : base(message, inner) { }
    protected NameIsEmptyException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class PriceSmallThanZeroException : Exception
{
    public PriceSmallThanZeroException() { }
    public PriceSmallThanZeroException(string message) : base(message) { }
    public PriceSmallThanZeroException(string message, Exception inner) : base(message, inner) { }
    protected PriceSmallThanZeroException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class InStokeSmallThanZeroException : Exception
{
    public InStokeSmallThanZeroException() { }
    public InStokeSmallThanZeroException(string message) : base(message) { }
    public InStokeSmallThanZeroException(string message, Exception inner) : base(message, inner) { }
    protected InStokeSmallThanZeroException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class CanNotDeleteProductException : Exception
{
    public CanNotDeleteProductException() { }
    public CanNotDeleteProductException(string message) : base(message) { }
    public CanNotDeleteProductException(string message, Exception inner) : base(message, inner) { }
    protected CanNotDeleteProductException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class AmountLessThenZero : Exception
{
    public AmountLessThenZero() { }
    public AmountLessThenZero(string message) : base(message) { }
    public AmountLessThenZero(string message, Exception inner) : base(message, inner) { }
    protected AmountLessThenZero(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class AddresIsempty : Exception
{
    public AddresIsempty() { }
    public AddresIsempty(string message) : base(message) { }
    public AddresIsempty(string message, Exception inner) : base(message, inner) { }
    protected AddresIsempty(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

}


[Serializable]
public class EmailInValidException : Exception
{
    public EmailInValidException() { }
    public EmailInValidException(string message) : base(message) { }
    public EmailInValidException(string message, Exception inner) : base(message, inner) { }
    protected EmailInValidException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}

[Serializable]
public class NotEnougeInStock : Exception
{
    public NotEnougeInStock() { }
    public NotEnougeInStock(string message) : base(message) { }
    public NotEnougeInStock(string message, Exception inner) : base(message, inner) { }
    protected NotEnougeInStock(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}


