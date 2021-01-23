using System;

namespace Framework.Tools
{
    public static class FileSizeFormatter
    {
        // Load all suffixes in an array  
        private static readonly string[] _suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public static string FormatSize(long bytes)
        {
            var counter = 0;
            var number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            return $"{number:n2} {_suffixes[counter]}";
        }
    }
}
