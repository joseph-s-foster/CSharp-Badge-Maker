﻿using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> employees = new List<string>() { "Josh", "Raquel" };

            employees.Add("Adam");
            employees.Add("Dreaquan");

            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine(employees[i]);
            }
        }
    }
}
