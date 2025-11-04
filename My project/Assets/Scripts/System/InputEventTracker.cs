using UnityEngine;

namespace Systems
{
    /// <summary>
    /// Tracks input events and triggers corresponding events in EventManager
    /// </summary>
    public class InputEventTracker : MonoBehaviour
    {
        private void Update()
        {
            // Check for key presses (excluding mouse buttons)
            CheckKeyPresses();
        }

        private void CheckKeyPresses()
        {
            // Check common keys
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                TriggerKeyEvent("LeftShift");
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                TriggerKeyEvent("LeftControl");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                TriggerKeyEvent("Space");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerKeyEvent("E");
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TriggerKeyEvent("1");
            }

            // Check WASD movement keys
            if (Input.GetKeyDown(KeyCode.W))
            {
                TriggerKeyEvent("W");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                TriggerKeyEvent("A");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                TriggerKeyEvent("S");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                TriggerKeyEvent("D");
            }
        }

        private void TriggerKeyEvent(string keyName)
        {
            if (EventManager.Instance != null)
            {
                EventManager.Instance.TriggerKeyPressed(keyName);
            }
        }
    }
}

