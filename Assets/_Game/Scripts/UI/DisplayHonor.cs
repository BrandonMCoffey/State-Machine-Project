using Scripts.Data;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class DisplayHonor : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData = null;
        [SerializeField] private TextMeshProUGUI _text = null;

        private void Start()
        {
            if (_playerData == null || _text == null) return;
            _playerData.OnHonorChanged += () => _text.text = _playerData.Honor.ToString();
        }
    }
}