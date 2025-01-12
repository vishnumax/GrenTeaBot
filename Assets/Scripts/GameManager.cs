using System;
using System.Collections;
using System.Collections.Generic;
using TictTackGame.Act;
using UnityEngine;

public class GameManager : MonoBehaviour,IStat
{
    private void Start()
    {
        GameActions.levelSelect += GameSelectAction;
        StatusManager.Instance.callback = this;
    }

    private void GameSelectAction(GameType type)
    {
        PageNavigator.Instance.NavAction(PageSet.Game);

        GameActions.RestartAction();
    }


    public void PlayAction()
    {
        GameActions.RestartAction();
    }

    public void HomeAction()
    {
        PageNavigator.Instance.NavAction(PageSet.Splash);
    }
}
