using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public partial class UserData
{
    private static string _id;
    private static string _pw;
    private static string _nickName;

    public static string GetID => _id;
    public static string GetPW => _pw;
    public static string GetNickName => _nickName;
    
    public static void Initialize(string id, string pw, string nickName)
    {
        _id = id;
        _pw = pw;
        _nickName = nickName;
    }
}