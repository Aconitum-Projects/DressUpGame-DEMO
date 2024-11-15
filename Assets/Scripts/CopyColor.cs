using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CopyColor : MonoBehaviour
{
    public Image spriteToChangeColor, spriteToCopyColorFrom;


    private void Start()
    {
    }

    void Update()
    {
        spriteToChangeColor.color = spriteToCopyColorFrom.color;
    }
}
