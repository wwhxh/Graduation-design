using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        CharacterController characterController;
        PlayerState playerState;
        Animator animator;

        Vector3 displacement;
        float horizontal;
        public float speed = 15f;
        public float turnedSpeed = 15f;
        Vector3 towards;

        public float jumpHeight = 1.5f;
        int jumpTimes;
        bool firstJump;

        float vertical;
        float climbSpeed = 5f;
        bool attachWall;
        bool climbing;
        public Transform wallCheckObject;
        public LayerMask wallLayer;

        public float max_FallSpeed = -50f;
        public float glideSpeed = -5f;
        public float gForce;
        public float jumpGravity = -30f;
        //change in Start()
        public float jumpHoldForce;
        //change in Start()
        float fallGravity;
        bool notFloating;
        public Transform groundCheckObject;
        float checkRadius = 0.2f;
        public LayerMask groundLayer;

        int animator_bool_RunParamID;
        int animator_trigger_JumpParamID;
        int animator_bool_LandParamID;
        int animator_float_LandActionParamID;
        int animator_bool_AttachWallParamID;
        int animator_bool_ClimbUpParamID;

        void Awake()
        {
            characterController = GetComponent<CharacterController>();
            playerState = GetComponent<PlayerState>();
            animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            jumpHoldForce = jumpGravity * -0.5f;
            fallGravity = jumpGravity * 1.5f;
            jumpTimes = playerState.JumpTimes;
            towards = transform.forward;

            animator_bool_RunParamID = Animator.StringToHash("Run");
            animator_trigger_JumpParamID = Animator.StringToHash("Jump");
            animator_bool_LandParamID = Animator.StringToHash("Land");
            animator_float_LandActionParamID = Animator.StringToHash("LandAction");
            animator_bool_AttachWallParamID = Animator.StringToHash("AttachWall");
            animator_bool_ClimbUpParamID = Animator.StringToHash("ClimbUp");
        }

        // Update is called once per frame
        void Update()
        {
            notFloating = Physics.CheckSphere(groundCheckObject.position, checkRadius, groundLayer);
            attachWall = Physics.CheckSphere(wallCheckObject.position, checkRadius, wallLayer);
            animator.SetBool(animator_bool_LandParamID, notFloating);
            animator.SetBool(animator_bool_AttachWallParamID, attachWall);

            Move();
            Jump();
            Fall();
            Climb();
            characterController.Move(displacement);
            turnedSmoothly(towards, Vector3.up);
        }

        private void Move()
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            if (climbing)
                return;

            if (horizontal > 0)
                towards.z = 1;
            else if (horizontal < 0)
                towards.z = -1;

            if (horizontal != 0)
                animator.SetBool(animator_bool_RunParamID, true);
            else
                animator.SetBool(animator_bool_RunParamID, false);

            displacement.x = horizontal * speed * Time.deltaTime;
        }

        public void turnedSmoothly(Vector3 direction, Vector3 axis)
        {
            //以y为轴，转向
            Quaternion quaDir = Quaternion.LookRotation(direction, axis);
            transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.deltaTime * turnedSpeed);
        }

        private void Climb()
        {
            vertical = Input.GetAxisRaw("Vertical");
            if (attachWall)
            {
                if (vertical > 0)
                {
                    climbing = true;
                    StartAnimator();
                    animator.SetBool(animator_bool_ClimbUpParamID, true);
                    displacement.y = climbSpeed * Time.deltaTime;
                }
                else if (vertical == 0 && !notFloating)
                {
                    StopAnimator();
                    climbing = true;
                    displacement = Vector3.zero;
                }
                else if (vertical < 0)
                {
                    StartAnimator();
                    animator.SetBool(animator_bool_ClimbUpParamID, false);
                    displacement.y = -climbSpeed * Time.deltaTime;
                    if (notFloating && climbing)
                    {
                        //TODO:animator
                        climbing = false;
                    }
                }
            }
            else
                climbing = false;
        }

        private void Jump()
        {
            if (!climbing)
            {
                if (Input.GetKeyDown(KeyCode.Space) && jumpTimes > 0)
                {
                    firstJump = false;
                    jumpTimes--;
                    gForce = (float)Math.Sqrt(jumpHeight * -2 * jumpGravity);
                    animator.SetTrigger(animator_trigger_JumpParamID);
                }
                else if (!notFloating && firstJump)
                {
                    firstJump = false;
                    jumpTimes--;
                }
                else if (notFloating)
                {
                    firstJump = true;
                    jumpTimes = playerState.JumpTimes;
                }

                if (Input.GetKey(KeyCode.Space) && !notFloating && gForce > 0)
                {
                    gForce += jumpHoldForce * Time.deltaTime;
                }
            }
            else if (climbing)
            {
                jumpTimes = playerState.JumpTimes;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (vertical >= 0)
                    {
                        gForce = (float)Math.Sqrt(jumpHeight * -2 * jumpGravity);
                    }

                    // turnedSmoothly(transform.forward * -1, Vector3.up);
                    animator.SetTrigger(animator_trigger_JumpParamID);
                    if (horizontal == 0)
                        towards.z *= -1;
                }
            }
        }

        private void Fall()
        {
            if (gForce > 0)
            {
                gForce += jumpGravity * Time.deltaTime;
            }
            else if (notFloating || climbing)
            {
                if (gForce < max_FallSpeed * 0.6f)
                {
                    animator.SetFloat(animator_float_LandActionParamID, 1);
                }
                else
                    animator.SetFloat(animator_float_LandActionParamID, 0);
                gForce = -1f;
            }
            else if (!notFloating)
            {
                if (gForce > max_FallSpeed)
                    gForce += fallGravity * Time.deltaTime;
                else if (playerState.playerCollections.glide && Input.GetKey(KeyCode.LeftShift))
                    Glide();
            }

            displacement.y = gForce * Time.deltaTime;
        }

        private void Glide()
        {
            gForce = glideSpeed;
        }

        private void StopAnimator()
        {
            animator.speed = 0;
        }

        private void StartAnimator()
        {
            animator.speed = 1;
        }
    }
}