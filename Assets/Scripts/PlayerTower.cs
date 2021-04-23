﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTower : MonoBehaviour
{
    [SerializeField] private Human _startHuman;
    [SerializeField] private Transform _distanceChecker;
    [SerializeField] private float _fixationMaxDistance;
    [SerializeField] private BoxCollider _chekcCollider;

    private List<Human> _humans;

    void Start()
    {
        _humans = new List<Human>();
        Vector3 spawnPoint = transform.position;
        _humans.Add(Instantiate(_startHuman, spawnPoint, Quaternion.identity, transform));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Human human))
        {
            Tower collisionTower = human.GetComponentInParent<Tower>();

            if (collisionTower != null)
            {
                List<Human> collectedHumans = collisionTower.CollectHumans(_distanceChecker, _fixationMaxDistance);

                if (collectedHumans != null)
                {
                    for (int i = collectedHumans.Count - 1; i >= 0; i--)
                    {
                        Human insertHuman = collectedHumans[i];
                        InsertHuman(insertHuman);
                        DisplaceChecker(insertHuman);
                    }
                }
            }
        }
    }

    private void InsertHuman(Human collectedHumans)
    {
        _humans.Insert(0, collectedHumans);
        SetHumanPosition(collectedHumans);
    }

    private void SetHumanPosition(Human human)
    {
        human.transform.parent = transform;
        human.transform.localPosition = new Vector3(0, human.transform.localPosition.y, 0);
        human.transform.localRotation = Quaternion.identity;
    }

    private void DisplaceChecker(Human human)
    {
        float displaceScale = 1.5f;
        Vector3 distanceCheckerNewPosition = _distanceChecker.position;

        distanceCheckerNewPosition.y -= human.transform.localScale.y * displaceScale;

        _distanceChecker.position = distanceCheckerNewPosition;
        _chekcCollider.center = _distanceChecker.localPosition;
    }
}
