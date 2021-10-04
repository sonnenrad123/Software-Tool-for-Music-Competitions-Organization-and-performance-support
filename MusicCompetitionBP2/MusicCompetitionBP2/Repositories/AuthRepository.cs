using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class AuthRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public AuthRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Common.Models.User ReadUser(string email, string password) 
        {
            var loggedUser = dbContext.Users.FirstOrDefault(x => x.EMAIL_SIN == email && (x.Password == password || x.Password == null || x.Password == "ChangePassword"));
            if(loggedUser == null)
            {
                return null;
            }

            return new Common.Models.User(loggedUser.JMBG_SIN, loggedUser.FIRSTNAME_SIN, loggedUser.LASTNAME_SIN, loggedUser.BIRTHDATE_SIN, loggedUser.EMAIL_SIN, loggedUser.PHONE_NO_SIN, new Common.Models.ADDRESS(loggedUser.ADDRESS_SIN.HOME_NUMBER, loggedUser.ADDRESS_SIN.CITY, loggedUser.ADDRESS_SIN.STREET)) { Type = loggedUser.Type,Password = loggedUser.Password };
        }
        public void ChangePassword(Common.Models.User u)
        {
            var temp = dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == u.JMBG_SIN);
            dbContext.Entry(temp).CurrentValues.SetValues(u);
            dbContext.SaveChanges();
        }
    }
}
