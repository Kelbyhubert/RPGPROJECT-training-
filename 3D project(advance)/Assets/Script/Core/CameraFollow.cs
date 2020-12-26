﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    void LateUpdate()
    {
        transform.position = target.position;
    }
}
    
}

