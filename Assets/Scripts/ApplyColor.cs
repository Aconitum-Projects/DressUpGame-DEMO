using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ApplyColor : MonoBehaviour
{
    public FlexibleColorPicker fcp;
    public Image spriteToApplyColorTo;


    private void Start()
    {
        fcp = GetComponent<FlexibleColorPicker>();
        spriteToApplyColorTo = GetComponent<FlexibleColorPicker>().ImageQuiVaChangerTMTC;
        GetComponent<FlexibleColorPicker>().enabled = true;
    }

    void Update()
    {
        if (spriteToApplyColorTo != null)
        {
            spriteToApplyColorTo.color = fcp.bufferedColor.color;
        }
    }
}
