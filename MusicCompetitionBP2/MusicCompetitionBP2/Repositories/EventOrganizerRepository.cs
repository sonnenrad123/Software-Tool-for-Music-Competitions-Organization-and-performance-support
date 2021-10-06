using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
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
            if (dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == eo.JMBG_SIN || x.EMAIL_SIN == eo.EMAIL_SIN) != null)//ako pronadjemo nekoga sa istim jmbg-om ne mere
            {
                return false;
            }

            string addition = RandomString(20);
            string password = "-FL-" + addition;

            EventOrganizer tempCmp = new EventOrganizer()
            {
                ADDRESS_SIN = new ADDRESS() { CITY = eo.ADDRESS_SIN.CITY, HOME_NUMBER = eo.ADDRESS_SIN.HOME_NUMBER, STREET = eo.ADDRESS_SIN.STREET },
                BIRTHDATE_SIN = eo.BIRTHDATE_SIN,
                EMAIL_SIN = eo.EMAIL_SIN,
                FIRSTNAME_SIN = eo.FIRSTNAME_SIN,
                JMBG_SIN = eo.JMBG_SIN,
                LASTNAME_SIN = eo.LASTNAME_SIN,
                PHONE_NO_SIN = eo.PHONE_NO_SIN,
                PublishingHouseID_PH = eo.PublishingHouseID_PH,
                Password = password,
                Type = "EventOrganizer"
            };
            dbContext.Users.Add(tempCmp);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mcompetition2021@gmail.com", "muzickotakmicenje"),
                EnableSsl = true,
            };

            smtpClient.Send("mcompetition2021@gmail.com", tempCmp.EMAIL_SIN, "Your login password for Global Music Competitions app.", string.Format("Your login credentials for Global Music Competitions app are:\nEmail: {0}\nPassword:{1}", tempCmp.EMAIL_SIN, tempCmp.Password)); ;


            return dbContext.SaveChanges() > 0 ? true : false;
        }


        public Common.Models.EventOrganizer Read(long JMBG)
        {
            EventOrganizer temp = (EventOrganizer) dbContext.Users.AsNoTracking().FirstOrDefault((x) => x.JMBG_SIN == JMBG);
            if (temp == null)
            {
                return null;
            }

            if (temp.Type != "EventOrganizer")
            {
                return null;
            }

            Common.Models.PublishingHouse phtemp =
                new Common.Models.PublishingHouse(temp.PublishingHouseID_PH, temp.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp.PublishingHouse.ADR_PH.HOME_NUMBER, temp.PublishingHouse.ADR_PH.CITY, temp.PublishingHouse.ADR_PH.STREET));


            return new Common.Models.EventOrganizer(temp.JMBG_SIN, temp.FIRSTNAME_SIN, temp.LASTNAME_SIN, temp.BIRTHDATE_SIN, temp.EMAIL_SIN, temp.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.ADDRESS_SIN.HOME_NUMBER, temp.ADDRESS_SIN.CITY, temp.ADDRESS_SIN.STREET)) { Type = "EventOrganizer", PublishingHouseID_PH=temp.PublishingHouse.ID_PH,PublishingHouse = phtemp };
        }

        public IEnumerable<Common.Models.EventOrganizer> ReadAll()
        {
            var ret = new List<Common.Models.EventOrganizer>();
            dbContext.Users.AsNoTracking().ToList().ForEach((temp) =>
            {
                if (temp.Type == "EventOrganizer")
                {
                    EventOrganizer temp2 = (EventOrganizer)temp;

                    Common.Models.PublishingHouse phtemp =
                new Common.Models.PublishingHouse(temp2.PublishingHouseID_PH, temp2.PublishingHouse.NAME_PH, new Common.Models.ADDRESS(temp2.PublishingHouse.ADR_PH.HOME_NUMBER, temp2.PublishingHouse.ADR_PH.CITY, temp2.PublishingHouse.ADR_PH.STREET));


                    ret.Add( new Common.Models.EventOrganizer(temp2.JMBG_SIN, temp2.FIRSTNAME_SIN, temp2.LASTNAME_SIN, temp2.BIRTHDATE_SIN, temp2.EMAIL_SIN, temp2.PHONE_NO_SIN, new Common.Models.ADDRESS(temp2.ADDRESS_SIN.HOME_NUMBER, temp2.ADDRESS_SIN.CITY, temp2.ADDRESS_SIN.STREET)) { Type = "EventOrganizer", PublishingHouseID_PH = temp2.PublishingHouse.ID_PH, PublishingHouse = phtemp });
                }
            });
            return ret;
        }

        public void Update(Common.Models.EventOrganizer comp)
        {
            var temp = dbContext.Users.FirstOrDefault((x) => x.JMBG_SIN == comp.JMBG_SIN);
            comp.Password = temp.Password;
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
        static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return res.ToString();
        }

        ~EventOrganizerRepository()
        {
            dbContext.Dispose();
        }
    }
}
