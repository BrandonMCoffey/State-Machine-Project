using System;
using Scripts.Data;
using Scripts.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.UI.MainMenu
{
    public class ModeSelectController : MonoBehaviour
    {
        private const int MarsPlanetSceneIndex = 1;
        private const int MoonPlanetSceneIndex = 2;

        [SerializeField] private bool _debug = false;
        [SerializeField] private GameData _gameData = null;
        [Header("Solo Player")]
        [SerializeField] private PlayerData _mainPlayer = null;
        [Header("AI Players")]
        [SerializeField] private PlayerData _aiEasyPlayer = null;
        [SerializeField] private PlayerData _aiMediumPlayer = null;
        [SerializeField] private PlayerData _aiHardPlayer = null;
        [Header("Hotseat Players")]
        [SerializeField] private PlayerData _hotseatPlayer1 = null;
        [SerializeField] private PlayerData _hotseatPlayer2 = null;

        private AiDifficultyLevels _aiDifficulty = AiDifficultyLevels.Easy;

        private void StartGame()
        {
            int gameScene = _gameData.Planet switch
            {
                PlanetType.Mars => MarsPlanetSceneIndex,
                PlanetType.Moon => MoonPlanetSceneIndex,
                _               => 0
            };
            Debug.Log("Starting Game on " + _gameData.Planet + " at build scene index " + gameScene, gameObject);
            SceneManager.LoadScene(gameScene);
        }

        public void StartSoloGame()
        {
            Log("Solo Game Started");
            _gameData.Player = _mainPlayer;
            _gameData.Opponent = null;
            StartGame();
        }

        public void SetAiDifficulty(int difficulty)
        {
            _aiDifficulty = difficulty switch
            {
                0 => AiDifficultyLevels.Easy,
                1 => AiDifficultyLevels.Medium,
                2 => AiDifficultyLevels.Hard,
                _ => _aiDifficulty
            };
            Log("AI Difficulty set to " + _aiDifficulty);
        }

        public void StartGameVsAi()
        {
            Log("AI Game Started");
            _gameData.Player = _mainPlayer;
            _gameData.Opponent = _aiDifficulty switch
            {
                AiDifficultyLevels.Easy   => _aiEasyPlayer,
                AiDifficultyLevels.Medium => _aiMediumPlayer,
                AiDifficultyLevels.Hard   => _aiHardPlayer,
                _                         => _aiEasyPlayer
            };
            StartGame();
        }

        public void StartHotseatGame()
        {
            Log("Hotseat Game Started");
            _gameData.Player = _hotseatPlayer1;
            _gameData.Opponent = _hotseatPlayer2;
            StartGame();
        }

        private void Log(string message)
        {
            if (_debug) Debug.Log(message, gameObject);
        }
    }
}