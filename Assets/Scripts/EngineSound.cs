using UnityEngine;

public class EngineSound : MonoBehaviour
{
    [SerializeField] private AudioSource idleAudio;
    [SerializeField] private AudioSource movingAudio;
    [SerializeField] private float fadeSpeed = 2f;

    private void Start()
    {
        idleAudio.volume = 1f;
        movingAudio.volume = 0f;
    }

    private void Update()
    {
        bool isMoving = Player.Instance.GetIsMoving();
        bool isForcedStop = Player.Instance.GetForcedStop();

        // Target volume
        float targetIdleVolume = isMoving && !isForcedStop ? 0f : 1f;
        float targetMovingVolume = isMoving && !isForcedStop ? 1f : 0f;

        // Interpolasi volume secara halus
        idleAudio.volume = Mathf.Lerp(idleAudio.volume, targetIdleVolume, fadeSpeed * Time.deltaTime);
        movingAudio.volume = Mathf.Lerp(movingAudio.volume, targetMovingVolume, fadeSpeed * Time.deltaTime);
    }
}
