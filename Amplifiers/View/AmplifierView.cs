using System;
using System.Collections.Generic;

namespace Amplifiers
{
    public class AmplifierView
    {
        public void All_Amplifiers()
        {
            AmplifierController AmpController = new AmplifierController();

            IEnumerable<Amplifier> Amps = AmpController.Get_Amplifiers();

            Columns_Amplifiers();

            foreach (Amplifier amp in Amps)
            {
                Console.Write((amp.ID + 1).ToString() + "\t");

                Console.Write(amp.Name + "\t");
            }
        }

        private void Columns_Amplifiers()
        {
            Console.WriteLine("ID" + "\t" + "Nombre" + "\t" + "\t" + "Bass" + "\t" + "Mid" + "\t" + "Treble");
        }
    }
}
