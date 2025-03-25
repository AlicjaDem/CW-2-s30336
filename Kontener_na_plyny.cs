namespace zaladowanie_kontenera;
public class Kontener_na_plyny : Kontener, IHazardNotifier
{
    public bool CzyNiebezpieczny { get; }
    public Kontener_na_plyny(double wysokosc, double wagaWlasna, double glebokosc, double maksLadownosc, bool niebezpieczny)
        : base("plyny", wysokosc, wagaWlasna, glebokosc, maksLadownosc)
    {
        CzyNiebezpieczny = niebezpieczny;
    }

    public override void ZaladujLadunek(double masa)
    {
        double wypełnanie = CzyNiebezpieczny ? 0.5 * MaksymalnaLadownosc : 0.9 * MaksymalnaLadownosc;

        if (masa > wypełnanie)
        {
            PowiadomONiebezpieczenstwie($"Niebezpieczenstwo: przeładowania niebezpiecznego Kontenera Na Płyny {NumerSeryjny}.");
            throw new OverfillException($"Kontener {NumerSeryjny} zostal przeładowany");
        }
        MasaLadunku = masa;
    }

    public void PowiadomONiebezpieczenstwie(string wiadomosc)
    {
        Console.WriteLine($"[Niebezpieczna operacja: ] {wiadomosc}");
    }
}