using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVacuum : MonoBehaviour {

    int dangleAnim = Animator.StringToHash("Dangle");           // animation parameter for Dangle
    int idleAnim = Animator.StringToHash("Idle");               // animation parameter for Idle
    bool playerInVacuum = false;                                // toogle bool to apply force for one frame
    Rigidbody playerRigidbody;                                  // refrence to player rigidbody
    Animator playerAnimator;                                    // refrence to player animator

    public GameObject Player;                                   // refrence to our player
    public Transform LerpPoint;                                 // transform attached to motion controller located in front of it
    public float LerpMod = 2.0f;                                // value multiplied by Time.deltaTime in lerp
    public float ForceMod = 100.0f;                             // force applied to player after trigger is released

    // cache refrences to rb and animator
    void Start()
    {
        playerRigidbody = Player.GetComponent<Rigidbody>();
        playerAnimator = Player.GetComponent<Animator>();
    }

    void Update()
    {
        // check if Trigger is full compressed, the trigger axis goes from 0.0 to 1.0
        if (Input.GetAxis("PrimaryIndexTrigger") == 1.0f)
        {
            playerRigidbody.isKinematic = true;
            playerAnimator.SetTrigger(dangleAnim);

            // move the players position towards the lerpPoint over time
            Player.transform.position = Vector3.Lerp(Player.transform.position, LerpPoint.position, Time.deltaTime * LerpMod);

            playerInVacuum = true;
        }
        // if the trigger is released and the trigger was fully compressed do this for one frame
        else if (playerInVacuum)
        {
            // direction based on players position, motion controller with slight upward force added
            Vector3 shootDirection = (LerpPoint.transform.position + Vector3.up) - this.transform.position;

            playerRigidbody.isKinematic = false;
            // apply force to player based on shoot direction vector
            playerRigidbody.AddForce(shootDirection * ForceMod);
            // set players animation back to idle
            playerAnimator.SetTrigger(idleAnim);
            // flip this toogle so the force is only applied for one frame
            playerInVacuum = false;
        }
    }
}
