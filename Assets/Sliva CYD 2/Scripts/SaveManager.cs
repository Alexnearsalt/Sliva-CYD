using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace SlivaCYD2
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private GameObject dicePrefab;
        [SerializeField] private MaterialContrioller _materialContrioller;

        private readonly List<GameObject> dices = new List<GameObject>();

        private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

        public void RegisterDice(GameObject dice)
        {
            if (dice == null)
                return;
            if (!dices.Contains(dice))
                dices.Add(dice);
        }

        public void SaveDices()
        {
            var saveData = new DicesData();

            foreach (var dice in dices)
            {
                if (dice == null)
                    continue;
                var meshRend = dice.GetComponent<MeshRenderer>();
                var diceData = new DiceData
                {
                    position = new Vector3SaveData(dice.transform.position),
                    rotation = new Vector3SaveData(dice.transform.eulerAngles),
                    materialName = GetMaterial(meshRend)
                };
                saveData.dices.Add(diceData);
            }

            var json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(SavePath,json);
            Debug.Log(SavePath);
        }

        public void LoadDices()
        {
            if (!File.Exists(SavePath))
            {
                Debug.Log("No file");
                return;
            }
            ClearCurrentDices();
            
            var json = File.ReadAllText(SavePath);
            var saveData = JsonConvert.DeserializeObject<DicesData>(json);
            
            foreach (var diceData in saveData.dices)
            {
                var position = new Vector3(
                    diceData.position.x,
                    diceData.position.y,
                    diceData.position.z
                );
                var rotation = Quaternion.Euler(
                    diceData.rotation.x,
                    diceData.rotation.y,
                    diceData.rotation.z
                );

                var newDice = Instantiate(dicePrefab, position, rotation);

                var meshRenderer = newDice.GetComponent<MeshRenderer>();
                var material = FindMaterial(diceData.materialName);

                if (meshRenderer != null && material != null)
                    meshRenderer.material = material;

                var rigidbody = newDice.GetComponent<Rigidbody>();

                if (rigidbody != null)
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                }

                RegisterDice(newDice);
            }
        }
        private void ClearCurrentDices()
        {
            foreach (var dice in dices)
            {
                if (dice != null)
                    Destroy(dice);
            }

            dices.Clear();
        }
        private string GetMaterial(MeshRenderer meshRenderer)
        {
            return GetMaterialName(meshRenderer.material.name);
        }
        private string GetMaterialName(string materialName)
        {
            return materialName.Replace(" (Instance)", "").Trim();
        }
        private Material FindMaterial(string materialName)
        {
            materialName = GetMaterialName(materialName);

            foreach (var material in _materialContrioller.Materials)
            {
                if (material == null)
                    continue;

                if (GetMaterialName(material.name) == materialName)
                    return material;
            }

            return null;
        }
        
        [Serializable]
        public class DicesData
        {
            public List<DiceData> dices = new List<DiceData>();
        }
        [Serializable]
        public class DiceData
        {
            public Vector3SaveData position;
            public Vector3SaveData rotation;
            public string materialName;
        }
        [Serializable]
        public class Vector3SaveData
        {
            public float x;
            public float y;
            public float z;
            
            public Vector3SaveData(Vector3 vector)
            {
                x = vector.x;
                y = vector.y;
                z = vector.z;
            }
            public Vector3SaveData()
            {
            }
        }
    }
}
