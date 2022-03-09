using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slab : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _graphics;

    // Update is called once per frame
    void Update()
    {
        if (_graphics.GetBlendShapeWeight(0) <= 100.0f)
        {
            StartCoroutine(SlabAnim());
        }
    }

    private IEnumerator SlabAnim()
    {
        while (_graphics.GetBlendShapeWeight(0) <= 100.0f)
        {
            yield return new WaitForSeconds(0.01f);
            _graphics.SetBlendShapeWeight(0, _graphics.GetBlendShapeWeight(0) + 1.0f);
        }
    }
}
