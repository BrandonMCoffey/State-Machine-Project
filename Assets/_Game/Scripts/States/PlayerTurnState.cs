using System;
using Scripts.Data;
using Scripts.Mechanics;
using Scripts.UI;
using UnityEngine;

namespace Scripts.States
{
    public class PlayerTurnState : State
    {
        public static event Action<bool> PlayerCanAct;

        private PlayerStandardProjects _standardProjects;
        private int _actionsThisTurn;
        private bool _playerCanAct;
        private float _canEndTurnTime;

        private PlayerData _playerData;

        public override void Enter()
        {
            _canEndTurnTime = Time.time + 2f;
            _standardProjects ??= new PlayerStandardProjects(_playerData);
            _actionsThisTurn = 0;
            SetPlayerCanAct(true);
            _standardProjects.OnPerformAction += UpdateActionsPerformed;
            StateMachine.Input.Confirm += OnEndTurn;
            StateMachine.Input.Cancel += OnPause;
            _playerData.StartTurn();
        }

        public override void Tick()
        {
        }

        public override void Exit()
        {
            SetPlayerCanAct(false);
            _standardProjects.OnPerformAction -= UpdateActionsPerformed;
            StateMachine.Input.Confirm -= OnEndTurn;
            StateMachine.Input.Cancel -= OnPause;
            _playerData.EndTurn();
        }

        public void Setup(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void UpdateActionsPerformed()
        {
            _actionsThisTurn++;
            //if (_actionsThisTurn >= StateMachine.Player.ActionsPerTurn) {
            //SetPlayerCanAct(false);
            //}
        }

        private void SetPlayerCanAct(bool canAct)
        {
            if (_playerCanAct == canAct) return;
            _standardProjects.PlayerCanAct(canAct);
            _playerCanAct = canAct;
            PlayerCanAct?.Invoke(canAct);
        }

        private void OnEndTurn()
        {
            if (Time.time < _canEndTurnTime) return;
            StateMachine.NextTurn();
        }

        private static void OnPause()
        {
            PauseMenuController.Instance.Pause();
        }
    }
}