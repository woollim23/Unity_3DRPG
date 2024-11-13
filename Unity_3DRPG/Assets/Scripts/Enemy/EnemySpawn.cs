using UnityEngine;

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
        stageLevel = GameManager.Instance.StageLevel;

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

                GameObject enemySpawn = ObjectPoolManager.Instance.pool.Get();
                enemySpawn.transform.GetComponent<Health>().IsDie = false;
                enemySpawn.transform.GetComponent<Health>().health = enemySpawn.transform.GetComponent<Health>().maxHealth;
                enemySpawn.transform.GetComponent<Enemy>().stateMachine.ChangeState(enemySpawn.transform.GetComponent<Enemy>().stateMachine.IdleState);
                enemySpawn.transform.position = position;
                enemyCount++;
            }
        }
    }
}
