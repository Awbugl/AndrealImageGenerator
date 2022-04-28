## Andreal ImageGenerator

* 为Bot开发者提供的Andreal查分图生成器

#### 感谢

> 本项目的图片UI设计来源于 GNAQ、linwenxuan04、雨笙Fracture (按首字母排序)。

#### 用户须知

> 您应知悉，使用本项目将违反 [Arcaea使用条款](https://arcaea.lowiro.com/zh/terms_of_service) 中的 3.2-4 和 3.2-6，以及 [Arcaea二次创作管理条例](https://arcaea.lowiro.com/zh/derivative_policy) 。
>
> 因使用本项目而造成的损失，Andreal开发组不予承担任何责任。

----

#### 使用前配置

* 创建 ImageGenerator 文件夹

* 根据您的服务器是否有dotnet sdk 6.0 选择 ImageGenerator.zip 版本，下载并解压到 ImageGenerator 文件夹中

* 解压 AndrealSource.zip 到 ImageGenerator 文件夹中

* 配置 ImageGenerator/Andreal/Config/config.json

* 安装 ImageGenerator/Andreal/Fonts 目录下的字体文件

----

#### 调用参数定义

```
    args[0]             args[1]     args[2]        args[3]                     args[4]
    1|2|3|b30|b40|ala   best|info   base64(json)   base64(ala-userinfo-json)   usercode

    args[3] and args[4] only for ala-b30
    args[2] json from AUA except args[0] is "ala"
```

##### Example:

```
    > ImageGenerator.exe 3 best $jsonb64

    > ImageGenerator.exe b30 - $jsonb64

    > ImageGenerator.exe ala - $jsonbest30b64 $jsonuserinfob64 $usercode
```

返回值为图像文件路径

----

#### 配置文件介绍

###### 配置文件目录:

    ./Andreal/Config/

**config.json**

* 外部Arcaea解包文件路径，**Arcaea解包文件需要手动更新**

**positioninfo.json**

* 图查立绘位置

**arcsong.json**

* Arcaea曲目信息，与ArcaeaUnlimitedApi的song/list接口一致，**需要手动更新**

