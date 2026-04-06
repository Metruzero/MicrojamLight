using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


public class Interactable : MonoBehaviour
{
    public float WorkDuration;
    public ResourceManager resourceManager;
    public List<ItemType> contents;
    public bool opened = false;
    public Sprite openCrate;
    public AudioClip openCrateClip;
    public AudioSource audioSource;


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
        audioSource.PlayOneShot(openCrateClip);
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
