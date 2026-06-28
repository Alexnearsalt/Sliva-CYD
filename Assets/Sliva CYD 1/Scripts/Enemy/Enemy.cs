using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int health = 50;

        public void TakeDamage(int damage)
        {
            health -= damage;

            Debug.Log("Enemy hp " + health);

            if (health <= 0)
                Destroy(gameObject);
        }
    }
}
