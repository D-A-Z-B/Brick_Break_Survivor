using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBS.Players {
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/InputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions {
        public event Action attackDragEvent;
        public event Action attackEvent;
        public event Action tpEvent;
        
        private Controls controls;

        private void OnEnable() {
            if (controls == null) {
                controls = new Controls();
                controls.Player.SetCallbacks(this);
            }
            controls.Player.Enable();
        }

        private void OnDisable() {
            controls.Player.Disable();
        }

        public void OnAttack(InputAction.CallbackContext context) {
            if (context.performed) {
                attackDragEvent?.Invoke();
            }
            else if (context.canceled) {
                attackEvent?.Invoke();
            }
        }

        public void OnTP(InputAction.CallbackContext context)
        {
            if (context.performed) {
                tpEvent?.Invoke();
            }
        }
    }
}
