using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class Character
    {
        public event Action<int, int> HealthChanged;

        private int maxHealth = 100;
        private int health = 100;

        private float speed = 5f;
        private float jumpHeight = 3f;

        public int Health => health;
        public int MaxHealth => maxHealth;
        public float Speed => speed;
        public float JumpHeight => jumpHeight;
        public bool IsDead { get; private set; }
        
        public void TakeDamage(int damage)
        {
            if (IsDead)
                return;

            health -= damage;

            if (health <= 0)
            {
                health = 0;
                IsDead = true;
            }

            HealthChanged?.Invoke(health, maxHealth);
        }
    }
}
