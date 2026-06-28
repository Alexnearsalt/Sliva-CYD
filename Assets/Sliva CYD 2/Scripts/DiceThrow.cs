using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SlivaCYD2
{
    public class DiceThrow : MonoBehaviour
    {
        [SerializeField] private GameObject dice;
        private int dicesCount = 2;
        [SerializeField] private MaterialContrioller materialContrioller;
        private MeshRenderer _meshRenderer;
        private List<GameObject> dices = new List<GameObject>();
        [SerializeField] private SaveManager saveManager;
        
        [Header("Jump Settings")]
        [SerializeField] private float minForce = 5f;
        [SerializeField] private float maxForce = 10f;
        [SerializeField] private float minTorque = 5f;
        [SerializeField] private float maxTorque = 10f;
        
        public void ThrowDice()
        {
            var objectsToRemove = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject obj in objectsToRemove)
            {
                Destroy(obj);
            }
            for (int i = 0; i < dicesCount; i++)
            {
                var spawnOffset = new Vector3(i * 1.5f, 0, 0);
                var newDice = Instantiate(dice, transform.position + spawnOffset, Quaternion.identity);
                dices.Add(newDice);
                saveManager.RegisterDice(newDice);
                newDice.GetComponent<MeshRenderer>().material = materialContrioller.Material;
                var rigidbody = newDice.GetComponent<Rigidbody>();
                ApplyRandomForce(rigidbody);
                ApplyRandomTorque(rigidbody);
            }
        }
        
        private void ApplyRandomForce(Rigidbody _rigidbody)
        {
            var randomDirection = new Vector3(
                Random.Range(-1f, 1f), 
                Random.Range(0.8f, 1.2f), 
                Random.Range(-1f, 1f)
            ).normalized;
        
            var randomForce = Random.Range(minForce, maxForce);
            var force = randomDirection * randomForce;
        
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }

        private void ApplyRandomTorque(Rigidbody _rigidbody)
        {
            var randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized * Random.Range(minTorque, maxTorque);
        
            _rigidbody.AddTorque(randomTorque, ForceMode.Impulse);
        }
    }
}
