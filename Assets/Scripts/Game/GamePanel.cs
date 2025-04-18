using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
using TictTackGame.Data;

public class GamePanel : MonoBehaviour
{
    [SerializeField] TMP_Text title;

    [Header("Panels:")]
    [SerializeField] GameObject p3_Panel;
    [SerializeField] GameObject p4_Panel;

    private void OnEnable()
    {
        title.text = GameValues.currentGameType switch
        {
            GameType.nineTack => "TIC TAC TOE 3",
            GameType.sixtheenTack => "TIC TAC TOE 4"
        };

       p3_Panel.SetActive(GameValues.currentGameType == GameType.nineTack);
       p4_Panel.SetActive(GameValues.currentGameType == GameType.sixtheenTack);
    }

    public void BackAction()
    {
        PageNavigator.Instance.NavAction(PageSet.level);
    }

}
