using System.Collections.Generic;
using Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Displays
{
    public class PatentDetailDisplay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameData _gameData;
        [SerializeField] private IconData _icons;

        [Header("Basic Patent Info")]
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _honor;

        [Header("Patent Cost")]
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Image _costIcon;
        [SerializeField] private TextMeshProUGUI _costAlt;
        [SerializeField] private Image _costAltIcon;

        [Header("Patent Tags")]
        [SerializeField] private List<Image> _tags;

        [Header("Patent Details")]
        [SerializeField] private Color _meetsRequirementsColor = Color.green;
        [SerializeField] private Color _missingRequirementsColor = Color.red;
        [SerializeField] private TextMeshProUGUI _requirements;
        [SerializeField] private TextMeshProUGUI _effects;

        [Header("Patent Buttons")]
        [SerializeField] private Button _activateButton;
        [SerializeField] private Button _activateAltButton;
        [SerializeField] private GameObject _sellButton;

        public void Display(PatentData patent, bool sell)
        {
            // Basic
            _title.text = patent.Name;
            _honor.text = "Gain " + patent.Honor + " Honor";

            // Tags
            var tags = patent.GetAltSprites(_icons);
            for (int i = 0; i < _tags.Count; i++) {
                bool valid = i < tags.Count && tags[i] != null;
                _tags[i].transform.parent.gameObject.SetActive(valid);
                if (valid) _tags[i].sprite = tags[i];
            }

            // Details
            _requirements.text = patent.GetConstraintsReadable();
            _requirements.color = PatentData.CheckConstraint(patent.Constraint1, _gameData) ? _meetsRequirementsColor : _missingRequirementsColor;
            _effects.text = patent.GetEffectsReadable();

            if (!sell) {
                // Cost
                _cost.text = "Activate - " + patent.Cost1.Amount;
                _costIcon.sprite = _icons.GetResource(patent.Cost1.Resource);

                bool alt = patent.Cost2.Active;
                if (alt) {
                    _costAlt.text = "Activate - " + patent.Cost2.Amount;
                    _costAltIcon.sprite = _icons.GetResource(patent.Cost2.Resource);
                }

                // Buttons
                _sellButton.gameObject.SetActive(false);
                _activateButton.interactable = patent.CanActivate(_gameData, false);
                _activateAltButton.gameObject.SetActive(alt);
                if (alt) {
                    _activateAltButton.interactable = patent.CanActivate(_gameData, true);
                }
            } else {
                _sellButton.gameObject.SetActive(true);
                _activateButton.gameObject.SetActive(false);
                _activateAltButton.gameObject.SetActive(false);
            }
        }
    }
}