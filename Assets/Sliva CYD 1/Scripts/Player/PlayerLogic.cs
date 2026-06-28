using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlivaCYD1
{
    public class PlayerLogic : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Character character = new Character();
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckRadius = 0.2f;
        
        public Character Character => character;
        public float YVelocity => _rigidbody.velocity.y;

        private void Awake()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Move(float direction, float speed)
        {
            _rigidbody.velocity = new Vector2(direction * speed, _rigidbody.velocity.y);
        }

        public void Jump(float jumpHeight)
        {
            if (IsGrounded() == false)
                return;

            var jumpForce = Mathf.Sqrt(jumpHeight * -2f * Physics2D.gravity.y);

            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
        }

        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }
        
        public void TakeDamage(int damage)
        {
            character.TakeDamage(damage);
            Debug.Log("hp " + character.Health);
        }
    }
}
