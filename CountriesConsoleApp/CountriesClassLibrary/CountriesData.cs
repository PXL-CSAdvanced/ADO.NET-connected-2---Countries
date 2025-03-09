
using Microsoft.Data.SqlClient;

namespace CountriesClassLibrary
{
    public static class CountriesData
    {
        private static string _connectionString = "jouw connectie string komt hier";

        public static int GetTotalNumberOfCountries()
        {
            throw new NotImplementedException();
        }

        public static long GetTotalPopulation()
        {
            throw new NotImplementedException();
        }


        public static List<string> GetTenSmallestCountries()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, int> GetPopulationByCountry()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, int> GetPopulationDensityByCountry()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, int> GetNumberOfCountriesPerContinent()
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, long> GetTotalAreaPerContinent()
        {
            throw new NotImplementedException();
        }

        public static string? GetCountryByCapital(string capital)
        {
            throw new NotImplementedException();
        }
    }
}
