using OrderProcessingSystem.Models;
using OrderProcessingSystem.Processors;
using OrderProcessingSystem.Services;

namespace OrderProcessingSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var processors = new List<IOrderProcessor>
            {
                new PhysicalProductProcessor(),
                new BookProcessor(),
                new MembershipProcessor(),
                new MembershipUpgradeProcessor(),
                new VideoProcessor()
            };

            var orderProcessingService = new OrderProcessingService(processors);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Order Processing System");
                Console.WriteLine("======================");
                Console.WriteLine("1. Process a new order");
                Console.WriteLine("2. Process example orders");
                Console.WriteLine("3. Exit");
                Console.Write("\nSelect an option (1-3): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ProcessNewOrder(orderProcessingService);
                            break;
                        case 2:
                            ProcessExampleOrders(orderProcessingService);
                            break;
                        case 3:
                            return;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }

        private static void ProcessNewOrder(OrderProcessingService orderProcessingService)
        {
            Console.Clear();
            Console.WriteLine("New Order Entry");
            Console.WriteLine("==============");

            var order = new Order
            {
                Id = new Random().Next(1000, 9999),
                Products = new List<Product>()
            };

            // Get Customer ID
            Console.Write("Enter Customer ID (or press Enter to skip): ");
            order.CustomerId = Console.ReadLine();

            // Get Agent ID
            Console.Write("Enter Agent ID (or press Enter to skip): ");
            order.AgentId = Console.ReadLine();

            // Add products
            while (true)
            {
                Console.WriteLine("\nAdd Product");
                Console.WriteLine("===========");
                Console.WriteLine("Available Product Types:");
                Console.WriteLine("1. Physical Product");
                Console.WriteLine("2. Book");
                Console.WriteLine("3. Membership");
                Console.WriteLine("4. Membership Upgrade");
                Console.WriteLine("5. Video");
                Console.WriteLine("0. Finish adding products");
                Console.Write("\nSelect product type (0-5): ");

                if (int.TryParse(Console.ReadLine(), out int productChoice))
                {
                    if (productChoice == 0)
                        break;

                    if (productChoice >= 1 && productChoice <= 5)
                    {
                        var product = new Product
                        {
                            Id = new Random().Next(1000, 9999)
                        };

                        Console.Write("Enter product name: ");
                        product.Name = Console.ReadLine() ?? string.Empty;

                        Console.Write("Enter price: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            product.Price = price;
                        }

                        product.Type = (ProductType)(productChoice - 1);
                        order.Products.Add(product);
                        Console.WriteLine("Product added successfully!");
                    }
                }
            }

            if (order.Products.Any())
            {
                Console.WriteLine("\nProcessing Order...\n");
                orderProcessingService.ProcessOrder(order);
            }
            else
            {
                Console.WriteLine("\nNo products added to the order.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void ProcessExampleOrders(OrderProcessingService orderProcessingService)
        {
            Console.Clear();
            Console.WriteLine("Processing Example Orders...\n");

            var orders = CreateExampleOrders();
            foreach (var order in orders)
            {
                orderProcessingService.ProcessOrder(order);
                Console.WriteLine(new string('-', 50));
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static List<Order> CreateExampleOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = "CUST001",
                    AgentId = "AGENT001",
                    Products = new List<Product>
                    {
                        new Product { Id = 1, Name = "Headphones", Type = ProductType.Physical, Price = 99.99m }
                    }
                },

                new Order
                {
                    Id = 2,
                    CustomerId = "CUST002",
                    AgentId = "AGENT001",
                    Products = new List<Product>
                    {
                        new Product { Id = 2, Name = "Clean Code", Type = ProductType.Book, Price = 49.99m }
                    }
                },

                new Order
                {
                    Id = 3,
                    CustomerId = "CUST003",
                    Products = new List<Product>
                    {
                        new Product { Id = 3, Name = "Premium Membership", Type = ProductType.Membership, Price = 199.99m }
                    }
                },

                new Order
                {
                    Id = 4,
                    CustomerId = "CUST003",
                    Products = new List<Product>
                    {
                        new Product { Id = 4, Name = "Premium Plus Upgrade", Type = ProductType.MembershipUpgrade, Price = 99.99m }
                    }
                },

                new Order
                {
                    Id = 5,
                    CustomerId = "CUST004",
                    AgentId = "AGENT002",
                    Products = new List<Product>
                    {
                        new Product { Id = 5, Name = "Learning to Ski", Type = ProductType.Video, Price = 29.99m }
                    }
                },

                new Order
                {
                    Id = 6,
                    CustomerId = "CUST005",
                    AgentId = "AGENT002",
                    Products = new List<Product>
                    {
                        new Product { Id = 6, Name = "Cooking Basics", Type = ProductType.Video, Price = 19.99m }
                    }
                },

                new Order
                {
                    Id = 7,
                    CustomerId = "CUST006",
                    AgentId = "AGENT003",
                    Products = new List<Product>
                    {
                        new Product { Id = 7, Name = "Mouse", Type = ProductType.Physical, Price = 29.99m },
                        new Product { Id = 8, Name = "Design Patterns", Type = ProductType.Book, Price = 54.99m },
                        new Product { Id = 9, Name = "Learning to Ski", Type = ProductType.Video, Price = 29.99m }
                    }
                }
            };
        }
    }
}