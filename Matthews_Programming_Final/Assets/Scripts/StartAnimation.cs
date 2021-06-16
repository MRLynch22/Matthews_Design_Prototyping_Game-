using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    [SerializeField] private Animator _myAnimationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            _myAnimationController.SetBool("playAnimation", true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            _myAnimationController.SetBool("playAnimation", true);
        }

    }
}
