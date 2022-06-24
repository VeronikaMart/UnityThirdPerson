using UnityEngine;

namespace UnityThirdPerson.StateMachines.Player
{
    public class PlayerStateMachine : StateMachine
    {
        // Allows to see in inspector
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationDamping { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        private void Start()
        {
            MainCameraTransform = Camera.main.transform;
            SwitchState(new PlayerFreeLookState(this));
        }
    }
}