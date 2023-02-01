using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    public enum HandModelType
    {
        Left,
        Right
    }

    [SerializeField] private HandModelType handType;
    [SerializeField] private Transform root;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform[] fingerBones;

}
