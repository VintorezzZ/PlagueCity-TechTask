using System;
using MyGame.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : SingletonBehaviour<GameManager>
    {
        public PlayerInfo playerInfo;
        private VisitorsGenerator _visitorsGenerator;

        private void Awake()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = 120;
#endif
            InitializeSingleton();
            playerInfo = DataManager.Load();
            VisitorsGenerator.LoadVisitorsData();
        }

        private void Start()
        {
            SoundManager.Instance.Init();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void PauseGame()
        {
            
        }

        public void ResumeGame()
        {
            
        }
    }

