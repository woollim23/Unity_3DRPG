using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : Singletone<ObjectPoolManager>
{
    [SerializeField] private GameObject objectPrefab;

    private const int minSize = 10;
    private const int maxSize = 100;

    List<GameObject> tempObject;
    public IObjectPool<GameObject> pool { get; private set; }

    protected override void Awake()
    {
        //base.Awake();
        init();
    }
    

    private void init()
    {
        pool = new ObjectPool<GameObject>(CreateObject, GetObject, ReleaseObject,
        DestroyPool, true, minSize, maxSize);
        for (int i = 0; i < minSize; i++)
        {
            GameObject obj = CreateObject();

            pool.Release(obj);
        }

        tempObject = new List<GameObject>();
    }

    private GameObject CreateObject()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.SetActive(false); // 초기에는 비활성화
        return newObject;
    }
    // 사용
    private void GetObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    // 반환
    private void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
        if (pool.CountInactive >= maxSize)
        {
            // 풀에 공간이 없으면 임시 오브젝트 리스트에 추가
            tempObject.Add(obj);
        }
    }
    // 삭제
    private void DestroyPool(GameObject obj)
    {
        if (tempObject.Contains(obj))
            tempObject.Remove(obj);

        Destroy(obj);
    }
}