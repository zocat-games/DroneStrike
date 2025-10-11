namespace Zocat
{
    public class ObjectId : InstanceBehaviour
    {
        public ObjectIdType ObjectIdType;
    }

    public enum ObjectIdType
    {
        None = 0,
        Head = 1,
        Body = 2
    }
}