using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private int currentItem = 0;
    private int[] items = new int[1];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AddItem(int pickUpType)
    {
        items[pickUpType]++;
    }


    internal bool GetCurrentItem()
    {
        if (items[currentItem] > 0)
        {
            items[currentItem]--;
            return true;
        }
        else
        {
            return false;
        }

    }
}
