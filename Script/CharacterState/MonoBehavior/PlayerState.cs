using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        public PlayerData_SO playerData;
        public Collection_SO playerCollections;
        public delegate void UpdateDelegate(int currentHealth);
        public UpdateDelegate UpdateHealthBar;

        public enum towards
        {
            right = 1, left = -1
        }

        //get => playerData.maxHealth : default 0
        int MaxHealth
        {
            get
            {
                if (playerData != null)
                    return playerData.maxHealth;
                return 0;
            }
        }

        //get => playerData.healthCollection : default 0
        //(private)set => playerData.healthCollection++
        public int HealthCollection
        {
            get
            {
                if (playerData != null)
                    return playerCollections.healthCollections;
                return 0;
            }
            private set
            {
                ++playerCollections.healthCollections;
            }
        }

        //get => playerData.currentHealth : default 0
        //(private)set = playerData.healthCollection = value;
        public int CurrentHealth
        {
            get
            {
                if (playerData != null)
                    return playerData.currentHealth;
                return 0;
            }
            private set
            {
                playerData.currentHealth = value;
            }
        }

        //get => playerData.jumpTimes : default 1
        //(private)set = playerData.jumpTimes++
        public int JumpTimes
        {
            get
            {
                if (playerData != null)
                    return playerCollections.jumpTimes;
                return 1;
            }
            private set
            {
                ++playerCollections.jumpTimes;
            }
        }

        //get => MaxHealth + HealthCollection / 2;
        public int TotalHealth
        {
            get
            {
                return MaxHealth + HealthCollection;
            }
        }

        // void Awake()
        // {
        //     GameManager.Instance.RigisterPlayer(this);
        // }
        void Start()
        {
            restoreFullHealth();
        }

        public bool getTreat(int treatment)
        {
            //TODO:Update UI
            if (CurrentHealth > 0)
            {
                CurrentHealth += treatment;
                if (CurrentHealth > TotalHealth)
                {
                    restoreFullHealth();
                    return true;
                }
                if(UpdateHealthBar != null)
                {
                    UpdateHealthBar(CurrentHealth);
                }
                return true;
            }
            return false;
        }

        public void restoreFullHealth()
        {
            CurrentHealth = TotalHealth;
            if(UpdateHealthBar != null)
            {
                UpdateHealthBar(CurrentHealth);
            }
        }

        public void addHealthCollection()
        {
            ++HealthCollection;
            Debug.Log("You have promote the max health, now your max health is : " + CurrentHealth);
            restoreFullHealth();
        }

        public void addJumpTimes()
        {
            Debug.Log("You can jump twice now");
            ++JumpTimes;
        }

        public void getClimbSkill()
        {
            playerCollections.climb = true;
        }

        public void getGlideSkill()
        {
            playerCollections.glide = true;
        }
    }
}