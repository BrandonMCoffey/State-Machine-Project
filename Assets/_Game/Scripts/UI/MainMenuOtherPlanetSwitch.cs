using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility.Buttons;

namespace Scripts.UI
{
    public class MainMenuOtherPlanetSwitch : MonoBehaviour
    {
        [SerializeField] private MainMenuSwitch _mainMenuSwitch = null;
        [SerializeField] private PlanetData _currentPlanet;
        [Header("Other Planet")]
        [SerializeField] private TextMeshProUGUI _switchPlanetTxt = null;
        [SerializeField] private TextMeshProUGUI _switchPlanetDesc = null;
        [SerializeField] private Image _switchPlanetImage = null;

        private void Start()
        {
            Setup();
        }

        [Button]
        public void Swap()
        {
            _currentPlanet = _mainMenuSwitch.Swap(_currentPlanet);
            Setup();
        }

        private void Setup()
        {
            _switchPlanetTxt.text = "Switch to " + _currentPlanet.PlanetName;
            _switchPlanetDesc.text = _currentPlanet.PlanetSwitchDescription;
            _switchPlanetImage.sprite = _currentPlanet.PlanetSprite;
        }
    }
}