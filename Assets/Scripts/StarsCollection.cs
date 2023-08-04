using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsCollection : MonoBehaviour
{
    /* This one is to deactivate the effect that will be played after collecting stars.
    void Awake()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            //Here will play the animation after collection.
            //gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(gameObject, 0.5f);
        }
    }
}
