using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayManager : MonoBehaviour
{
    public Canvas activeCanvas;
    private int numChildren;
    public int curActiveChild = 0;
    private void Start()
    {
        numChildren = activeCanvas.transform.childCount;
        activeCanvas.transform.GetChild(curActiveChild).gameObject.SetActive(true);
    }

    public void nextCharacter()
    {
        activeCanvas.transform.GetChild(curActiveChild).gameObject.SetActive(false);
        curActiveChild++;
        if(curActiveChild >= numChildren - 2)
        {
            curActiveChild = 0;
        }
        activeCanvas.transform.GetChild(curActiveChild).gameObject.SetActive(true);
    }

    public void previousCharacter()
    {
        activeCanvas.transform.GetChild(curActiveChild).gameObject.SetActive(false);
        curActiveChild--;
        if (curActiveChild < 0)
        {
            curActiveChild = numChildren - 3;
        }
        activeCanvas.transform.GetChild(curActiveChild).gameObject.SetActive(true);
    }
}
