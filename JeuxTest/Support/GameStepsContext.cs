using JeuxLibrary.Common.Interfaces;

namespace JeuxTest.Support
{
    public class GameStepsContext
    {
        public IGame? Game { get; set; }
        public Exception? Exception { get; set; }
    }
}
