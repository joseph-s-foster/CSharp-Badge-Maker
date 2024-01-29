using CatWorx.BadgeMaker;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        bool fetchFromApi = ShouldFetchFromApi();
        List<Employee> employees = await PeopleFetcher.GetEmployeesOrFromApi(fetchFromApi);
        Util.PrintEmployees(employees);
        Util.MakeCSV(employees);
        await Util.MakeBadges(employees);
    }

    static bool ShouldFetchFromApi()
    {
        while (true)
        {
            Console.WriteLine("Do you want to fetch data from the API? (yes/no)");
            string response = Console.ReadLine()?.ToLower() ?? "";
            if (response == "yes")
            {
                return true;
            }
            else if (response == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid response. Please enter 'yes' or 'no'.");
            }
        }
    }
}
