using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.MainMenu
{
    public class MenuPlanetSwitch : MonoBehaviour
    {
        [SerializeField] private GameData _gameData = null;
        [SerializeField] private PlanetData _currentPlanet;
        [Header("Main Planet")]
        [SerializeField] private TextMeshProUGUI _terraformingPlanetTxt = null;
        [SerializeField] private Image _mainPlanetImage1 = null;
        [SerializeField] private Image _mainPlanetImage2 = null;

        private void Start()
        {
            _gameData.Planet = _currentPlanet.PlanetType;
        }

        public PlanetData Swap(PlanetData planet)
        {
            _gameData.Planet = planet.PlanetType;
            PlanetData old = _currentPlanet;
            _currentPlanet = planet;
            Setup();
            return old;
        }

        private void Setup()
        {
            _terraformingPlanetTxt.text = "Terraforming " + _currentPlanet.PlanetName;
            _mainPlanetImage1.sprite = _currentPlanet.PlanetSprite;
            _mainPlanetImage2.sprite = _currentPlanet.PlanetSprite;
        }
    }
}