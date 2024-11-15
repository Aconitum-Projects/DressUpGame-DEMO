using UnityEngine;
using UnityEngine.UI;

public class DestroyingButton : MonoBehaviour
{
    public Button destroyButton;
    public GameObject colorButtonToDestroy;
    public GameObject previousSibling;

    void Start()
    {
        previousSibling = GetPreviousSibling(); // Assign the previous sibling here
        destroyButton = GetComponent<Button>();
        colorButtonToDestroy = transform.parent.gameObject;

        destroyButton.onClick.AddListener(DestroyButton);
    }

    GameObject GetPreviousSibling()
    {
        Transform parentTransform = colorButtonToDestroy.transform.parent; // Get the parent transform

        int siblingIndex = colorButtonToDestroy.transform.GetSiblingIndex();

        if (siblingIndex == 0)
        {
            // This is the first child, so there is no previous sibling
            return null;
        }

        // Get the previous sibling
        Transform previousSiblingTransform = parentTransform.GetChild(siblingIndex - 1);

        return previousSiblingTransform.gameObject;
    }

    void DestroyButton()
    {
        // Check if destroy is the last child of its parent
        Transform parentTransform = colorButtonToDestroy.transform.parent;
        int siblingIndex = colorButtonToDestroy.transform.GetSiblingIndex();
        int lastIndex = parentTransform.childCount - 1;

        bool isLastChild = siblingIndex == lastIndex;

        if (previousSibling != null && isLastChild)
        {
            var colorHistoric = previousSibling.GetComponent<ColorHistoric>();
            if (colorHistoric != null)
            {
                colorHistoric.enabled = true;
                
                var addButton = previousSibling.transform.Find("add");
                addButton.gameObject.SetActive(true);
            }
        }

        Destroy(colorButtonToDestroy);
    }
}