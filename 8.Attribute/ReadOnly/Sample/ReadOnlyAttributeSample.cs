using UnityEngine;

namespace MyLibrary.Attribute.Sample
{
    public class ReadOnlyAttributeSample : MonoBehaviour
    {
        //Always you can't Modify
        [ReadOnlyInEditorAttribute(false), SerializeField]
        private string name;

        [ReadOnlyInEditorAttribute(false), SerializeField]
        private string info;

        //if Unity is Playing GameMode. you can't Modify
        [ReadOnlyInEditorAttribute(true), SerializeField]
        private int Lv;
    }
}