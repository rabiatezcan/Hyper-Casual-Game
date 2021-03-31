using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playerControl : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool collisionControl;
    private float currentTime;
    private bool isInvintable;
    private GameObject fireEffect;
    private int planeCounter;
    private AudioSource audioSource; 
    public AudioClip normalBreak;
    public AudioClip invintableBreak;
    public AudioClip jump;

    // UI FEATURES
    private int breakCounter = 0;
    private int obstacleCount;
    public Image fillBackground;
    public Text currentLevelText;
    public Text nextLevelText;
    public Image invintableFill;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        fireEffect = transform.GetChild(0).gameObject;
        fireEffect.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        planeCounter = 0;
        currentLevelText.text = "" + PlayerPrefs.GetInt("level");
        nextLevelText.text = "" + (PlayerPrefs.GetInt("level") + 1);
        invintableFill.transform.gameObject.SetActive(false); 
        obstacleCount = ((ringControl.addNumber + PlayerPrefs.GetInt("level")) * 2) + 1;
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

        if (invintableFill.IsActive())
        {
            invintableFill.transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = currentTime;
        }

        levelSliderFill((float)((float)breakCounter / obstacleCount));

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
            audioSource.clip = jump;
            audioSource.Play();
            rigidbody.velocity = new Vector3(0, 250 * Time.fixedDeltaTime, 0);
        }
        else
        {
            if (isInvintable)
            {
                invintableFill.transform.gameObject.SetActive(true);
                audioSource.clip = invintableBreak;
                audioSource.Play();
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                      collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    breakCounter++;
                }                 
            }
            else
            {
                invintableFill.transform.gameObject.SetActive(false);
                if (collision.gameObject.tag == "enemy")
                   {
                       audioSource.clip = normalBreak;
                       audioSource.Play();
                       collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                       breakCounter++;

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

    public void levelSliderFill(float fillAmount)
    {
        fillBackground.fillAmount = fillAmount; 
        if(fillAmount >= 1) {
            nextLevelText.transform.parent.transform.parent.gameObject.GetComponent<Image>().color= new Color(255,76,76,255);
        }

    }
}
