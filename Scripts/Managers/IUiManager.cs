using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ui manager interface
public interface IUiManager
{
    void ToggleTimer();
    void SetLevel(int level);
    void IncrementLevel();

}