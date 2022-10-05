using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DevToolsManager : MonoBehaviour
{
    public static DevToolsManager Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }


    public void InfiniteStamina_Toggled(Toggle self)
    {
        PlayerMaster.Instance.PlayerStats_REF.DEV_InfiniteStamina = self.isOn;
        PlayerMaster.Instance.PlayerStats_REF.DEV_FillStamina();
    }

}
