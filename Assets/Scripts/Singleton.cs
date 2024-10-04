using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    public static T Instance;

    protected virtual void Awake() 
    {
        if (Instance != null)
            Destroy(this);
        Instance = this as T;
    }

}
