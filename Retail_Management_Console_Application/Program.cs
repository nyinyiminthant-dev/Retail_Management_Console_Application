
using Retail_Management_Console_Application;

Menu menu = new Menu();

menu.SeedData();

bool running = true;
while (running)
{
    Console.WriteLine("\n=== Retail Management System ===");
    Console.WriteLine("1. Stock Menu");
    Console.WriteLine("2. Cashier Menu");
    Console.WriteLine("3. Manager Menu");
    Console.WriteLine("4. Exit");
    Console.Write("Select an option: ");
    int choice = int.Parse(Console.ReadLine()!);

    switch (choice)
    {
        case 1:
            menu.ShowStockMenu();
            break;
        case 2:
            menu.ShowCashierMenu();
            break;
        case 3:
            menu.ShowManagerMenu();
            break;
        case 4:
            running = false;
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}