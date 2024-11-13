using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletone<GameManager>
{
    public int StageLevel { get; private set; }
    public int EnemyDeathCount { get; private set; }


}
