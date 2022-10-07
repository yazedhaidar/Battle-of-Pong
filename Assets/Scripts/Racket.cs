using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public string axis = "Vertical";
    private Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (axis == "Vertical2" && GameData.instance.isSinglePlayer) 
        {
            return;
        }
        //  Ngambil variabel dari Axis yang sudah di setting di Unity Input dengan output (-1,1)
        float v = Input.GetAxis(axis);
        rb.velocity = new Vector2(0, v) * speed;

        //Agar tidak keluar batas atas
        if (transform.position.y > 1f)
        {
            transform.position = new Vector2(transform.position.x, 1f);
        }

        //Agar tidak keluar batas atas
        if (transform.position.y < -1f)
        {
            transform.position = new Vector2(transform.position.x, -1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Anim.SetTrigger("Shoot");
        }
    }
}
