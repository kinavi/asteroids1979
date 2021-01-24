using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class UIController : MonoBehaviour
    {
        private Text _score;
        private Button _restartButton;
        private GameObject _gameOverLayer;
        private Text _scoreLayer; 
        
        public int Score { 
            get => Convert.ToInt16(_score.text);
            set
            {
                _score.text = value.ToString();
                _scoreLayer.text = value.ToString();
            }
        }

        public void SetVisibilityGameOverLayer(bool status)
        {
            _gameOverLayer.SetActive(status);
        }
        
        private void onReset()
        {
            _score.text = "0";
            SceneManager.LoadScene("Main");
        }
        
        private void Awake()
        {
            _gameOverLayer = GameObject.FindWithTag("game_over_layer");
            _score = GameObject.FindWithTag("score_value").GetComponent<Text>();
            _scoreLayer = GameObject.FindWithTag("score_layer_value").GetComponent<Text>();
            _restartButton = GameObject.FindWithTag("reset_button").GetComponent<Button>();
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(onReset);
        }
    }
}