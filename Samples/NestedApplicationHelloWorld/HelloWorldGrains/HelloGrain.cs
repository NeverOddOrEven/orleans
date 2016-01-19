using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using HelloWorldServices;
using Orleans;

namespace HelloWorldGrains
{
    /// <summary>
    /// Orleans grain implementation class HelloGrain.
    /// </summary>
    public class HelloGrain : Grain, HelloWorldInterfaces.IHello
    {
        private readonly IHelloWorldService _helloWorldService;
        public HelloGrain(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        Task<string> HelloWorldInterfaces.IHello.SayHello(string greeting)
        {
            return Task.FromResult(_helloWorldService.RespondToGreetings(greeting));
        }
    }
}
