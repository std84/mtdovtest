using Microsoft.VisualStudio.TestTools.UnitTesting;
using missiontest.REPOSITORY;
using missiontest.MODAL;
namespace missiontest.testing
{
      [TestClass]
    public class missionTest
    {
        private readonly ImissionRepository _primeService;
        private readonly var mission = {
	            userId= 1,
                name= "mission three",
                missionDate= "2012-04-23T18:25:43.511Z",
                priority= 2,
                isActive= false,
                isDone= false
            };
        public UnitTest1()
        {
           // _primeService = new ImissionRepository();
        }

        [TestMethod]
        public void getMissions()
        {
            var result = _primeService.GetMission(mission);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
        [TestMethod]
        public void addMissions()
        {
          
            var result = _primeService.addMissions(mission);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
        [TestMethod]
        public void ShareMission()
        {
            var email = "test@gamil.com";
            var result = _primeService.ShareMission(email,mission);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
        [TestMethod]
        public void SendMission()
        {
            var email = "test@gamil.com";
            var result = _primeService.ShareMission(email,mission);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
       [TestMethod]
        public void DeleteMission()
        {
            var email = "test@gamil.com";
            var result = _primeService.DeleteMission(email,mission);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
        [TestMethod]
        public void UpdateMission()
        {
            var email = "test@gamil.com";
            var result = _primeService.UpdateMission(email,mission);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
    }
}