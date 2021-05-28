using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportInteractive : MonoBehaviour, ICollisionable
{
    #region Collisions
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "Local 2")
                GameSceneController.Instance.Teleport("Local 1");
            else
                GameSceneController.Instance.Teleport("Local 2");
        }     
    }
    public void OnTriggerExit(Collider other)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
