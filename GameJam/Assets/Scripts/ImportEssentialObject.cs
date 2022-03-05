using UnityEngine;

public class ImportEssentialObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _essentialObjects;

    private void Awake()
    {
        foreach (GameObject obj in _essentialObjects)
        {
           GameObject instance = GameObject.Find(obj.name);
            if(instance == null)
            {
                Instantiate(obj);
            }            
        }
    }
}
