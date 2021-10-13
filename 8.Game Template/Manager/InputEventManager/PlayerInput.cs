using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [System.Serializable]
    public struct InputEvent
    {
        [Tooltip("Describe the event name.")]
        [SerializeField] private string _eventName;
        [Tooltip("Describe the event function.")]
        [SerializeField] private string _eventInfo;
        public KeyCode _keyCode;
        public keyEventType _eventType;
        public UnityEvent _event;
    }
    
    [SetElementTitle("_eventName")]
    [SerializeField] private List<InputEvent> _inputEvents;
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
