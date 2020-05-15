using System;
using System.Collections.Generic;

namespace Amplifiers
{
    public class AmplifierView
    {
        public void All_Amplifiers()
        {
            AmplifierController AmpController = new AmplifierController();

            IEnumerable<Amplifier> Amps = AmpController.GET_Amplifiers();

            Columns_Amplifiers();

            foreach (Amplifier amp in Amps)
            {
                Console.Write((amp.Id_Amplifier).ToString() + "\t");

                Console.Write(amp.Name + "\t");

                Console.Write(amp.BassKnob + "\t");

                Console.Write(amp.MidKnob + "\t");

                Console.WriteLine(amp.TrebleKnob);
            }
        }

        private void Columns_Amplifiers()
        {
            Console.WriteLine("ID" + "\t" + "Nombre" + "\t" + "\t" + "Bass" + "\t" + "Mid" + "\t" + "Treble");
        }
    }
}
