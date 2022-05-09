using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Character Stats/Player Data")]
    public class PlayerData_SO : ScriptableObject
    {
        [Header("Base State")]
        public int maxHealth;
        public int currentHealth;
    }
}