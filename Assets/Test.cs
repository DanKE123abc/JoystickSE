using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoystickSE;

///<summary>
///脚本名称： Test.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class Test : MonoBehaviour
{
    private Vector3 dir;
    void Start()
    {
        JoystickManager.Instance().AddListener<Vector2>(CheckDirChange);
    }

    void Update()
    {
        this.transform.Translate(dir*Time.deltaTime,Space.World);
    }

    private void CheckDirChange(Vector2 dir)
    {
        this.dir.x = dir.x;
        this.dir.z = dir.y;
        
    }
}
