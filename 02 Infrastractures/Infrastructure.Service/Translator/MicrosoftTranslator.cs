using Framework.Domain.Translator;
using Microsoft.Extensions.Localization;
using YQB.DomainModels.Properties;

namespace YQB.Infra.Service.Translator
{
    public class MicrosoftTranslator : ITranslator
    {
        private readonly IStringLocalizer _localizer;
        public MicrosoftTranslator(IStringLocalizerFactory localizerFactory)
            => _localizer = localizerFactory.Create(typeof(Resources));

        public string this[string name] => GetString(name);

        public string this[string name, params string[] arguments]
            => GetString(name, arguments);

        public string this[char separator, params string[] names]
            => GetConcatString(separator, names);

        public string GetString(string name) => _localizer[name];

        public string GetString(string pattern, params string[] arguments)
        {
            for (var i = 0; i < arguments.Length; i++)
                arguments[i] = GetString(arguments[i]);

            return _localizer[pattern, arguments];
        }

        public string GetConcatString(char separator = ' ', params string[] names)
        {
            for (var i = 0; i < names.Length; i++)
                names[i] = GetString(names[i]);

            return string.Join(separator, names);
        }
    }
}