using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour {
    float change_delayed = 0;
    float delay_day = 10f;
    float delay_night = 1f;
    public float speed = 1;
    bool is_day = true;
    GameObject day_cam;
    GameObject night_cam;
    private void Start()
    {
        day_cam = transform.GetChild(0).gameObject;
        night_cam = transform.GetChild(1).gameObject;
        change_delayed = Time.time + delay_day;
        day_cam.SetActive(true);
        night_cam.SetActive(false);
    }

    void Update ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed , 0);
        if (is_day && change_delayed < Time.time)
        {
            is_day = false;
            change_delayed = Time.time + delay_night;
            day_cam.SetActive(false);
            night_cam.SetActive(true);
        }
        else if (!is_day && change_delayed < Time.time)
        {
            delay_day -= delay_day * 0.05f;
            speed += speed * 0.01f;
            delay_day = Mathf.Clamp(delay_day, 2, 10);
            is_day = true;
            change_delayed = Time.time + delay_day;
            day_cam.SetActive(true);
            night_cam.SetActive(false);
        }
    }
}
