using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private TilemapGenerator map;

    int stageLevel;
    int enemyCount;

    private void Start()
    {
        // TODO : �������� ���� ��ŭ ����, �ִ� �������� ������ŭ ����(������Ʈ Ǯ���� ����������?)
        // TODO : �������� ������ ���� �Ŵ����� �����ؾ߰ڴ�
        enemyCount = 0;
        stageLevel = 2;

        EnemySpawnOnMap();
    }

    void EnemySpawnOnMap()
    {
        while(stageLevel !=  enemyCount)
        {
            int x = Random.Range(1, map.gridSize);
            int z = Random.Range(1, map.gridSize);
            if ( map.tileMap[x, z] == TileType.EMPTY)
            {

                if (ObjectPoolManager.Instance.pool == null)
                {
                    Debug.LogError("������Ʈ ����!");
                    return;
                }

                Vector3 position = new Vector3(x - map.gridSize / 2, map.tilePrefab.transform.localScale.y / 2 + 1, z - map.gridSize / 2);

                var enemySpawn = ObjectPoolManager.Instance.pool.Get();
                enemySpawn.transform.position = position;
                enemyCount++;
            }
        }
    }
}
