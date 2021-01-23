using System.IO;

namespace $safeprojectname$
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            using var streamReader = new MemoryStream();
            stream.CopyTo(streamReader);
            var result = streamReader.ToArray();
            return result;
        }
    }
}