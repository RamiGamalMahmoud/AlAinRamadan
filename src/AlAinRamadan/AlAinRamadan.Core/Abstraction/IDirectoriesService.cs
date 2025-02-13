namespace AlAinRamadan.Core.Abstraction
{
    public interface IDirectoriesService
    {
        string AppDataPath { get; }
        string DataPath { get; }
        string DatabasePath { get; }

        void CreateDatabase();
        void CreateDirectories();
    }
}
