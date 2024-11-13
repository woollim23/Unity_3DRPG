using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject ground;
    public float gridWidth;
    public float gridHeight;

    void Start()
    {
        Place3DTiles();
        gridWidth = ground.transform.localScale.x / 2;
        gridHeight = ground.transform.localScale.y / 2;

    }

    void Place3DTiles()
    {
        for (float x = -(gridWidth-0.5f); x < gridWidth; x++)
        {
            for (float z = -(gridHeight - 0.5f); z < gridHeight; z++)
            {
                if ((x + 0.5f) % 2 == 0)
                {
                    // 미로생성 테스트중
                    Vector3 position = new Vector3(x, 1f, z);
                    Instantiate(tilePrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

}
