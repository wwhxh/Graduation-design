using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMath;
using Enemy;

namespace Player{
    public class BulletController : MonoBehaviour
    {
        Vector3 original;
        Vector3 p1;
        GameObject target;
        public Vector3 targetPosition;
        int damage;
        float range;
        public float flySpeed;
        float t = 0;

        void Start()
        {
            original = transform.position;
            p1 = original + Quaternion.Euler(Random.Range(0, 180), 0, 0) * Vector3.back * 3;
            if (damage == 0)
            {
                Debug.LogError("Damage cannot be Zero!");
            }
            if (range == 0)
            {
                Debug.LogError("Range cannot be Zero!");
            }
        }

        public void set(int damage, float range)
        {
            this.damage = damage;
            this.range = range;
        }

        public void setTarget(GameObject target)
        {
            this.target = target;
            if(target != null)
            {
                targetPosition = target.transform.position;
            }else{
                //TODO:子弹需要人物往前射击
                targetPosition = transform.position + transform.right * range;
            }
        }

        void Update()
        {
            TrackingTarget();
        }

        void FixedUpdate()
        {
            if(t <= 1)
            {
                t += Time.fixedDeltaTime * flySpeed;
            }
        }

        public void TrackingTarget()
        {
            if(t > 1){
                Destroy(gameObject);
                // gameObject.SetActive(false);
            }else
            {
                //problem
                transform.position = BezierMath.Bezier_2(original, p1, targetPosition, t);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == target)
            {
                target.GetComponent<EnemyState>().TakeDamaged(damage);
                Destroy(gameObject);
            }
        }

    }
}