# 游戏开发全链路陪跑计划

## 当前工具环境

- Unity Hub: `E:\Unity\Hub\Unity Hub.exe`
- Unity Editor: `E:\Unity\Editors\2022.3.62f3c1\Editor\Unity.exe`
- Unity 模块: WebGL Build Support, Windows IL2CPP Build Support
- VS Code 便携版: `E:\Unity\VSCode\Code.exe`
- VS Code 扩展: Unity, C# Dev Kit, C#
- .NET SDK: `E:\Unity\dotnet`
- itch.io butler: `E:\Unity\Butler\butler.exe`
- Unity 项目目录: `E:\Unity\Projects`

## 当前状态

Unity Editor 已安装、许可证已可用，`TopDownSurvivor` 项目已创建。

第一节课已完成：生成 `Main.unity` 场景，创建 Player、Camera、Grid，并写入玩家移动脚本。

## 主线项目

项目名：`TopDownSurvivor`

类型：2D 俯视角生存游戏。

目标：玩家移动躲避敌人，自动攻击，撑过指定时间获胜，最终打包发布到 itch.io。

## 第 1 阶段：工具与第一个项目

- [x] 登录 Unity Hub 并激活许可证
- [x] 创建 `E:\Unity\Projects\TopDownSurvivor`
- [x] 配置 VS Code 作为外部脚本编辑器
- [x] 初始化 Git 与 Unity `.gitignore`
- [x] 创建第一个 Scene
- [x] 创建 Player GameObject
- [x] 编写 `PlayerController2D.cs`
- [ ] 在 Unity Editor 中进入 Play Mode，确认角色移动

## 第 2 阶段：核心玩法原型

- 加入敌人 Prefab
- 实现敌人追踪玩家
- 实现敌人生成器
- 加入玩家生命值
- 加入碰撞伤害
- 加入 Game Over 与 Restart
- 加入计时胜利条件

## 第 3 阶段：游戏产品化

- 加入主菜单、暂停菜单、结算界面
- 加入音效、背景音乐、命中反馈
- 加入简单粒子和屏幕震动
- 调整移动手感和难度曲线
- 做 3 次试玩记录并修复问题

## 第 4 阶段：构建与测试

- 构建 Windows 版本
- 构建 WebGL 版本
- 检查不同分辨率 UI
- 准备测试清单
- 修复构建错误和首轮玩家反馈

## 第 5 阶段：发布

- 创建 itch.io 项目页
- 准备封面、截图、说明文字
- 使用 butler 上传构建
- 发布 v0.1.0
- 写更新日志
- 收集反馈并发布 v0.1.1

## 每节课的节奏

每节课都按这个顺序推进：

1. 明确今天要完成的游戏能力
2. 我操作环境或代码，你观察关键步骤
3. 我解释 Unity 概念和前端类比
4. 你运行项目并确认现象
5. 我整理本节课总结和下一步
