using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private Image imageText;

    public void SetMessage(Sprite message)
    {
        imageText.sprite = message;
    }

    public void OnBackToMenuBtnClick()
    {
        GameManager.BackToMenu();
    }

}
