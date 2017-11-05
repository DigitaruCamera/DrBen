using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class back : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "map" || collision.tag == "enemy" || collision.tag == "points")
        {
            Destroy(collision.gameObject, 3);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "map" || collision.gameObject.tag == "enemy" || collision.gameObject.tag == "points")
        {
            Destroy(collision.gameObject, 3);
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
