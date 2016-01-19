using System;
using HelloWorldInterfaces;
using Orleans;

namespace HellowWorldClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GrainClient.Initialize("DevTestClientConfiguration.xml");

            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            Console.WriteLine("\n\n{0}\n\n", friend.SayHello("Good morning, my friend!").Result);

            Console.WriteLine("Press a key to exit...");
            Console.ReadLine();
        }
    }
}
