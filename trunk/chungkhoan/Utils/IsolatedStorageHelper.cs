using System.IO;
using System.IO.IsolatedStorage;

namespace PhoneApp1.Utils
{
    public class IsolatedStorageHelper
    {
        private static IsolatedStorageFile GetStore()
        {
            return IsolatedStorageFile.GetUserStoreForApplication();
        }

        public static string ReadFile(string fileName)
        {
            using (var isoFileStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, GetStore()))
            {
                using (var isoFileReader = new StreamReader(isoFileStream))
                {
                    return isoFileReader.ReadToEnd();
                }
            }
        }

        public static void WriteFile(string fileName, string content)
        {
            using (var isoFileStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, GetStore()))
            {
                using (var isoFileWriter = new StreamWriter(isoFileStream))
                {
                    isoFileWriter.Write(content);
                }
            }
        }
    }
}
