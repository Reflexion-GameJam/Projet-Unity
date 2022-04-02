using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text messageText;

    public void SetMessage(string message)
    {
        messageText.text = message;
    }

}
