using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private PlayerMaster _master;


    [Header("Effects")]
    [SerializeField]
    private GameObject p_landingDust;
    [SerializeField]
    private GameObject p_jumpDust;
    [SerializeField]
    private Transform _jumpDustPoint;

    private void Awake()
    {
        _master = GetComponent<PlayerMaster>();
    }

    public void SpawnJumpDustEffect(float dustXOffset = 0, float dustYOffset = 0)
    {
        if (p_jumpDust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = new Vector3(_jumpDustPoint.position.x, _jumpDustPoint.position.y, 0.0f);
            GameObject newDust = Instantiate(p_jumpDust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(.7f, .7f, .7f);
        }
    }

    public void SpawnLandingDustEffect(float dustXOffset = 0, float dustYOffset = 0)
    {
        if (p_landingDust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = new Vector3(_jumpDustPoint.position.x, _jumpDustPoint.position.y, 0.0f);
            GameObject newDust = Instantiate(p_landingDust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(.7f, .7f, .7f);
        }
    }
}
