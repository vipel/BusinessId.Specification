using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessId.Specification.App
{
    class Program
    {
        static void Main(string[] args)
        {
            ISpecification<string> specification = new BusinessIdSpecification();

            string value = string.Empty;

            while (!value.Equals("E"))
            {
                Console.Write("Enter Business ID value or 'E' to exit application: ");
                value = Console.ReadLine();

                if (!value.Equals("E"))
                {
                    if (specification.IsSatisfiedBy(value))
                    {
                        Console.WriteLine("Business ID value is correct");
                    }
                    else
                    {
                        Console.WriteLine("Business ID value is wrong, reasons:");
                        List<string> reasons = specification.ReasonsForDissatisfaction.ToList();
                        reasons.ForEach(reason => Console.WriteLine(reason));
                    }
                }
                Console.WriteLine();
            } // while

            Console.WriteLine("Application finished");
        }
    }
}
