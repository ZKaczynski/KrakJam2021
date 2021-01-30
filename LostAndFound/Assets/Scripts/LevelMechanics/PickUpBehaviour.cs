using System;
using System.Collections;
using System.Collections.Generic;
using General;
using UnityEngine;

public class PickUpBehaviour : SceneObject, IInteractable
{

    public int pickUpType = 0;

    public void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        GameMaster.GetInventory().AddItem(pickUpType);
        Destroy(gameObject);
    }
}
