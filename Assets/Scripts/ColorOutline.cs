using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorOutline : MonoBehaviour
{
    [Tooltip("Image component whose color will be used as the base color.")]
    public Image sourceImage;

    [Tooltip("Color to multiply with the source image's color.")]
    public Color multiplyColor = Color.white;

    private Image targetImage;

    void Start()
    {
        // Get the Image component attached to this GameObject
        targetImage = GetComponent<Image>();

        if (sourceImage == null)
        {
            Debug.LogWarning("Source Image is not set. Please assign a source Image component.");
        }
    }

    void Update()
    {
        if (sourceImage != null)
        {
            // Get the color from the source image
            Color sourceColor = sourceImage.color;

            // Multiply the source color by the editable color
            Color finalColor = new Color(
                sourceColor.r * multiplyColor.r,
                sourceColor.g * multiplyColor.g,
                sourceColor.b * multiplyColor.b,
                sourceColor.a * multiplyColor.a
            );

            // Apply the final color to the target image
            targetImage.color = finalColor;
        }
    }
}