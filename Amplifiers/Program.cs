using System;

namespace Amplifiers
{
    class Program
    {
        static void Main(string[] args)
        {
            Amplifier prueba = new Amplifier();

            prueba.Name = "Mesa Boogie";

            prueba.BassKnob = 7;

            prueba.MidKnob = 5;

            prueba.TrebleKnob = 7;

            AmplifierController AmpController = new AmplifierController();

            AmpController.POST_Amplifier(prueba);

            AmplifierView AmpView = new AmplifierView();

            AmpView.All_Amplifiers();

            AmpController.DELETE_Amplifier(2);

            AmpView.All_Amplifiers();

            Console.ReadKey();
        }
    }
}
