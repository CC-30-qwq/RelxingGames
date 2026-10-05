# RelxingGames

基于 Unity 2022.3 开发的**休闲解压小游戏合集**，共 **30 个独立关卡**，每关一个场景、一套互动玩法与一个胜利判定。

关卡按 `Game_0 → Game_29` 顺序线性推进：达成条件后弹出胜利提示，短暂停留后自动进入下一关；通关 `Game_29` 后回到 `Game_0` 循环。

## 技术栈

| 分类 | 技术 |
|---|---|
| 引擎 | Unity 2022.3.62f1c1 |
| 渲染 | 2D（`com.unity.feature.2d`） |
| 补间 | DOTween |
| UI / 文本 | UGUI + TextMeshPro |
| 场景管理 | `UnityEngine.SceneManagement` |

## 核心机制

### 关卡流程
- `Assets/GameManager.cs` 只做两件事：
  - `StartGame(int index)` → `SceneManager.LoadScene("Game_" + index)`
  - `BackToMenu()` → 加载主菜单 `GameMenu`
- 每关的 `Win_N.cs` 负责：
  1. 在 `Update()` 中检测通关条件（例如某个物体被激活 `a.activeSelf`、或累计数值 `a > target`）；
  2. 条件达成 → `Invoke("Win", 1f)` 显示胜利 UI；
  3. 再 `Invoke("StartGame", 2f)` 延迟 2 秒切换到下一关。

```csharp
public class Win_5 : MonoBehaviour
{
    public float target;
    public float a;
    public GameObject win;

    void Update()
    {
        if (a > target)                      // 通关条件
        {
            Invoke("Win", 1f);               // 1s 后显示胜利
            Invoke("StartGame", 2f);         // 2s 后进入下一关
        }
    }
}
```

### 关卡玩法
各关玩法彼此独立，涵盖拖拽、切割、挤压、倾倒等解压向交互，例如：

| 关卡 | 玩法要点 |
|---|---|
| `Game_1` | 倒糖 / 瓶子相关交互（`Suger`、`Bottle`） |
| `Game_10` | 点火（`Fire`） |
| `Game_12` | 切水果与掉落（`fruit`、`fruitdrop`） |
| `Game_13` | 气球与绳索（`Ballon`、`rope`、`neilCtrl`） |
| `Game_14` | 切割与随机掉落（`Cutting`、`RandomDrop`） |
| `Game_16` | 旋转塔（`TowerRotate`） |
| `Game_22` | 电视天线（`antenna`、`tv`） |
| `Game_23` | 玉米爆米花（`cornDropPlus`、`getPopcorn`） |
| `Game_27` | 曲奇碎裂（`Cookie`、`destoryPiece`） |
| `Game_28` | 灭火计数（`FireCount`） |

（其余关卡结构相同：`Win_N.cs` + `res/` 下的玩法脚本与资源。）

## 项目结构

```
Assets/
├── GameManager.cs              # 关卡切换与返回菜单
├── Menu/
│   ├── GameMenu.unity          # 主菜单场景
│   ├── Win.prefab              # 通用胜利提示预制体
│   └── res/                    # 菜单图标（图层 1~32.png）
├── Games/
│   ├── Game_0/ … Game_29/      # 30 个关卡
│   │   ├── Game_N.unity        # 关卡场景
│   │   ├── Win_N.cs            # 胜利判定与跳转
│   │   └── res/                # 该关卡的玩法脚本与美术资源
└── Resources/
```

## 快速开始

1. 使用 **Unity 2022.3.62f1c1** 打开项目并等待依赖还原。
2. 打开 `Assets/Menu/GameMenu.unity` 作为入口（**必须从主菜单启动**，因为它负责初始化关卡序号）。
3. 也可直接打开 `Assets/Games/Game_N/Game_N.unity` 调试单个关卡。

> 新增关卡的方式：复制任一 `Game_N` 目录 → 重命名为下一个序号 → 修改 `Win_N` 中的通关条件与 `StartGame` 跳转目标 → 在主菜单补上入口图标。

## 备注

各关卡代码结构高度同构（`Win_N` + `res/`），新增玩法只需关注该关目录内的脚本，不会影响其他关卡。
