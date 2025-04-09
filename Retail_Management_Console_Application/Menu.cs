using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Management_Console_Application;
public class Menu
{

    private List<Product> productList = new List<Product>();
    private List<Sale> sales = new List<Sale>();

    public void ShowStockMenu()
    {
        Console.WriteLine("+------------+---------------------+--------+-----------+---------+");
        Console.WriteLine("| Product ID | Product Name        | Stock  | Price     | Profit   ");
        Console.WriteLine("+------------+---------------------+--------+-----------+---------+");


        foreach (var product in productList)
        {
            Console.WriteLine($"| {product.Id,-10} | {product.Name,-19} | {product.Stock,-6} | {product.Price,6:C} | {product.Profit} ");
        }


        Console.WriteLine("+------------+---------------------+--------+-----------+---------+");
        Console.WriteLine("1. Add Product\n2. Edit Product\n3. Back");
        Console.Write("Choose an option: ");
        int option = int.Parse(Console.ReadLine()!);

        if (option == 1)
        {
            int stockround = 1;
            int priceround = 1;
            int profitround = 1;

            int id = productList.Count + 1;
            Console.WriteLine("Enter your product name : ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid input. Product name cannot be empty.");
                return;
            }
            if (name.Any(c => !char.IsDigit(c)))
            {
                Console.WriteLine("Invalid input.");
                return;
            }


            while (stockround == 1 || stockround > 1)
            {
                Console.WriteLine("Enter your product stock : ");
                string stockInput = Console.ReadLine()!;
                if (!int.TryParse(stockInput, out int stock) || stock < 1)
                {
                    Console.WriteLine("Invalid input. Stock must be a positive number.");
                }
                else
                {
                    stockround = 0;

                    while (priceround == 1 || priceround > 1)
                    {
                        Console.WriteLine("Enter your product price : ");
                        string priceInput = Console.ReadLine()!;
                        if (!decimal.TryParse(priceInput, out decimal price) || price < 1)
                        {
                            Console.WriteLine("Invalid input. Price must be a positive number.");
                        }
                        else
                        {
                            priceround = 0;

                            while (profitround == 1 || profitround > 1)
                            {
                                Console.WriteLine("Enter your product profit : ");
                                string profitInput = Console.ReadLine()!;
                                if (!decimal.TryParse(profitInput, out decimal profit) || profit < 1)
                                {
                                    Console.WriteLine("Invalid input. Profit must be a positive number.");
                                }
                                else
                                {
                                    profitround = 0;
                                    productList.Add(new Product { Id = id, Name = name, Stock = stock, Price = price, Profit = profit });
                                    Console.WriteLine("Product added successfully.");
                                }
                            }
                        }
                    }
                }
            }
        }
        else if (option == 2)
        {
            Console.WriteLine("Enter the ID of the product to edit: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
            }
            else
            {
                var product = productList.FirstOrDefault(p => p.Id == id);

                if (product is not null)
                {
                    while (true)
                    {
                        Console.WriteLine("\nWhich field would you like to update?");
                        Console.WriteLine("1. Name\n2. Stock\n3. Price\n4. Profit\n5. Back");
                        Console.Write("Choose an option: ");
                        string choice = Console.ReadLine()!;

                        switch (choice)
                        {
                            case "1":
                                Console.Write("Enter new product name: ");
                                string newName = Console.ReadLine()!;
                                if (string.IsNullOrWhiteSpace(newName) || newName.All(char.IsDigit))
                                {
                                    Console.WriteLine("Invalid input. Name must contain letters and not be empty.");
                                }
                                else
                                {
                                    product.Name = newName;
                                    Console.WriteLine("Product name updated successfully.");
                                }
                                break;

                            case "2":
                                Console.Write("Enter new product stock: ");
                                string newStockInput = Console.ReadLine()!;
                                if (int.TryParse(newStockInput, out int newStock) && newStock > 0)
                                {
                                    product.Stock = newStock;
                                    Console.WriteLine("Product stock updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Stock must be a positive number.");
                                }
                                break;

                            case "3":
                                Console.Write("Enter new product price: ");
                                string newPriceInput = Console.ReadLine()!;
                                if (decimal.TryParse(newPriceInput, out decimal newPrice) && newPrice > 0)
                                {
                                    product.Price = newPrice;
                                    Console.WriteLine("Product price updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Price must be a positive number.");
                                }
                                break;

                            case "4":
                                Console.Write("Enter new product profit: ");
                                string newProfitInput = Console.ReadLine()!;
                                if (decimal.TryParse(newProfitInput, out decimal newProfit) && newProfit > 0)
                                {
                                    product.Profit = newProfit;
                                    Console.WriteLine("Product profit updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Profit must be a positive number.");
                                }
                                break;

                            case "5":
                                return;

                            default:
                                Console.WriteLine("Invalid option. Please choose between 1 and 5.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }

            }
        }
    }



    public void ShowCashierMenu()
    {
        List<Order> customerOrders = new List<Order>();
        bool ordering = true;

        while (ordering)
        {
            Console.WriteLine("\n--- Cashier Menu ---");
            Console.WriteLine("1. Add Order\n2. View Orders\n3. Back");
            Console.Write("Choose an option: ");

            // Validate menu option
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the option.");
                continue;
            }

            if (option == 1)
            {
                Console.Write("Enter Product ID: ");
                // Validate product ID input
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("Invalid input. Product ID must be a valid number.");
                    continue;
                }

                var product = productList.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    Console.Write("Enter Quantity: ");
                    // Validate quantity input
                    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Invalid input. Quantity must be a positive number.");
                        continue;
                    }

                    if (quantity <= product.Stock)
                    {
                        customerOrders.Add(new Order
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            Quantity = quantity,
                            Price = product.Price,
                            Profit = product.Profit
                        });
                        product.Stock -= quantity;
                        Console.WriteLine("Order added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient stock.");
                    }
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }

            else if (option == 2)
            {
                Console.WriteLine("\n--- Orders summary ---");
                decimal total = 0;
                foreach (var order in customerOrders)
                {
                    Console.WriteLine($"Product ID: {order.ProductId}, Name: {order.ProductName}, Quantity: {order.Quantity}, Price: {order.Price}, Profit: {order.Profit}");
                    total += order.Price * order.Quantity;
                }
                Console.WriteLine($"Total: {total}");

                Console.Write("Confirm purchase? (y/n): ");
                string confirmInput = Console.ReadLine()?.ToLower();
                if (confirmInput == "y")
                {
                    foreach (var order in customerOrders)
                    {
                        var product = productList.FirstOrDefault(p => p.Id == order.ProductId);
                        if (product != null)
                        {
                            product.Stock -= order.Quantity;
                            sales.Add(new Sale { ProductId = product.Id, ProductName = product.Name, Quantity = order.Quantity, Price = product.Price, Profit = product.Profit });
                        }

                    }
                    if (customerOrders.Count == 0)
                    {
                        Console.WriteLine("No orders to process.");
                    }
                    else
                    {
                        Console.WriteLine("Purchase completed.");
                    }

                }
                else if (confirmInput == "n")
                {
                    Console.WriteLine("Purchase cancelled.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'y' for yes or 'n' for no.");
                }
            }
            else if (option == 3)
            {
                ordering = false;
            }
            else
            {
                Console.WriteLine("Invalid option. Please choose a valid option.");
            }
        }
    }





    public void ShowManagerMenu()
    {
        if (sales == null || sales.Count == 0)
        {
            Console.WriteLine("\nNo sales data available.");
            return;
        }


        Console.WriteLine("\n--- Sales Report ---");
        Console.WriteLine("+------------+---------------------+--------+------------+--------+");
        Console.WriteLine("| Product ID | Product Name        | Sold   | Revenue    | Profit ");
        Console.WriteLine("+------------+---------------------+--------+------------+--------+");


        foreach (var sale in sales)
        {
            if (sale == null || sale.Quantity < 0 || sale.Price < 0 || sale.Profit < 0)
            {
                Console.WriteLine("Error: Invalid sale data.");
                continue;
            }


            Console.WriteLine($"| {sale.ProductId,-10} | {sale.ProductName,-19} | {sale.Quantity,-6} | {sale.Quantity * sale.Price,10:C} | {sale.Quantity * sale.Profit} ");
        }


        Console.WriteLine("+------------+---------------------+--------+------------+--------+");


        try
        {
            decimal totalRevenue = sales.Sum(s => s.Quantity * s.Price);
            decimal totalProfit = sales.Sum(s => s.Quantity * s.Profit);

            Console.WriteLine($"\nTotal Revenue: {totalRevenue:C}, Total Profit: {totalProfit:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calculating totals: {ex.Message}");
        }
    }

    public void SeedData()
    {
        productList.Add(new Product { Id = 1, Name = "Coke", Stock = 50, Price = 1500, Profit = 300 });
        productList.Add(new Product { Id = 2, Name = "Sprite", Stock = 30, Price = 1200, Profit = 200 });
    }
}
