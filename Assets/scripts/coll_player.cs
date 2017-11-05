using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coll_player : MonoBehaviour {
    public bool falling = true;

	void Start () {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), transform.parent.gameObject.GetComponent<Collider2D>());
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        falling = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        falling = true;
    }
}
