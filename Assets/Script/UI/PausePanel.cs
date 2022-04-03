using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public void OnBackToMenuBtnClick()
    {
        GameManager.Instance.BackToMenu();
    }
}
