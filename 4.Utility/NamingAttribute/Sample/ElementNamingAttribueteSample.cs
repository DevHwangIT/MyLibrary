using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.Utility.Sample
{
    [System.Serializable]
    public class Animal
    {
        [SerializeField] private string _name;
        [SerializeField] private string _info;

        public Animal(string name, string info)
        {
            _name = name;
            _info = info;
        }
    }

    public class ElementNamingAttribueteSample : MonoBehaviour
    {
        [StringVariableToElementName("_name"), SerializeField]
        private List<Animal> _animals = new List<Animal>();

        [VariableName("테스트")]
        [SerializeField] private string variable_Name;
    }
}