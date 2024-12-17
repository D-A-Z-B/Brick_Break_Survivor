using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBS.Player {
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Player/InputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions {
        private Action<bool> attackEvent;
        
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
                attackEvent?.Invoke(true);
            }
            else if (context.canceled) {
                attackEvent?.Invoke(false);
            }
        }
    }
}
