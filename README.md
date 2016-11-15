# Console-link
简易联动框架（初代的）

团队继危机编写器（已集成于Console iT Client）之后的又一力作，危机联动系统的好帮手！

## 内容 ##
- 包含服务端类和客户端类以及对应的示例程序。
- 服务端类和客户端类均以DLL形式发布。
- 基于.NET Framework 4编写，支持Windows Vista及以上系统。
- 可用于危机联动场景下MPC向会场发送危机。
- 开发者可利用本扩展制作联动MPC/主持程序。

## 使用方法 ##
1. 使用Windows Vista和Windows 7的用户请先[下载安装.NET Framework 4](https://www.microsoft.com/zh-CN/download/details.aspx?id=17718)
1. 克隆本项目，并在Visual Studio生成
2. 打开server_holder，选择本机IP地址、端口和路径之后点击Start开始服务
3. 打开若干个client_holder，输入URL并且点击Start连接服务
4. 在服务端编辑框输入标题和正文
5. 选择编码模式（纯文本、MarkDown或LaTeX，其实没有用，都是预留的）
6. 发危机！！！

## 开发者注意
在开发服务端时，需保证服务端程序使用管理员身份运行。请参考server_holder项目下的app.manifest文件。

## 维护者
- [CRH380B-6216L](mailto:muner_szr6216@outlook.com)

## 版权与协议
Copyright 2016 北京市高中生模拟联合国协会

本项目所有代码在 AGPL-3.0 协议下公开。细节请查阅 LICENSE 文件