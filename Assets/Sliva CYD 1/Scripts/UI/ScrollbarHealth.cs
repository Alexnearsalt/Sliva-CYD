using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SlivaCYD1
{
    public class ScrollbarHealth : MonoBehaviour
    {
        [SerializeField] private PlayerLogic playerLogic;
        [SerializeField] private Scrollbar healthScrollBar;

        private Coroutine coroutine;

        private void Start()
        {
            playerLogic.Character.HealthChanged += ChangeHealthBar;

            ChangeHealthBar(
                playerLogic.Character.Health,
                playerLogic.Character.MaxHealth
            );
        }

        private void OnDestroy()
        {
            playerLogic.Character.HealthChanged -= ChangeHealthBar;
        }

        private void ChangeHealthBar(int health, int maxHealth)
        {
            var targetSize = (float)health / maxHealth;

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(ChangeSize(targetSize));
        }

        private IEnumerator ChangeSize(float targetSize)
        {
            while (Mathf.Abs(healthScrollBar.size - targetSize) > 0.01f)
            {
                healthScrollBar.size = Mathf.Lerp(
                    healthScrollBar.size,
                    targetSize,
                    Time.deltaTime * 5f
                );

                yield return null;
            }

            healthScrollBar.size = targetSize;
        }
    }
}
