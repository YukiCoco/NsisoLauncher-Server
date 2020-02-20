# NsisoLauncher-Server(Master)
A New Minecraft Launcher (unofficial) from Nsiso (Master)
[![Build status](https://ci.appveyor.com/api/projects/status/st6w0l4x1fvf6m5f/branch/master?svg=true)](https://ci.appveyor.com/project/nsisogf/nsisolauncher/branch/master)
[![Build Status](https://nsisogf.visualstudio.com/NsisoLauncher/_apis/build/status/Nsiso.NsisoLauncher?branchName=master)](https://nsisogf.visualstudio.com/NsisoLauncher/_build/latest?definitionId=1&branchName=master)

感谢您关注到我们Nsiso启动器的开源项目  
此项目开源目的在于学习，错误排查，公开安全策略  
作者：Nsiso工作室-siso  
请注意该项目遵循GNU GPL3.0开源协议  
## 使用项目
[NisisoLauncher](https://github.com/Nsiso/NsisoLauncher)

[yggdrasil-api](https://github.com/bs-community/yggdrasil-api)

## 二次开发内容
 + NsisoLauncher 从 Blessing-Skin 获取服务器公告
 + 扩展 [yggdrasil-api](https://github.com/bs-community/yggdrasil-api) ，加入防作弊功能
 
## Get Start
从 Releases 下载文件。  
将插件解压到 Blessing Skin 目录下的 plugins 文件夹里。  
打开启动器设置中的 Minecraft服务器设置 ，勾选服务器模式，填入皮肤站地址。  
启用防作弊：  
1. 在 Blessing Skin 中安装 yggdrasil 插件，[为游戏服务端加载 authlib-injector](https://github.com/bs-community/yggdrasil-api/wiki/0x03-%E9%85%8D%E5%90%88-authlib-injector-%E4%BD%BF%E7%94%A8)。  
2. 使用 AuthController.php 替换掉 plugins/yggdrasil-api/src/Controllers 中的同名文件。  
3. 使用 GetMd5.exe 获取 mods 文件夹下所有文件的 md5 值，并在网页插件后台填入值。  
4. 为 NsisoLauncher 启用 authlib-injector 验证模型。  

## 自动更新使用方法
自动更新方法为两个模块，**删除** 与 **下载**，启动器的逻辑为先删除，后下载更新。
网页端填写规范：
+ 版本号：版本号命名规范为 `x.x.x` 例如 1.0.0
+ 更新文件：文件1url,文件1保存路径;文件2url,文件2保存路径
+ 删除文件：文件1路径;文件2路径

## 演示  
（如无法查看图片，请刷新网页）
![mainmenu.png](https://github.com/YukiCoco/NsisoLauncher-Server/blob/master/mainmenu.png?raw=true)
![bs-plugin1.png](https://github.com/YukiCoco/NsisoLauncher-Server/blob/master/bs-plugin1.png?raw=true)
