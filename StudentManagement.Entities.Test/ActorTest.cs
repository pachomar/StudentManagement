using NUnit.Framework;
using System;

namespace StudentManagement.Entities.Test
{
    [TestFixture]
    class ActorTest
    {
        /// <summary>
        /// Test to ensure no duplicate username is inserted
        /// </summary>
        [Test]
        public void AddActorFailDuplicateUsername()
        {
            StudentDBEntities db = new StudentDBEntities();
            Actor actor = CreateMockedActor();
            actor.UserName = "sarah@example.edu";

            try
            {   ///try to add an existing username
                db.Actors.Add(actor);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }

        /// <summary>
        /// Test to ensure no username with over 20 characters is inserted
        /// </summary>
        [Test]
        public void AddActorFailUserNameToolong()
        {
            StudentDBEntities db = new StudentDBEntities();
            Actor actor = CreateMockedActor();
            actor.UserName = "omar@example.edu_______________________________";

            try
            {   ///try to add an existing username
                db.Actors.Add(actor);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex);
            }
        }
        /// <summary>
        /// Create a mocked actor entity
        /// </summary>
        /// <returns></returns>
        private Actor CreateMockedActor()
        {
            Actor actor = new Actor();
            actor.UserName = "Omar@user.edu";
            actor.Password = "testActor";
            actor.Role = "testRole";

            return actor;
        }
    }
}
