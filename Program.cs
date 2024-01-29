using CatWorx.BadgeMaker;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        List<Employee> employees = await GetFromApi();
        Util.PrintEmployees(employees);
        Util.MakeCSV(employees);
        await Util.MakeBadges(employees);
    }

    static async Task<List<Employee>> GetFromApi()
    {
        return await PeopleFetcher.GetFromApi();
    }
}
