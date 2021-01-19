using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance = null;
        
        public Enemy modelEnemy;
        public float DelaySpawn;
        public float enemySpeed;
        public int Score { get; set; }
        
        private List<Enemy> _enemies;
        private float _time;
        private Camera _camera;
        
        private void Awake()
        {
            if (instance == null)
            { 
                instance = this; 
            }
            
            else if (instance == this)
            { 
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            
            _enemies = new List<Enemy>();
            _camera = Camera.main; 
        }
        
        private void Update()
        {
            DelayBeforeInvoke(SpawnEnemy);
        }

        void SpawnEnemy()
        {
            float minY = _camera.orthographicSize;
            float minX = minY * _camera.aspect;

            Vector3[] positions = new[]
            {
                new Vector3(minX, Random.Range(-minY, minY)),
                new Vector3(Random.Range(-minX, minX), minY),
                new Vector3(Random.Range(-minX, minX), -minY),
                new Vector3(-minX, Random.Range(-minY, minY))
            };

            Enemy enemy = GameObject.Instantiate(
                modelEnemy, 
                positions[Random.Range(0, positions.Length - 1)],
                Quaternion.identity
            );
            
            enemy.Launching(
                new Vector3(
                    Random.Range(-minX, minX), 
                    Random.Range(-minY, minY), 
                    0
                ).normalized, 
                enemySpeed
            );
            
            _enemies.Add(enemy);
        }

        private void DelayBeforeInvoke(Action func)
        {
            _time += Time.deltaTime;
            if (_time > DelaySpawn)
            {
                _time = 0;
                func();
            }
        }
        
    }
}