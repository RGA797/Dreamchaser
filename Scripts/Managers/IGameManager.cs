using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//game manger interface. it should have a next level and exit function at least
public interface IGameManager
{
    void NextLevel();
    void ExitGame();
}