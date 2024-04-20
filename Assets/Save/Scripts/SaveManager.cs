using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData()
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
        }
    }

    public void ResetSaveData()
    {
        string filePath = Application.persistentDataPath + "/playerData.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Save data reset successfully.");
        }
        else
        {
            Debug.LogWarning("No save data found to reset.");
        }
    }
}