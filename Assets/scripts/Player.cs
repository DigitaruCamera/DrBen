using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float default_speed = 5;
    public float speed = 5;
    public float jumps = 2;
    public int HPMax = 3;
    public GameObject dead;
    public Text best_score;
    public Text hp_ui;
    public Text points_ui;
    public string playercontrol = "";
    private int HP;
    float points = 0;
    // Use this for initialization
    void Start () {
        HP = HPMax;
        Time.timeScale = 1;
        speed = default_speed;
    }

    float delayed_time = 0;

    void mouv ()
    {
        Rigidbody2D rg = GetComponent<Rigidbody2D>();
        rg.velocity = new Vector2(Input.GetAxis("Horizontal" + playercontrol) * speed, Mathf.Clamp(Input.GetAxis("Vertical" + playercontrol), -1, 0) * (speed / 2));
        if (Input.GetAxis("Vertical" + playercontrol) < -0.2f)
        {
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            speed = default_speed * 0.75f;
        } else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            speed = default_speed;
        }
        if (Input.GetAxis("Horizontal" + playercontrol) > 0.1)
        {
            print("walk");
            GetComponent<Animation>().Play("walk");
            transform.GetChild(1).position = transform.position + new Vector3(0.4f, 0, 0);
        } else if (Input.GetAxis("Horizontal" + playercontrol) < -0.1)
        {
            GetComponent<Animation>().Play("walk");
            transform.GetChild(1).position = transform.position + new Vector3(-0.4f, 0, 0);
        } else
        {
            GetComponent<Animation>().Play("idle");
        }
        bool falling = transform.GetChild(0).gameObject.GetComponent<coll_player>().falling;
        if (falling == true && rg.gravityScale <= 9.81f)
        {
            rg.gravityScale += Time.deltaTime * 50;
        }
        else if (falling == false && delayed_time < Time.time)
        {
            jumps = 2;
            rg.gravityScale = 0.3f;
        }
        else if (rg.gravityScale > 9.81f)
        {
            rg.gravityScale = 9.81f;
        }
        if (Input.GetButtonDown("Fire1" + playercontrol) && jumps > 0)
        {
            GetComponent<Animation>().Play("jump");
            delayed_time = Time.time + 0.1f;
            jumps--;
            rg.gravityScale = -40;
        }
    }

    void aff()
    {
        float gelpercent = ((gel_delayed - Time.time) / gel_delay) * 100;
        gelpercent = Mathf.Clamp(gelpercent, 0, 100);
        hp_ui.text = "Player" + playercontrol + " : " + HP + " / " + HPMax + " HP :: " + (int) (100 - gelpercent) + "%";
        points_ui.text = "Player" + playercontrol + " : " + (int) points + "points";
    }


    float delayed_death = 0;
    float delay_death = 2;
    void life()
    {
        Rigidbody2D rg = GetComponent<Rigidbody2D>();
        if (HP > 0)
        {
            delayed_death = Time.time + delay_death;
            GetComponent<Collider2D>().isTrigger = false;
        } else
        {
            GetComponent<Collider2D>().isTrigger = true;
            rg.velocity = new Vector2(0, 0);
            rg.gravityScale = -1;
            immortalFrame = Time.time + immortalDelay;
        }
        if (delayed_death < Time.time)
        {
            PlayerPrefs.SetFloat("actual", (int) points);
            if (PlayerPrefs.GetFloat("best") < points)
            {
                PlayerPrefs.SetFloat("best", points);
            }
            if (GameObject.FindGameObjectsWithTag("Player").Length <= 1)
            {
                best_score.text = "DEAD\nbest score : " + PlayerPrefs.GetFloat("best");
                dead.SetActive(true);
            }
            Destroy(gameObject);
        }
    }

    float clign_delayed = 0;
    float clign_delay = 0.1f;
    void clignote()
    {
        if (clign_delayed < Time.time)
        {
            clign_delayed = clign_delay + Time.time;
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
        }
    }

    float gel_delayed = 0;
    float active_gel_delayed = 0;
    float gel_delay = 10f;
    bool gel_stopped = false;
    void gel()
    {
        if (Input.GetButtonDown("Fire2" + playercontrol) && gel_delayed < Time.time && Time.timeScale != 0.5f)
        {
            gel_stopped = false;
            gel_delayed = Time.time + gel_delay;
            active_gel_delayed = Time.time + gel_delay / 3;
            Time.timeScale = 0.5f;
        }
        if (active_gel_delayed < Time.time && !gel_stopped)
        {
            gel_stopped = true;
            Time.timeScale = 1f;
        }
    }

    private float att_delayed = 0;
    private float att_delay = 1;
    void att()
    {
        if (Input.GetButtonDown("Fire3" + playercontrol) && att_delayed < Time.time)
        {
            att_delayed = Time.time + att_delay;
            transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = true;
        } else
        {
            transform.GetChild(1).gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

	void FixedUpdate () {
        points += Time.deltaTime;
        mouv();
        aff();
        life();
        att();
        if (Time.time < immortalFrame)
        {
            clignote();
        } else
        {
            GetComponent<Renderer>().enabled = true;
        }
        gel();
    }

    float immortalFrame = 0;
    float immortalDelay = 2;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && Time.time > immortalFrame)
        {
            immortalFrame = Time.time + immortalDelay;
            HP--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().HP = 1;
        }

        if (collision.tag == "points")
        {
            Destroy(collision.gameObject);
            points += 5;
        }
    }
}