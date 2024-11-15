using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Megaman : MonoBehaviour
{
    public float speed;
    public GameObject bullet;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spr;

    // Start is called before the first frame update
    void Start()
    {
        _spr=GetComponent<SpriteRenderer>();
        _rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            _spr.flipX = false;
            _rb.velocity=new Vector2(speed,0);
            //_rb.MovePosition(new Vector2(transform.position.x+speed*Time.deltaTime, transform.position.y));
            //transform.Translate(speed*Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _spr.flipX = true;
            _rb.velocity = new Vector2(-speed, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rb.velocity = new Vector2(0, speed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _rb.velocity = new Vector2(0, -speed);
        }
        if (!Input.anyKey)
            _rb.velocity = Vector2.zero;
    }
}
