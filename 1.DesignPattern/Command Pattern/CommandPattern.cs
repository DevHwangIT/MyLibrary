using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary.DesignPattern
{
    public interface ICommand
    {
        void Execute();
    }

    public class JumpCommand : ICommand
    {
        GameObject player;

        public JumpCommand(GameObject obj)
        {
            player = obj;
        }

        public void Execute()
        {
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
    }

    public class MoveCommand : ICommand
    {
        GameObject player;

        public MoveCommand(GameObject obj)
        {
            player = obj;
        }

        public void Execute()
        {
            player.GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 0);
        }
    }

    public class StopCommand : ICommand
    {
        GameObject player;

        public StopCommand(GameObject obj)
        {
            player = obj;
        }

        public void Execute()
        {
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
    }
}