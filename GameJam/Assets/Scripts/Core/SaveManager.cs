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


    public SaveData GetSaveData()
    {
        string path = GetSavePath();
        if (File.Exists(path))
        {
            string fileTxt = File.ReadAllText(path);

            try
            {
                SaveData save = JsonUtility.FromJson<SaveData>(fileTxt);
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
            //TODO créer une sauvegarde
            return new SaveData();
        }
    }
}
