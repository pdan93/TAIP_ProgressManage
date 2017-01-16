using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using ProgressManage.Controllers;
using Shouldly;


namespace PetriNets.Tests
{
    [TestFixture]
    public class PetriNetsControllerTests
    {
        private readonly string _revionsFilePathInvalid = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\ProgressManage\App_Data\taskRevisions.json");
        private readonly string _revionsFilePathValid = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\ProgressManage\App_Data\taskRevisionsValid.json");
        private readonly string _transitionsDataPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\ProgressManage\App_Data\transitions.json");

        [Test]
        public void ValidTaskHistory_Get_ShouldBeValid()
        {
            //Arrange 
            var controller = new PetriNetsController();
            //Act
            string response = controller.Get(_revionsFilePathValid, _transitionsDataPath);
            //Assert
            response.ShouldBe("The History of the task passed the test");
        }

        [Test]
        public void InValidTaskHistory_Get_ShouldBeInvalid()
        {
            //Arrange
            var controller = new PetriNetsController();
            //Act
            string response = controller.Get(_revionsFilePathInvalid, _transitionsDataPath);
            //Assert
            response.ShouldNotBe("The History of the task passed the test");
        }

    }
}
