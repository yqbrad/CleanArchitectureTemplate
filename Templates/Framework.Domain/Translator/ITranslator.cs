namespace $safeprojectname$.Translator
{
    public interface ITranslator
    {
        string this[string name] { get; }
        string this[string name, params string[] arguments] { get; }
        string this[char separator, params string[] names] { get; }

        string GetString(string name);
        string GetString(string pattern, params string[] arguments);
        string GetConcatString(char separator = ' ', params string[] names);
    }
}