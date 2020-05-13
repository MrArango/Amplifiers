using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Amplifiers
{
    public class AmplifierController
    {
        public IEnumerable<Amplifier> Get_Amplifiers()
        {
            using (var db = new SqlConnection(Properties.Resources.strcon))
            {
                IEnumerable < Amplifier > amplifiers = db.Query<Amplifier>("select * from Amplifiers");

                return amplifiers;
            }
        }
    }
}
