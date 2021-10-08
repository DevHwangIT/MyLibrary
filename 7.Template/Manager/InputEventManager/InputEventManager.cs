using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEngine;

public class InputEventManager : MonoBehaviour
{
    public enum keyEventType
    {
        GetKeyDown,
        GetKey,
        GetKeyUp
    }
    
    private int[] keyCodeArray;
    private static Dictionary<KeyCode, Action> keyDownEvent = new Dictionary<KeyCode, Action>();
    private static Dictionary<KeyCode, Action> keyEvent = new Dictionary<KeyCode, Action>();
    private static Dictionary<KeyCode, Action> keyUpEvent = new Dictionary<KeyCode, Action>();

    private static Dictionary<KeyCode, Action> SelectEventTypeDictionary(keyEventType eventType)
    {
        switch (eventType)
        {
            case keyEventType.GetKeyDown: return keyDownEvent;
            case keyEventType.GetKeyUp: return keyEvent;
            case keyEventType.GetKey: return keyUpEvent;
            default: return keyEvent;
        }
    }
    
    public static void Add(KeyCode key, Action method, keyEventType eventType = keyEventType.GetKey)
    {
        Dictionary<KeyCode, Action> eventDictionary = SelectEventTypeDictionary(eventType);
        if (eventDictionary.ContainsKey(key))
            eventDictionary[key] += method;
        else
            eventDictionary.Add(key, method);
    }

    public static void Remove(KeyCode key, Action method, keyEventType eventType = keyEventType.GetKey)
    {
        Dictionary<KeyCode, Action> eventDictionary = SelectEventTypeDictionary(eventType);
        if (eventDictionary.ContainsKey(key))
            eventDictionary[key] -= method;
    }

    public static void Clear(KeyCode key, keyEventType eventType = keyEventType.GetKey)
    {
        Dictionary<KeyCode, Action> eventDictionary = SelectEventTypeDictionary(eventType);
        if (eventDictionary.ContainsKey(key))
            eventDictionary[key] = null;
    }

    void Awake() 
    {
        keyCodeArray = (int[])System.Enum.GetValues(typeof(KeyCode));
    }
 
    void Update() 
    {
        if (Input.anyKey)
        {
            for (int i = 0; i < keyCodeArray.Length; i++)
            {
                KeyCode eKeyCode = (KeyCode) keyCodeArray[i];
                if (Input.GetKeyDown(eKeyCode) && keyDownEvent.ContainsKey(eKeyCode))
                    keyDownEvent[eKeyCode]?.Invoke();
                if (Input.GetKey(eKeyCode) && keyEvent.ContainsKey(eKeyCode))
                    keyEvent[eKeyCode]?.Invoke();
                if (Input.GetKeyUp(eKeyCode) && keyUpEvent.ContainsKey(eKeyCode))
                    keyUpEvent[eKeyCode]?.Invoke();
            }
        }
    }
}
