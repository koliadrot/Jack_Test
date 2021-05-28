using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GateInteractive : InteractiveObject
{
    #region Field Declarations
    [SerializeField] private int expGate = 5;
    [SerializeField] private TMP_InputField expGateField;
    [SerializeField] private GameObject teleportObject;

    public int ExpGate
    {
        get => expGate;
        set
        {
            expGate = value;
            expGateField.text = expGate.ToString();
            CheckExperience(true);
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        expGateField.text = expGate.ToString();
        expGateField.onValueChanged.AddListener(OnGateExperienceFieldChanged);//Add method on InputField--->
        expGateField.onEndEdit.AddListener(OnGateExperienceFieldChanged);//<---
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    protected override void SubscribeOnAction(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnGateAction;
        else
            InteractiveAction -= OnGateAction;
    }
    #endregion

    #region Actions
    private void OnGateAction()//Main action of event
    {
        if (expGate <= GameSceneController.Instance.OnGetExperiencePoint())
            teleportObject.SetActive(true);
    }
    #endregion

    #region UI
    private void OnGateExperienceFieldChanged(string expGateIn)//Set value of necessary experience
    {
        if (int.Parse(expGateIn) > 0)
            ExpGate = int.Parse(expGateIn);
        else
            ExpGate = 0;
    }
    #endregion

    #region Collisions
    public override void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            CheckExperience(true);
        }
    }
    public override void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            CheckExperience(false);
        }
    }
    private void CheckExperience(bool entrance)//Compare necessary exp with exp of player
    {
        if (entrance)
        {
            if (expGate > GameSceneController.Instance.OnGetExperiencePoint())
            {
                indicateArea.material.color = Color.red;//Switch on Indicate that necessary more experience
                teleportObject.SetActive(false);
            }
            else
                indicateArea.material.color = Color.white;
        }
        else
        {
            if (expGate <= GameSceneController.Instance.OnGetExperiencePoint())
            {
                teleportObject.SetActive(false);
            }
        }
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
