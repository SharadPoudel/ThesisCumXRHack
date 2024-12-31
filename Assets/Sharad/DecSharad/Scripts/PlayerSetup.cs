using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] private GameObject OVRRig;

    public override void Spawned()
    {
        base.Spawned();

        if (Object.HasInputAuthority)
        {
            //Local Player
            gameObject.name = "LocalPlayer";
            OVRRig.SetActive(true);
        }
        else{
            //remote Player
            gameObject.name = "RemotePlayer";
            OVRRig.SetActive(false);
        }

    }
}
