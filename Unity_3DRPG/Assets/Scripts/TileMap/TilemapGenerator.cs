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
        // ù �� �۾�, ������ ������ �ΰ� �������
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

        // �������� ���� Ȥ�� �Ʒ��� ���� �մ� �۾�
        for (int x = 0; x <= gridSize; x++)
        {
            for (int z = 0; z <= gridSize; z++)
            {
                if (z % 2 == 0 || x % 2 == 0)
                {
                    // �� �϶��� �۾� ��ŵ
                    continue;
                }

                if (z == gridSize && x == gridSize)
                {
                    // �� ���κ�(�ⱸ)�� ��ŵ
                    continue;
                }

                if (x == gridSize - 1)
                {
                    // �� �Ʒ��ʿ� ������ �������θ� �� ��
                    tileMap[x, z + 1] = TileType.EMPTY;
                    continue;
                }

                if (z == gridSize - 1)
                {
                    // �� �����ʿ� ������ �Ʒ��θ� �� ��
                    tileMap[x + 1, z] = TileType.EMPTY;
                    continue;
                }

                if (Random.Range(1, 100) % 2 == 0)
                {
                    // �������� �� ��
                    tileMap[x, z + 1] = TileType.EMPTY;
                }
                else
                {
                    // �Ʒ��� �� ��
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
