using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Systems
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        // Dictionary to store event callbacks
        private Dictionary<string, UnityEvent> events = new Dictionary<string, UnityEvent>();

        // Events for specific game actions
        public UnityEvent<string> OnObjectDestroyedByShot; // Object name as parameter
        public UnityEvent OnMouseMoved;
        public UnityEvent OnKeyPressed; // Key name as parameter
        public UnityEvent<string> OnKeyPressedWithName; // Key name as parameter
        public UnityEvent OnObjectPickedUp; // Object name as parameter
        public UnityEvent<string> OnObjectPickedUpWithName; // Object name as parameter
        public UnityEvent OnDoorOpened;
        public UnityEvent OnWeaponDrawn;
        public UnityEvent OnWeaponFired;
        public UnityEvent OnTargetHit; // Hit target/milestone

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Subscribe to a custom event by name
        /// </summary>
        public void SubscribeToEvent(string eventName, UnityAction callback)
        {
            if (!events.ContainsKey(eventName))
            {
                events[eventName] = new UnityEvent();
            }
            events[eventName].AddListener(callback);
        }

        /// <summary>
        /// Unsubscribe from a custom event by name
        /// </summary>
        public void UnsubscribeFromEvent(string eventName, UnityAction callback)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].RemoveListener(callback);
            }
        }

        /// <summary>
        /// Trigger a custom event by name
        /// </summary>
        public void TriggerEvent(string eventName)
        {
            if (events.ContainsKey(eventName))
            {
                events[eventName].Invoke();
                Debug.Log($"[EventManager] Triggered event: {eventName}");
            }
            else
            {
                Debug.LogWarning($"[EventManager] Event '{eventName}' not found. No listeners registered.");
            }
        }

        /// <summary>
        /// Check if an event has been triggered (for one-time events)
        /// Note: This is a simple implementation. For persistent state, consider using a HashSet
        /// </summary>
        public bool HasEventListeners(string eventName)
        {
            return events.ContainsKey(eventName) && events[eventName].GetPersistentEventCount() > 0;
        }

        /// <summary>
        /// Clear all event listeners (useful for scene transitions)
        /// </summary>
        public void ClearAllEvents()
        {
            events.Clear();
        }

        // Public methods to trigger specific events
        public void TriggerObjectDestroyedByShot(string objectName)
        {
            OnObjectDestroyedByShot?.Invoke(objectName);
            TriggerEvent("ObjectDestroyedByShot");
            Debug.Log($"[EventManager] Object destroyed by shot: {objectName}");
        }

        public void TriggerMouseMoved()
        {
            OnMouseMoved?.Invoke();
            TriggerEvent("MouseMoved");
        }

        public void TriggerKeyPressed(string keyName)
        {
            OnKeyPressedWithName?.Invoke(keyName);
            OnKeyPressed?.Invoke();
            TriggerEvent($"KeyPressed_{keyName}");
            TriggerEvent("KeyPressed");
        }

        public void TriggerObjectPickedUp(string objectName)
        {
            OnObjectPickedUpWithName?.Invoke(objectName);
            OnObjectPickedUp?.Invoke();
            TriggerEvent($"ObjectPickedUp_{objectName}");
            TriggerEvent("ObjectPickedUp");
        }

        public void TriggerDoorOpened()
        {
            OnDoorOpened?.Invoke();
            TriggerEvent("DoorOpened");
        }

        public void TriggerWeaponDrawn()
        {
            OnWeaponDrawn?.Invoke();
            TriggerEvent("WeaponDrawn");
        }

        public void TriggerWeaponFired()
        {
            OnWeaponFired?.Invoke();
            TriggerEvent("WeaponFired");
        }

        public void TriggerTargetHit()
        {
            OnTargetHit?.Invoke();
            TriggerEvent("TargetHit");
        }
    }
}

