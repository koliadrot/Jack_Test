using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExperienceInteractive : InteractiveObject
{
    #region Field Declarations

    public event Action InteractiveAction = () => { };

    [SerializeField] private bool experience = true;
    [SerializeField] private Toggle experienceBox;
    [SerializeField] private int exp = 1;
    [SerializeField] private TMP_InputField expField;

    public int Exp
    {
        get => exp;
        set
        {
            exp = value;
            expField.text = exp.ToString();
        }
    }
    public bool Experience
    {
        get => experience;
        set
        {
            experience = value;
            SubscribeOnExperience(Experience);
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        expField.text = exp.ToString();
        experienceBox.isOn = experience;
        interfaceObject.transform.GetChild(2).gameObject.SetActive(true);
        expField.onValueChanged.AddListener(OnExperienceFieldChanged);//Add method on InputField--->
        expField.onEndEdit.AddListener(OnExperienceFieldChanged);//<---
        experienceBox.onValueChanged.AddListener((bool status) => OnExperienceChanged());//Add method on checkBox
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    private void OnExperienceChanged()//Method for switch on or switch off in the game
    {
        Experience = experienceBox.isOn;
    }

    private void SubscribeOnExperience(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnExperienceAction;
        else
            InteractiveAction -= OnExperienceAction;
    }
    #endregion

    #region Actions
    public override void OnAcionInteraction()//Main action of event
    {
        InteractiveAction();
    }

    private void OnExperienceAction()//Method for call outside
    {
        player.Experience += Exp;
    }
    #endregion

    #region UI
    public void OnExperienceFieldChanged(string expIn)//Set Experience value
    {
        if (int.Parse(expIn) > 0)
            Exp = int.Parse(expIn);
        else
            Exp = 0;
    }
    #endregion

    #region Collisions
    protected override void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
            SubscribeOnExperience(Experience);//Subscribe actions at event
    }

    protected override void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
            SubscribeOnExperience(false);//Unsubscribe actions from event
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
