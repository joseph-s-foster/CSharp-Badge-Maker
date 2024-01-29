using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            while (true)
            {
                Console.WriteLine("Enter first name (leave empty to exit): ");
                string firstName = Console.ReadLine() ?? "";
                if (firstName == "")
                {
                    break;
                }

                Console.Write("Enter last name: ");
                string lastName = Console.ReadLine() ?? "";
                Console.Write("Enter ID: ");
                int id = Int32.Parse(Console.ReadLine() ?? "");
                Console.Write("Enter Photo URL:");
                string photoUrl = Console.ReadLine() ?? "";
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                employees.Add(currentEmployee);
            }

            return employees;
        }

        public static async Task<List<Employee>> GetFromApi()
        {
            List<Employee> employees = new List<Employee>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                    Console.WriteLine("API Response:");

                    // Parse the entire JSON response into a JObject
                    JObject json = JObject.Parse(response);

                    // Extract the array of people objects using SelectToken
                    JArray resultsArray = (JArray)json.SelectToken("results")!;

                    // Check if the resultsArray is not null
                    if (resultsArray != null)
                    {
                        // Iterate over each person object in the array
                        foreach (JToken personToken in resultsArray)
                        {
                            // Extract the first name of the person and print it to the console
                            string? firstName = personToken?.SelectToken("name.first")?.ToString();

                            Console.WriteLine($"{firstName}");

                            // You can add code here to create Employee objects using the extracted data
                            // For now, we'll just print the first names to the console
                        }
                    }
                    else
                    {
                        Console.WriteLine("No 'results' array found in the JSON response.");
                    }

                    // The rest of your code to parse and create Employee objects goes here
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data from API: {ex.Message}");
                }
            }

            return employees;
        }
    }
}