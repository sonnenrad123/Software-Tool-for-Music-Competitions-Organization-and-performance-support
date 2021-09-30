using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI.ViewModel
{
    class SingersTableViewModel:BindableBase
    {
        public ObservableCollection<Common.Models.Singer> Singers { get; set; } = new ObservableCollection<Common.Models.Singer>();
        private Common.Models.Competitor selectedCompetitor;


        public SingersTableViewModel()
        {
            RepositoryCommunicationProvider repo = new RepositoryCommunicationProvider();
            List<Common.Models.Singer> ret = new List<Common.Models.Singer>();


            foreach(Common.Models.Competitor cmp in repo.RepositoryProxy.ReadCompetitors())
            {
                ret.Add(cmp);
            }

            foreach(Common.Models.JuryMember jm in repo.RepositoryProxy.ReadJuryMembers())
            {
                ret.Add(jm);
            }

            Singers = new ObservableCollection<Common.Models.Singer>(ret);
        }
    }
}
