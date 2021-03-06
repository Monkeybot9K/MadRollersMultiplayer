﻿using UnityEngine;
using System.Collections;

public class ParticlesSceneObject : SceneObject {

    public ParticleSystem _particleSystem;
    public ParticleSystem explotion;
    public ParticleSystem[] explotions_to_colorize;

    public override void OnRestart(Vector3 pos)
    {
        base.OnRestart(pos);

        if (_particleSystem)
        {
            _particleSystem.Clear();
            _particleSystem.Play();
        }

        explotion.Clear();
        explotion.Play();
    }
    public void SetColor(Color color)
    {      
        color.a = 0.45f;

        foreach (ParticleSystem ps in explotions_to_colorize)
            ps.startColor = color;
    }

}
