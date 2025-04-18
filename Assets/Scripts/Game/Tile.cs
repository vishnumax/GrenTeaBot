using System;
using System.Collections;
using System.Collections.Generic;
using TictTackGame.Act;
using UnityEngine;
using UnityEngine.UI;

public interface ITile
{
    public void SetTile(PlayerSet playerSet, Vector2 pos);
}

[RequireComponent(typeof(Button))]
public class Tile : MonoBehaviour
{
    Button button;

    [SerializeField] Image markImage;
    public bool isMark => markImage.isActiveAndEnabled;

    [SerializeField] Sprite playerOne;
    [SerializeField] Sprite playerTwo;

    PlayerSet playerSet;

    [SerializeField] Vector2 pos;

    public Vector2 Position => pos;


    public ITile callback;

    bool isDone = false;


    private void OnEnable()
    {
        isDone = true;

        if (button == null)
            button = this.GetComponent<Button>();

        button.onClick.AddListener(Mark);

        markImage.sprite = null;
        markImage.enabled = false;

        GameActions.StopAction += StopAction;
        GameActions.RestartAction += RestartAction;
    }

    private void StopAction()
    {
        isDone = true;
        EnableInteract(false);
    }

    public void EnableInteract(bool enable) { button.enabled = enable;}    

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();

        GameActions.StopAction -= StopAction;
        GameActions.RestartAction -= RestartAction;
    }


    public void SetPlayer(PlayerSet set)
    {
        if (button == null || markImage.isActiveAndEnabled)
            return;

        playerSet = set;

        markImage.sprite = set switch
        {
            PlayerSet.first => playerOne,
            PlayerSet.second => playerTwo,
            _ => throw new System.NotImplementedException(),
        };

        markImage.color = set switch
        {
            PlayerSet.first => Color.red,
            PlayerSet.second => Color.blue,
            _ => throw new System.NotImplementedException(),
        };
    }


    public void Mark()
    {
        markImage.enabled = true;
        EnableInteract(false);

        if(!isDone)
        callback.SetTile(playerSet, pos);
    }


    public void WinAction()
    {
        InvokeRepeating("ToggleBlink", 2, 0.3f);
    }

    void ToggleBlink()
    {
        markImage.color = markImage.color == Color.white? Color.green : Color.white;
    }

    void RestartAction()
    {
       // markImage.sprite = null;
        markImage.enabled = false;
        EnableInteract(true);

        isDone = false;
        CancelInvoke("ToggleBlink");

        markImage.color = Color.red;  
    }

}

public enum PlayerSet { first,second}
