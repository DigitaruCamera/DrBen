using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour {
    public GameObject pauseobj;

    void pauseaction()
    {
        if (Input.GetButtonDown("Start"))
        {
            pauseobj.SetActive(!pauseobj.activeSelf);
            if (pauseobj.activeSelf == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    void Update () {
        pauseaction();
	}
}
