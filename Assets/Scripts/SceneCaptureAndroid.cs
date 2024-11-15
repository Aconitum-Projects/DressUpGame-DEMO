using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine.Android;

public class SceneCaptureAndroid : MonoBehaviour
{
    public Button captureButton;         // Bouton pour capturer l'écran
    public RawImage displayImage;        // RawImage pour afficher l'image capturée
    public Button saveButton;            // Bouton pour télécharger l'image
    public GameObject savePanel;         // Panneau pour afficher l'emplacement de sauvegarde
    public List<GameObject> objectsToHide; // Liste des objets à cacher pendant la capture

    private Texture2D screenTexture;
    private bool isSavePanelVisible = false; // État actuel du panneau de sauvegarde

    void Start()
    {
        captureButton.onClick.AddListener(ToggleSavePanel);
        saveButton.onClick.AddListener(SaveImage);
        savePanel.transform.localScale = Vector3.zero;

        // Demander la permission d'écriture dès le début
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
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

        int captureWidth = Screen.width;  // Utilise la largeur de l'écran
        int captureHeight = Screen.height;  // Utilise la hauteur de l'écran

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
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            return;
        }

        string fileName = "screenshot.png";
        byte[] bytes = screenTexture.EncodeToPNG();

        string folderPath = Path.Combine(Application.persistentDataPath, "DCIM/Camera");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string filePath = Path.Combine(folderPath, fileName);
        File.WriteAllBytes(filePath, bytes);

        // Scanner le fichier pour qu'il apparaisse dans la galerie
        AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection");
        mediaScanner.CallStatic("scanFile", new string[] { filePath }, new string[] { "image/png" }, null);

        Debug.Log("Image saved to gallery: " + filePath);

        savePanel.transform.DOScale(Vector3.zero, 0.05f);
        isSavePanelVisible = false;
    }
}
