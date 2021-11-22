using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "TM/Player Patent Data")]
    public class PlayerPatentData : ScriptableObject
    {
        [SerializeField] [Range(0, 100)] private int _credits;

        public List<PatentData> OwnedPatents;
        public List<PatentData> ActivePatents;
        public List<PatentData> CompletedPatents;

        public event Action<int> OnCreditsChanged;

        public void AddCredits(int amount)
        {
            if (amount <= 0) return;

            _credits += amount;
            OnCreditsChanged?.Invoke(_credits);
        }

        public bool RemoveCredits(int amount)
        {
            if (_credits < amount) return false;

            _credits -= amount;
            OnCreditsChanged?.Invoke(_credits);
            return true;
        }
    }
}