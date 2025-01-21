using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public static Track instance;

    [SerializeField] private Transform[] trackCorners;
    public Transform[] TrackCorners { get => trackCorners; private set => trackCorners = value; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
}
