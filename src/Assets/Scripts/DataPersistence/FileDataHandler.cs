using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string _filePath;
    private string _fileName;

    public FileDataHandler(string filePath, string fileName)
    {
        this._filePath = filePath;
        this._fileName = fileName;
    }

    public GameData Load(string saveId)
    {
        if (saveId == null)
        {
            return null;
        }

        // Ensure compatibility with different separators
        string fullPath = Path.Combine(_filePath, _fileName);

        GameData data = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string json = "";

                // Read from file
                using (FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        json = reader.ReadToEnd();
                    }
                }

                // Deserialize data
                data = JsonUtility.FromJson<GameData>(json);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured while loading data from file: " + fullPath + "\n" + e.Message);              
            }
        }

        return data;
    }

    public void Save(GameData data, string saveId)
    {
        if (data == null || saveId == null)
        {
            return;
        }

        // Ensure compatibility with different separators
        string fullPath = Path.Combine(_filePath, _fileName);

        try
        {
            // Create Dir if not exists
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // Serialize data
            string json = JsonUtility.ToJson(data, true);

            // Write to file
            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(json);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured while saving data to file: " + fullPath + "\n" + e.Message);
        }
    }

    public void Delete(string saveId)
    {
        if (saveId == null)
        {
            return;
        }

        // Ensure compatibility with different separators
        string fullPath = Path.Combine(_filePath, _fileName);

        if (File.Exists(fullPath))
        {
            try
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured while deleting data from file: " + fullPath + "\n" + e.Message);
            }
        }
    }

    public Dictionary<string, GameData> GetAllSaves()
    {
        Dictionary<string, GameData> saves = new Dictionary<string, GameData>();

        // Ensure compatibility with different separators
        string fullPath = Path.Combine(_filePath, _fileName);

        IEnumerable<DirectoryInfo> directories = new DirectoryInfo(_filePath).EnumerateDirectories();
        foreach (DirectoryInfo directory in directories)
        {
            string saveId = directory.Name;
            string saveDataPath = Path.Combine(fullPath, saveId, _fileName);

            if (!File.Exists(saveDataPath))
            {
                continue;
            }

            GameData data = Load(saveId);
            
            if (data != null)
            {
                saves.Add(saveId, data);
            }
        }

        return saves;
    }
}
