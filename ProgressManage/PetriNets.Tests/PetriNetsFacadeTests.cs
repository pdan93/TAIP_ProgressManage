using NUnit.Framework;
using PetriNets.States;
using PetriNets;
using Shouldly;
using Workflow.SprintEntities;
using Task = Workflow.Task;

namespace PetriNets.Tests
{
    [TestFixture]
    public class PetriNetsFacadeTests
    {
		[Test]
		public void Initialization_ShouldBeDone() {
			//Arrange
			PetriNetsFacade Facade = new PetriNetsFacade();
			//Act 
			
			//Assert
			Facade.ShouldContain(PetriNetwork);
			Facade.ShouldContain(TaskStrategy);
			Facade.ShouldContain(PetriNetsEvaluator);
		}
		
		[Test]
		public void InsertSprintEntity_ShouldBeDone() {
			//Arrange
			PetriNetsFacade Facade = new PetriNetsFacade();
			SprintEntity testEntity = new Tested();
			Facade.InsertSprintEntity(testEntity);
			//Act
			Facade.PetriNetwork.ShouldContain(testEntity);
		}
		
		[Test]
		public void Evaluate_ShouldBeDone() {
			//Arrange
			PetriNetsFacade Facade = new PetriNetsFacade();
			SprintEntity testEntity = new Tested();
			Facade.InsertSprintEntity(testEntity);
			Facade.Evaluate(Facade.PetriNetwork.Entities);
			//Act
			Facade.resultEvaluator.ShouldAllBe(string);
		}
		
		[Test]
		public void Run_ShouldBeDone() {
			//Arrange
			PetriNetsFacade Facade = new PetriNetsFacade();
			SprintEntity testEntity = new Tested();
			SprintEntity token = new SprintEntities();
			Facade.InsertSprintEntity(testEntity);
			Facade.Run(token);
			//Act
			Facade.resultRun.ShouldAllBe(string);
		}
		

    }
}
