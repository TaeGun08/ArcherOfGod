using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                
                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }
            
            return instance;
        }
    }
    
    [Header("DontDestroyOnLoad Checker")]
    [SerializeField] private bool dontDestroyOnLoadChecker = true;
    
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            
            if (dontDestroyOnLoadChecker == false) return;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
