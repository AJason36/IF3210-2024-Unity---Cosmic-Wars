using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{

    public static DataPersistenceManager Instance { get; private set; }

    private GameData _gameData;
    private FileDataHandler _fileDataHandler;

    private readonly string _saveFileName = "save.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Duplicate instance of DataPersistenceManager found, destroying game object.");
            Destroy(gameObject);
        }

        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _saveFileName);
    }

    private void onEnable()
    {
        LoadGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame(string saveId)
    {
        _gameData = _fileDataHandler.Load(saveId);

        foreach (var dataPersistenceObject in GetDataPersistenceObjects())
        {
            dataPersistenceObject.LoadData(_gameData);
        }
    }

    public void SaveGame(string saveId)
    {
        if (this._gameData == null)
        {
            return;
        }

        foreach (var dataPersistenceObject in GetDataPersistenceObjects())
        {
            dataPersistenceObject.SaveData(_gameData);
        }

        _gameData.lastUpdated = System.DateTime.Now.ToBinary();

        _fileDataHandler.Save(_gameData, saveId);
    }

    public void DeleteSave(string saveId)
    {
        _fileDataHandler.Delete(saveId);
    }

    private List<IDataPersistence> GetDataPersistenceObjects()
    {
        if (_dataPersistenceObjects == null)
        {
            _dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>().ToList();
        }

        return _dataPersistenceObjects;
    }

    public bool HasGameData()
    {
        return _gameData != null;
    }

    public GameData GetGameData()
    {
        return _gameData;
    }

    public Dictionary<string, GameData> GetAllSaves()
    {
        return _fileDataHandler.GetAllSaves();
    }
}