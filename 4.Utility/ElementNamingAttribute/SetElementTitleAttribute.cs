using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetElementTitle : PropertyAttribute
{
    public string Varname;
    public SetElementTitle(string ElementTitleVar)
    {
        Varname = ElementTitleVar;
    }
}