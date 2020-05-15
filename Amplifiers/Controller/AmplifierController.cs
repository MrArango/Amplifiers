using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace Amplifiers
{
    public class AmplifierController
    {
        public IEnumerable<Amplifier> GET_Amplifiers()
        {
            using (var db = new SqlConnection(Properties.Resources.strcon))
            {
                IEnumerable<Amplifier> amplifiers = db.Query<Amplifier>("select Id_Amplifier, Name, BassKnob, MidKnob, TrebleKnob from Amplifiers");

                return amplifiers;
            }
        }

        public void POST_Amplifier(Amplifier newAmp)
        {
            using (var db = new SqlConnection(Properties.Resources.strcon))
            {
                var result = db.Execute("Insert into Amplifiers Values(@Name, @Bass, @Mid, @Trebel)", new { Name = newAmp.Name, Bass = newAmp.BassKnob, Mid = newAmp.MidKnob, Trebel = newAmp.TrebleKnob });
            }
        }

        public void DELETE_Amplifier(int ID)
        {
            using (var db = new SqlConnection(Properties.Resources.strcon))
            {
                var result = db.Execute("delete from Amplifiers where Id_Amplifier = @ID", new { ID });
            }
        }
    }
}
