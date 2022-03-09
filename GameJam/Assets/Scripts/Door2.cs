using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _graphics;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_graphics.GetBlendShapeWeight(0) <= 100.0f)
        {
            StartCoroutine(OpenDoor());
        }
        Debug.Log(_graphics.GetBlendShapeWeight(0));
    }
    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.01f);
        _graphics.SetBlendShapeWeight(0, _graphics.GetBlendShapeWeight(0) + 1.0f);
    }
}
