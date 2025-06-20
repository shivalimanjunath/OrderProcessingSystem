namespace OrderProcessingSystem.Tests
{
    [TestFixture]
    public class BookProcessorTests
    {
        private BookProcessor _bookProcessor;
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            _bookProcessor = new BookProcessor();
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
        }

        [Test]
        public void It_Should_Generate_PackingSlip_And_Duplicate_For_Royalty_Department()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Clean Code", Type = ProductType.Book, Price = 49.99m }
                }
            };

            // Act
            _bookProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for book: Clean Code"));
                Assert.That(output, Does.Contain("Generating duplicate packing slip for royalty department: Clean Code"));
                Assert.That(output, Does.Contain("Generating commission payment for book: Clean Code"));
            });
        }

        [Test]
        public void It_Should_Process_Multiple_Books_In_Same_Order()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Clean Code", Type = ProductType.Book, Price = 49.99m },
                    new Product { Id = 2, Name = "Design Patterns", Type = ProductType.Book, Price = 54.99m }
                }
            };

            // Act
            _bookProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for book: Clean Code"));
                Assert.That(output, Does.Contain("Generating duplicate packing slip for royalty department: Clean Code"));
                Assert.That(output, Does.Contain("Generating commission payment for book: Clean Code"));
                Assert.That(output, Does.Contain("Generating packing slip for book: Design Patterns"));
                Assert.That(output, Does.Contain("Generating duplicate packing slip for royalty department: Design Patterns"));
                Assert.That(output, Does.Contain("Generating commission payment for book: Design Patterns"));
            });
        }

        [Test]
        public void It_Should_Do_Nothing_For_NonBook_Product()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Headphones", Type = ProductType.Physical, Price = 99.99m }
                }
            };

            // Act
            _bookProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.That(output, Is.Empty);
        }
    }
} 