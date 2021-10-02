using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerData
{
    private static int _lv;
    private static float _exp;
    private static long _money;
    
    public static int GetLevel => _lv;
    public static float GetExp => _exp;
    public static float GetMoney => _money;
    
}
