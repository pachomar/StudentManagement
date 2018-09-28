using NUnit.Framework;
using StudentManagement.Services.Utils;

namespace StudentManagement.Services.Tests.Utils
{
    class StudentNumberGeneratorTest
    {
        /// <summary>
        /// Test to verify the length of the StudentNumber
        /// </summary>
        [Test]
        public void TestGetValidActor()
        {
            string number = new StudentNumberGenerator().GenerateNumber();
            Assert.AreEqual(25,number.Length);
        }
    }
}
