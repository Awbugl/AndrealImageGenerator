## Andreal ImageGenerator

* 为Bot开发者提供的Andreal查分图生成器

#### 感谢

> 本项目的图片UI设计来源于 GNAQ、linwenxuan04、雨笙Fracture (按首字母排序)。

#### 用户须知

> 您应知悉，使用本项目将违反 [Arcaea使用条款](https://arcaea.lowiro.com/zh/terms_of_service) 中的 3.2-4 和 3.2-6，以及 [Arcaea二次创作管理条例](https://arcaea.lowiro.com/zh/derivative_policy) 。
>
> 因使用本项目而造成的损失，Andreal开发组不予承担任何责任。

#### 对外API

- `POST /user/best30/` 生成用户的最近30首谱面的查分图
  - `image_version` 图片类型，作为查询参数，可选值为 `1` 和 `2`
  - `body` UnofficialArcaeaAPI 的对应API的返回值，作为请求体

- `POST /user/best/` 生成用户的最高分数样式的查分图
  - `image_version` 图片类型，作为查询参数，可选值为 `1`、`2`、`3`
  - `body` UnofficialArcaeaAPI 的对应API的返回值，作为请求体

- `POST /user/info/` 生成用户的最近分数样式的查分图
  - `image_version` 图片类型，作为查询参数，可选值为 `1`、`2`、`3`
  - `body` UnofficialArcaeaAPI 的对应API的返回值，作为请求体

* UnofficialArcaeaAPI 的返回值格式请参考 [UnofficialArcaeaAPI.Docs](https://github.com/Arcaea-Infinity/UnofficialArcaeaAPI.Docs)

#### 本地部署

1. 下载本项目的[release版本](https://github.com/Awbugl/AndrealImageGenerator/releases/)

2. 运行 `AndrealImageGenerator.exe` 即可启动服务
