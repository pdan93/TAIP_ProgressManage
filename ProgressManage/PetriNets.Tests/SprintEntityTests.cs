using NUnit.Framework;
using PetriNets.States;
using Shouldly;
using Workflow.SprintEntities;
using Task = Workflow.SprintEntities.Task;

namespace PetriNets.Tests
{
    [TestFixture]
    public class SprintEntityTests
    {
        [Test]
        public void Defect_Close_ShouldBeFixed()
        {
            //Arrange
            State inProgressState = new InProgress();
            Defect defect = new Defect(inProgressState);
            //Act
            defect.Close();
            //Assert
            defect.State.ShouldBeOfType(typeof(Fixed));
        }

        [Test]
        public void TaskTested_Close_ShouldBeDone()
        {
            //Arrange
            State tested = new Tested();
            Task task = new Task(tested);
            //Act
            task.Close();
            //Assert
            task.State.ShouldBeOfType(typeof(Done));
        }

        [Test]
        public void UntestedTask_Close_ShouldNotBePossible()
        {
            //Arrange
            State implemented = new Implemented();
            SprintEntity task = new Task(implemented);
            //Act
            task.Close();
            //Assert
            task.State.ShouldBeOfType(typeof(Implemented));
        }

        [Test]
        public void NewTask_MoveToInProgress_ShouldNotBePossible()
        {
            //Arrange
            State New = new New();
            Task task = new Task(New);
            //Act
            task.MoveTo(new InProgress());
            //Assert
            task.State.ShouldBeOfType(typeof(New));
        }

    }
}
