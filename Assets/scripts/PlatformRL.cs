using UnityEngine;

public class PlatformRL : MonoBehaviour
{
    public float Distance = 0;
    public float Height = 3;
    public float speed = 1;
    Vector2 Direction;

    // Use this for initialization
    void Start()
    {
        Direction = new Vector2(transform.position.x, transform.position.y);
    }
    bool dir;
    // Update is called once per frame
    void Update()
    {
        if (dir == true && transform.position.x > Direction.x + Distance)
        {
            speed *= -1;
            dir = false;
        } else if (dir == false && transform.position.x < Direction.x - Distance)
        {
            speed *= -1;
            dir = true;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        } else if (collision.gameObject.tag != "Player")
        {
            speed *= -1;
        }
    }
}