using UnityEngine;

namespace MyLibrary.Attribute.Sample
{
    public class ReadOnlyAttributeSample : MonoBehaviour
    {
        //Always you can't Modify
        [ReadOnly(false), SerializeField]
        private string name;
        [ReadOnly(false), SerializeField]
        private string info;
        
        //if Unity is Playing GameMode. you can't Modify
        [ReadOnly(true), SerializeField]
        private int Lv;
    }
}