﻿using UnityEngine;
using System.Collections;

public class InputManager {

    private static InputType[] inputs;
    private static bool initialized;
    private const int PLAYERS_COUNT = 4;

   
    public static bool getStart(int id = 0)
    {
        if (!initialized) Init();
        return inputs[id].getStart();
    }
    public static float getHorizontal(int id = 0)
    {
        if (!initialized) Init();
        return inputs[id].getHorizontal();
    }
	public static float getVertical(int id = 0)
	{
		if (!initialized) Init();
		return inputs[id].getVertical();
	}
    public static bool getOpenMenu(int id = 0)
    {
        if (!initialized) Init();
        return inputs[id].getOpenMenu();
    }
    public static bool getFireDown(int id = 0)
    {
         if (!initialized) Init();
         return inputs[id].getFireDown();
    }
	public static bool getFireUp(int id = 0)
	{
		if (!initialized) Init();
		return inputs[id].getFireUp();
	}
    public static bool getJump(int id = 0)
    {
        if (!initialized) Init();
        return inputs[id].getJump();
    }

    static void Init()
    {        
        inputs = new InputType[PLAYERS_COUNT];
        for (int a = 0; a < PLAYERS_COUNT; a++)
        {
            inputs[a] = new InputKeyboard(a);
        }
        if (Data.Instance.switchPlayerInputs)
        {
            InputType inputs0 = inputs[0];
            InputType inputs1 = inputs[1];
            inputs[0] = inputs[2];
            inputs[1] = inputs[3];
            inputs[2] = inputs0;
            inputs[3] = inputs1;
        }
        initialized = true;
    }
}
