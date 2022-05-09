using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour
    {
        public Transform playerCheck;
        Rigidbody rb;
        RaycastHit hitInfo;
        int layerMask = 1 << 2;
        GameObject target;
        public EnemyData_SO enemyData;
        Animator animator;
        Vector3 diff;
        Vector3 turned = Vector3.back;
        bool foundPlayer;
        float turnedSpeed = 8.0f;
        public float speed = 10.0f;
        public float disToWall = 0.5f;
        [Range(0f, 1.5f)]
        public float minRange = 1.0f;
        [Range(1.5f, 3f)]
        public float maxRange = 2.0f;
        float swc;
        float distance;
        float duration;
        delegate void MoveDelegate();
        MoveDelegate moveDelegate;

        int animator_RunBoolID;
        int animator_AttackTriggerID;
        int animator_DeathTriggerID;
        int animator_GetDamageTriggerID;
        public int FoundRange
        {
            get
            {
                if (enemyData != null)
                    return enemyData.foundRange;
                else
                    return 0;
            }
        }

        public int AttackRange
        {
            get
            {
                if (enemyData != null)
                    return enemyData.attackRange;
                else
                    return 0;
            }
        }

        public int AttackValue
        {
            get
            {
                if (enemyData != null)
                    return enemyData.attackValue;
                else
                    return 0;
            }
        }

        public virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            if(animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
        }

        public virtual void Start() 
        {
            animator_RunBoolID = Animator.StringToHash("Run");
            animator_AttackTriggerID = Animator.StringToHash("Attack");
            animator_DeathTriggerID = Animator.StringToHash("Death");
            animator_GetDamageTriggerID = Animator.StringToHash("GetDamage");
            layerMask = ~layerMask;
            swc = minRange * 0.7f + maxRange * 0.3f;
            distance = AttackRange * AttackRange;
        }

        public virtual void Update() {
            turnedSmoothly(turned, Vector3.up);
            duration -= Time.deltaTime;
            if(moveDelegate != null){
                moveDelegate();
            }
        }

        public virtual void FixedUpdate() {
            if(!foundPlayer){
                checkPlayer();
                if(target != null && target.CompareTag("Player")){
                    foundPlayer = true;
                    moveDelegate -= Move;
                    moveDelegate -= Stay;
                }
                else if(moveDelegate == null){
                    duration = Random.Range(minRange, maxRange);
                    if(duration > swc){
                        if(Random.Range(0f, 1f) < 0.5f)
                        {
                            if(turned != Vector3.left)
                                rb.velocity = Vector3.zero;
                            turned = Vector3.left;
                        }
                        else
                        {
                            if(turned != Vector3.right)
                                rb.velocity = Vector3.zero;
                            turned = Vector3.right;
                        }
                        animator.SetBool(animator_RunBoolID, true);
                        moveDelegate += Move;
                    }
                    else{
                        turned = Vector3.back;
                        rb.velocity = Vector3.zero;
                        animator.SetBool(animator_RunBoolID, false);
                        moveDelegate += Stay;
                    }
                }
            }
            else if(moveDelegate == null && foundPlayer){
                moveDelegate += MoveToTarget;
            }
        }

        public void Move() {
            rb.AddForce(turned * Time.deltaTime * speed);
            if(hitInfo.distance > 0.05f && hitInfo.distance < disToWall){
                rb.velocity = Vector3.zero;
                moveDelegate -= Move;
                animator.SetBool(animator_RunBoolID, false);
                moveDelegate += Stay;
            }
            if(duration < 0){
                moveDelegate -= Move;
            }
        }
        
        public void Stay() {
            if(duration < 0){
                moveDelegate -= Stay;
            }
        }

        public void MoveToTarget() {
            diff = target.transform.position - transform.position;
            if(diff.x < 0){
                turned = Vector3.left;
            }
            else if(diff.x > 0){
                turned = Vector3.right;
            }
            if( diff.sqrMagnitude > distance)
            {
                animator.SetBool(animator_RunBoolID, true);
                rb.AddForce(transform.forward * Time.deltaTime * speed);
            }
            else
            {
                animator.SetBool(animator_RunBoolID, false);
                animator.SetTrigger(animator_AttackTriggerID);
            }
        }

        public void turnedSmoothly(Vector3 direction, Vector3 axis)
        {
            //以y为轴，转向
            Quaternion quaDir = Quaternion.LookRotation(direction, axis);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.deltaTime * turnedSpeed);
        }

        public void checkPlayer() {
            if(Physics.Raycast(playerCheck.position, turned, out hitInfo, FoundRange, layerMask))
            {
                target = hitInfo.collider.gameObject;
            }
        }
    }
}