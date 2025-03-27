using System;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveHandler : MonoBehaviour
{
    public static SaveHandler instance;

    // ---

    [SerializeField] private string FirstLevelScene;

    // ---



    private float counter;
    private const float saveEach = 50;
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= saveEach)
        {
            AutoSaveGame();
        }
    }



    // ---

    private const string SaveFileName = "data.sav";

    [Serializable]
    public struct SaveData
    {
        public SkillsHandler.HavingSkills haveSkills;
        public CountersHandler.Counters counters;
    }


    private void Awake()
    {
        #region Instance
        if (instance != null)
        {
            Debug.Log("An instance of 'SaveHandler' already exists.");
            
            Destroy(gameObject);
            return;
        }

        instance = this;
        #endregion

        // Dontdestroyonload
        DontDestroyOnLoad(gameObject);
    }


    #region Functions
    public void AutoSaveGame()
    {
        counter = 0;

        // ---
        
        SaveData dataToSave = new SaveData {
            haveSkills = SkillsHandler.instance.haveTheSkill,
            counters = CountersHandler.instance.counters,

            // Add everything here
        };

        SaveGame(dataToSave);
    }


    private void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        string encodedJson = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
        File.WriteAllText(GetSaveFilePath(), encodedJson);
    }

    public SaveData GetSaveData()
    {
        string path = GetSaveFilePath();
        if (File.Exists(path))
        {
            string encodedJson = File.ReadAllText(path);
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(encodedJson));
            return JsonUtility.FromJson<SaveData>(json);
        }
        // Return default data if no save exists
        return new SaveData();
    }

    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }
    #endregion

}
