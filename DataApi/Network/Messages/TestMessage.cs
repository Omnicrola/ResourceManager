using ProtoBuf;

namespace DataApi.Network.Messages
{
    [ProtoContract]
    public class TestMessage
    {
        [ProtoMember(1)]
        public string Message { get; set; }
    }
}