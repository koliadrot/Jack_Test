using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GateInteractive : InteractiveObject
{
    #region Field Declarations
    public event Action InteractiveAction = () => { };

    [SerializeField] private bool gate = true;
    [SerializeField] private Toggle gateBox;
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
    public bool Gate
    {
        get => gate;
        set
        {
            gate = value;
            SubscribeOnGate(Gate);
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        expGateField.text = expGate.ToString();
        gateBox.isOn = gate;
        interfaceObject.transform.GetChild(4).gameObject.SetActive(true);
        expGateField.onValueChanged.AddListener(OnGateExperienceFieldChanged);//Add method on InputField--->
        expGateField.onEndEdit.AddListener(OnGateExperienceFieldChanged);//<---
        gateBox.onValueChanged.AddListener((bool status) => OnGateExperienceChanged());//Add method on checkBox
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    private void OnGateExperienceChanged()//Method for switch on or switch off in the game
    {
        Gate = gateBox.isOn;
    }
    private void SubscribeOnGate(bool status)//Subscribe or unsubscribe at/from event
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
        if (expGate <= player.Experience)
            teleportObject.SetActive(true);
    }
    public override void OnAcionInteraction()//Method for call outside
    {
        InteractiveAction();
    }
    #endregion

    #region UI
    public void OnGateExperienceFieldChanged(string expGateIn)//Set value of necessary experience
    {
        if (int.Parse(expGateIn) > 0)
            ExpGate = int.Parse(expGateIn);
        else
            ExpGate = 0;
    }
    #endregion

    #region Collisions
    protected override void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            SubscribeOnGate(Gate);//Subscribe actions at event
            CheckExperience(true);
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SubscribeOnGate(false);//Unsubscribe actions from event
            CheckExperience(false);
        }
        base.OnTriggerExit(other);
    }
    private void CheckExperience(bool entrance)//Compare necessary exp with exp of player
    {
        if (entrance)
        {
            if (expGate > player.Experience)
            {
                indicateArea.material.color = Color.red;//Switch on Indicate that necessary more experience
                teleportObject.SetActive(false);
            }
            else
                indicateArea.material.color = Color.white;
        }
        else
        {
            if (expGate <= player.Experience)
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
