using System;
using System.Collections.Generic;
using System.Text;

namespace LALAPATAPA.Services.File
{
    public interface IFileService
    {
        List<string> GetEmbeddedResourceNames();

        string ReadStringFromAssemblyEmbeddedResource(string path);

        string ReadStringFromLocalAppDataFolder(string fileName);

        bool WriteStringToLocalAppDataFolder(string fileName, string textToWrite);

        bool ExistsInLocalAppDataFolder(string fileName);

    }
}
