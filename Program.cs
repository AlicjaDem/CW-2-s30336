namespace zaladowanie_kontenera;
using System;
using System.Collections.Generic;
using System.Linq;

    class Program
    { static void Main(string[] args)
        {
            List<Kontenerowiec> kontenerowce = new List<Kontenerowiec>();
            List<Kontener> magazyn = new List<Kontener>();

            while (true)
            {
                Console.WriteLine("\n..................................");
                Console.WriteLine("Lista kontenerowców:");
                if (kontenerowce.Count == 0)
                    Console.WriteLine("Brak");
                else
                    for (int licznik = 0; licznik < kontenerowce.Count; licznik++)
                        Console.WriteLine($"{licznik + 1}. {kontenerowce[licznik].Nazwa}");

                Console.WriteLine("\nLista kontenerów w magazynie:");
                if (magazyn.Count == 0)
                    Console.WriteLine("Brak");
                else
                    for (int i = 0; i < magazyn.Count; i++)
                        Console.WriteLine($"{i + 1}. {magazyn[i]}");

                Console.WriteLine("\nMożliwe akcje(dodaj odpowiednią cyfrę):");
                Console.WriteLine("1. Dodaj kontenerowiec");
                Console.WriteLine("2. Usuń kontenerowiec");
                Console.WriteLine("3. Dodaj kontener");
                Console.WriteLine("4. Załaduj kontener na kontenerowiec");
                Console.WriteLine("5. Usuń kontener ze statku");
                Console.WriteLine("6. Rozładuj kontener");
                Console.WriteLine("7. Zamień kontener na statku");
                Console.WriteLine("8. Przenieś kontener między statkami");
                Console.WriteLine("9. Wyświetl informacje o kontenerowcu");
                Console.WriteLine("0. Wyjdź");

                Console.Write("\nTwoja wybrana cyfra to: ");
                string opcja = Console.ReadLine();

                try
                {
                    switch (opcja)
                    {
                        case "1":
                            DodajKontenerowiec(kontenerowce);
                            break;
                        case "2":
                            UsunKontenerowiec(kontenerowce);
                            break;
                        case "3":
                            DodajKontener(magazyn);
                            break;
                        case "4":
                            ZaladujKontener(kontenerowce, magazyn);
                            break;
                        case "5":
                            UsunKontenerZeStatku(kontenerowce, magazyn);
                            break;
                        case "6":
                            RozladujKontener(kontenerowce, magazyn);
                            break;
                        case "7":
                            ZamienKontenerNaStatku(kontenerowce, magazyn);
                            break;
                        case "8":
                            PrzeniesKontener(kontenerowce);
                            break;
                        case "9":
                            WyswietlStatek(kontenerowce);
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Wybór niezgodny z dostępnymi");
                            break;
                    }
                }
                catch (Exception blad)
                {
                    Console.WriteLine($"Błąd: {blad.Message}");
                }
            }
        }

        static void DodajKontenerowiec(List<Kontenerowiec> lista)
        {
            Console.Write("Podaj nazwę kontenerowca: ");
            string nazwa = Console.ReadLine();
            Console.Write("Podaj maksymalną prędkość jaką kontenerowiec może rozwijać (węzły): ");
            double predkosc = double.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną liczbę kontenerów, które mogą być przewożone: ");
            int maxKontenery = int.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną wagę kontenerów (t): ");
            double maxWaga = double.Parse(Console.ReadLine());
            //Przygotowywanie naszego kontenerowca ...
            lista.Add(new Kontenerowiec(nazwa, predkosc, maxKontenery, maxWaga));
            Console.WriteLine("Kontenerowiec został dodany");
        }

        static void UsunKontenerowiec(List<Kontenerowiec> lista)
        {
            if (lista.Count == 0) { Console.WriteLine("Nie ma istniejacych kontenerow"); return; }
            Console.Write("Podaj numer kontenerowca do usunięcia: ");
            int index_dla_numeru_kontenerowca = int.Parse(Console.ReadLine()) - 1;
            lista.RemoveAt(index_dla_numeru_kontenerowca);
            Console.WriteLine("Usunieto wybrany kontenerowiec");
        }

        static void DodajKontener(List<Kontener> magazyn)
        {
            Console.WriteLine("Typ kontenera:");
            Console.WriteLine("1. Na płyny");
            Console.WriteLine("2. Na gaz");
            Console.WriteLine("3. Chłodniczy");
            string rodzaj_kontenera = Console.ReadLine();

            Console.Write("Podaj wysokość (w centymetrach): ");
            double wys = double.Parse(Console.ReadLine());
            Console.Write("Podaj wagę własną (waga samego kontenera, w kilogramach): ");
            double waga = double.Parse(Console.ReadLine());
            Console.Write("Podaj głębokość (w centymetrach): ");
            double gleb = double.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną ładowność danego kontenera w kilogramach: ");
            double ladownosc = double.Parse(Console.ReadLine());

            switch (rodzaj_kontenera)
            {
                case "1":
                    Console.Write("Czy ładunek jest niebezpieczny? (tak lub nie): ");
                    bool niebezp = Console.ReadLine().ToLower() == "tak";
                    magazyn.Add(new Kontener_na_plyny(wys, waga, gleb, ladownosc, niebezp));
                    break;
                case "2":
                    Console.Write("Podaj ciśnienie (atm): ");
                    double cisnienie = double.Parse(Console.ReadLine());
                    magazyn.Add(new Kontener_na_gaz(wys, waga, gleb, ladownosc, cisnienie));
                    break;
                case "3":
                    Kontener_chlodniczy.WyswietlDostepneProdukty();
                    Console.Write("Podaj rodzaj produktu: ");
                    string prod = Console.ReadLine();
                    Console.Write("Podaj temperaturę (dla stopni °C): ");
                    double temperatura = double.Parse(Console.ReadLine());
                    magazyn.Add(new Kontener_chlodniczy(wys, waga, gleb, ladownosc, prod, temperatura));
                    break;

                default:
                    Console.WriteLine("Wprowadzono zła wartość dla identyfikacji kontenera");
                    break;
            }

            Console.WriteLine("Dodano nowy kontener");
        }

        static void ZaladujKontener(List<Kontenerowiec> statki, List<Kontener> magazyn)
        {
            if (statki.Count == 0 || magazyn.Count == 0)
            {
                Console.WriteLine("Brak statków bądź kontenerów dostępnych w magazynie.");
                return;
            }

            Console.Write("Podaj numer kontenerowca: ");
            int nrStatku_indeksowo = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Podaj numer kontenera z magazynu: ");
            int nrKontenera_indeksowo = int.Parse(Console.ReadLine()) - 1;

            Kontener kontener = magazyn[nrKontenera_indeksowo];
            statki[nrStatku_indeksowo].ZaladujKontener(kontener);
            magazyn.RemoveAt(nrKontenera_indeksowo);

            Console.WriteLine("Załadowano kontener na statek");
        }

        static void UsunKontenerZeStatku(List<Kontenerowiec> statki, List<Kontener> magazyn)
        {
            Console.Write("Podaj numer kontenerowca: ");
            int nrStatku = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Podaj numer seryjny kontenera do usunięcia: ");
            string nrSer = Console.ReadLine();

            var kontenerowiec = statki[nrStatku];
            var kontener = kontenerowiec.Kontenery.FirstOrDefault(k => k.NumerSeryjny == nrSer);
            if (kontener != null)
            {
                kontenerowiec.RozladujKontener(nrSer);
                magazyn.Add(kontener);
                Console.WriteLine("Kontener usunięty ze statku i dodany do magazynu.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadnego kontenera.");
            }
        }

        static void RozladujKontener(List<Kontenerowiec> statki, List<Kontener> magazyn)
        {
            Console.Write("Podaj numer kontenerowca: ");
            int nrStatku = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Podaj numer seryjny kontenera do opróżnienia: ");
            string numerSeryjny = Console.ReadLine();

            var kontener = statki[nrStatku].Kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
            if (kontener != null)
            {
                kontener.OproznijLadunek();
                Console.WriteLine("Kontener zostal opróżniony.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono zadnego kontenera.");
            }
        }

        static void ZamienKontenerNaStatku(List<Kontenerowiec> statki, List<Kontener> magazyn)
        {
            Console.Write("Podaj numer kontenerowca: ");
            int numerStatku = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Podaj numer seryjny kontenera do zastąpienia: ");
            string nrSer = Console.ReadLine();

            if (magazyn.Count == 0)
            {
                Console.WriteLine("Brak kontenerów w magazynie.");
                return;
            }

            Console.Write("Podaj numer kontenera z magazynu: ");
            int nrMag = int.Parse(Console.ReadLine()) - 1;

            statki[numerStatku].ZamienKontener(nrSer, magazyn[nrMag]);
            magazyn.RemoveAt(nrMag);

            Console.WriteLine("Kontener zastąpiony.");
        }

        static void PrzeniesKontener(List<Kontenerowiec> statki)
        {
            Console.Write("Podaj numer statku źródłowego: ");
            int statek_zrodlowy = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Podaj numer statku docelowego: ");
            int d = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Podaj numer seryjny kontenera do przeniesienia: ");
            string numerSeryjny = Console.ReadLine();

            var kontener = statki[statek_zrodlowy].Kontenery.FirstOrDefault(k => k.NumerSeryjny == numerSeryjny);
            if (kontener != null)
            {
                statki[d].ZaladujKontener(kontener);
                statki[statek_zrodlowy].RozladujKontener(numerSeryjny);
                Console.WriteLine("Kontener przeniesiony.");
            }
            else
            {
                Console.WriteLine("Nie znaleziono kontenera.");
            }
        }

        static void WyswietlStatek(List<Kontenerowiec> statki)
        {
            Console.Write("Podaj numer kontenerowca: ");
            int numer_kontenowca = int.Parse(Console.ReadLine()) - 1;
            statki[numer_kontenowca].WypiszInformacje();
        }
    }

