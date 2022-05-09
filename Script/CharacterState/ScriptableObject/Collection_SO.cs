using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Collections/Player")]
    public class Collection_SO : ScriptableObject
    {
        public int healthCollections;
        public int jumpTimes = 1;
        public bool climb;
        public bool glide;

    }
}