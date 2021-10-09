using UnityEngine;

namespace MyLibrary.Utility
{
    public static class InputMediator
    {
        public static Vector2 Position()
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#endif
            return Input.GetTouch(0).position;
        }

        public static bool StartInput()
        {
            return Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
        }

        public static bool Holding()
        {
            return Input.GetMouseButton(0) ||
                   (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved ||
                                             Input.GetTouch(0).phase == TouchPhase.Stationary));
        }

        public static bool Released()
        {
            return Input.GetMouseButtonUp(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended);
        }
    }
}