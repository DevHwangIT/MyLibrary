using UnityEngine;

public class VariableNameAttribute : PropertyAttribute
{
    public string NewName { get; private set; }
    public VariableNameAttribute(string name)
    {
        NewName = name;
    }
}