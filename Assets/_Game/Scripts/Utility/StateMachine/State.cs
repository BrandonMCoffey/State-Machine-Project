using UnityEngine;

namespace Scripts.Utility.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        public abstract void Enter();
        public abstract void Tick();

        public abstract void Exit();
    }
}