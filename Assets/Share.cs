using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

public class Share : MonoBehaviour {

    public string screenshotName = "screenshot.png";

    public void ShareScreenshotWithText(string text)
    {
        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);
        ScreenCapture.CaptureScreenshot(screenshotName);
        StartCoroutine(DelayedShare(screenShotPath, text));
    }

    //CaptureScreenshot runs asynchronously, so you'll need to either capture the screenshot early and wait a fixed time
    //for it to save, or set a unique image name and check if the file has been created yet before sharing.
    IEnumerator DelayedShare(string screenShotPath, string text)
    {
        while (!File.Exists(screenShotPath))
        {
            // WaitForSeconds freezes if the game is paused (Time.timeScale = 0). 
            // This method is not affected by this.
            yield return new WaitForSecondsRealtime(.05f);
        }

        NativeShare.Share(text, screenShotPath, "", "", "image/png", true, "");
    }
}