using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Enemy{
    public class EnemyState : MonoBehaviour
    {
        public EnemyData_SO enemyData;
        public Animator animator;
        int animator_DeathParamID;
        public event Action<int, int> UpdateHealthBar;

        //get => enemyData.maxHealth : default 0
        public int MaxHealth
        {
            get
            {
                return enemyData.maxHealth;
            }
        }

        public int currentHealth;
        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;
            }
        }
        
        public virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void Start()
        {
            if(enemyData == null)
            {
                Debug.LogError("enemyData not found!");
            }
            animator_DeathParamID = Animator.StringToHash("Death");
            restoreFullHealth();
        }

        public void restoreFullHealth()
        {
            CurrentHealth = MaxHealth;
            // UpdateHealthBar?.Invoke(CurrentHealth, MaxHealth);
        }

        public void TakeDamaged(int damage)
        {
            CurrentHealth -= damage;
            UpdateHealthBar?.Invoke(CurrentHealth, MaxHealth);
            beKilled();
        }

        public void beKilled()
        {
            if (CurrentHealth <= 0)
            {
                kill();
            }
        }

        public void kill()
        {
            if (animator != null)
            {
                animator.SetTrigger(animator_DeathParamID);
            }
            else
                DestroySelf();
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

    }
}