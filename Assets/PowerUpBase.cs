using System;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    [SerializeField] private GameObject mesh;

    public PowerUpSetup powerUpSetup;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            this.mesh.SetActive(false);
    }
}

[Serializable]
public class PowerUpSetup
{
    public ItemType type;
    public Material material;
}
