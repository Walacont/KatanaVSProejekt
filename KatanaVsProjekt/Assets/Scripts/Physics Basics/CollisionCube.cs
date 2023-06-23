using System;
using TMPro;
using UnityEngine;

public class CollisionCube : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI collisionLabel;

    private void Start()
    {
        collisionLabel.SetText("");
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collisionLabel.SetText("Col ENTER");
    }
    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collisionLabel.SetText("Col STAY");
    }
    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collisionLabel.SetText("Col EXIT");
    }
}
