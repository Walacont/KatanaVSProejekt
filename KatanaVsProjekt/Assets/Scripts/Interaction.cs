using System;
using UnityEngine.Events;

[Serializable]
public class Interaction
{
    public Interactable Interactable;
    public string Instruction;
    public string HelpMsg;
    public string ErrorMsg;
    public float Duration;
    public float DurationExtra;
    public UnityEvent OnStart;
    public UnityEvent OnEnd;
    public UnityEvent OnExtra;
    public bool HelpCounted { get; set; }
}