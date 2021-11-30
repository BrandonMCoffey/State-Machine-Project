using System;
using Scripts.Enums;

namespace Scripts.Data.Structs
{
    [Serializable]
    public struct PatentConstraint
    {
        public bool Active;
        public PlanetStatusType Type;
        public ComparisonType Comparison;
        public int Amount;
    }
}