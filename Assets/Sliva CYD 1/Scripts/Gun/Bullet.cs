using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private int damage;
        private BulletOwner owner;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Init(Vector2 direction, float speed, int damage, BulletOwner owner)
        {
            this.damage = damage;
            this.owner = owner;
            _rigidbody.velocity = direction * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (owner == BulletOwner.Player)
            {
                Enemy enemy = other.GetComponentInParent<Enemy>();

                if (enemy != null)
                    enemy.TakeDamage(damage);
            }

            if (owner == BulletOwner.Enemy)
            {
                PlayerLogic player = other.GetComponentInParent<PlayerLogic>();

                if (player != null)
                    player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
