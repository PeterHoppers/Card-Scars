using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hand Display Settings", menuName = "ScriptableObjects/HandDisplaySettings", order = 1)]
public class HandDisplaySettings : ScriptableObject
{
    public float rotationPerCard = 20f;
    public float spacingPerCard = 35f;
    public int offsetFromAnchor = 200;
}
