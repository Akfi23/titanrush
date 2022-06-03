﻿using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerComponent: MonoBehaviour
{
    [SerializeField] private Mutation[] mutationScales;

    public NavMeshAgent NavMesh;
    public OnTriggerEnterComponent OnTriggerEnterComp;
    public PlayerCanvasComponent PlayerCanvas;

    [HideInInspector]
    public PlayerAnimatorComponent PlayerAnimator;

    private float currentHealth;

    private int currentMutation = 0;

    public void Init()
    {
        currentMutation = 0;

        RotateModel(transform.position - Vector3.forward);

        SetMutation();
    }
    public void SetHealth(float value)
    {
        currentHealth = value;
    }
    public void ReceiveDamage(float value)
    {
        currentHealth -= value;
    }
    public float GetHealth()
    {
        return currentHealth;
    }

    public void Mutate()
    {
        if (currentMutation >= mutationScales.Length)
        {
            return;
        }

        currentMutation++;

        SetMutation();
        StartRunning(true);
    }

    private void SetMutation()
    {
        foreach (var mutation in mutationScales)
        {
            mutation.model.SetActive(false);
        }

        mutationScales[currentMutation].model.SetActive(true);
        PlayerAnimator = mutationScales[currentMutation].playerAnimator;
    }
    public void StartRunning(bool enable)
    {
        mutationScales[currentMutation].playerAnimator.SetRunAnimation(enable);
    }

    public void RotateModel(Vector3 lookAt)
    {
        mutationScales[currentMutation].model.transform.LookAt(lookAt);
    }
}

[Serializable]
public struct Mutation
{
    public GameObject model;
    public PlayerAnimatorComponent playerAnimator;
}