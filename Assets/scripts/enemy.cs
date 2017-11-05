using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2;
    public float PatternSize = 10;
    
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, Mathf.Sin(transform.position.x) * PatternSize);
    }
}