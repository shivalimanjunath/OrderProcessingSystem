namespace OrderProcessingSystem.Tests
{
    [TestFixture]
    public class MembershipUpgradeProcessorTests
    {
        private MembershipUpgradeProcessor _membershipUpgradeProcessor;
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            _membershipUpgradeProcessor = new MembershipUpgradeProcessor();
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
        }

        [Test]
        public void It_Should_Apply_Upgrade_And_Send_Email()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Premium Plus Upgrade", Type = ProductType.MembershipUpgrade, Price = 99.99m }
                }
            };

            // Act
            _membershipUpgradeProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Applying membership upgrade: Premium Plus Upgrade for customer: CUST001"));
                Assert.That(output, Does.Contain("Sending membership upgrade email to customer: CUST001"));
            });
        }

        [Test]
        public void It_Should_Process_Multiple_Upgrades_In_Same_Order()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Premium Plus Upgrade", Type = ProductType.MembershipUpgrade, Price = 99.99m },
                    new Product { Id = 2, Name = "VIP Upgrade", Type = ProductType.MembershipUpgrade, Price = 149.99m }
                }
            };

            // Act
            _membershipUpgradeProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Applying membership upgrade: Premium Plus Upgrade for customer: CUST001"));
                Assert.That(output, Does.Contain("Sending membership upgrade email to customer: CUST001"));
                Assert.That(output, Does.Contain("Applying membership upgrade: VIP Upgrade for customer: CUST001"));
                Assert.That(output, Does.Contain("Sending membership upgrade email to customer: CUST001"));
            });
        }

        [Test]
        public void It_Should_Do_Nothing_For_NonUpgrade_Product()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Basic Membership", Type = ProductType.Membership, Price = 99.99m }
                }
            };

            // Act
            _membershipUpgradeProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.That(output, Is.Empty);
        }

        [Test]
        public void It_Should_Handle_Order_Without_CustomerId()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Premium Plus Upgrade", Type = ProductType.MembershipUpgrade, Price = 99.99m }
                }
            };

            // Act
            _membershipUpgradeProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Applying membership upgrade: Premium Plus Upgrade for customer: "));
                Assert.That(output, Does.Contain("Sending membership upgrade email to customer: "));
            });
        }
    }
} 