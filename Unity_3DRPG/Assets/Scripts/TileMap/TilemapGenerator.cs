using UnityEngine;

public enum TileType
{
    NONE = 0,
    EMPTY,
    WALL,
}

public class TilemapGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject ground;
    public int gridSize;
    public TileType[,] tileMap;

    private void Awake()
    {
        gridSize = (int)ground.transform.localScale.x * 10;
        Debug.Log(GameManager.Instance.StageLevel);
        tileMap = new TileType[100, 100];

        GenerateMap();

        Render();
    }

    void Render()
    {
        for (int x = 0; x <= gridSize; x++)
        {
            for (int z = 0; z <= gridSize; z++)
            {
                if (tileMap[x, z] == TileType.WALL)
                {
                    Vector3 position = new Vector3(x - gridSize / 2, tilePrefab.transform.localScale.y / 2, z - gridSize / 2);
                    Instantiate(tilePrefab, position, Quaternion.identity, transform);
                }
            }
        }


    }


    void GenerateMap()
    {
        // 첫 맵 작업, 일정한 간격을 두고 비어있음
        for (int x = 0; x <= gridSize; x++)
        {
            for (int z = 0; z <= gridSize; z++)
            {
                if (x % 2 == 0 || z % 2 == 0)
                {
                    tileMap[x, z] = TileType.WALL;
                }
                else
                {
                    tileMap[x, z] = TileType.EMPTY;
                }
            }
        }

        // 랜덤으로 우측 혹은 아래로 길을 뚫는 작업
        for (int x = 0; x <= gridSize; x++)
        {
            for (int z = 0; z <= gridSize; z++)
            {
                if (z % 2 == 0 || x % 2 == 0)
                {
                    // 벽 일때는 작업 스킵
                    continue;
                }

                if (z == gridSize && x == gridSize)
                {
                    // 맵 끝부분(출구)도 스킵
                    continue;
                }

                if (x == gridSize - 1)
                {
                    // 맨 아래쪽에 왔을땐 우측으로만 길 뚫
                    tileMap[x, z + 1] = TileType.EMPTY;
                    continue;
                }

                if (z == gridSize - 1)
                {
                    // 맨 오른쪽에 왔을땐 아래로만 길 뚫
                    tileMap[x + 1, z] = TileType.EMPTY;
                    continue;
                }

                if (Random.Range(1, 100) % 2 == 0)
                {
                    // 우측으로 길 뚫
                    tileMap[x, z + 1] = TileType.EMPTY;
                }
                else
                {
                    // 아래로 길 뚫
                    tileMap[x + 1, z] = TileType.EMPTY;
                }
            }
        }
    }


    public void ResetMap()
    {
        tileMap = new TileType[100, 100];

        GenerateMap();
        Render();
    }

}
