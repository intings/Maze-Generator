
using System;
using Algorithms;
using UnityEngine;

public class GridGenerator : GridGeneratorBase
{
    [SerializeField] private Transform wallPrefab2d;
    [SerializeField] private Algo algorithmType;    
    [SerializeField] private bool is3d;
    [SerializeField] private int nameCtr = 1;
    [SerializeField] private string namePrefix = "Aldous";
    
    private Transform _parentDuplicate = null;
    private bool _doDuplicate;

    private void Start()
    {
        UpdateAlgorithm();
    }

    protected override void Update()
    {
        if (_doDuplicate)
        {
            _doDuplicate = false;
            _parentDuplicate = Instantiate<Transform>(parent);
            _parentDuplicate.gameObject.name = namePrefix + nameCtr++;
        }
        if (_parentDuplicate != null)
        { 
            Destroy(_parentDuplicate.gameObject);
            _parentDuplicate = null;
        }
        base.Update();
    }

    protected override void UpdateAlgorithm()
    {
        switch (algorithmType)
        {
            case Algo.BinaryTree:
                MyGrid = new BinaryTree(width, height);
                break;
            case Algo.SideWinder:
                MyGrid = new SideWinder(width, height);
                break;
            case Algo.AldousBroder:
                MyGrid = new AldousBroder(width, height);
                break;
            case Algo.HuntAndKill:
                MyGrid = new HuntAndKill(width, height);
                break;
            case Algo.RecursiveBacktracker:
                MyGrid = new RecursiveBacktracker(width, height);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        IWallInstantiable wall;
        if (is3d)
            wall = new WallPrefab3d {WallPrefab = wallPrefab3d};
        else
            wall = new WallPrefab2d {WallPrefab = wallPrefab2d};
        GridRenderer.Instance.SetGrid(MyGrid).ParentToTransform(parent).RenderWithPrefab(wall);
        MyGrid.Execute(this);
    }

    
    public void OnDuplicateButtonClick()
    {
        _doDuplicate = true;
    }
}
