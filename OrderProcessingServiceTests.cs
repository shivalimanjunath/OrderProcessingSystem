namespace OrderProcessingSystem.Tests
{
    [TestFixture]
    public class OrderProcessingServiceTests
    {
        private Mock<IOrderProcessor> _mockPhysicalProductProcessor;
        private Mock<IOrderProcessor> _mockBookProcessor;
        private Mock<IOrderProcessor> _mockMembershipProcessor;
        private Mock<IOrderProcessor> _mockMembershipUpgradeProcessor;
        private Mock<IOrderProcessor> _mockVideoProcessor;
        private OrderProcessingService _orderProcessingService;
        private List<IOrderProcessor> _processors;
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            // Setup console output redirection
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);

            // Initialize mocks
            _mockPhysicalProductProcessor = new Mock<IOrderProcessor>();
            _mockBookProcessor = new Mock<IOrderProcessor>();
            _mockMembershipProcessor = new Mock<IOrderProcessor>();
            _mockMembershipUpgradeProcessor = new Mock<IOrderProcessor>();
            _mockVideoProcessor = new Mock<IOrderProcessor>();

            // Create list of processors
            _processors = new List<IOrderProcessor>
            {
                _mockPhysicalProductProcessor.Object,
                _mockBookProcessor.Object,
                _mockMembershipProcessor.Object,
                _mockMembershipUpgradeProcessor.Object,
                _mockVideoProcessor.Object
            };

            // Initialize service with mock processors
            _orderProcessingService = new OrderProcessingService(_processors);
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
        }

        [Test]
        public void It_Should_Process_Physical_Product_Through_All_Processors()
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
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Book_Through_All_Processors()
        {
            // Arrange
            var order = new Order
            {
                Id = 2,
                CustomerId = "CUST002",
                Products = new List<Product>
                {
                    new Product { Id = 2, Name = "Clean Code", Type = ProductType.Book, Price = 49.99m }
                }
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Membership_Through_All_Processors()
        {
            // Arrange
            var order = new Order
            {
                Id = 3,
                CustomerId = "CUST003",
                Products = new List<Product>
                {
                    new Product { Id = 3, Name = "Premium Membership", Type = ProductType.Membership, Price = 199.99m }
                }
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Membership_Upgrade_Through_All_Processors()
        {
            // Arrange
            var order = new Order
            {
                Id = 4,
                CustomerId = "CUST003",
                Products = new List<Product>
                {
                    new Product { Id = 4, Name = "Premium Plus Upgrade", Type = ProductType.MembershipUpgrade, Price = 99.99m }
                }
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Video_Through_All_Processors()
        {
            // Arrange
            var order = new Order
            {
                Id = 5,
                CustomerId = "CUST004",
                Products = new List<Product>
                {
                    new Product { Id = 5, Name = "Learning to Ski", Type = ProductType.Video, Price = 29.99m }
                }
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Multiple_Products_Through_All_Processors()
        {
            // Arrange
            var order = new Order
            {
                Id = 7,
                CustomerId = "CUST006",
                Products = new List<Product>
                {
                    new Product { Id = 7, Name = "Mouse", Type = ProductType.Physical, Price = 29.99m },
                    new Product { Id = 8, Name = "Design Patterns", Type = ProductType.Book, Price = 54.99m },
                    new Product { Id = 9, Name = "Learning to Ski", Type = ProductType.Video, Price = 29.99m }
                }
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Empty_Order_Through_All_Processors()
        {
            // Arrange
            var order = new Order
            {
                Id = 8,
                CustomerId = "CUST007",
                Products = new List<Product>()
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }

        [Test]
        public void It_Should_Process_Order_Without_CustomerId()
        {
            // Arrange
            var order = new Order
            {
                Id = 9,
                Products = new List<Product>
                {
                    new Product { Id = 10, Name = "Mouse", Type = ProductType.Physical, Price = 29.99m }
                }
            };

            // Act
            _orderProcessingService.ProcessOrder(order);

            // Assert
            Assert.Multiple(() =>
            {
                _mockPhysicalProductProcessor.Verify(p => p.Process(order), Times.Once);
                _mockBookProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipProcessor.Verify(p => p.Process(order), Times.Once);
                _mockMembershipUpgradeProcessor.Verify(p => p.Process(order), Times.Once);
                _mockVideoProcessor.Verify(p => p.Process(order), Times.Once);
            });
        }
    }
} 