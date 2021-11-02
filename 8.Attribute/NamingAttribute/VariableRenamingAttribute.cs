using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MyLibrary.Attribute
{
    public class VariableRenamingAttribute : PropertyAttribute
    {
        public string label;

        public VariableRenamingAttribute(string label)
        {
            this.label = label;
        }

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(VariableRenamingAttribute))]
        public class ThisPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                try
                {
                    var propertyAttribute = this.attribute as VariableRenamingAttribute;
                    if (IsItBloodyArrayTho(property) == false)
                    {
                        label.text = propertyAttribute.label;

                    }
                    else
                    {
                        Debug.LogWarningFormat(
                            "{0}(\"{1}\") doesn't support arrays ",
                            typeof(VariableRenamingAttribute).Name,
                            propertyAttribute.label
                        );
                    }

                    EditorGUI.PropertyField(position, property, label);
                }
                catch (System.Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            bool IsItBloodyArrayTho(SerializedProperty property)
            {
                string path = property.propertyPath;
                int idot = path.IndexOf('.');
                if (idot == -1) return false;
                string propName = path.Substring(0, idot);
                SerializedProperty p = property.serializedObject.FindProperty(propName);
                return p.isArray;
            }
        }
#endif
    }
}