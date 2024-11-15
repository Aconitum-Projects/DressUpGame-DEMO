using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHistoric : MonoBehaviour
{

    public Color actualColor;
    public Image colorCenter;
    public Button colorButton;
    public GameObject actualColorPicker;
    public Image imageToChangeColor;

    // Start is called before the first frame update
    void Start()
    {
        colorButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
        actualColorPicker = GameObject.Find("ColorPicker(Clone)");
        
        if (actualColorPicker != null)
        {
            actualColor = actualColorPicker.GetComponent<FlexibleColorPicker>().color;
            imageToChangeColor = actualColorPicker.GetComponent<FlexibleColorPicker>().ImageQuiVaChangerTMTC;
        }
        if (colorCenter != null)
        {
            colorCenter.color = actualColor;
        }
        
        if (colorButton != null)
        {
            colorButton.onClick.AddListener(colorClicked);
        }
    }

    void colorClicked()
    {
        if (actualColorPicker != null)
        {
            actualColorPicker.GetComponent<FlexibleColorPicker>().color = actualColor;
        }
        else
        {
            actualColorPicker = GameObject.Find("ColorPicker(Clone)");
        }
        
    }
}
