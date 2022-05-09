using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Enemy;

public class HealthBarUI : MonoBehaviour
{
    public GameObject healthUIPrefab;
    public GameObject healthBarCanvas;
    public Transform barPoint;
    public float visibleTime;

    Image currentHealthSlider;
    EnemyState enemyState;
    GameObject healthBar;
    // bool visible;
    float currentVisibleTime;

    void Awake() {
        enemyState = GetComponent<EnemyState>();
        enemyState.UpdateHealthBar += UpdateHealth;
        healthBarCanvas = GameObject.Find("HealthBar Canvas");
    }

    void OnEnable() {
        healthBar = Instantiate(healthUIPrefab, healthBarCanvas.transform);
        currentHealthSlider = healthBar.transform.GetChild(0).GetComponent<Image>();
        healthBar.SetActive(false);
    }

    void Update() {
        currentVisibleTime -= Time.deltaTime;
        if(currentVisibleTime < 0){
            healthBar.SetActive(false);
        }
    }

    void LateUpdate() {
        if(healthBar != null){
            healthBar.transform.position = barPoint.transform.position;
        }
    }

    void UpdateHealth(int currentHealth, int maxHealth) {
        healthBar.SetActive(true);
        currentVisibleTime = visibleTime;
        if(currentHealth <= 0){
            DestroyUI();
        }
        float sliderPercent = (float)currentHealth / maxHealth;
        currentHealthSlider.fillAmount = sliderPercent;
    }
    
    void DestroyUI() {
        Destroy(healthBar);
    }
}
