using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlivaCYD1
{
    public class EnemyShoot : MonoBehaviour
    {
        [SerializeField] private GunView gunView;
        [SerializeField] private float shootDelay = 1f;

        private Coroutine shootCoroutine;

        private void OnEnable()
        {
            shootCoroutine = StartCoroutine(ShootEverySecond());
        }

        private void OnDisable()
        {
            if (shootCoroutine != null)
                StopCoroutine(shootCoroutine);
        }

        private IEnumerator ShootEverySecond()
        {
            while (true)
            {
                yield return new WaitForSeconds(shootDelay);

                gunView.Shoot(-10f,10, BulletOwner.Enemy);
            }
        }
    }
}
