using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppChamaGas.Helper
{
    public class FileManager
    {
        public static string Save(string content)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string settingspath = Path.Combine(path, "relat.html");
            StreamWriter stream = File.CreateText(settingspath);
            stream.Write(content);
            stream.Close();

            return settingspath;
        }
    }
}
