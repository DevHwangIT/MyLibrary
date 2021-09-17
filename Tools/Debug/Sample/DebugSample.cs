using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyLibrary;

namespace MyLibrary.Sample
{
    public class DebugSample : MonoBehaviour
    {
        private void Start()
        {
            TEST();
        }

        private void TEST()
        {
            DebugEventHandler eventHandler = DebugSystem.Instance.GetEventHandler;
            
            UnityEvent testevent1 = new UnityEvent();
            testevent1.AddListener(() => { Debug.Log("돈 증가 치트 입니다."); });
            eventHandler.Add(new CheatEvent("돈 증가 치트", "돈을 일정만큼 증가시킵니다.", testevent1));

            UnityEvent testevent2 = new UnityEvent();
            testevent2.AddListener(() => { Debug.Log("레드 팝잇 증가 치트 입니다."); });
            eventHandler.Add(new CheatEvent("레드 팝잇 증가 치트", "팝잇을 증가시킵니다.", testevent2));

            UnityEvent testevent3 = new UnityEvent();
            testevent3.AddListener(() => { Debug.Log("블루 팝잇 증가 치트 입니다."); });
            eventHandler.Add(new CheatEvent("블루 팝잇 증가 치트", "팝잇돈을 증가시킵니다.", testevent3));

            UnityEvent testevent4 = new UnityEvent();
            testevent4.AddListener(() => { Debug.Log("그린 팝잇 증가 치트 입니다."); });
            eventHandler.Add(new CheatEvent("그린 팝잇 증가 치트", "팝잇을 증가시킵니다.", testevent4));

            UnityEvent testevent5 = new UnityEvent();
            testevent5.AddListener(() => { Debug.Log("옐로우 팝잇 증가 치트 입니다."); });
            eventHandler.Add(new CheatEvent("옐로우 팝잇 증가 치트", "팝잇을 증가시킵니다.", testevent5));
            
            UnityEvent testevent6 = new UnityEvent();
            testevent6.AddListener(() => { Debug.LogWarning("테스트 경고 로그"); });
            eventHandler.Add(new CheatEvent("경고 로그 발생", "테스트 경고 로그.", testevent6));
            
            UnityEvent testevent7 = new UnityEvent();
            testevent7.AddListener(() => { Debug.LogError("테스트 에러 로그"); });
            eventHandler.Add(new CheatEvent("경고 에러 발생", "테스트 에러 로그.", testevent7));
        }
    }
}
