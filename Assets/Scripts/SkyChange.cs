using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SkyChange : MonoBehaviour
    {
        [SerializeField] private Material skybox;
        private float _elapsedTime = 0f;
        private float _timeScale = 0.8f; 
        private static readonly int Rotation = Shader.PropertyToID("_Rotation");
        private static readonly int Exposure = Shader.PropertyToID("_Exposure");

        void Update()
        {
            _elapsedTime += Time.deltaTime;
            
            skybox.SetFloat(Rotation, _elapsedTime * _timeScale);
            
            float exposureValue = Mathf.Clamp(Mathf.Cos(_elapsedTime / 300f), 0.4f, 1f);
            skybox.SetFloat(Exposure, exposureValue);
            
        }
        
    }

