using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace movement
{
    public class MovementScript2D : MonoBehaviour
    {
        public SenseWithRays raycaster;
        public float speed;
        public float gravity;
        // public float jumpTime;
        // public float jumpSpeed;
        public AnimationCurve gravityCurve;
        public AnimationCurve jumpCurve;
     
        //public float slopeMaxThreshold = 50f;

        //public float jumpApex;
        //FULL RECAP OF JUMP AT 1:38 


        bool weAreJumping;
        float timeSinceJumped;
        bool isGrounded;
        float timeSinceFalling;

        public bool isWin = false;

        private void Start()
        {
            // weAreJumping = false;
        }

        void Update()
        {
            UpdateHorizontalMovement();
            UpdateVerticalMovement();
        }


        void UpdateHorizontalMovement() // TAKES FROM THE UPDATE AND TAKES THE INPUT TO GIVE A NEW VECTOR TO BE TRANSFORMED INTO HORIZONTAL MOVEMENT 
        {
            float currentMovement = 0f;
            if (Input.GetKey(KeyCode.RightArrow)) currentMovement++;
            if (Input.GetKey(KeyCode.LeftArrow)) currentMovement--;
            HorizontalMove(speed * currentMovement * Time.deltaTime);
        }

        float previousFrameHeight;
        void UpdateVerticalMovement()
        {
            timeSinceFalling += Time.deltaTime;

            float currentVerticalMovement = 0f;
            if (weAreJumping) //going up
            {
                float currentFrameHeight = jumpCurve.Evaluate(timeSinceJumped);
                currentVerticalMovement = currentFrameHeight - previousFrameHeight;

                //save for next frame
                previousFrameHeight = currentFrameHeight;

            }
            else // going down
            {
                currentVerticalMovement = Time.deltaTime * gravity * -1.0f * gravityCurve.Evaluate(timeSinceFalling);
            }

            VerticalMove(currentVerticalMovement);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                JumpStart();

            }

            JumpUpdate();
        }// TAKES FROM THE UPDATE AND TAKES THE INPUT TO GIVE A NEW VECTOR TO BE TRANSFORMED INTO VERTICAL MOVEMENT 

        void JumpStart()
        {
            if (!isGrounded) return;

            isGrounded = false;
            weAreJumping = true;
            timeSinceJumped = 0f;
            previousFrameHeight = 0f;

        } // TAKES FROM VERTICAL MOVEMENT. STARTS THE JUMP AND CHANGES SOME BOOLEAN VALUES

        void JumpUpdate() // TAKES FROM VERTICAL MOVEMENT AND ENSURES THE JUMP IS NOT TOO LONG
        {
            if (!weAreJumping) return;


            timeSinceJumped += Time.deltaTime;

            int lastKeyIndex = jumpCurve.keys.Length - 1;
            if (timeSinceJumped > jumpCurve.keys[lastKeyIndex].time)
            {
                weAreJumping = false;
                timeSinceFalling = 0f;
            }

        }

        public void Move(Vector2 totalMovement)
        {
            HorizontalMove(totalMovement.x);
            VerticalMove(totalMovement.y);
        }

        public void HorizontalMove(float distance)
        {
            if (distance == 0)
            {
                return;
            }

            MovementDirection dir = MovementDirection.Right;
            if (distance < 0) dir = MovementDirection.Left;

            bool weAreColliding = raycaster.ThrowRays(dir, distance);
            if (!weAreColliding)
            {
                transform.Translate(Vector3.right * distance);
            }
        } // TAKES FROM UPDATEHORIZONTALMOVEMENT AND TRANSFORMS THE MOVEMENT
        public void VerticalMove(float distance)
        {
            if (distance == 0)
            {
                return;
            }

            MovementDirection dir = MovementDirection.Up;
            if (distance < 0) dir = MovementDirection.Down;

            bool weAreColliding = raycaster.ThrowRays(dir, distance);
            if (!weAreColliding)
            {
                transform.Translate(Vector3.up * distance);
                if (dir == MovementDirection.Down)
                {
                    isGrounded = false;
                }
            }
            else if (dir == MovementDirection.Down)
            {
                weAreJumping = false;
                isGrounded = true;
                timeSinceFalling = 0;
            }
        } // TAKES FROM UYPDATEVERTICALMOVEMENT AND TRANSFORMS IT INTO UPWARDS/JUMP MOVEMENT

    }
}