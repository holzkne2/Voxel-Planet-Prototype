using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeScreenShot : MonoBehaviour {

    public KeyCode Button = KeyCode.F;

	void Update () {
        if (Input.GetKeyDown(Button))
        {
            Application.CaptureScreenshot(SceneManager.GetActiveScene().name + "_" + System.DateTime.Now.ToString("dd.MM.yy_h.mm.ss") + ".png");
        }
	}
}
