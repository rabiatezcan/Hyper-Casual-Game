using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class ringControl : MonoBehaviour
{
    public GameObject[] obstacleModel;
    GameObject[] obstaclePrefab = new GameObject[4];
    public GameObject winPrefab;
    GameObject tempObs, tempObs2;
    public static int addNumber = 7;
    float speed = 80f;
    public int levelCounter = 1;
    [HideInInspector]
    const int modelCount = 4;
    const int totalPartNumber = 4;
    void Start()
    {
        levelCounter = PlayerPrefs.GetInt("level");
        Debug.Log("level = " + levelCounter);
        randomObsGenerator();
        float i = 0;
        while (i > (-levelCounter - addNumber))
        {

            tempObs = Instantiate(obstaclePrefab[Random.Range(0,totalPartNumber)]);
            tempObs.transform.position = new Vector3(0, i-0.01f, 0);
            tempObs.transform.eulerAngles = new Vector3(0,i*8,0);
            if (Mathf.Abs(i) >= (levelCounter * 0.3f) && Mathf.Abs(i) <= (levelCounter * 0.6f))
            {
                tempObs.transform.eulerAngles += Vector3.up * 180;

            }
            else
            {
                tempObs.transform.eulerAngles += Vector3.up * 90;

            }
            tempObs.transform.parent = gameObject.transform; 
            i -= 0.5f;
        }

        tempObs2 = Instantiate(winPrefab);
        tempObs2.transform.position = new Vector3(0, i - 0.01f,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,speed*Time.deltaTime,0));
    }
     
    public void randomObsGenerator()
    {
        int randomNumber = Random.Range(0,5);
         for (int i = 0; i < modelCount; i++)
        {
            obstaclePrefab[i] = obstacleModel[i + (randomNumber*modelCount)];
        }
    }

   
}
