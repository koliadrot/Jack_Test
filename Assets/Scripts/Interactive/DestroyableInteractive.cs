using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DestroyableInteractive : InteractiveObject
{
    #region Field Declarations
    public event Action InteractiveAction = () => { };

    [SerializeField] private bool destroyable = true;
    
    [Header("Health")]
    [SerializeField] private Toggle destroyableBox;
    [SerializeField] private int health = 10;
    [SerializeField] private TMP_InputField healthField;

    [Header("Damage")]
    [SerializeField] private int damage = 1;
    [SerializeField] private TMP_InputField damageField;

    public int Health
    {
        get => health;
        set
        {
            health = value;
            healthField.text = health.ToString();
            if (health <= 0)
                Destroy(gameObject);
        }
    }
    public int Damage
    {
        get => damage;
        set
        {
            damage = value;
            damageField.text = damage.ToString();
        }
    }

    public bool Destroyable
    {
        get => destroyable;
        set
        {
            destroyable = value;
            SubscribeOnDestroyable(Destroyable);
        }
    }
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        healthField.text = health.ToString();
        damageField.text = damage.ToString();
        destroyableBox.isOn = destroyable;
        interfaceObject.transform.GetChild(1).gameObject.SetActive(true);
        healthField.onValueChanged.AddListener(OnHealthFieldChanged);//Add method on InputField--->
        healthField.onEndEdit.AddListener(OnHealthFieldChanged);//---
        damageField.onValueChanged.AddListener(OnDamageFieldChanged);//---
        damageField.onEndEdit.AddListener(OnDamageFieldChanged);//<---
        destroyableBox.onValueChanged.AddListener((bool status) => OnDestroyableChanged());//Add method on checkBox
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods

    private void OnDestroyableChanged()//Method for switch on or switch off in the game
    {
        Destroyable = destroyableBox.isOn;
    }

    private void SubscribeOnDestroyable(bool status)//Subscribe or unsubscribe at/from event
    {
        if (status)
            InteractiveAction += OnDestroyableAction;
        else
            InteractiveAction -= OnDestroyableAction;
    }
    #endregion

    #region Actions
    private void OnDestroyableAction()//Main action of event
    {
        Health -= Damage;
    }
    public override void OnAcionInteraction()//Method for call outside
    {
        if (player != null)
            InteractiveAction();
    }
    #endregion

    #region UI
    public void OnHealthFieldChanged(string healthIn)//Set Health value
    {
        if (int.Parse(healthIn) > 0)
            Health = int.Parse(healthIn);

    }
    public void OnDamageFieldChanged(string damageIn)//Set Damage value
    {
        Damage = int.Parse(damageIn);
    }
    #endregion

    #region Collisions
    protected override void OnTriggerEnter(Collider other)//Activate, when object to come in zone
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
            SubscribeOnDestroyable(Destroyable);//Subscribe actions at event
    }

    protected override void OnTriggerExit(Collider other)//Activate, when object to come out zone
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
            SubscribeOnDestroyable(false);//Unsubscribe actions from event
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
