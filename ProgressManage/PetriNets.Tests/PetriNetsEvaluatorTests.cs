using NUnit.Framework;
using PetriNets.States;
using Shouldly;
using Workflow;
using Workflow.SprintEntities;

namespace PetriNets.Tests
{
    [TestFixture]
    public class PetriNetsEvaluatorTests
    {
        [Test]
        public void MajorDefect_MoveToInProgress_ShouldBePossible()
        {
            //Arrange
            State newState = new New();
            var defect = new Defect(newState) { Priority = PriorityEnum.Major };
            //Act
            defect.MoveTo(new InProgress());
            //Assert
            defect.State.ShouldBeOfType(typeof(InProgress));
        }

        [Test]
        public void MinorDefectWithoutNewMajorDefect_MoveToInProgress_ShouldBePossible()
        {
            //Arrange
            State newState = new New();
            var defect = new Defect(newState) { Priority = PriorityEnum.Minor };
            var petriNetwork = new PetriNetwork();
            petriNetwork.Entities.Add(defect);
            var sprint = new Sprint() {PetriNetwork = petriNetwork};
            //Act
            sprint.MoveDefectTo(defect, new InProgress());
            //Assert
            defect.State.ShouldBeOfType(typeof(InProgress));
        }

        [Test]
        public void MinorDefectWithNewMajorDefect_MoveToInProgress_ShouldNotBePossible()
        {
            //Arrange
            var defect = new Defect(new New()) { Priority = PriorityEnum.Minor };
            var petriNetwork = new PetriNetwork();
            petriNetwork.Entities.Add(new Defect(new New()) { Priority = PriorityEnum.Major});
            petriNetwork.Entities.Add(defect);
            var sprint = new Sprint() { PetriNetwork = petriNetwork };
            //Act
            sprint.MoveDefectTo(defect, new InProgress());
            //Assert
            defect.State.ShouldBeOfType(typeof(New));
        }
        

        //test for not accessign withouth netsFacade
        //test for network to have all added entities
        //test for evaluator to return correct
    }
}
