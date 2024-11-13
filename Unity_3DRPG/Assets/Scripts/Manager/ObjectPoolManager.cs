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
        newObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        return newObject;
    }
    // ���
    private void GetObject(GameObject obj)
    {
        obj.SetActive(true);
    }

    // ��ȯ
    private void ReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
        if (pool.CountInactive >= maxSize)
        {
            // Ǯ�� ������ ������ �ӽ� ������Ʈ ����Ʈ�� �߰�
            tempObject.Add(obj);
        }
    }
    // ����
    private void DestroyPool(GameObject obj)
    {
        if (tempObject.Contains(obj))
            tempObject.Remove(obj);

        Destroy(obj);
    }
}