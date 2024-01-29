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

                    JObject json = JObject.Parse(response);

                    JArray resultsArray = (JArray)json.SelectToken("results")!;

                    if (resultsArray != null)
                    {
                        foreach (JToken personToken in resultsArray)
                        {
                            // Parse JSON data and create Employee objects
                            Employee emp = new Employee
                            (
                                personToken?.SelectToken("name.first")?.ToString() ?? "",
                                personToken?.SelectToken("name.last")?.ToString() ?? "",
                                Int32.Parse(personToken?.SelectToken("id.value")?.ToString().Replace("-", "") ?? "0"),
                                personToken?.SelectToken("picture.large")?.ToString() ?? ""
                            );

                            employees.Add(emp);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No 'results' array found in the JSON response.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching data from API: {ex.Message}");
                }
            }

            return employees;
        }
        public static async Task<List<Employee>> GetEmployeesOrFromApi(bool fetchFromApi)
        {
            return fetchFromApi ? await GetFromApi() : GetEmployees();
        }
    }
}
