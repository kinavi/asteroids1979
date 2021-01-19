using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Asteroids
{
    public class Projectile : MonoBehaviour
    {
        private bool isStart;
        private Vector3 _direction;
        private float _speed;
        
        private void Awake()
        {
            isStart = false;
        }
        
        private void Update()
        {
            if (isStart)
            {
                gameObject.transform.Translate(_direction * _speed * Time.deltaTime);
            }
        }
        
        public void Launching(Vector3 direction, float speed)
        {
            _direction = direction;
            _speed = speed;
            isStart = true;
        }
    }
}