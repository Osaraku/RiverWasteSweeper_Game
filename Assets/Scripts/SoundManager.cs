using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private AudioSource audioSource;

    private float volume = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Trash.OnTrashCollected += Trash_OnTrashCollected;
        GarbageTruck.OnTrashSold += GarbageTruck_OnTrashSold;
        Barrier.OnBarrierDestroyed += Barrier_OnBarrierDestroyed;
        MoveableTrash.OnTrashSold += MoveableTrash_OnTrashSold;
        BuyableBoat.OnBoatPurchased += BuyableBoat_OnBoatPurchased;
    }

    private void BuyableBoat_OnBoatPurchased(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.boatPurchased);
    }

    private void MoveableTrash_OnTrashSold(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.trashSold);
    }

    private void Barrier_OnBarrierDestroyed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.barrierDestroyed);
    }

    private void GarbageTruck_OnTrashSold(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.trashSold);
    }

    private void Trash_OnTrashCollected(object sender, EventArgs e)
    {
        Trash trash = sender as Trash;
        PlaySound(audioClipRefsSO.trashCollected);
    }

    private void PlaySound(AudioClip audioClip, float volumeMultiplier = 1f)
    {
        audioSource.PlayOneShot(audioClip, volumeMultiplier * volume);
    }
}
