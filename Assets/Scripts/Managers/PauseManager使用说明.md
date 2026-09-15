# 暂停功能挂载

1. 在每个关卡创建一个始终启用的空对象 `PauseManager`，挂载 `PauseManager.cs`。不要挂在会被隐藏的暂停面板上。
2. 将你制作的整个暂停面板拖到 `Pause UI`，将面板的 Animator 拖到 `Pause Animator`。不挂 UI 也能先测试 Esc 暂停。
3. Animator 添加 Bool 参数 `IsPaused`（默认 false），默认状态为 Idle。从 Idle 到暂停入场动画的过渡条件设为 `IsPaused = true`，关闭 Has Exit Time；入场动画关闭 Loop Time。
4. 脚本会在初始化时隐藏面板；暂停时显示面板、设置参数，并自动把指定 Animator 设为 Unscaled Time，让动画在游戏时间为 0 时继续播放。UI 若有其他独立 Animator，也请手动设为 Unscaled Time。
5. 再按 Esc 会立即隐藏面板并继续游戏；本版本只播放暂停入场动画，没有等待退场动画。
6. “继续游戏”按钮的 OnClick 可直接绑定 `PauseManager.Resume()`；也提供 `Pause()` 和 `TogglePause()`。
7. 将管理器和 UI 一起做成 Prefab，放进各个关卡即可重复使用，不需要改关卡名称。它不跨场景常驻。主菜单不放此 Prefab；即使误放，在 `MainMenu` 场景也不会暂停。菜单改名时同步修改 Main Menu Scene Name。

## 行为说明

- 通过 Time.timeScale = 0 暂停角色、物理和普通动画，恢复时还原暂停前的时间倍率。
- 离开场景或禁用管理器时自动恢复时间，避免下一场景仍然暂停。
- 已为当前木板机关补上暂停输入拦截，拖动中的手势在暂停时取消。
- 新增游戏输入脚本可用 `PauseManager.IsPaused` 判断是否暂停。Time.timeScale 不会自动停止 Update 或鼠标输入。
- 此版本不额外控制声音、光标或退场动画。

## 在 Unity 中验收

- 进入关卡按 Esc：角色停止，面板出现且入场动画能完整播放。
- 再按 Esc 或点击继续：角色继续；重复暂停仍能播放入场动画。
- 暂停时点击、拖动机关：机关不响应；恢复后仍能正常操作。
- 暂停状态返回主菜单、再进关卡：时间正常；主菜单 Esc 不弹暂停面板。
