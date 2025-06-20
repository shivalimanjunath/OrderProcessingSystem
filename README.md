Order Processing Rule Engine
This project is a C# console application demonstrating a robust and extensible system for processing business rules related to customer orders. It was designed to replace a chaotic mix of manual and ad-hoc practices with a single, unified, and maintainable application.
The solution is built following modern software design principles to ensure it is open for extension but closed for modification.
The Problem
A company needs to consolidate its various order-handling practices into a new system. The initial set of business rules to be managed are:
If the payment is for a physical product, generate a packing slip for shipping.
If the payment is for a book, create a duplicate packing slip for the royalty department.
If the payment is for a membership, activate that membership.
If the payment is an upgrade to a membership, apply the upgrade.
If the payment is for a membership or upgrade, e-mail the owner and inform them of the activation/upgrade.
If the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip.
If the payment is for a physical product or a book, generate a commission payment to the agent.
The key challenge is to design a system that can handle these rules and be easily extended with new rules in the future without modifying existing code.
Core Design Principles
This solution avoids a large, fragile if-else structure by implementing a Rule Engine pattern, which is a variation of the Chain of Responsibility.
Open/Closed Principle (OCP): The primary goal is to create a system that is open to extension (we can add new rules) but closed for modification (we don't need to change existing, tested code to do so).
Rule Encapsulation: Each business rule is encapsulated in its own class. All rule classes implement a common IOrderProcessingRule interface.
Rule Engine (OrderProcessor): A central OrderProcessor class maintains a collection of all active rules. When an order is processed, the engine iterates through its rules and applies each one to the order. The engine is completely decoupled from the specific logic within each rule.
Dependency Injection (DI): Rules do not perform actions (like sending emails or generating slips) directly. Instead, they depend on service interfaces (e.g., IShippingService, IMembershipService). These dependencies are "injected" into each rule's constructor. This decouples the rule's logic from the implementation of the actions, making the rules highly focused and easy to unit test.
Project Structure
The solution is divided into three distinct projects for a clean separation of concerns:
Project	Description
OrderProcessing.Core	A class library containing the core domain models (Order, Product), service interfaces (IShippingService), and the rule engine logic (IOrderProcessingRule).
OrderProcessing.App	A .NET Console Application that acts as the Composition Root. It is responsible for wiring up all the dependencies, creating the rule instances, and running a simulation.
OrderProcessing.Tests	An xUnit test project for unit testing the individual business rules in complete isolation, using Moq for mocking service dependencies.
Prerequisites
.NET 8 SDK (or newer)
Git
