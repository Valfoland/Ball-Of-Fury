using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource soundButton;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.currentSelectedGameObject)
        {
            soundButton.Play();
        }
    }
}
