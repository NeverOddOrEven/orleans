namespace HelloWorldServices
{
    public interface IHelloWorldService
    {
        string RespondToGreetings(string greeting);
    }

    public class HelloWorldService : IHelloWorldService
    {
        public string RespondToGreetings(string greeting)
        {
            return string.Format("Thanks for the greeting: \"{0}\"", greeting);
        }
    }
}
