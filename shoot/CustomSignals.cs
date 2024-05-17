using Godot;
using System;

public partial class CustomSignals : Node
{
    [Signal]
    public delegate void UIKeyShowEventHandler();

    [Signal]
    public delegate void HealEventHandler();

    [Signal]
    public delegate void UIHealEventHandler();

    [Signal]
    public delegate void AmmoEventHandler();

    [Signal]
    public delegate void UIAmmoEventHandler();

    [Signal]
    public delegate void ScoreEventHandler();

    [Signal]
    public delegate void UIScoreEventHandler();
}
