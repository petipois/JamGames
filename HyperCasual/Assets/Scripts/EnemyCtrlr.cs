using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrlr : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    public Transform target;
    public GameObject deathEffect,clickEffect;
    bool moveAllowed = false;
    Collider2D col;
    private void Awake()
    {
        target = GameSessionManager.instance.player.transform;
        col = this.GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
       TouchControls();
       transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    
    void TouchControls()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCol = Physics2D.OverlapPoint(touchPos);
                if (col == touchedCol)
                {

                    Instantiate(clickEffect, transform.position, Quaternion.identity);
                    Destroy(col.gameObject);
                }
            }

            /*            if(touch.phase == TouchPhase.Moved)
                        {
                            if (moveAllowed)
                                transform.position = new Vector2(touchPos.x, touchPos.y);
                        }

                        if(touch.phase == TouchPhase.Ended)
                        {
                            moveAllowed = false;            }
                        }*/
        }
    }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameSessionManager.instance.HitPlayer();
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            }
            Destroy(gameObject);

        }
    }
