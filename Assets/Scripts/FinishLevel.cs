using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    
    

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1f);

        foreach (Collider col in hitColliders)
        {
            if (col.tag == "End")
                SceneManager.LoadScene("MenuScene");
        }
    }
}
