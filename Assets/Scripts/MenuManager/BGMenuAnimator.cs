using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMenuAnimator : MonoBehaviour
{
    [SerializeField] private Material materialBG;
    [SerializeField] private int valueBlur = 25;

    private void OnEnable()
    {
        Panel.onChangeBackGround += ChangeBG;
    }

    private void OnDisable()
    {
        Panel.onChangeBackGround -= ChangeBG;
    }

    private void ChangeBG(bool isShow)
    {     
        StartCoroutine(StartChaneBG(isShow));
    }

    IEnumerator StartChaneBG(bool isShow)
    {
        Debug.Log("sdsdsd");
        if (isShow)
        {
            int k = 0;
            while (k < valueBlur)
            {
                materialBG.SetInt("_Radius", k);
                k++;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            int k = valueBlur;
            while (k > 0)
            {
                materialBG.SetInt("_Radius", k);
                k--;
                yield return new WaitForSeconds(0.05f);
            }
        }
       
    }
}
