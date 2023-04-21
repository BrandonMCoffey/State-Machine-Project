﻿using System;
using UnityEngine;

namespace Utility.CustomFloats
{
    [CreateAssetMenu(menuName = "Utility/Float Variable")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] private float _value;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<float> OnValueChanged = delegate { };

        public void SetValue(float value)
        {
            Value = value;
        }

        public void SetValue(FloatVariable value)
        {
            Value = value.Value;
        }

        public void ApplyChange(float amount)
        {
            Value += amount;
        }

        public void ApplyChange(FloatVariable amount)
        {
            Value += amount.Value;
        }
    }
}