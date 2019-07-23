using PCLExt.FileStorage.Folders;
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

            string settingspath = Path.Combine("/mnt/sdcard/Download",/*System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonDocuments),*/
                "relat.html");
            

            StreamWriter stream = File.CreateText(settingspath);
            stream.Write(content);
            stream.Close();

            return settingspath;
        }
    }
}
