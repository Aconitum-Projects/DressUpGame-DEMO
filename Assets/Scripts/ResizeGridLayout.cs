using UnityEngine;
using UnityEngine.UI;

public class ResizeGridLayout : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup; // Référence au GridLayoutGroup
    public RectTransform parentRectTransform; // Référence au RectTransform parent à ajuster

    public float initialRowSpacing = 20f; // Espacement initial entre les lignes en pixels

    private int previousChildCount;
    private float paddingHeight;
    private float paddingWidth;

    void Start()
    {
        if (gridLayoutGroup == null || parentRectTransform == null)
        {
            Debug.LogError("Grid Layout Group ou Parent Rect Transform non défini.");
            return;
        }

        // Calculer les paddings initiaux
        CalculateInitialPaddings();

        // Mémoriser le nombre initial d'enfants
        previousChildCount = gridLayoutGroup.transform.childCount;

        // Appeler AdjustParentSize une fois au début pour ajuster la taille initiale
        AdjustParentSize();
    }

    void LateUpdate()
    {
        // Vérifier si le nombre d'enfants a changé
        if (gridLayoutGroup.transform.childCount != previousChildCount)
        {
            // Mettre à jour le nombre d'enfants
            previousChildCount = gridLayoutGroup.transform.childCount;

            // Ajuster la taille du parent
            AdjustParentSize();
        }
    }

    void CalculateInitialPaddings()
    {
        // Calculer la taille nécessaire en fonction du nombre de lignes et de colonnes
        int rowCount = Mathf.CeilToInt((float)gridLayoutGroup.transform.childCount / gridLayoutGroup.constraintCount);
        int columnCount = Mathf.Min(gridLayoutGroup.constraintCount, gridLayoutGroup.transform.childCount);

        // Calculer la hauteur nécessaire
        float cellHeight = gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y;
        paddingHeight = (rowCount - 1) * gridLayoutGroup.spacing.y + initialRowSpacing; // Ajout de l'espacement entre les lignes seulement au début

        // Calculer la largeur nécessaire
        float cellWidth = gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x;
        paddingWidth = (columnCount - 1) * gridLayoutGroup.spacing.x + initialRowSpacing; // Ajout de l'espacement entre les colonnes seulement au début
    }

    void AdjustParentSize()
    {
        // Calculer la taille nécessaire en fonction du nombre de lignes et de colonnes
        int rowCount = Mathf.CeilToInt((float)gridLayoutGroup.transform.childCount / gridLayoutGroup.constraintCount);
        int columnCount = Mathf.Min(gridLayoutGroup.constraintCount, gridLayoutGroup.transform.childCount);

        // Calculer la hauteur nécessaire
        float cellHeight = gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y;
        float newHeight = rowCount * cellHeight + paddingHeight;

        // Calculer la largeur nécessaire
        float cellWidth = gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x;
        float newWidth = columnCount * cellWidth + paddingWidth;

        // Appliquer la nouvelle taille au RectTransform parent
        parentRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
