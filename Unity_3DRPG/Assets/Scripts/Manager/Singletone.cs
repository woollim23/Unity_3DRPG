using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Component
{
    // �̱���
    private static T instance;

    public static T Instance
    {
        get
        {
            // �̱��� �����

            // 1. instance - null üũ
            if (instance == null)
            {
                // ���ٸ� ���ο� �̱��� �����
                // 2. ����ó�� - Ȥ�� ���� �̱����� �ִ���
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    // 3. ���ο� ������Ʈ �����
                    string tName = typeof(T).ToString(); // ������Ʈ �̸� ���ϱ�
                    var singletoneObj = new GameObject(tName); // Ÿ�� �̸���� �����Ǿ� ������
                    // 4. ������Ʈ�� �߰� <- T �߰�
                    // 5. instance �Ҵ�
                    instance = singletoneObj.AddComponent<T>();
                }
            }
            // �ִٸ� instance ���� 
            return instance;

        }
    }

    private void Awake()
    {
        if (instance != null)
            DontDestroyOnLoad(instance);
    }

    public void Init()
    {

    }

    public void Release()
    {

    }
}

