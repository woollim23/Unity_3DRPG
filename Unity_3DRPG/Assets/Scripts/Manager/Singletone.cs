using UnityEngine;
public class Singletone<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    string tName = typeof(T).ToString(); // ������Ʈ �̸� ���ϱ�
                    var singletoneObj = new GameObject(tName); // Ÿ�� �̸���� �����Ǿ� ������
                    _instance = singletoneObj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance != null)
            DontDestroyOnLoad(Instance);
    }
}
