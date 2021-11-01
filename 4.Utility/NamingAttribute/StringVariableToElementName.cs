using UnityEngine;

public class StringVariableToElementName : PropertyAttribute
{
    public string Varname;
    public StringVariableToElementName(string ElementTitleVar)
    {
        Varname = ElementTitleVar;
    }
}