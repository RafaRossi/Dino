using UnityEngine;
public class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
    #region(Singleton)
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();

                if (!_instance)
                {
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                }
            }

            return _instance;
        }
        protected set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
        }

        Initialize();
    }
    #endregion

    protected virtual void Initialize()
    {

    }
}
