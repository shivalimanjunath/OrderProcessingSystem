namespace OrderProcessingSystem.Tests
{
    [TestFixture]
    public class MembershipProcessorTests
    {
        private MembershipProcessor _membershipProcessor;
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            _membershipProcessor = new MembershipProcessor();
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
        }

        [Test]
        public void It_Should_Activate_Membership_And_Send_Email()
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
            _membershipProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Activating membership: Premium Membership for customer: CUST001"));
                Assert.That(output, Does.Contain("Sending membership activation email to customer: CUST001"));
            });
        }

        [Test]
        public void It_Should_Process_Multiple_Memberships_In_Same_Order()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Premium Membership", Type = ProductType.Membership, Price = 199.99m },
                    new Product { Id = 2, Name = "Basic Membership", Type = ProductType.Membership, Price = 99.99m }
                }
            };

            // Act
            _membershipProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Activating membership: Premium Membership for customer: CUST001"));
                Assert.That(output, Does.Contain("Sending membership activation email to customer: CUST001"));
                Assert.That(output, Does.Contain("Activating membership: Basic Membership for customer: CUST001"));
                Assert.That(output, Does.Contain("Sending membership activation email to customer: CUST001"));
            });
        }

        [Test]
        public void It_Should_Do_Nothing_For_NonMembership_Product()
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
            _membershipProcessor.Process(order);
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
                    new Product { Id = 1, Name = "Premium Membership", Type = ProductType.Membership, Price = 199.99m }
                }
            };

            // Act
            _membershipProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Activating membership: Premium Membership for customer: "));
                Assert.That(output, Does.Contain("Sending membership activation email to customer: "));
            });
        }
    }
} 