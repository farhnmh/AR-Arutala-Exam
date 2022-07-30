using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ScreenshotHandler : MonoBehaviour
{
    public GameObject settingCanvas;
    public TimeConvertHandler timeConverter;
    public TextMeshProUGUI detailScreenshotTime;
    public string shareSubject, shareMessage;

    public void Screenshot()
    {
        StartCoroutine(SaveAndShareScreenshot());
    }

    IEnumerator SaveAndShareScreenshot()
    {
        settingCanvas.SetActive(false);
        detailScreenshotTime.text = timeConverter.resultText;

        yield return new WaitForEndOfFrame();

        //take screenshot file
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        //save screenshot file
        string fileName = $"Screenshot {System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.png";
        NativeGallery.SaveImageToGallery(texture, "Arutala Screenshot Exam", fileName);

        //share screenshot file
        string filePath = Path.Combine(Application.temporaryCachePath, fileName);
        File.WriteAllBytes(filePath, texture.EncodeToPNG());
        new NativeShare().AddFile(filePath).SetSubject(shareSubject).SetText(shareMessage).Share();

        yield return new WaitForEndOfFrame();

        Destroy(texture);
        settingCanvas.SetActive(true);
        detailScreenshotTime.text = string.Empty;
    }
}