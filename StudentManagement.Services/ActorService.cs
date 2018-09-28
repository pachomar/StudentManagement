using StudentManagement.Entities;
using StudentManagement.Services.Interfaces;
using System.Linq;

namespace StudentManagement.Services
{
    public class ActorService: IActorService
    {
        private StudentDBEntities db = new StudentDBEntities();

        /// <summary>
        /// Get actor from table matching username and password
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public Actor GetValidActor(Actor actor)
        {
            return (from act in db.Actors
                    where act.UserName == actor.UserName
                    && act.Password == actor.Password
                    select act).FirstOrDefault();
        }
    }
}
