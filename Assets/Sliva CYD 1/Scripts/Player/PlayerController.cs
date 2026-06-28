using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace SlivaCYD1
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Character character = new Character();
        [SerializeField] private PlayerLogic logic;
        [SerializeField] private Animator animator;

        private float moveDirection;
        private float lastDirection = 1f;


        private void Awake()
        {
            if (logic == null)
                logic = GetComponent<PlayerLogic>();
        }

        private void FixedUpdate()
        {
            logic.Move(moveDirection, character.Speed);
            
            if (moveDirection < 0)
                lastDirection = -1;

            if (moveDirection > 0)
                lastDirection = 1;
            
            animator.SetFloat("MoveX", lastDirection);
            animator.SetFloat("MoveAmount", Mathf.Abs(moveDirection));
            animator.SetFloat("YVelocity", logic.YVelocity);
            animator.SetBool("IsGrounded", logic.IsGrounded());
        }

        public void OnMove(InputValue value)
        {
            moveDirection = value.Get<float>();
        }

        public void OnJump(InputValue value)
        {
            if (value.isPressed)
                logic.Jump(character.JumpHeight);
        }
    }
}
