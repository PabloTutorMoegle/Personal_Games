using UnityEngine;
using System.Collections.Generic;

public class InventoryShowSystem : MonoBehaviour
{
    public GameObject inventoryUI;
    public PlayerController player;
    public List<SlotsSystem> slots = new List<SlotsSystem>();

    void SetUp()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (player.GetPlayerInventory().GetAllItems()[i] != null)
            {
                slots[i].SetSlot(player.GetPlayerInventory().GetAllItems()[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
