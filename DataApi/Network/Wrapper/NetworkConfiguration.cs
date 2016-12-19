namespace DataApi.Network.Wrapper
{
    public class NetworkConfiguration
    {
        public string Address { get; }
        public int Port { get; }

        public NetworkConfiguration(string address, int port)
        {
            Address = address;
            Port = port;
        }
    }
}