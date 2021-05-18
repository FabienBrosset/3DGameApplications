using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{

    private bool fly = true;

    void Start()
    {
        
    }


    void Update()
    {
        if (fly)
            transform.Translate(Vector3.up * Time.deltaTime, Space.World);

        Collider[] hitColliders = Physics.OverlapSphere(this.transform.GetComponent<Renderer>().bounds.center, this.GetComponent<Renderer>().bounds.size.y);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag != "Player" && hitCollider.transform.tag != "DrawingSpot" && hitCollider.transform.tag != "BalloonInk")
            {
                fly = false;
            }
        }
        

    }
    /*
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(this.transform.GetComponent<Renderer>().bounds.center, this.GetComponent<Renderer>().bounds.size.y);
    }*/

}
