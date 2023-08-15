using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    [SerializeField] private int _key;
    [SerializeField] private ParticleSystem _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameManager.instance.SaveCheckpoint(_key, this.gameObject);
            _particleSystem.Play();

            SaveManager.instance.SaveGame();
        }
    }
}
