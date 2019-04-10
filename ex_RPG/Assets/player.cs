using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float speed = 30;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        // アニメーショントリガー
        if (Input.GetKey(KeyCode.LeftArrow))
        { 
            anim.SetTrigger("left");
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetTrigger("right");
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetTrigger("up");
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetTrigger("down");
        }

    }

    /*
    void OnCollisionEnter( Collision collision )
    {
        SceneManager.LoadScene("battle_Scene");
        Debug.Log("Load Scene");
        Debug.Log(collision.gameObject.name);
    }
    */

    
    private void OnTriggerEnter2D( Collider2D other )
    {
        SceneManager.LoadScene("battle_Scene");
    }
    
}
