using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player{
    public class DemonController : MonoBehaviour,
                    CharacterActionDispatcher.IMoveReceiver,
                    CharacterActionDispatcher.IAttackReceiver
    {
        public Transform player;
        public float range;
        public PlayerAttack_SO attackData;

        public GameObject bullet;
        public GameObject attackTarget;
        public float coldDown = 0.5f;

        public int AttackValue{
            get
            {
                return attackData.attackValue;
            }
            set
            {
                attackData.attackValue = value;
            }
        }

        public int AttackRange{
            get
            {
                return attackData.attackRange;
            }
            set
            {
                attackData.attackRange = value;
            }
        }

        void Start()
        {
            if(attackData == null)
            {
                Debug.LogError("Player Attack ScriptObject is null!");
            }
            if(bullet == null)
            {
                Debug.LogError("The Bullet was not be reference");
            }
        }

        // Update is called once per frame
        void Update()
        {
            Attack();
        }

        public void Move()
        {

        }

        public void Attack()
        {
            coldDown -= Time.deltaTime;
            if (coldDown < 0 && Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject blt = Instantiate(bullet, transform.position, Quaternion.identity);
                blt.GetComponent<BulletController>().set(AttackValue, AttackRange);
                blt.GetComponent<BulletController>().setTarget(attackTarget);
                coldDown = 0.5f;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && attackTarget == null)
            {
                attackTarget = other.gameObject;
            }
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && attackTarget == null)
            {
                attackTarget = other.gameObject;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == attackTarget)
                attackTarget = null;
        }
    }
}