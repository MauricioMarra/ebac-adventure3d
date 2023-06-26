using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item")]
public class SOItem : ScriptableObject
{
    public int value;

    public void Add()
    {
        value++;
    }

    public void Subtract()
    {
        value--;
    }

    public void Reset()
    {
        value = 0;
    }
}
