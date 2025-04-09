using System;
using System.Collections.Generic;
using System.Linq;
using Retail_Management_Console_Application.Dto;

namespace Retail_Management_Console_Application;

public class Menu
{
    private readonly AppDbContext context = new AppDbContext();

    public void ShowStockMenu()
    {
        var productList = context.Products.ToList();

        Console.WriteLine("+------------+---------------------+--------+-----------+---------+");
        Console.WriteLine("| Product ID | Product Name        | Stock  | Price     | Profit   ");
        Console.WriteLine("+------------+---------------------+--------+-----------+---------+");

        foreach (var product in productList)
        {
            Console.WriteLine($"| {product.ProductId,-10} | {product.Name,-19} | {product.Stock,-6} | {product.Price,6:C} | {product.Profit}");
        }

        Console.WriteLine("+------------+---------------------+--------+-----------+---------+");
        Console.WriteLine("1. Add Product\n2. Edit Product\n3. Back");
        Console.Write("Choose an option: ");
        int option = int.Parse(Console.ReadLine()!);

        if (option == 1)
        {
            Console.Write("Enter your product name: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid input. Product name cannot be empty.");
                return;
            }

            Console.Write("Enter stock: ");
            if (!int.TryParse(Console.ReadLine(), out int stock) || stock < 1)
            {
                Console.WriteLine("Invalid input. Stock must be positive.");
                return;
            }

            Console.Write("Enter price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 1)
            {
                Console.WriteLine("Invalid input. Price must be positive.");
                return;
            }

            Console.Write("Enter profit: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal profit) || profit < 1)
            {
                Console.WriteLine("Invalid input. Profit must be positive.");
                return;
            }

            var product = new Product { Name = name, Stock = stock, Price = price, Profit = profit };
            context.Products.Add(product);
            context.SaveChanges();
            Console.WriteLine("Product added successfully.");
        }
        else if (option == 2)
        {
            Console.Write("Enter the ID of the product to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var product = context.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            while (true)
            {
                Console.WriteLine("1. Name\n2. Stock\n3. Price\n4. Profit\n5. Back");
                Console.Write("Choose an option to update: ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Console.Write("New name: ");
                        string newName = Console.ReadLine()!;
                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            product.Name = newName;
                            context.SaveChanges();
                            Console.WriteLine("Name updated.");
                        }
                        break;

                    case "2":
                        Console.Write("New stock: ");
                        if (int.TryParse(Console.ReadLine(), out int newStock))
                        {
                            product.Stock = newStock;
                            context.SaveChanges();
                            Console.WriteLine("Stock updated.");
                        }
                        break;

                    case "3":
                        Console.Write("New price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                        {
                            product.Price = newPrice;
                            context.SaveChanges();
                            Console.WriteLine("Price updated.");
                        }
                        break;

                    case "4":
                        Console.Write("New profit: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newProfit))
                        {
                            product.Profit = newProfit;
                            context.SaveChanges();
                            Console.WriteLine("Profit updated.");
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }

    public void ShowCashierMenu()
    {
        List<Order> customerOrders = new();

        while (true)
        {
            Console.WriteLine("\n--- Cashier Menu ---");
            Console.WriteLine("1. Add Order\n2. View Orders\n3. Back");
            Console.Write("Choose an option: ");
            if (!int.TryParse(Console.ReadLine(), out int option)) continue;

            if (option == 1)
            {
                Console.Write("Enter Product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId)) continue;

                var product = context.Products.FirstOrDefault(p => p.ProductId == productId);
                if (product == null)
                {
                    Console.WriteLine("Product not found.");
                    continue;
                }

                Console.Write("Enter Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0) continue;

                if (quantity > product.Stock)
                {
                    Console.WriteLine("Insufficient stock.");
                    continue;
                }

                customerOrders.Add(new Order
                {
                  
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    Quantity = quantity,
                    Price = product.Price,
                    Profit = product.Profit
                });

                product.Stock -= quantity;
                context.SaveChanges();
                Console.WriteLine("Order added successfully.");
            }

            else if (option == 2)
            {
                Console.WriteLine("--- Orders Summary ---");
                decimal total = 0;
                foreach (var order in customerOrders)
                {
                    Console.WriteLine($"Product: {order.ProductName}, Qty: {order.Quantity}, Price: {order.Price}");
                    total += order.Price * order.Quantity;
                }
                Console.WriteLine($"Total: {total:C}");

                Console.Write("Confirm purchase? (y/n): ");
                string confirm = Console.ReadLine()?.ToLower();
                if (confirm == "y")
                {
                    foreach (var order in customerOrders)
                    {
                        context.Orders.Add(order);
                    }
                    context.SaveChanges();
                    Console.WriteLine("Purchase completed.");
                    customerOrders.Clear();
                }
                else
                {
                    Console.WriteLine("Purchase cancelled.");
                }
            }

            else if (option == 3)
            {
                break;
            }
        }
    }

    public void ShowManagerMenu()
    {
        var orders = context.Orders.ToList();
        if (!orders.Any())
        {
            Console.WriteLine("No sales data.");
            return;
        }

        Console.WriteLine("--- Sales Report ---");
        Console.WriteLine("+------------+---------------------+--------+------------+--------+");
        Console.WriteLine("| Product ID | Product Name        | Sold   | Revenue    | Profit ");
        Console.WriteLine("+------------+---------------------+--------+------------+--------+");

        foreach (var order in orders)
        {
            decimal revenue = order.Quantity * order.Price;
            Console.WriteLine($"| {order.ProductId,-10} | {order.ProductName,-19} | {order.Quantity,-6} | {revenue,10:C} | {order.Profit}");
        }

        Console.WriteLine("+------------+---------------------+--------+------------+--------+");
    }
}
