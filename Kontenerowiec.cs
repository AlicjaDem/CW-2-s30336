namespace zaladowanie_kontenera;

public class Kontenerowiec
{
    public string Nazwa { get; }
    public double MaksymalnaPredkosc { get; }
    public int MaksLiczbaKontenerow { get; }
    public double MaksWagaKonteneow { get; } 
    public List<Kontener> Kontenery { get; }

    public Kontenerowiec(string nazwa, double maksPredkosc, int maksKontenery, double maksWaga)
    {
        Nazwa = nazwa;
        MaksymalnaPredkosc = maksPredkosc;
        MaksLiczbaKontenerow = maksKontenery;
        MaksWagaKonteneow = maksWaga;
        Kontenery = new List<Kontener>();
    }

    public void ZaladujKontener(Kontener kontener)
    {
        if (Kontenery.Count >= MaksLiczbaKontenerow)
            throw new Exception("Nie można załadować więcej kontenerów - jest juz limitowana liczba miejsc.");

        double aktualnaWaga = Kontenery.Sum(k => k.MasaLadunku + k.MasaLadunku) / 1000.0; //zamieniamy z kg na tony
        double wagaNowego = (kontener.MasaLadunku + kontener.WagaWlasna) / 1000.0; //tez zamiana na tony
        if (aktualnaWaga + wagaNowego > MaksWagaKonteneow)
            throw new Exception("Przekroczono limit wagi kontenerów jakie moga byc transportowane przez statek.");

        Kontenery.Add(kontener);
    }

    public void RozladujKontener(string numerSeryjny)
    {
        var kontener = Kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
        if (kontener != null)
        {
            Kontenery.Remove(kontener);
        }
    }

    public void ZamienKontener(string numerSeryjny, Kontener nowyKontener)
    {
        var index = Kontenery.FindIndex(k => k.NumerSeryjny == numerSeryjny);
        if (index != -1)
        {
            Kontenery[index] = nowyKontener;
        }
    }

    public void WypiszInformacje()
    {
        Console.WriteLine($"Statek: {Nazwa}, Prędkość: {MaksymalnaPredkosc} węzłów, Kontenery: {Kontenery.Count} na mozliwych -> {MaksLiczbaKontenerow}");
        foreach (var kontener in Kontenery)
        {
            Console.WriteLine($"  {kontener}");
        }
    }
}
