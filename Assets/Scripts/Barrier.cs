using System;
using Unity.Cinemachine;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private enum State
    {
        Waiting,
        TransitionToBarrier,
        BarrierDisappear,
        TransitionToPlayer
    }

    [SerializeField] private CinemachineCamera barrierCamera;
    [SerializeField] private Transform barrierVisual;
    [SerializeField] private int trashValueRequired;
    [SerializeField] private float transitionInTimer = 3f;
    [SerializeField] private float transitionOutTimer = 3f;

    private State state;

    private void Awake()
    {
        state = State.Waiting;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Waiting:
                if (Player.Instance.GetTotalTrashValue() >= trashValueRequired)
                {
                    Player.Instance.SetForcedStop(true);
                    state = State.TransitionToBarrier;
                }
                break;
            case State.TransitionToBarrier:
                barrierCamera.gameObject.SetActive(true);
                transitionInTimer -= Time.deltaTime;
                if (transitionInTimer < 0f)
                {
                    state = State.BarrierDisappear;
                }
                break;
            case State.BarrierDisappear:
                barrierVisual.gameObject.SetActive(false);
                state = State.TransitionToPlayer;
                break;
            case State.TransitionToPlayer:
                barrierCamera.gameObject.SetActive(false);
                transitionOutTimer -= Time.deltaTime;
                if (transitionOutTimer < 0f)
                {
                    Player.Instance.SetForcedStop(false);
                    Destroy(gameObject);
                }
                break;
        }
    }
}
