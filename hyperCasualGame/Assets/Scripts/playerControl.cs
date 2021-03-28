using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerControl : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool collisionControl;
    private float currentTime;
    private bool isInvintable;
    private GameObject fireEffect;
    private int planeCounter;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        fireEffect = transform.GetChild(0).gameObject;
        fireEffect.SetActive(false);
        planeCounter = 0;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            collisionControl = true; 
        }
        if (Input.GetMouseButtonUp(0))
        {
            collisionControl = false;
        }
        if (isInvintable)
        {
            currentTime -= Time.deltaTime * .35f;
            fireEffect.SetActive(true);
        }
        else
        {
            fireEffect.SetActive(false);
            if (collisionControl)
            {
                currentTime += Time.deltaTime * .8f;
            }
            else
            {
                currentTime -= Time.deltaTime * .5f;
            }

        }

        if(currentTime >= 1)
        {
            currentTime = 1; 
            isInvintable = true;
        }else if(currentTime <= 0)
        {
            currentTime = 0; 
            isInvintable = false; 
        }
    }

    private void FixedUpdate()
    {
            if (collisionControl)
            {
                rigidbody.velocity = new Vector3(0,-350*Time.fixedDeltaTime,0);
            } 
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (!collisionControl)
        {
            rigidbody.velocity = new Vector3(0, 250 * Time.fixedDeltaTime, 0);
        }
        else
        {
            if (isInvintable)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                      collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                }
                  
            }
            else
            {
                   if (collision.gameObject.tag == "enemy")
                   {
                       collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();

                   }
                   else if (collision.gameObject.tag == "plane")
                   {
                       if(planeCounter < 3)
                       {
                         planeCounter++;
                       }
                       else
                       {
                           PlayerPrefs.SetInt("finishLevel", 0);
                           Invoke("levelTransition", .4f);
                       }
                   }
            }

            if(collision.gameObject.tag == "Finish")
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
                PlayerPrefs.SetInt("finishLevel", 1);
                Invoke("levelTransition", .4f);
            }
            
        }
       
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!collisionControl)
        {
            rigidbody.velocity = new Vector3(0, 250 * Time.fixedDeltaTime, 0);
        }
    }

    public void levelTransition()
    {
        SceneManager.LoadScene(2);
    }
}
