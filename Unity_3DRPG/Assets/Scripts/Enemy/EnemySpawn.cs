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
        // TODO : 스테이지 레벨 만큼 적생, 최대 스테이지 레벨만큼 생성(오브젝트 풀링도 괜찮으려나?)
        // TODO : 스테이지 레벨은 게임 매니저에 저장해야겠다
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
                    Debug.LogError("오브젝트 없음!");
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
