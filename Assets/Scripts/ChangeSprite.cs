using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public GameObject targetGameObject;
    public List<Sprite> sprites;
    private int currentSpriteIndex = 0;

    public Button right, left;
    
    private Image targetImage;

    void Start()
    {
        if (targetGameObject != null)
        {
            targetImage = targetGameObject.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Aucun GameObject cible assigné.");
            return;
        }

        if (targetImage == null)
        {
            Debug.LogError("Le GameObject cible n'a pas de composant Image.");
            return;
        }

        if (right != null)
        {
            right.onClick.AddListener(OnButtonClickRight);
        }
        else
        {
        }

        if (left != null)
        {
            left.onClick.AddListener(OnButtonClickLeft);
        }
        else
        {
        }
    }

    void OnButtonClickRight()
    {
        if (sprites.Count == 0)
        {
        }

        currentSpriteIndex = (currentSpriteIndex + 1) % sprites.Count;
        targetImage.sprite = sprites[currentSpriteIndex];
    }

    void OnButtonClickLeft()
    {
        if (sprites.Count == 0)
        {
        }

        currentSpriteIndex = (currentSpriteIndex - 1 + sprites.Count) % sprites.Count;
        targetImage.sprite = sprites[currentSpriteIndex];
    }
}