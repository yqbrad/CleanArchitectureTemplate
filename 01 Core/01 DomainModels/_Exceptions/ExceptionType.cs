using DDD.DomainModels.Properties;

namespace DDD.DomainModels._Exceptions
{
    public enum ExceptionType
    {
        InvalidValueObjectStateException = 101
    }

    public static class ExceptionTypeExtension
    {
        public static int Code(this ExceptionType type) => (int)type;

        public static string Message(this ExceptionType type)
            => Resources.ResourceManager.GetString(type.ToString()) ?? type.ToString();
    }
}