using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : SingletonBase<SaveManager>
{
    [SerializeField] private List<MonoBehaviour> saveDataObjects = new List<MonoBehaviour>(); // Перетаскиваем в Inspector

    protected override void Awake()
    {
        base.Awake();
        if (saveDataObjects.Count == 0)
        {
            Debug.LogWarning("SaveManager: No objects assigned to saveDataObjects. Nothing will be saved.");
        }

        LoadAll();
    }

    public void SaveAll(string slot = "default")
    {
        try
        {
            var allData = new Dictionary<string, string>();
            foreach (var dataObject in saveDataObjects)
            {
                string id = dataObject.GetType().Name; // Используем имя типа
                string json = JsonUtility.ToJson(dataObject);
                allData[id] = json;
                Debug.Log($"Serialized {id}: {json}");
            }

            string fullJson = JsonUtility.ToJson(new SaveFile
            {
                version = 1,
                data = allData
            });

            File.WriteAllText(GetSavePath(slot), fullJson);

#if UNITY_WEBGL
            Application.ExternalEval("syncfs()");
#endif

            Debug.Log($"Game state saved to slot: {slot}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save: {e.Message}");
        }
    }

    public void LoadAll(string slot = "default")
    {
        try
        {
            string path = GetSavePath(slot);
            if (File.Exists(path))
            {
                string fullJson = File.ReadAllText(path);
                var saveFile = JsonUtility.FromJson<SaveFile>(fullJson);

                if (saveFile.version == 1)
                {
                    foreach (var dataObject in saveDataObjects)
                    {
                        string id = dataObject.GetType().Name;
                        if (saveFile.data.TryGetValue(id, out string json))
                        {
                            JsonUtility.FromJsonOverwrite(json, dataObject);
                            Debug.Log($"Loaded {id}: {json}");
                        }
                    }

                    Debug.Log($"Game state loaded from slot: {slot}");
                }
                else
                {
                    Debug.LogWarning($"Unsupported save file version: {saveFile.version}");
                }
            }
            else
            {
                Debug.Log($"No save file found for slot: {slot}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load: {e.Message}");
        }
    }

    private string GetSavePath(string slot)
    {
        return Path.Combine(Application.persistentDataPath, $"gameSave_{slot}.json");
    }

    [System.Serializable]
    private class SaveFile
    {
        public int version = 1;
        public Dictionary<string, string> data = new Dictionary<string, string>();
    }
}