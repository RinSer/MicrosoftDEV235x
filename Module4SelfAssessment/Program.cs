using System;
using System.Threading.Tasks;


namespace Module4SelfAssessment
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*var eap = new Eap2Tap();
            eap.Execute();
            await eap.ExecuteAsync();
            
            var apm = new Apm2Tap();
            apm.Execute();
            await apm.ExecuteAsync();*/

            var tap = new TapTap();
            await tap.ExecuteAsync();
            
            Console.ReadKey();
        }
    }
}
