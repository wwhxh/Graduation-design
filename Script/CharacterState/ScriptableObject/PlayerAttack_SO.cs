using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Character Stats/Player Attack Data")]
    public class PlayerAttack_SO : ScriptableObject
    {
        public int attackValue = 3;
        public int attackRange = 5;
    }
}