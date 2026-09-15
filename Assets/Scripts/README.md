# 脚本入口

- `SleepwalkerScript/`：`SleepwalkerController` 负责 Idle/Move/Die 状态和 Animator 的 Int 参数 `State`；`SleepWalkerMove` 负责实际移动；`SleepwalkerDeath` 负责死亡后停移动及动画结束通知。
- `Managers/`：`LevelManager` 是跨场景的加载入口；`PauseManager` 管理 Esc、时间倍率和暂停面板。详细挂载方法见 `PauseManager使用说明.md`。
- `Levelcleared/`：`Levelcleared` 是通关碰撞区域；`SwitchLevel_btn` 提供按钮切关和重开关卡的方法。
- `UI/`：`GameOver` 展开失败界面；`UIShowTrigger` 显示指定 UI 并设置展开 Bool；`UIReturnBool` 将同一 Bool 设为 false，供返回按钮调用。

死亡流程：木板机关的碰撞区域上报梦游者 → `SleepwalkerController.Die()` → 状态变为 Die/`State=2` → `SleepwalkerDeath.HandleDeath()` 调用 `SleepWalkerMove.StopMoving()` → 死亡动画的 Animation Event 调用 `OnDeathAnimationFinished()` → `GameOver.ShowGameOver()`。

Unity 场景中的脚本组件按 `.meta` GUID 引用；整理文件时必须连同 `.meta` 一起移动。场景名称应与 Build Settings 中注册的名称一致。
