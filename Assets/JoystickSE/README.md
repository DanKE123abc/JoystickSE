# JoystickSE

当前版本：v1.1.0

一个简单的虚拟摇杆，遵循MIT开源协议

### 接入指南

##### 接入前提：

1.必须作为Canvas的子物体

2.必须有EventSystem

3.必须导入[DanKeTools框架](https://github.com/DanKE123abc/DanKeTools)

##### 插入代码：

使用命名空间 JoystickSE

添加事件监听

```c#
using JoystickSE
```

```c#
JoystickManager.instance.AddListener<Vector2>(CheckDirChange);//传参函数
```

##### 修改触控范围：

修改 JoystickSE下的TouchRect范围大小即可

##### 模式选择：

Normal —— 默认位置不动的摇杆

DefaultHide —— 默认隐藏的摇杆

Move —— 可移动的摇杆



*可参考示例项目*

### 开源

基于开源项目 [DanKeTools](https://github.com/DanKE123abc/DanKeTools/)，遵循MIT 开源协议。

使用了DanKeTools以下组件：

- 单例模式（Base）
- 事件中心（Event）
- UI面板基类（UI）
- UI事件管理器（UI）
