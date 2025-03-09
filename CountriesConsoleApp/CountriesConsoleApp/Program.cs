using CountriesClassLibrary;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n--- Data analyse van landen ---\n");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("(Niet alle landen in deze lijst worden herkend door alle andere landen.)");
        Console.ResetColor();

        // 1. Totaal aantal landen & totaal populatie
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\nTotaal aantal landen: {CountriesData.GetTotalNumberOfCountries()}");
        Console.WriteLine($"Totaal populatie planeet: {CountriesData.GetTotalPopulation()}");
        Console.ResetColor();

        // 2. Top tien kleinste landen
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nTop tien kleinste landen in oppervlakte");
        Console.ForegroundColor = ConsoleColor.White;
        List<string> countries = CountriesData.GetTenSmallestCountries();
        for (int i = 0; i < countries.Count; i++)
        {
            Console.WriteLine($"{i+1}. {countries[i]}");
        }
        Console.ResetColor();

        // 3. Populatie per land
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nPopulatie per land\n");
        Console.ResetColor();

        Dictionary<string, int> populationPerCountry = CountriesData.GetPopulationByCountry();
        DisplayVerticalBarChart(populationPerCountry, 15);

        // 4. Bevolkingsdichtheid per land
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nBevolkingsdichtheid per land (#/km²)\n");
        Console.ResetColor();

        Dictionary<string, int> populationDensityPerCountry = CountriesData.GetPopulationDensityByCountry();
        DisplayVerticalBarChart(populationDensityPerCountry, 15);


        // 5. Aantal landen per continent
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nAantal landen per continent\n");
        Console.ResetColor();

        Dictionary<string, int> numberOfCountriesPerContinent = CountriesData.GetNumberOfCountriesPerContinent();
        foreach (string key in numberOfCountriesPerContinent.Keys)
        {
            Console.WriteLine($"{key}: {numberOfCountriesPerContinent[key]}");
        }

        // 6. Totale oppervlakte per continent
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nTotale oppervlakte per continent\n");
        Console.ResetColor();

        Dictionary<string, long> totalAreaPerContinent = CountriesData.GetTotalAreaPerContinent();
        foreach (string key in totalAreaPerContinent.Keys)
        {
            Console.WriteLine($"{key}: {totalAreaPerContinent[key]}");
        }

        // 7. Zoeken op hoofdstad
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nTotale oppervlakte per continent\n");
        Console.ResetColor();

        Console.WriteLine("Geef de naam in van een hoofdstad om de naam van het land te zien.");
        Console.Write("Naam van hoofdstad (in het engels): ");
        string capital = Console.ReadLine();

        string? country = CountriesData.GetCountryByCapital(capital);
        if (country != null)
        {
            Console.WriteLine(country);
        }
        else
        {
            Console.WriteLine("Ongeldige naam.");
        }
    }



    static void DisplayVerticalBarChart(Dictionary<string, int> populationPerCountry, int topX)
    {
        var topCountries = populationPerCountry.OrderByDescending(c => c.Value).Take(topX);
        long maxValue = topCountries.Max(c => c.Value);

        int chartHeight = 16;
        long scaleFactor = maxValue / chartHeight;

        for (int i = chartHeight; i >= 1; i--)
        {
            long markerValue = (i * maxValue) / (long) chartHeight;
            string label = i % 2 == 0 ? markerValue.ToString().PadLeft(10) + " | " : "           | ";

            Console.Write(label);
            foreach (var country in topCountries)
            {
                long numOfBars = country.Value / scaleFactor;

                if (numOfBars >= i)
                    Console.Write(" ███ ");
                else
                    Console.Write("     ");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
        Console.Write("           | ");
        foreach (var country in topCountries)
        {
            Console.Write($" {country.Key.Substring(0, 3).ToUpper()} ");
        }
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.White;
    }
}