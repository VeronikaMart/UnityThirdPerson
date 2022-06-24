using UnityEngine;

namespace UnityThirdPerson.StateMachines
{
    // Should not be added as component
    public abstract class StateMachine : MonoBehaviour
    {
        private State currentState;

        private void Update()
        {
            // if currentState != null
            currentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}