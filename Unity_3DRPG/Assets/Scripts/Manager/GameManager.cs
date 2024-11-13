using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] public int StageLevel { get; private set; }
    public int DeathEnemyCount { get; private set; }

    [SerializeField] private GameObject mazePrefab;

    void Awake()
    {
        StageLevel = 1;
        DeathEnemyCount = 0;

        CreateMaze();
    }

    public void StageClear()
    {
        StageLevelUp();

        Invoke("NewStage", 10);
    }

    private void StageLevelUp()
    {
        StageLevel++;
    }

    public void NewStage()
    {
        DeathEnemyCount = 0;

        GameObject currentMaze = GameObject.Find("Maze");
        if (currentMaze != null)
        {
            Destroy(currentMaze);
        }

        CreateMaze();
    }

    private void CreateMaze()
    {
        if (mazePrefab != null)
        {
            Instantiate(mazePrefab, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Maze prefab is not assigned!");
        }
    }

    public void DeathEnemy()
    {
        DeathEnemyCount++;

        if (DeathEnemyCount == StageLevel)
            StageClear();
    }

}
