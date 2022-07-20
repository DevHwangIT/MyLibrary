using UnityEngine;

public class SingletonScripatableObject<T> : ScriptableObject where T : SingletonScripatableObject<T>
{
    private static T _instance;
    public static T Instance
    {
        get{
            if(_instance == null)
            {
                T[] asset = Resources.LoadAll<T>("");
                if(asset == null || asset.Length<1)
                {
                    throw new System.Exception($"Could not find any singleton scriptable object instance in the resource.");
                }
                else if( asset.Length> 1){
                    Debug.LogWarning("Multiple instance of the singleton scripate boject found in the resource.");
                }

                _instance =asset[0];
            }

            return _instance;
        }
    }
}