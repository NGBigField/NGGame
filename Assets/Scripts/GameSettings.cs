﻿using System;
using UnityEngine;

public enum GameControlMode
{
    Joystick,
    Gyroscope
}

public class GameSettings
{
    private static GameSettings instance;

    public static GameSettings Instance
    {
        get
        {
            if (instance == null) instance = new GameSettings();
            return instance;
        }
    }

    public GameSettings()
    {
        LoadSettings();
    }

    public GameControlMode controlMode;

    public void LoadSettings()
    {
        controlMode = (GameControlMode)Enum.ToObject(typeof(GameControlMode), PlayerPrefs.GetInt("ControlMode", (int)GameControlMode.Joystick));
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ControlMode", (int)controlMode);
    }
}
