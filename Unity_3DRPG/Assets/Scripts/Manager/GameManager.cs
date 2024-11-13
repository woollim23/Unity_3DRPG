using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    [SerializeField] public int StageLevel { get; private set; }
    public int DeathEnemyCount { get; private set; }

    [SerializeField] private GameObject mazePrefab;

    [SerializeField] private TextMeshProUGUI UIStageLevel;


    void Start()
    {
        StageLevel = 1;
        NewStage();
    }

    public void ShowLevelText()
    {
        UIStageLevel.text = "Stage : " + StageLevel.ToString();
    }

    public void StageClear()
    {
        StageLevelUp();

        Invoke("NewStage", 6);
    }

    private void StageLevelUp()
    {
        StageLevel++;
    }

    public void NewStage()
    {

        ShowLevelText();
        DeathEnemyCount = 0;

        GameObject currentMaze = GameObject.FindGameObjectWithTag("Maze");
        if (currentMaze != null)
        {
            Destroy(currentMaze);
        }

        CreateMaze();
    }

    private void CreateMaze()
    {
        Instantiate(mazePrefab, Vector3.zero, Quaternion.identity);
    }

    public void DeathEnemy()
    {
        DeathEnemyCount++;

        if (DeathEnemyCount == StageLevel)
            StageClear();
    }

}
