namespace zaladowanie_kontenera;


public class Kontener_chlodniczy : Kontener
{
    public string RodzajProduktu { get; private set; }
    public double Temperatura { get; private set; }

    private static readonly Dictionary<string, double> MinimalneTemperatury = new Dictionary<string, double>
    {
        { "bananas", 13.3 },
        { "chocolate", 18 },
        { "fish", 2 },
        { "meat", -15 },
        { "ice cream", -18 },
        { "frozen pizza", -30 },
        { "cheese", 7.2 },
        { "sausages", 5 },
        { "nutter", 20.5 },
        { "eggs", 19 }
    };

    public Kontener_chlodniczy(
        double wysokosc, 
        double wagaWlasna, 
        double glebokosc, 
        double maksLadownosc, 
        string rodzajProduktu, 
        double temperatura) 
        : base("Chlodniczy", wysokosc, wagaWlasna, glebokosc, maksLadownosc) 
    {
        RodzajProduktu = rodzajProduktu.ToLower();

        if (MinimalneTemperatury.ContainsKey(RodzajProduktu))
        { double minTemp = MinimalneTemperatury[RodzajProduktu];
            if (temperatura < minTemp)
                throw new ArgumentException($"Temperatura dla '{RodzajProduktu}' nie może być niższa niż {minTemp}°C.");
        }
        else
        {
            Console.WriteLine($"Błąd dla : '{RodzajProduktu}'.Kontener moze przechowywac wylacznie produkty tego samego typu.");
        }

        Temperatura =temperatura;
    }

    public override string ToString()
    {
        return $"{base.ToString()} || Produkt: {RodzajProduktu}, Temp: {Temperatura}°C";
    }
    
    public static void WyswietlDostepneProdukty()
    {
        Console.WriteLine("Dostępne produkty i ich minimalne temperatury - nizszych nie mozna:");
        foreach (var produkt in MinimalneTemperatury)
        {
            Console.WriteLine($"- {produkt.Key}: min {produkt.Value}°C");
        }
    }
}