using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class FlareBehaviour : SceneObject, IInteractable
{

    public void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        GameMaster.GetInventory().GetFlare();
        Destroy(this);
    }
}
