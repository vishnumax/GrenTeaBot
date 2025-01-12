using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PageNavigator : MonoBehaviour
{
    static PageNavigator instance;
    public static PageNavigator Instance { get { return instance; } }

    [Header("PageStore")]
    [SerializeField] PageStore pageStore;

    [Space]
    [SerializeField] Transform pageParent;

    List<Page> currentPages = new List<Page>();

   
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        NavAction(PageSet.Splash);
    }

 

    public void NavAction(PageSet page)
    {
        StatusManager.Instance.Message("");

        if (pageParent.childCount > 0)
        {
            for (int i = 0; i < pageParent.childCount; i++)
            {
                GameObject child = pageParent.GetChild(i).gameObject;
                child.SetActive(false);
            }
        }

        Page currentPage = pageStore.GetPage(page);

        if (currentPages.Contains(currentPage))
        {
            string name = currentPage.panel.name;

            Debug.LogWarning("Loading ..." + name + pageParent.childCount);

            for (int i = 0; i < pageParent.childCount; ++i)
            {
                GameObject child = pageParent.GetChild(i).gameObject;
                Debug.Log("Child.." + child.name);

                if (child.name.Contains(name))
                {
                    child.SetActive(true);
                    break;
                }
            }

            return;
        }

        GameObject go = Instantiate(currentPage.panel, pageParent);

        go.transform.localScale = Vector3.one;
        go.transform.position = Vector3.zero;
        go.SetActive(true);

        RectTransform rectGo = go.GetComponent<RectTransform>();
       
        rectGo.offsetMin = new Vector2(0, rectGo.offsetMin.y);
        rectGo.offsetMax = new Vector2(0, rectGo.offsetMax.y);
        rectGo.offsetMin = new Vector2(rectGo.offsetMin.x, 0);
        rectGo.offsetMax = new Vector2(rectGo.offsetMax.x, 0);

        RectTransform rectP = pageParent.GetComponent<RectTransform>();
        go.GetComponent<RectTransform>().sizeDelta = new Vector2(rectP.sizeDelta.x, rectP.sizeDelta.y);

        currentPages.Add(currentPage);
    }

}
