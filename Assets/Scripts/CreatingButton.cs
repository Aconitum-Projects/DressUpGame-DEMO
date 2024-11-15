using UnityEngine;
using UnityEngine.UI;

public class CreatingButton : MonoBehaviour
{
    public Button button;
    public GameObject create,whereToCreate;
    public GameObject destroyButton;
    public Color savedColor;

    void Start()
    {
        button = GetComponent<Button>();
        create = transform.parent.gameObject;
        whereToCreate = create.transform.parent.gameObject;

        button.onClick.AddListener(CreateButtton);
    }

    void CreateButtton()
    {
        destroyButton.SetActive(true);
        Instantiate(create, whereToCreate.transform);
        savedColor = create.GetComponent<ColorHistoric>().actualColor;
        create.GetComponent<ColorHistoric>().enabled = false;
        transform.gameObject.SetActive(false);
    }
}