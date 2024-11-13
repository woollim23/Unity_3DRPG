using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Component
{
    // 싱글톤
    private static T instance;

    public static T Instance
    {
        get
        {
            // 싱글톤 만들기

            // 1. instance - null 체크
            if (instance == null)
            {
                // 없다면 새로운 싱글톤 만들기
                // 2. 예외처리 - 혹시 씬에 싱글톤이 있는지
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    // 3. 새로운 오브젝트 만들기
                    string tName = typeof(T).ToString(); // 오브젝트 이름 정하기
                    var singletoneObj = new GameObject(tName); // 타입 이름대로 지정되어 생성됨
                    // 4. 컴포넌트를 추가 <- T 추가
                    // 5. instance 할당
                    instance = singletoneObj.AddComponent<T>();
                }
            }
            // 있다면 instance 리턴 
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

