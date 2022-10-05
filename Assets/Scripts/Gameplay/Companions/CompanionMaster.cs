using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMaster : MonoBehaviour
{
    public static CompanionMaster Instance { get; private set; }

    public CompanionFollow CompanionFollow_REF;

    private void Awake()
    {
        Instance = this;

        CompanionFollow_REF = GetComponent<CompanionFollow>();
    }
}
