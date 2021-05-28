using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionable
{
    void OnTriggerEnter(Collider other);
    void OnTriggerExit(Collider other);
}
public interface IObserverable
{
    void Notify();
}

