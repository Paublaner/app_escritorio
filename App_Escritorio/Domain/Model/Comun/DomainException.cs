namespace App_Escritorio.Domain.Model.Comun;
public class DomainException : Exception
{
	public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

}
