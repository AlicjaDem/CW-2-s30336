namespace zaladowanie_kontenera;

public class OverfillException : Exception
{
    public OverfillException(string wiadomosc) : base(wiadomosc)
    {
    }
}