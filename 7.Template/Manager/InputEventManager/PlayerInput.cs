using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public struct inputEvent
    {
        public KeyCode _keyCode;
        public keyEventType _eventType;
        public UnityEvent _event;
    }
    [SerializeField] private List<inputEvent> _inputEvents;
    private InputEventManager inputEventManager;
    
    private void Awake()
    {
        inputEventManager = InputEventManager.Instance;
        
        foreach (var inputEvent in _inputEvents)
        {
            InputEventManager.Add(inputEvent._keyCode, inputEvent._event, inputEvent._eventType);
        }
    }
}
