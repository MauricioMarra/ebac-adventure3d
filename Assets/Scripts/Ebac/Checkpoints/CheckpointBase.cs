using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    [SerializeField] private int _key;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _isActive = false;

    private void Start()
    {
        GameManager.instance.RegisterCheckpoint(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !this._isActive)
        {
            GameManager.instance.SaveCheckpoint(_key, this.gameObject);
            _particleSystem.Play();

            SaveManager.instance.SaveGame();

            _isActive = true;
        }
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public void ActivateCheckpoint()
    {
        _particleSystem.Play();
        _isActive = true;
    }

    public int GetKey()
    {
        return _key;
    }
}
