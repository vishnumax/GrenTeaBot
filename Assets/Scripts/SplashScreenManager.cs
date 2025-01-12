using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    
    private void OnEnable()
    {
        StartCoroutine(StartAction());
    }

    IEnumerator StartAction()
    {
        StatusManager.Instance.Message("Loading...");

        yield return new WaitForSeconds(2.0f);

       PageNavigator.Instance.NavAction(PageSet.level);

    }
}
