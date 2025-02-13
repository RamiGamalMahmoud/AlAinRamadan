using AlAinRamadan.Core.Abstraction;
using System;
using System.IO;

namespace AlAinRamadan.Services
{
    internal class DirectoriesService : IDirectoriesService
    {
        public string AppDataPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AlAinRamadan");

        public string DataPath => Path.Combine(AppDataPath, "Data");

        public string DatabasePath
        {
            get
            {
#if DEBUG
                return Path.Combine(DataPath, "al_ain_ramadan.dev.db");
#else
                return Path.Combine(DataPath, "al_ain_ramadan.db");
#endif
            }
        }

        public void CreateDirectories()
        {
            Directory.CreateDirectory(AppDataPath);
            Directory.CreateDirectory(DataPath);
        }

        public void CreateDatabase()
        {
            if (!File.Exists(DatabasePath))
            {
                string sourceDatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DB", "al_ain_ramadan.db");
                File.Copy(sourceDatabasePath, DatabasePath);
            }
        }
    }
}
