using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringVariableToElementName : PropertyAttribute
{
    public string Varname;
    public StringVariableToElementName(string ElementTitleVar)
    {
        Varname = ElementTitleVar;
    }
}