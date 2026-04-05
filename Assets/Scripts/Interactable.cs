using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Interactable : MonoBehaviour
{
    public float WorkDuration;
    public ResourceManager resourceManager;
    public List<ItemType> contents;
    private bool opened = false;
    public Sprite openCrate;


    private void Awake()
    {
        contents = new List<ItemType>();
        resourceManager = FindAnyObjectByType<ResourceManager>();
    }

    virtual public void Complete()
    {
        if (opened) return;
        foreach (var item in contents)
        {
            resourceManager.AddItem(item);
        }
        GetComponent<SpriteRenderer>().sprite = openCrate;
        opened = true;
    }

    public void SetResourceManager(ResourceManager rm)
    {
        resourceManager = rm;
    }

    public float GetWorkDuration() { return WorkDuration; }

    public void AddItem(ItemType item)
    {
        Debug.Log("Adding to crate: " + item);
        contents.Add(item);
    }
}
