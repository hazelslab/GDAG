using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollow : MonoBehaviour
{

    [SerializeField]
    private PlayerMaster _playerMaster;

    [SerializeField]
    private Transform _followPoint;
    [SerializeField]
    private float _followDistance = 1f;
    [SerializeField]
    private float _followSpeed = 0.8f;


    private void Update()
    {
        float dist = Vector2.Distance(transform.position, _followPoint.position);
        if (dist > _followDistance)
        {
            transform.position = Vector2.Lerp(transform.position, _followPoint.position, (_followSpeed * dist) * Time.deltaTime);
        }
    }

}
