using UnityEditor;
using UnityEngine;

public class VariableNamePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        VariableNameAttribute nameAttribute = (VariableNameAttribute)this.attribute;
        label.text = nameAttribute.NewName;
        EditorGUI.PropertyField(position, property, label );
    }
}