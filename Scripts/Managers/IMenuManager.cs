using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//menu manager interface
public interface IMenuManager
{
    void PauseGame();
    void ResumeGame();
    void ResetGame();
    void ExitGame();
    void ToggleTimer();

}