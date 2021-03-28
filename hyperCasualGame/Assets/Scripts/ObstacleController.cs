using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Rigidbody[] rigidbodies;
    private MeshRenderer[] meshRenderers;
    private Collider[] colliders;
   

    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        colliders = GetComponentsInChildren<Collider>();
    }
     public void ShatterAllObstacles()
    {        
        ShatterAnimation();
        StartCoroutine(RemoveAllShatterParts());


    }
    public void ShatterAnimation()
    {   Vector3 forcePoint = transform.position;
        float xPosition = transform.position.x;
        Vector3[] subDirection = new Vector3[transform.childCount];
        Vector3[] direction = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            rigidbodies[i].isKinematic = false;
            colliders[i].enabled = false;
            float childrenXPosition = meshRenderers[i].bounds.center.x;
            subDirection[i] = (xPosition - childrenXPosition < 0) ? Vector3.right : Vector3.left;
            direction[i] = (Vector3.up * 1.5f + subDirection[i]).normalized;
            float force = UnityEngine.Random.Range(20, 35);
            float torque = UnityEngine.Random.Range(110, 180);    
            rigidbodies[i].AddForceAtPosition(direction[i] * force, forcePoint, ForceMode.Impulse);
            rigidbodies[i].AddTorque(Vector3.left * torque);
            rigidbodies[i].velocity = Vector3.down;
        }
    }

    IEnumerator RemoveAllShatterParts()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        
    }

}
