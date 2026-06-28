using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlivaCYD1
{
    public class GunView : MonoBehaviour
    {
        [SerializeField] private Transform gunTransform;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Bullet bulletPrefab;

        private Vector2 shootDirection = Vector2.right;

        private void Awake()
        {
            if (gunTransform == null)
                gunTransform = transform;
        }

        public void LookAt(Vector2 targetPosition)
        {
            Vector2 gunPosition = gunTransform.position;
            shootDirection = targetPosition - gunPosition;

            var angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        public void Shoot(float bulletSpeed, int damage, BulletOwner owner)
        {
            Bullet bullet = Instantiate(
                bulletPrefab,
                shootPoint.position,
                shootPoint.rotation
            );

            bullet.Init(shootDirection.normalized, bulletSpeed, damage, owner);
        }
    }
}
