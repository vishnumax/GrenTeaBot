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


    private void OnEnable()
    {
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
        EnableInteract(false);
    }

    public void EnableInteract(bool enable) { button.enabled = enable;}    

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();

        GameActions.StopAction -= StopAction;
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
    }

    public void Mark()
    {
        markImage.enabled = true;
        EnableInteract(false);

        callback.SetTile(playerSet, pos);
    }


    public void WinAction()
    {
        markImage.color = Color.green;
    }

    void RestartAction()
    {
       // markImage.sprite = null;
        markImage.enabled = false;
        EnableInteract(true);

        markImage.color = Color.white;  
    }

}

public enum PlayerSet { first,second}
