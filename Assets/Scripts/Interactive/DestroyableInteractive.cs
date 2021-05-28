using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DestroyableInteractive : InteractiveObject
{
    #region Field Declarations

    [Header("Health")]
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
    #endregion

    #region Startup
    protected override void Start()
    {
        base.Start();
        healthField.text = health.ToString();
        damageField.text = damage.ToString();
        healthField.onValueChanged.AddListener(OnHealthFieldChanged);//Add method on InputField--->
        healthField.onEndEdit.AddListener(OnHealthFieldChanged);//---
        damageField.onValueChanged.AddListener(OnDamageFieldChanged);//---
        damageField.onEndEdit.AddListener(OnDamageFieldChanged);//<---
    }
    #endregion

    #region Subject Implementation

    #region Subscribe Methods
    protected override void SubscribeOnAction(bool status)//Subscribe or unsubscribe at/from event
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

    #region Observer Action
    public override void Notify()
    {
        base.Notify();
    }
    #endregion

    #endregion
}
