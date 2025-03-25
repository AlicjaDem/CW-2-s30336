namespace zaladowanie_kontenera;

public class Kontener
{
    public string NumerSeryjny { get; }
    public double MasaLadunku { get; protected set; } 
    public double Wysokosc { get; }
    public double WagaWlasna { get; }
    public double Glebokosc { get; }
    public double MaksymalnaLadownosc { get; }
    private static int licznikId = 1; 

    protected Kontener(string typ, double wysokosc, double wagaWlasna, double glebokosc, double maksLadownosc)
    {
        NumerSeryjny = $"KON-{typ}-{licznikId++}";
        Wysokosc = wysokosc;
        WagaWlasna = wagaWlasna;
        Glebokosc = glebokosc;
        MaksymalnaLadownosc = maksLadownosc;
    } public virtual void ZaladujLadunek(double masa)
    {
        if (masa > MaksymalnaLadownosc)
            throw new OverfillException($"Kontener {NumerSeryjny} przeładowany!");
        MasaLadunku = masa;
    }

    public virtual void OproznijLadunek()
    {
        MasaLadunku = 0;
    }

    public override string ToString()
    {
        return $"{NumerSeryjny} (Ładunek: {MasaLadunku} kg, Maks: {MaksymalnaLadownosc} kg)";
    }

}