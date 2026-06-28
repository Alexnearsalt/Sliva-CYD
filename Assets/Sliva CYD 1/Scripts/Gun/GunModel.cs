using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class GunModel 
    {
        public int Damage = 10;
        public float BulletSpeed = 10f;
        public float ShootCooldown = 0.3f;

        private float lastShootTime = -100f;

        public bool CanShoot(float currentTime)
        {
            return currentTime >= lastShootTime + ShootCooldown;
        }

        public void SaveShootTime(float currentTime)
        {
            lastShootTime = currentTime;
        }
    }
}
