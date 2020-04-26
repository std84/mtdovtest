using Microsoft.VisualStudio.TestTools.UnitTesting;
using missiontest.REPOSITORY;
using missiontest.MODAL;
namespace missiontest.testing
{
    [TestClass]
    public class UnitTest1
    {
  private readonly ImissionRepository _primeService;

        public UnitTest1()
        {
           // _primeService = new ImissionRepository();
        }

        [TestMethod]
        public void getMissions()
        {
            var result = _primeService.GetMission(1);
            Assert.IsNotNull(result);
            //Assert.IsInstanceOfType(result,MISSION);
            //Assert.IsFalse(result, "1 should not be prime");
        }
    }
}
