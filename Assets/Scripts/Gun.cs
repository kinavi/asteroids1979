using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Asteroids
{
    public class Gun : MonoBehaviour
    {
        private GameObject _gameObject;
    
        private void Awake()
        {
            _gameObject = GameObject.Find("Gun");
        }
    
        public Vector3 getPosition()
        {
            return transform.TransformDirection(_gameObject.transform.localPosition.normalized);
        }
    }
}