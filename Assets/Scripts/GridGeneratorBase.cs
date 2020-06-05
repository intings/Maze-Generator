using System;
using UnityEngine;

public abstract class GridGeneratorBase : MonoBehaviour
{
    public int width;
    public int height;
    [SerializeField] protected Transform wallPrefab3d;
    [SerializeField] protected Transform parent;

    protected AlgorithmBase MyGrid;
    private bool _updateAlgo;
    private void Awake()
    {
        _updateAlgo = false;
    }

    protected virtual void Update()
    {
        if (!_updateAlgo) return;
        GridRenderer.DestroyGrid(MyGrid);
        _updateAlgo = false;
        UpdateAlgorithm();
    }
    

    protected abstract void UpdateAlgorithm();

    public virtual void EndCoroutine()
    {
        StopAllCoroutines();
    }
    public void OnUpdateButtonClick()
    {
        _updateAlgo = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
