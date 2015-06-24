namespace Messenger
{
    public sealed class Singleton
    {
        private static readonly Singleton Instance = new Singleton();
        public Server Server {get; private set; }

	    static Singleton()
	    {
	    }

        private Singleton()
        {
            Server = new Server();
        }

	    public static Server GetServer
	    {
            get { return Instance.Server; } 
	    }
    }
}