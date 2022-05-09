using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SavePlayerData();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            LoadPlayerData();
        }
    }

    private void SavePlayerData()
    {
        Debug.Log("Save");
        Save("PlayerData", GameManager.Instance.playerState.playerData);
    }

    private void LoadPlayerData()
    {
        Debug.Log("Load");
        Save("PlayerData", GameManager.Instance.playerState.playerData);
    }

    public void Save (string key, Object data){
        var jsonData = JsonUtility.ToJson(data, true);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }

    public void Load (string key, Object data){
        if(PlayerPrefs.HasKey(key)){
            JsonUtility.FromJsonOverwrite(key, data);
        }
    }
}
