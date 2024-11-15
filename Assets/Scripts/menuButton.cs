using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    public GameObject panelToSetActive;


    public AudioSource panelSound, closingSound;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ToggleObject);
    }

    void ToggleObject()
    {
        if (panelToSetActive == null || panelToSetActive.transform.parent == null)
        {
            return;
        }

        Transform parentTransform = panelToSetActive.transform.parent;

        bool isCurrentlyActive = panelToSetActive.activeSelf;

        foreach (Transform child in parentTransform)
        {
            if (child.gameObject != panelToSetActive)
            {
                child.transform.DOScale(Vector3.zero, 0.05f).OnComplete(() =>
                {
                    child.gameObject.SetActive(false);
                });
               
            }
        }

        if (isCurrentlyActive)
        {
            if (closingSound != null)
            {
                closingSound.Play();
            }

            panelToSetActive.transform.DOScale(Vector3.zero, 0.05f).OnComplete(() =>
            {
                panelToSetActive.SetActive(false);
            });
        }
        else
        {
            if (panelSound != null)
            {
                panelSound.Play();
            }

            panelToSetActive.SetActive(true);
            panelToSetActive.transform.DOScale(Vector3.one, 0.2f);
        }
    }
}