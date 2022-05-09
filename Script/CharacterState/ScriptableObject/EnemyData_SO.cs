using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Character Stats/Enemy Data")]
    public class EnemyData_SO : ScriptableObject
    {
        public int maxHealth;
        public int foundRange;
        public int attackRange;
        public int attackValue;
        public int speed = 1;
    }
}
