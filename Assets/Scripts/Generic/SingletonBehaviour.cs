using UnityEngine;

namespace MagicOwl
{
    public class SingletonBehaviour<T> : CommonBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if(!instance)
                {
                    instance = FindObjectOfType<T>();

                    if(!instance)
                    {
                        instance = new GameObject(typeof(T).Name).AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        void Awake()
        {
            T component = GetComponent<T>();
            if(instance && instance != component)
            {
                DestroyImmediate(gameObject);
                return;
            }

            instance = component;
            DontDestroyOnLoad(gameObject);

            Init();
        }

        public virtual void Init()
        {

        }
    }
}