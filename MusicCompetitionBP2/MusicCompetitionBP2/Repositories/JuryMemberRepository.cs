﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCompetitionBP2.Repositories
{
    public class JuryMemberRepository
    {
        private readonly MusicCompetitionDbContext dbContext;
        public JuryMemberRepository(MusicCompetitionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public bool Create(Common.Models.JuryMember competitor)
        {
            if (dbContext.Singers.FirstOrDefault((x) => x.JMBG_SIN == competitor.JMBG_SIN) != null)//ako pronadjemo nekoga sa istim jmbg-om ne mere
            {
                return false;
            }

            JuryMember tempCmp = new JuryMember()
            {
                ADDRESS_SIN = new ADDRESS() { CITY = competitor.ADDRESS_SIN.CITY, HOME_NUMBER = competitor.ADDRESS_SIN.HOME_NUMBER, STREET = competitor.ADDRESS_SIN.STREET },
                BIRTHDATE_SIN = competitor.BIRTHDATE_SIN,
                EMAIL_SIN = competitor.EMAIL_SIN,
                FIRSTNAME_SIN = competitor.FIRSTNAME_SIN,
                JMBG_SIN = competitor.JMBG_SIN,
                LASTNAME_SIN = competitor.LASTNAME_SIN,
                PHONE_NO_SIN = competitor.PHONE_NO_SIN,
                Type = "JuryMember"
            };
            dbContext.Singers.Add(tempCmp);

            return dbContext.SaveChanges() > 0 ? true : false;
        }


        public Common.Models.JuryMember Read(long JMBG)
        {
            var temp = dbContext.Singers.AsNoTracking().FirstOrDefault((x) => x.JMBG_SIN == JMBG);
            if (temp == null)
            {
                return null;
            }

            if (temp.Type != "JuryMember")
            {
                return null;
            }
            return new Common.Models.JuryMember(temp.JMBG_SIN, temp.FIRSTNAME_SIN, temp.LASTNAME_SIN, temp.BIRTHDATE_SIN, temp.EMAIL_SIN, temp.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.ADDRESS_SIN.HOME_NUMBER, temp.ADDRESS_SIN.CITY, temp.ADDRESS_SIN.STREET));
        }

        public IEnumerable<Common.Models.JuryMember> ReadAll()
        {
            var ret = new List<Common.Models.JuryMember>();
            dbContext.Singers.AsNoTracking().ToList().ForEach((temp) =>
            {
                if (temp.Type == "JuryMember")
                {
                    ret.Add(new Common.Models.JuryMember(temp.JMBG_SIN, temp.FIRSTNAME_SIN, temp.LASTNAME_SIN, temp.BIRTHDATE_SIN, temp.EMAIL_SIN, temp.PHONE_NO_SIN, new Common.Models.ADDRESS(temp.ADDRESS_SIN.HOME_NUMBER, temp.ADDRESS_SIN.CITY, temp.ADDRESS_SIN.STREET)));
                }
            });
            return ret;
        }

        public void Update(Common.Models.JuryMember comp)
        {
            var temp = dbContext.Singers.FirstOrDefault((x) => x.JMBG_SIN == comp.JMBG_SIN);
            dbContext.Entry(temp).CurrentValues.SetValues(comp);
            dbContext.SaveChanges();
        }


        public bool Remove(long JMBG)
        {
            try
            {
                Singer s = dbContext.Singers.FirstOrDefault((x) => x.JMBG_SIN == JMBG);
                if (s == null)
                {
                    return false;
                }
                if (s.Type == "JuryMember")
                {
                    dbContext.Singers.Remove(s);
                    dbContext.SaveChanges();
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


        ~JuryMemberRepository()
        {
            dbContext.Dispose();
        }
    }
}
