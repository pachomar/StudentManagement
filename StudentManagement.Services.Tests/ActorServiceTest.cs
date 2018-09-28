using Moq;
using NUnit.Framework;
using StudentManagement.Entities;

namespace StudentManagement.Services.Tests
{
    [TestFixture]
    class ActorServiceTest
    {
        /// <summary>
        /// Test GetValidActor method
        /// </summary>
        [Test]
        public void TestGetValidActor()
        {
            //Mock the actor entity
            Actor actor = new Actor();
            actor.UserName = "sarah@example.edu";
            actor.Password = "studentAdmin";

            //Create a mocked service
            Mock<ActorService> actorService = new Mock<ActorService>();
            var dbActor = actorService.Object.GetValidActor(actor);

            Assert.IsNotNull(actor);
            Assert.IsInstanceOf<Actor>(dbActor);
            Assert.AreEqual("sarah@example.edu", dbActor.UserName);
        }

        /// <summary>
        /// Test GetValidActor with invalid values
        /// </summary>
        [Test]
        public void TestGetValidActorFail()
        {
            //Mock the actor entity with invalid values
            Actor actor = new Actor();
            actor.UserName = "sarah@example.";
            actor.Password = "studentAd";

            //Create a mocked service
            Mock<ActorService> actorService = new Mock<ActorService>();
            var dbActor = actorService.Object.GetValidActor(actor);

            Assert.AreEqual(0,actor.Id);
        }
    }
}
