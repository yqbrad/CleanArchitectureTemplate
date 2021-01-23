using $safeprojectname$.Properties;

namespace $safeprojectname$._Exceptions
{
    public enum ExceptionType
    {

    }

    public static class ExceptionTypeExtension
    {
        public static int Code(this ExceptionType type) => (int)type;

        public static string Message(this ExceptionType type)
            => Resources.ResourceManager.GetString(type.ToString()) ?? type.ToString();
    }
}