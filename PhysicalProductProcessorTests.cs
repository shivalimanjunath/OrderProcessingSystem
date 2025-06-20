namespace OrderProcessingSystem.Tests
{
    [TestFixture]
    public class PhysicalProductProcessorTests
    {
        private PhysicalProductProcessor _physicalProductProcessor;
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            _physicalProductProcessor = new PhysicalProductProcessor();
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
        }

        [Test]
        public void It_Should_Generate_PackingSlip_And_Commission_Payment()
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
            _physicalProductProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for physical product: Headphones"));
                Assert.That(output, Does.Contain("Generating commission payment for physical product: Headphones"));
            });
        }

        [Test]
        public void It_Should_Process_Multiple_Physical_Products_In_Same_Order()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Headphones", Type = ProductType.Physical, Price = 99.99m },
                    new Product { Id = 2, Name = "Mouse", Type = ProductType.Physical, Price = 29.99m }
                }
            };

            // Act
            _physicalProductProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for physical product: Headphones"));
                Assert.That(output, Does.Contain("Generating commission payment for physical product: Headphones"));
                Assert.That(output, Does.Contain("Generating packing slip for physical product: Mouse"));
                Assert.That(output, Does.Contain("Generating commission payment for physical product: Mouse"));
            });
        }

        [Test]
        public void It_Should_Do_Nothing_For_NonPhysical_Product()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Premium Membership", Type = ProductType.Membership, Price = 199.99m }
                }
            };

            // Act
            _physicalProductProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.That(output, Is.Empty);
        }

        [Test]
        public void It_Should_Process_Physical_Product_Without_CustomerId()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Headphones", Type = ProductType.Physical, Price = 99.99m }
                }
            };

            // Act
            _physicalProductProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for physical product: Headphones"));
                Assert.That(output, Does.Contain("Generating commission payment for physical product: Headphones"));
            });
        }
    }
} 