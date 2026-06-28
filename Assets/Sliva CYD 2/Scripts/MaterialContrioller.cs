using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


namespace SlivaCYD2
{
    public class MaterialContrioller : MonoBehaviour
    {
        [SerializeField] private List<Material> materials;
        public Material Material { get; set; }
        public List<Material> Materials => materials;

        private void Awake()
        {
            Material =  materials[0];
        }

        public void CheckMaterial(int index)
        {
            Material = materials[index];
            Debug.Log(index.ToString());
        }
    }
}
