using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    private const string _saveFileName = "Save.json";

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Instance of SaveManager already exist");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, _saveFileName);
    }

    public void Save(int levelReached)
    {
        try
        {
            string jsonString = JsonUtility.ToJson(new SaveData() { LevelReached = levelReached }, true);
            string path = GetSavePath();

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            File.WriteAllText(path, jsonString, System.Text.Encoding.UTF8);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error while saving data" + System.Environment.NewLine + ex.Message);
        }
    }


    public SaveData GetSaveData()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string fileTxt = File.ReadAllText(path);

            try
            {
                SaveData save = JsonUtility.FromJson<SaveData>(fileTxt);
                if(save == null)
                {
                    Save(0);
                    return new SaveData();
                }
                return save;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error while deserializing SaveData" + System.Environment.NewLine + ex.Message);
                return new SaveData();
            }
        }
        else
        {
            Save(0);
            return new SaveData();
        }
    }
}
