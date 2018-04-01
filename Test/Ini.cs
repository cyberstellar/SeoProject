using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Test
{
    internal class Ini
    {
        public Ini(string path)
        {
            Path = new FileInfo(path).FullName;
        }

        private string Path { get; }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string @default,
            StringBuilder retVal, int size, string filePath);

        public string Read(string section, string key)
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", retVal, 255, Path);
            return retVal.ToString();
        }

        public void Write(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }

        public void DeleteKey(string key, string section = null)
        {
            Write(section, key, null);
        }

        public void DeleteSection(string section = null)
        {
            Write(section, null, null);
        }

        public bool KeyExists(string key, string section = null)
        {
            return Read(section, key).Length > 0;
        }
    }
}