using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;

    //Positioning Variables
    private Transform prevCorner;
    private Transform nextCorner;
    private float trackSectionPos;
    private int trackSectionIndex = -1;

    private void Start()
    {
        OnEndOfSectionReached();
    }

    private void Update()
    {
        trackSectionPos += Time.deltaTime * speed / Vector2.Distance(prevCorner.position, nextCorner.position);

        if (trackSectionPos >= 1)
            OnEndOfSectionReached();

        transform.position = Vector2.Lerp(prevCorner.position, nextCorner.position, trackSectionPos);
    }

    public float TrackDistance()
    {
        return trackSectionIndex + trackSectionPos;
    }

    public void TakeDamage(float dmg)
    {
        MoneyManager.instance.ChangeMoney(Mathf.Min(dmg, health));
        health -= dmg;
        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnEndOfSectionReached()
    {
        trackSectionPos = 0;
        trackSectionIndex++;

        if (trackSectionIndex == Track.instance.TrackCorners.Length - 1)
        {
            OnEndOfTrackReached();
            return;
        }

        prevCorner = Track.instance.TrackCorners[trackSectionIndex];
        nextCorner = Track.instance.TrackCorners[trackSectionIndex + 1];
    }

    private void OnEndOfTrackReached()
    {
        HealthManager.instance.ChangeHealth(-health);
        Destroy(gameObject);
    }
}
