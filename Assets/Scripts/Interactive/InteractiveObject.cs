using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public abstract class InteractiveObject : MonoBehaviour, IObserverable, ICollisionable
{
    #region Field Declarations
    public event Action InteractiveAction = () => { };

    public GameObject interfaceObject;
    [SerializeField] protected Toggle activateBox;
    [SerializeField] protected MeshRenderer indicateArea;
    protected bool playerEntered;
    #endregion

    #region Startup
    protected virtual void Start()
    {
        GameSceneController.Instance.AddObserver(this);//Subscribe on observer
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    protected abstract void SubscribeOnAction(bool status);//Subscribe or unsubscribe at/from event
    #endregion

    #region Actions
    public virtual void OnAcionInteraction()//Method for call outside
    {
        if (playerEntered && activateBox.isOn)
            InteractiveAction();
    }
    #endregion

    #region Collisions
    public virtual void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        if (other.CompareTag("Player"))
        {
            indicateArea.material.color = Color.white;
            playerEntered = true;
            SubscribeOnAction(true);//Subscribe actions at event
        }
    }
    public virtual void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        if (other.CompareTag("Player"))
        {
            indicateArea.material.color = Color.grey;
            playerEntered = false;
            SubscribeOnAction(false);//Unsubscribe actions from event
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
