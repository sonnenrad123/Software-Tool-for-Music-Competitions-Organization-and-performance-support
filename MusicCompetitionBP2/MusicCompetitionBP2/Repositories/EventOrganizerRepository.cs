using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class EventOrganizerRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public EventOrganizerRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Create(Common.Models.EventOrganizer eo)
        {
            if (dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == eo.JMBG_SIN) != null)//ako pronadjemo nekoga sa istim jmbg-om ne mere
            {
                return false;
            }

            EventOrganizer tempCmp = new EventOrganizer()
            {
                ADDRESS_SIN = new ADDRESS() { CITY = eo.ADDRESS_SIN.CITY, HOME_NUMBER = eo.ADDRESS_SIN.HOME_NUMBER, STREET = eo.ADDRESS_SIN.STREET },
                BIRTHDATE_SIN = eo.BIRTHDATE_SIN,
                EMAIL_SIN = eo.EMAIL_SIN,
                FIRSTNAME_SIN = eo.FIRSTNAME_SIN,
                JMBG_SIN = eo.JMBG_SIN,
                LASTNAME_SIN = eo.LASTNAME_SIN,
                PHONE_NO_SIN = eo.PHONE_NO_SIN,
                Type = "EventOrganizer"
            };
            dbContext.Users.Add(tempCmp);

            return dbContext.SaveChanges() > 0 ? true : false;
        }


        public Common.Models.EventOrganizer Read(long JMBG)
        {
            var temp = dbContext.Users.AsNoTracking().FirstOrDefault((x) => x.JMBG_SIN == JMBG);
            if (temp == null)
            {
                return null;
            }

            if (temp.Type != "EventOrganizer")
            {
                return null;
            }
            return new Common.Models.EventOrganizer(temp.JMBG_SIN, temp.FIRSTNAME_SIN, temp.LASTNAME_SIN, temp.BIRTHDATE_SIN, temp.EMAIL_SIN, temp.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.ADDRESS_SIN.HOME_NUMBER, temp.ADDRESS_SIN.CITY, temp.ADDRESS_SIN.STREET)) { Type = "EventOrganizer" };
        }

        public IEnumerable<Common.Models.EventOrganizer> ReadAll()
        {
            var ret = new List<Common.Models.EventOrganizer>();
            dbContext.Users.AsNoTracking().ToList().ForEach((temp) =>
            {
                if (temp.Type == "EventOrganizer")
                {
                    ret.Add(new Common.Models.EventOrganizer(temp.JMBG_SIN, temp.FIRSTNAME_SIN, temp.LASTNAME_SIN, temp.BIRTHDATE_SIN, temp.EMAIL_SIN, temp.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.ADDRESS_SIN.HOME_NUMBER, temp.ADDRESS_SIN.CITY, temp.ADDRESS_SIN.STREET)) { Type = "EventOrganizer" });
                }
            });
            return ret;
        }

        public void Update(Common.Models.EventOrganizer comp)
        {
            var temp = dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == comp.JMBG_SIN);
            dbContext.Entry(temp).CurrentValues.SetValues(comp);
            dbContext.SaveChanges();
        }


        public bool Remove(long JMBG)
        {

            try
            {
                User s = dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == JMBG);
                if (s == null)
                {
                    return false;
                }
                if (s.Type == "EventOrganizer")
                {
                    using (var sqlLogFile = new StreamWriter("LogFile.txt"))
                    {
                        dbContext.Database.Log = sqlLogFile.Write;
                        dbContext.Users.Remove(s);
                        dbContext.SaveChanges();
                    }


                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {

                return false;
            }
        }


        ~EventOrganizerRepository()
        {
            dbContext.Dispose();
        }
    }
}
