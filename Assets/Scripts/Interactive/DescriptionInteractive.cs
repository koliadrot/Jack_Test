using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DescriptionInteractive : InteractiveObject
{
    #region Field Declarations
    public event Action InteractiveAction = () => { };
    
    [SerializeField] private bool description = true;
    [SerializeField] private Toggle descriptionBox;
    [SerializeField] private TextMeshProUGUI descriptionText;

    public bool Description
    {
        get => description;
        set
        {
            description = value;
            SubscribeOnDescription(Description);
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        descriptionBox.isOn = description;
        interfaceObject.transform.GetChild(0).gameObject.SetActive(true);
        descriptionBox.onValueChanged.AddListener((bool status) => OnDescriptionChanged());//Add method on checkBox
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    private void OnDescriptionChanged()//Method for switch on or switch off in the game
    {
        Description = descriptionBox.isOn;
    }
    private void SubscribeOnDescription(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
        {
            InteractiveAction += OnDescriptionAction;
        }
        else
            InteractiveAction -= OnDescriptionAction;
    }
    #endregion
    
    #region Actions
    private void OnDescriptionAction()//Main action of event
    {
        descriptionText.text = "Hello! I'm " + name.Replace("(", " ").Split()[0];
    }

    public override void OnAcionInteraction()//Method for call outside
    {
        InteractiveAction();
    }
    #endregion

    #region Collisions
    protected override void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
            SubscribeOnDescription(Description);//Subscribe actions at event
    }

    protected override void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
            SubscribeOnDescription(false);//Unsubscribe actions from event
    }
    #endregion

    #region Observer Action
    public override void Notify()
    {
        base.Notify();
    }
    #endregion

    #endregion
}
