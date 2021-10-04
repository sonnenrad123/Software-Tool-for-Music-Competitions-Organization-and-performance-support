using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI.ViewModel
{
    class UsersTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.User> Singers { get; set; } = new ObservableCollection<Common.Models.User>();
        private Common.Models.Competitor selectedCompetitor;


        public UsersTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            List<Common.Models.User> ret = new List<Common.Models.User>();


            foreach(Common.Models.Competitor cmp in repo.RepositoryProxy.ReadCompetitors())
            {
                ret.Add(cmp);
            }

            foreach(Common.Models.JuryMember jm in repo.RepositoryProxy.ReadJuryMembers())
            {
                ret.Add(jm);
            }

            Singers = new ObservableCollection<Common.Models.User>(ret);
        }
    }
}
