namespace OrderProcessingSystem.Tests
{
    [TestFixture]
    public class VideoProcessorTests
    {
        private VideoProcessor _videoProcessor;
        private StringWriter _consoleOutput;

        [SetUp]
        public void Setup()
        {
            _videoProcessor = new VideoProcessor();
            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);
        }

        [TearDown]
        public void TearDown()
        {
            _consoleOutput.Dispose();
        }

        [Test]
        public void It_Should_Generate_PackingSlip_And_Add_FirstAidVideo_For_LearningToSki()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Learning to Ski", Type = ProductType.Video, Price = 29.99m }
                }
            };

            // Act
            _videoProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for video: Learning to Ski"));
                Assert.That(output, Does.Contain("Adding free \"First Aid\" video to packing slip due to court decision in 1997"));
            });
        }

        [Test]
        public void It_Should_Only_Generate_PackingSlip_For_Regular_Video()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Cooking Basics", Type = ProductType.Video, Price = 19.99m }
                }
            };

            // Act
            _videoProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for video: Cooking Basics"));
                Assert.That(output, Does.Not.Contain("Adding free \"First Aid\" video"));
            });
        }

        [Test]
        public void It_Should_Do_Nothing_For_NonVideo_Product()
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
            _videoProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.That(output, Is.Empty);
        }

        [Test]
        public void It_Should_Process_Multiple_Videos_In_Same_Order()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                CustomerId = "CUST001",
                Products = new List<Product>
                {
                    new Product { Id = 1, Name = "Learning to Ski", Type = ProductType.Video, Price = 29.99m },
                    new Product { Id = 2, Name = "Cooking Basics", Type = ProductType.Video, Price = 19.99m }
                }
            };

            // Act
            _videoProcessor.Process(order);
            var output = _consoleOutput.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(output, Does.Contain("Generating packing slip for video: Learning to Ski"));
                Assert.That(output, Does.Contain("Adding free \"First Aid\" video to packing slip due to court decision in 1997"));
                Assert.That(output, Does.Contain("Generating packing slip for video: Cooking Basics"));
            });
        }
    }
} 