namespace zaladowanie_kontenera;

public class Kontener_na_gaz : Kontener, IHazardNotifier
{
    public double Cisnienie { get; }
    public Kontener_na_gaz(double wysokosc, double wagaWlasna, double glebokosc, double maksLadownosc, double cisnienie)
        : base("Gaz", wysokosc, wagaWlasna, glebokosc, maksLadownosc)
    {
        Cisnienie = cisnienie;
    }

    public override void ZaladujLadunek(double masa)
    {
        if (masa > MaksymalnaLadownosc)
        {
            PowiadomONiebezpieczenstwie($"Przeładowanie Kontenera Na Gaz[numSer: {NumerSeryjny}.]");
            throw new OverfillException($"Kontener o numerze seryjnym: {NumerSeryjny} został przeladowany");
        }
        MasaLadunku = masa;
    }

    public override void OproznijLadunek()
    {
        MasaLadunku = 0.05 * MasaLadunku; 
    }
    public void PowiadomONiebezpieczenstwie(string wiadomosc)
    { Console.WriteLine($"[Niebezpieczna operacja:] {wiadomosc}");
    }
}