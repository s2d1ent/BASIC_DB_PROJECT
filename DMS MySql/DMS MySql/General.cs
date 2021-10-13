using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DMS_MySql
{
    class General
    {
        public void FoldersProject(string path)
        {
            var BasePath = path;
            var data = new DirectoryInfo($"{BasePath}/data");
            var backup = new DirectoryInfo($"{BasePath}/backups");
            if (!data.Exists)
                data.Create();
            if (!backup.Exists)
                backup.Create();
        }
        public void LastConnectedXml()
        {

        }
        public void SaveConnectedXml()
        {

        }
        public void LoadConnectedXml()
        {

        }
    }
}
