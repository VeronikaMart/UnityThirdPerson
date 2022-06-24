using UnityEngine;

namespace UnityThirdPerson.StateMachines.Player
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        private const float AnimatorDampTime = .1f;
        private int gravityForce = 100;

        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter() { }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = CalculateMovement();

            // Move() part of character controller
            stateMachine.CharacterController.Move(movement.normalized *
                deltaTime * stateMachine.MovementSpeed + Vector3.down * gravityForce * deltaTime);

            if (stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
                return;
            }

            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
            // Look at same side where player holds key
            FaceMovementDirection(movement, deltaTime);
        }

        public override void Exit() { }

        private Vector3 CalculateMovement()
        {
            Vector3 forward = stateMachine.MainCameraTransform.forward;
            Vector3 right = stateMachine.MainCameraTransform.right;

            forward.y = 0f;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            return forward * stateMachine.InputReader.MovementValue.y +
                right * stateMachine.InputReader.MovementValue.x;
        }

        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(
                stateMachine.transform.rotation,
                Quaternion.LookRotation(movement),
                deltaTime * stateMachine.RotationDamping);
        }
    }
}