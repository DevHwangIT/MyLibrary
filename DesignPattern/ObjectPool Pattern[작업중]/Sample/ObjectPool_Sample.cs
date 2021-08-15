using System.Collections;
using System.Collections.Generic;
using MyLibrary.DesignPattern;
using UnityEngine;

public class ObjectPool_Sample : MonoBehaviour
{
    [SerializeField]
    private GameObject CapsulePrefab;
    [SerializeField]
    private GameObject CubePrefab;
    [SerializeField]
    private GameObject CylinderPrefab;
    [SerializeField]
    private GameObject SpherePrefab;
    [SerializeField]
    private GameObject FailedPrefab;
    
    void Start()
    {
    }

    public void HowToGetPooling()
    {
        ObjectPool.Spawn(FailedPrefab).transform.position = Vector3.zero;
    }
}
