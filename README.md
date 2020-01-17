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
从 Release 下载文件。  
将插件解压到 Blessing Skin 目录下的 plugins 文件夹里。  
打开启动器设置中的 Minecraft服务器设置 ，勾选服务器模式，填入皮肤站地址。  
启用防作弊：  
1. 在 Blessing Skin 中安装 yggdrasil 插件，[为游戏服务端加载 authlib-injector](https://github.com/bs-community/yggdrasil-api/wiki/0x03-%E9%85%8D%E5%90%88-authlib-injector-%E4%BD%BF%E7%94%A8)。  
2. 使用 AuthController.php 替换掉 plugins/yggdrasil-api/src/Controllers 中的同名文件。  
3. 使用 GetMd5.exe 获取 mods 文件夹下所有文件的 md5 值，并在网页插件后台填入值。  
4. 为 NsisoLauncher 启用 authlib-injector 验证模型。  

Enjoy it!
## 演示
![QQ截图20200113010525.png](https://magic.yukino.co/view/1/2020/01/13/fo3pnJGa/QQ%E6%88%AA%E5%9B%BE20200113010525.png)
![QQ截图20200118000050.png](https://magic.yukino.co/view/1/2020/01/18/jBxCJwzw/QQ%E6%88%AA%E5%9B%BE20200118000050.png)
![QQ截图20200113010539.png](https://magic.yukino.co/view/1/2020/01/17/S5UBA0SR/QQ%E6%88%AA%E5%9B%BE20200117233345.png)
