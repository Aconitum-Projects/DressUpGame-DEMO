using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreateColorPicker : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public GameObject colorHistoricPanel;
    public Button createButton;
    public GameObject colorPickerPanel;
    public Image SPRITE;

    private GameObject instantiatedObject;

    public FlexibleColorPicker fcp;

    public AudioSource colorSound;
    void Start()
    {
        createButton.onClick.AddListener(ToggleGameObject);
        colorHistoricPanel = GameObject.Find("_colorHistoric");

        if (colorHistoricPanel == null)
        {
            Debug.LogWarning("colorHistoricPanel not found!");
        }
    }

    private void Update()
    {
        createButton.image.color = SPRITE.color;
    }

    void ToggleGameObject()
    {
        if (instantiatedObject != null)
        {
            instantiatedObject.transform.DOScale(Vector3.zero, 0.05f).OnComplete(() =>
            {
                if (colorSound != null)
                {
                    colorSound.Play();
                }
                Destroy(instantiatedObject);
                instantiatedObject = null;
            });

            if (colorHistoricPanel != null)
            {
                colorHistoricPanel.transform.DOScale(Vector3.zero, 0.05f);
            }
        }
        else
        {
            if (prefabToInstantiate != null && colorPickerPanel != null)
            {
                DestroyChildren(colorPickerPanel);
                
                if (colorSound != null)
                {
                    colorSound.Play();
                }
                
                instantiatedObject = Instantiate(prefabToInstantiate, colorPickerPanel.transform);
                instantiatedObject.transform.DOScale(Vector3.one, 0.2f); // Use Vector3.one for clarity
                
                
                if (colorHistoricPanel != null)
                {
                    colorHistoricPanel.transform.DOScale(Vector3.one, 0.2f); // Use Vector3.one for clarity
                }
                
                fcp = instantiatedObject.GetComponent<FlexibleColorPicker>();
                fcp.ImageQuiVaChangerTMTC = SPRITE;
                
                SetStretchAnchors(instantiatedObject);
            }
        }
    }

    void SetStretchAnchors(GameObject obj)
    {
        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    void DestroyChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
