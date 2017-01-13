using NUnit.Framework;
using PetriNets.States;
using ProgressManage.Controllers;
using Shouldly;
using Workflow;
using Workflow.SprintEntities;


namespace PetriNets.Tests
{
    [TestFixture]
    public class PetriNetsControllerTests
    {
        private const string RevionsFilePathInvalid = "E:\\Informatica\\Master\\Advanced Software Engineering Techniques\\ProgressManage\\TestFiles\\taskRevisions.json";
        private const string RevionsFilePathValid = "E:\\Informatica\\Master\\Advanced Software Engineering Techniques\\ProgressManage\\TestFiles\\taskRevisionsValid.json";

        [Test]
        public void ValidTaskHistory_Get_ShouldBeValid()
        {
            //Arrange 
            var controller = new PetriNetsController();
            //Act
            string response = controller.Get(RevionsFilePathValid);
            //Assert
            response.ShouldBe("The History of the task passed the test");
        }

        [Test]
        public void InValidTaskHistory_Get_ShouldBeInvalid()
        {
            //Arrange
            var controller = new PetriNetsController();
            //Act
            string response = controller.Get(RevionsFilePathInvalid);
            //Assert
            response.ShouldNotBe("The History of the task passed the test");
        }
    }
}
