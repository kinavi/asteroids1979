using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    struct Level
    {
        public float DelaySpawn;
        public float Time;
    }
    public class GameController : MonoBehaviour
    {
        public static GameController instance = null;
        
        public Enemy modelEnemy;
        public float enemySpeed;

        private UIController _uiController;
        private List<Enemy> _enemies;
        private float _totalTime;
        private float _time;
        private Camera _camera;
        private bool _isGameOver;
        private Dictionary<int, Level> _levelDictionary;
        private int _currentLevel;
        private float _delaySpawn;

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
            _enemies = new List<Enemy>();
            _camera = Camera.main;
            _uiController = gameObject.AddComponent<UIController>();
            _uiController.SetVisibilityGameOverLayer(false);
            _isGameOver = false;
            _levelDictionary = new Dictionary<int, Level>()
            {
                {1, new Level(){Time = 0, DelaySpawn = 3}},
                {2, new Level(){Time = 15, DelaySpawn = 2}},
                {3, new Level(){Time = 30, DelaySpawn = 1}},
                {4, new Level(){Time = 40, DelaySpawn = 0.5f}},
                {5, new Level(){Time = 50, DelaySpawn = 0.2f}}
            };
            _currentLevel = 1;
            _delaySpawn = _levelDictionary[_currentLevel].DelaySpawn;
        }
        
        private void Update()
        {
            if (!_isGameOver)
            {
                setLevelByTime();
                DelayBeforeInvoke(SpawnEnemy);
            }
        }

        private void setLevelByTime()
        {
            if (_levelDictionary.Count != _currentLevel + 1 && Time.time > _levelDictionary[_currentLevel + 1].Time)
            {
                ++_currentLevel;
                _delaySpawn = _levelDictionary[_currentLevel].DelaySpawn;
            }
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
                    -enemy.transform.position.x,
                    -enemy.transform.position.y,
                    0
                ).normalized, 
                enemySpeed
            );

            
            _enemies.Add(enemy);
        }
        
        private void DelayBeforeInvoke(Action func)
        {
            _time += Time.deltaTime;
            if (_time > _delaySpawn)
            {
                _time = 0;
                func();
            }
        }

        public void SetPoint()
        {
            _uiController.Score++;
        }

        public int ShowPoint()
        {
            return _uiController.Score;
        }

        public void onGameOver()
        {
            _isGameOver = true;
            _uiController.SetVisibilityGameOverLayer(true);
        }
    }
}