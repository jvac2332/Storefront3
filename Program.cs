// See https://aka.ms/new-console-template for more information
// Storefront V1.0


using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Linq;


class Storefront_Assignment
{
    static void Main()
    {
        Console.WriteLine("************************************************************");
        Console.WriteLine("                        STOREFRONT V1.0");
        Console.WriteLine("************************************************************\n");
        
         // askes the user for file name
        Console.Write("Enter name of grocery item: ");
         string filename(Groceries.txt) = Console.ReadLine();
        
        Dictionary<string, double> inventory = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        Dictionary<string, int> cart = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        // Dictionary is a type of storage function that store key-values pairs I asked ChatGPt what function I could use to find value pairs
        // and it recommended the dictionary function
        try
        {
            foreach (string line in File.ReadLines(Groceries.txt))
            {
                string[] parts = line.Split(new[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && double.TryParse(parts[1], out double price))
                {
                    inventory[parts[0]] = price;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: Could not read file. {e.Message}");
            return;
        }
        
        Console.WriteLine("\nWhat would you like to buy?");
        DisplayInventory(inventory);

        while (true)
        {
            Console.Write("\n Enter item name, or 'quit' to end: ");
            string item = Console.ReadLine().Trim();

            if (item.Equals("quit", StringComparison.OrdinalIgnoreCase))
                break;
            if (!inventory.ContainsKey(item))
            {
                Console.WriteLine("We don't have that item. Please try again.");
                continue;
            }

            int quantity;
            while (true)
            {
                Console.Write(" How many would you like to buy? ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    break;
                
                Console.WriteLine("You must enter a positive integer.");
            }

            if (cart.ContainsKey(item))
                cart[item] += quantity;
            else
                cart[item] = quantity;
            Console.WriteLine($"You added {item}, quantity {quantity}, on your cart.");
        }
        
        // This code line shows the final receipt
        Console.WriteLine("\n So here is what you bought: ");
        double total = 0;
        foreach (var entry in cart.OrderBy(e => e.Key))
        {
            Console.WriteLine($"{entry.Key, -30}  {entry.Value}");
            total += inventory[entry.Key] * entry.Value;
        }
        Console.WriteLine($"\nYour total for today is: {total:C2}. ");
        Console.WriteLine($"Thank you for shopping with us.");
          // Allow user to see the inventory 
        static void DisplayInventory(Dictionary<string, double> inventory)
        {
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Key, -30}  ${item.Value:F2}");
            }
        }
            
    }
}
    
