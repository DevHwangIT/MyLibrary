using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.DesignPattern
{
    public interface ICommand
    {
        IEnumerator Undo();
        IEnumerator Execute();
    }
}