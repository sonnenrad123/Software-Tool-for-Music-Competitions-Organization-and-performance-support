using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Common.Models
{
    [DataContract]
    public class Administrator:User
    {
        public Administrator(long jMBG_SIN, string fIRSTNAME_SIN, string lASTNAME_SIN, DateTime bIRTHDATE_SIN, string eMAIL_SIN, string pHONE_NO_SIN, ADDRESS aDDRESS_SIN) : base(jMBG_SIN, fIRSTNAME_SIN, lASTNAME_SIN, bIRTHDATE_SIN, eMAIL_SIN, pHONE_NO_SIN, aDDRESS_SIN)
        {
            Type = "Administrator";
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
