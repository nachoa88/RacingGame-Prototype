using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilEffects : MonoBehaviour
{
    public float speedDecrease = -25.0f;
    public float effectDuration = 5.0f;

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
        // We get acces to the script of the other object.
        PlayerController playerController = other.gameObject.GetComponentInParent<PlayerController>();
        // If the "other" has a "player controller" component, is not = null, then do the thing.
        if (playerController != null)
        {
            // powerup sequence
            StartCoroutine(OilEffecSequence(playerController));
        }
    }
    public IEnumerator OilEffecSequence(PlayerController playerController)
        {
            // soft disable
            GetComponent<MeshRenderer>().enabled = false;
            ActivateEffects(playerController);
            // wait for an amount of time
            yield return new WaitForSeconds(effectDuration);
            DeactivateEffects(playerController);
            // Destroy or maybe reenable if you want it to be there again after an X amount of time.
            Destroy(gameObject);
        }

    private void ActivateEffects(PlayerController playerController)
        {
            playerController.SetMoveSpeed(speedDecrease);
        }

    private void DeactivateEffects(PlayerController playerController)
        {
            playerController.SetMoveSpeed(-speedDecrease);
        }
}
