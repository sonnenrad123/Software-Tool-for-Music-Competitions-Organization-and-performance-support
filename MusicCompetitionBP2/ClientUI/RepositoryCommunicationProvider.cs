using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public class RepositoryCommunicationProvider
    {
        public Common.Interfaces.IAllOperations RepositoryProxy;
        private ChannelFactory<Common.Interfaces.IAllOperations> cF;
        public RepositoryCommunicationProvider()
        {

            ChannelFactory<Common.Interfaces.IAllOperations> cF = new ChannelFactory<Common.Interfaces.IAllOperations>("Client");

            RepositoryProxy = cF.CreateChannel();
            
            
        }



        
    }
}
