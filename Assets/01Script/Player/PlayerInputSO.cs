using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _01Script.Player
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput")]
    public class PlayerInputSO : ScriptableObject, Contorl.IPlayerActions
    {
        public Vector2 MovementKey { get; private set; }

        private Contorl _contorl;

        private void OnEnable()
        {
            if (_contorl == null)
            {
                _contorl = new Contorl();
                _contorl.Player.SetCallbacks(this);
            }
            _contorl.Player.Enable();
        }

        private void OnDisable()
        {
            _contorl.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementKey = context.ReadValue<Vector2>();
        }
    }
}