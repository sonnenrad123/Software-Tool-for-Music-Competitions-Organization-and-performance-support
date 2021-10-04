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
           

            var loggedUser = dbContext.Users.FirstOrDefault(x => x.EMAIL_SIN == email);

            if(loggedUser == null)
            {
                return null;
            }

            if(loggedUser.Password == "ChangePassword" || loggedUser.Password == "Admin")
            {
                return new Common.Models.User(loggedUser.JMBG_SIN, loggedUser.FIRSTNAME_SIN, loggedUser.LASTNAME_SIN, loggedUser.BIRTHDATE_SIN, loggedUser.EMAIL_SIN, loggedUser.PHONE_NO_SIN, new Common.Models.ADDRESS(loggedUser.ADDRESS_SIN.HOME_NUMBER, loggedUser.ADDRESS_SIN.CITY, loggedUser.ADDRESS_SIN.STREET)) { Type = loggedUser.Type, Password = loggedUser.Password };
            }

            if (loggedUser.Password.Contains("-FL-"))
            {
                if(loggedUser.Password == password)
                {
                    return new Common.Models.User(loggedUser.JMBG_SIN, loggedUser.FIRSTNAME_SIN, loggedUser.LASTNAME_SIN, loggedUser.BIRTHDATE_SIN, loggedUser.EMAIL_SIN, loggedUser.PHONE_NO_SIN, new Common.Models.ADDRESS(loggedUser.ADDRESS_SIN.HOME_NUMBER, loggedUser.ADDRESS_SIN.CITY, loggedUser.ADDRESS_SIN.STREET)) { Type = loggedUser.Type, Password = loggedUser.Password };
                }
                else
                {
                    return null;
                }
            }

            if (Common.PasswordHasher.Verify(password, loggedUser.Password))
            {
                return new Common.Models.User(loggedUser.JMBG_SIN, loggedUser.FIRSTNAME_SIN, loggedUser.LASTNAME_SIN, loggedUser.BIRTHDATE_SIN, loggedUser.EMAIL_SIN, loggedUser.PHONE_NO_SIN, new Common.Models.ADDRESS(loggedUser.ADDRESS_SIN.HOME_NUMBER, loggedUser.ADDRESS_SIN.CITY, loggedUser.ADDRESS_SIN.STREET)) { Type = loggedUser.Type, Password = loggedUser.Password };
            }
            else
            {
                return null;
            }

            
        }
        public void ChangePassword(Common.Models.User u)
        {
            var temp = dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == u.JMBG_SIN);
            dbContext.Entry(temp).CurrentValues.SetValues(u);
            dbContext.SaveChanges();
        }
    }
}
