using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using TMPro;
using System.Runtime.InteropServices;

public class SceneCapture : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveFile(byte[] array, int length, string fileName);

    public Button captureButton;         // Bouton pour capturer l'écran
    public RawImage displayImage;        // RawImage pour afficher l'image capturée
    public Button saveButton;            // Bouton pour télécharger l'image
    public GameObject savePanel;         // Panneau de sauvegarde

    public List<GameObject> objectsToHide; // Liste des objets à cacher pendant la capture

    private Texture2D screenTexture;
    private bool isSavePanelVisible = false; // État actuel du panneau de sauvegarde

    void Start()
    {
        captureButton.onClick.AddListener(ToggleSavePanel);
        saveButton.onClick.AddListener(SaveImage);
        savePanel.transform.localScale = Vector3.zero; // Initialiser l'échelle du panneau à 0
    }

    void ToggleSavePanel()
    {
        if (isSavePanelVisible)
        {
            savePanel.transform.DOScale(Vector3.zero, 0.05f);
        }
        else
        {
            StartCoroutine(Capture());
        }

        isSavePanelVisible = !isSavePanelVisible;
    }

    System.Collections.IEnumerator Capture()
    {
        HideObjects();

        yield return new WaitForEndOfFrame();

        int captureWidth = Screen.width;
        int captureHeight = Screen.height;

        screenTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);
        Rect rect = new Rect(0, 0, captureWidth, captureHeight);
        screenTexture.ReadPixels(rect, 0, 0);
        screenTexture.Apply();

        ShowObjects();

        displayImage.texture = screenTexture;
        savePanel.transform.DOScale(Vector3.one, 0.2f);
    }

    void HideObjects()
    {
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
    }

    void ShowObjects()
    {
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(true);
        }
    }

    void SaveImage()
    {
        string fileName = "screenshot.png";
        byte[] bytes = screenTexture.EncodeToPNG();
        SaveFileInBrowser(fileName, bytes);

        Debug.Log("Image saved to: " + fileName);
        savePanel.transform.DOScale(Vector3.zero, 0.05f);
        isSavePanelVisible = false;
    }

    void SaveFileInBrowser(string fileName, byte[] bytes)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SaveFile(bytes, bytes.Length, fileName);
#else
        // For non-WebGL platforms, we can save to the persistent data path
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllBytes(filePath, bytes);
        Debug.Log("File saved to: " + filePath);
#endif
    }
}
  