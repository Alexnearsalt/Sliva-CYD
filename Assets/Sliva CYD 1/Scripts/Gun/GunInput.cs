using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SlivaCYD1
{
    public class GunInput : MonoBehaviour
    {
        [SerializeField] private GunModel model = new GunModel();
        [SerializeField] private GunView view;
        [SerializeField] private BulletOwner bulletOwner;

        private Camera mainCamera;
        private Vector2 mousePosition;

        private void Awake()
        {
            mainCamera = Camera.main;

            if (view == null)
                view = GetComponent<GunView>();
        }

        private void Update()
        {
            Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
            view.LookAt(worldMousePosition);
        }

        public void OnAim(InputValue value)
        {
            mousePosition = value.Get<Vector2>();
        }

        public void OnShoot(InputValue value)
        {
            if (value.isPressed == false)
                return;

            if (model.CanShoot(Time.time) == false)
                return;

            view.Shoot(model.BulletSpeed, model.Damage, bulletOwner);
            model.SaveShootTime(Time.time);
        }
    }
}
