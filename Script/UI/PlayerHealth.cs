using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerHealth : MonoBehaviour
{
    List<GameObject> healthList = new List<GameObject>();
    public int maxHealthCapacity = 9;
    public GameObject player;
    PlayerState playerState;
    void Awake() {
        for(int i = 0; i < maxHealthCapacity; i++){
            healthList.Add(transform.GetChild(i).gameObject);
        }
        playerState = player.GetComponent<PlayerState>();
        playerState.UpdateHealthBar = UpdatePlayerHealthBar;
    }

    void Start() {
        if(playerState == null){
            UpdatePlayerHealthBar(3);
        }
    }

    void UpdatePlayerHealthBar(int currentHealth){
        // Debug.Log(currentHealth);
        if(currentHealth > maxHealthCapacity)
        {
            Debug.LogError("CurrentHealth was out of boundry");
            return;
        }
        if(currentHealth <= 0){
            // Debug.Log("Dead");
            foreach(GameObject gameObject in healthList){
                if(gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            return;
        }
        if(!healthList[currentHealth - 1].activeSelf){
            // Debug.Log("Treat");
            for(int i = 0; i < currentHealth; i++){
                if(!healthList[i].activeSelf)
                    healthList[i].SetActive(true);
            }
        }
        else
        {
            // Debug.Log("Injured");
            for(int i = maxHealthCapacity - 1; i > currentHealth - 1; i--){
                if(healthList[i].activeSelf)
                    healthList[i].SetActive(false);
            }
        }
    }
}
