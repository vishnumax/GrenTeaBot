using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public interface IStat
{
    public void PlayAction();
    public void HomeAction();
}

public class StatusManager : MonoBehaviour
{
    static StatusManager instance;
    public static StatusManager Instance { get { return instance; } }

    [SerializeField] TMP_Text statusMessage;

    [Space]
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text messageText;

    [Header("Buttons:")]
    [SerializeField] Button playBtn;
    [SerializeField] Button homeBtn;

    [Header("Star Settings:")]
    [SerializeField] Sprite loss;
    [SerializeField] Sprite win;

    [SerializeField] Image star;

    public IStat callback;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        playBtn.onClick.AddListener(Play);
        homeBtn.onClick.AddListener(Home);
    }

    public void Message(string message)
    {
        statusMessage.text = message;
    }

    public void EnableMenu(bool enable, string message, ResultStat stats)
    {
        star.sprite = stats switch
        {
            ResultStat.win => win,
            ResultStat.loss => loss,
        };

        StartCoroutine(EnableMenuAction(enable, message));
    }

    IEnumerator EnableMenuAction(bool enable, string message)
    {
        yield return new WaitForSeconds(3.0f);

        panel.SetActive(enable);
        messageText.text = message;
    }

    void Home()
    {
        callback.HomeAction();
        panel.SetActive(false);
    }

    void Play()
    {
        callback.PlayAction();
        panel.SetActive(false);
    }
}

public enum ResultStat {win,loss}
