
using Microsoft.Data.SqlClient;

namespace CountriesClassLibrary
{
    public static class CountriesData
    {
        private static string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Countries;Integrated Security=True;Connect Timeout=30;";

        public static int GetTotalNumberOfCountries()
        {
            string query = "SELECT COUNT(*) FROM Countries";
            int totalCountries = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    totalCountries = (int)command.ExecuteScalar();
                }
            }
            return totalCountries;
        }

        public static long GetTotalPopulation()
        {
            string query = "SELECT SUM(cast(Population as BIGINT)) FROM Countries";
            long totalPopulation = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    totalPopulation = (long)command.ExecuteScalar();
                }
            }
            return totalPopulation;
        }


        public static List<string> GetTenSmallestCountries()
        {
            List<string> countries = new List<string>();
            string query = "SELECT TOP 10 Name FROM Countries ORDER BY Area";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countries.Add(reader["name"].ToString());
                        }
                    }
                }
            }
            return countries;
        }

        public static Dictionary<string, int> GetPopulationByCountry()
        {
            var populationPerCountry = new Dictionary<string, int>();

            string query = "SELECT Name, Population FROM Countries";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string countryName = reader["name"].ToString();
                            int population = Convert.ToInt32(reader["population"]);

                            populationPerCountry.Add(countryName, population);
                        }
                    }
                }
            }
            return populationPerCountry;
        }

        public static Dictionary<string, int> GetPopulationDensityByCountry()
        {
            var populationDensityPerCountry = new Dictionary<string, int>();
            string query = "SELECT Name, Population, Area FROM Countries";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string countryName = reader["name"].ToString();
                            int population = Convert.ToInt32(reader["population"]);
                            int area = Convert.ToInt32(reader["area"]);
                            int density = (int)Math.Round(population / (double)area);

                            populationDensityPerCountry.Add(countryName, density);
                        }
                    }
                }
            }
            return populationDensityPerCountry;
        }

        public static Dictionary<string, int> GetNumberOfCountriesPerContinent()
        {
            Dictionary<string, int> countriesPerContinent = new Dictionary<string, int>();
            string query = "SELECT Region, COUNT(*) AS CountryCount FROM Countries GROUP BY Region";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string continent = reader.GetString(0);
                        int countryCount = reader.GetInt32(1);
                        countriesPerContinent[continent] = countryCount;
                    }
                }
            }
            return countriesPerContinent;
        }

        public static Dictionary<string, long> GetTotalAreaPerContinent()
        {
            Dictionary<string, long> areaPerContinent = new Dictionary<string, long>();
            string query = "SELECT Region, SUM(Area) AS TotalArea FROM Countries GROUP BY Region";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string continent = reader.GetString(0);
                        long totalArea = reader.GetInt32(1);
                        areaPerContinent[continent] = totalArea;
                    }
                }
            }
            return areaPerContinent;
        }

        public static string? GetCountryByCapital(string capital)
        {
            string? country = null;
            string query = "SELECT Name FROM Countries WHERE Capital = @Capital";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Capital", capital);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            country = reader.GetString(0);
                        }
                    }
                }
            }

            return String.IsNullOrWhiteSpace(country) ? null : country;
        }
    }
}
