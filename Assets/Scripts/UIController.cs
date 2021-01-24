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
        private Text _level;
        private Text _time;

        private Button _restartButton;
        private GameObject _gameOverLayer;
        private Text _scoreLayer;
        private Text _levelLayer;
        private Text _timeLayer;

    
        public int Time
        {
            get => Convert.ToInt16(_time.text);
            set
            {
                _timeLayer.text = value.ToString();
                _time.text = value.ToString();
            }
        }
        
        public int Level
        {
            get => Convert.ToInt16(_level.text);
            set
            {
                _levelLayer.text = value.ToString();
                _level.text = value.ToString();
            }
        }

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
            _time.text = "0";
            _level.text = "0";
            SceneManager.LoadScene("Main");
        }
        
        private void Awake()
        {
            _gameOverLayer = GameObject.FindWithTag("game_over_layer");
            _score = GameObject.FindWithTag("score_value").GetComponent<Text>();
            _level = GameObject.FindWithTag("level_value").GetComponent<Text>();
            _time = GameObject.FindWithTag("time_value").GetComponent<Text>();
            _scoreLayer = GameObject.FindWithTag("score_layer_value").GetComponent<Text>();
            _levelLayer = GameObject.FindWithTag("level_layer_value").GetComponent<Text>();
            _timeLayer = GameObject.FindWithTag("time_layer_value").GetComponent<Text>();
            _restartButton = GameObject.FindWithTag("reset_button").GetComponent<Button>();
            _restartButton.onClick.AddListener(onReset);
        }
    }
}