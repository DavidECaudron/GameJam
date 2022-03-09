using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slab : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _graphics;

    private void Update()
    {
        StartCoroutine(SlabAnimation());
    }

    private IEnumerator SlabAnimation()
    {
        while (_graphics.GetBlendShapeWeight(0) <= 100.0f)
        {
            yield return new WaitForSeconds(0.01f);
            _graphics.SetBlendShapeWeight(0, _graphics.GetBlendShapeWeight(0) + 1.0f);
        }
    }
}
