using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrackUI : MonoBehaviour
{
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform Subject;
    [SerializeField] private Text targetText; // Add this line

    void Start()
    {
        if (targetText != null)
        {
            targetText.enabled = false;
        }
    }

    void Update()
    {
        if (Subject)
        {
            transform.position = PlayerCamera.WorldToScreenPoint(Subject.position);
        }
    }

     public void SetTarget(Transform target)
    {
        Subject = target;

        if (targetText != null)
        {
            targetText.enabled = true;
            targetText.transform.position = PlayerCamera.WorldToScreenPoint(Subject.position);
        }
    }
}