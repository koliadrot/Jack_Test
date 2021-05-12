using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour, IObserverable
{
    #region Field Declarations
    public GameObject interfaceObject;
    protected Player player;

    [SerializeField] protected MeshRenderer indicateArea;
    #endregion

    #region Startup
    protected virtual void Start()
    {
        GameSceneController.Instance.AddObserver(this);//Subscribe on observer
    }
    #endregion

    #region Subject Implementation

    #region Actions
    public virtual void OnAcionInteraction() { }//Method for call outside
    #endregion

    #region Collisions
    protected virtual void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        if (other.CompareTag("Player"))
        {
            indicateArea.material.color=Color.white;
            player = other.GetComponent<Player>();
        }
    }
    protected virtual void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        if (other.CompareTag("Player"))
        {
            indicateArea.material.color = Color.grey;
            player = null;
        }
    }
    #endregion

    #region Observer Action
    public virtual void Notify()
    {
        interfaceObject.SetActive(false);
    }
    #endregion

    #endregion
}
