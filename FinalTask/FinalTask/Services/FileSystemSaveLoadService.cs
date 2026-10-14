namespace FinalTask.Services
{
    using System;
    using System.IO;
    using System.Text.Json;
    using FinalTask.Interfaces;

    public interface ISaveLoadService<T>
    {
        void SaveData(T data, string identifier);

        T LoadData(string identifier);
    }

    public class FileSystemSaveLoadService : ISaveLoadService<string>
    {
        private readonly string _path;

        public FileSystemSaveLoadService(string path)
        {
            
            _path = path;

            if (!Directory.Exists(_path))
            {
                
                Directory.CreateDirectory(_path);
            }
        }

        public void SaveData(string data, string identifier)
        {
            
            string filePath = Path.Combine(_path, identifier + ".txt");

            File.WriteAllText(filePath, data);
        }

        public string LoadData(string identifier)
        {
            
            string filePath = Path.Combine(_path, identifier + ".txt");

            if (!File.Exists(filePath))
            {
                
                return null;
            }

            return File.ReadAllText(filePath);
        }
    }
}
