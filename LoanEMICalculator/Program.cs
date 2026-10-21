using System;

namespace LoanEmiCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Display a header for the application
            Console.WriteLine("=========================================");
            Console.WriteLine("      LOAN EMI CALCULATOR      ");
            Console.WriteLine("=========================================");
            Console.WriteLine();

            // 2. Get user inputs
            // We use a helper method 'GetValidDoubleInput' to ensure the program 
            // doesn't crash if the user types letters instead of numbers.
            double principal = GetValidDoubleInput("Enter the Principal Amount (e.g., 500000): ");
            double annualInterestRate = GetValidDoubleInput("Enter the Annual Interest Rate (e.g., 7.5 for 7.5%): ");
            int tenureInYears = (int)GetValidDoubleInput("Enter the Loan Tenure in Years (e.g., 5): ");

            // 3. Process and Calculate
            // The formula requires Monthly Interest (R) and Total Months (N)

            // Convert annual interest rate to monthly decimal rate
            // Example: 7.5% annual -> 7.5 / 12 / 100 = 0.00625
            double monthlyInterestRate = (annualInterestRate / 12) / 100;

            // Convert tenure from years to months
            int numberOfMonths = tenureInYears * 12;

            // Calculate EMI using the formula
            double emi = CalculateEmi(principal, monthlyInterestRate, numberOfMonths);

            // 4. Display the output
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("CALCULATION RESULTS:");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Principal Amount:   {principal:C}"); // :C formats as currency
            Console.WriteLine($"Annual Interest:    {annualInterestRate}%");
            Console.WriteLine($"Loan Tenure:        {tenureInYears} Years ({numberOfMonths} Months)");
            Console.WriteLine("-----------------------------------------");
            // F2 formats the number to 2 decimal places
            Console.WriteLine($"Monthly EMI:        {emi:F2}");
            Console.WriteLine("-----------------------------------------");

            // Keep console open
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Calculates the EMI based on the standard formula.
        /// EMI = [P x R x (1+R)^N] / [(1+R)^N - 1]
        /// </summary>
        static double CalculateEmi(double p, double r, int n)
        {
            // Handle edge case where interest rate is 0 (division by zero error)
            if (r == 0)
            {
                return p / n;
            }

            // Calculate (1 + R)^N
            double compoundFactor = Math.Pow(1 + r, n);

            // Apply the formula
            double emi = (p * r * compoundFactor) / (compoundFactor - 1);

            return emi;
        }

        /// <summary>
        /// Helper method to keep asking user until they provide a valid number.
        /// </summary>
        static double GetValidDoubleInput(string prompt)
        {
            double value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // TryParse returns true if conversion is successful
                if (double.TryParse(input, out value) && value >= 0)
                {
                    return value;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive number.");
                }
            }
        }
    }
}