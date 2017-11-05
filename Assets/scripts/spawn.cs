using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {
    public GameObject[] obj;
    public float delay_min = 0;
    public float delay_max = 5;
    public float rangeX = 3;
    public float destroy_time = 15;
    float delayed = 0;
	void Update () {
        if (Time.time > delayed)
        {
            delayed = Time.time + Random.Range(delay_min, delay_max);
            GameObject clone = Instantiate(obj[(int)Random.Range(0, obj.Length)], transform.position + new Vector3(0, Random.Range(-rangeX, rangeX), 0), transform.rotation) as GameObject;
            Destroy(clone, destroy_time);
        }
    }
}
