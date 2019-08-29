# I18NImage 组件

I18NImage 是用来标记一个 GameObject 上存在需要 I18N 处理的 [Image](https://docs.unity3d.com/Manual/script-Image.html) 的组件。

在运行时，I18NImage 组件通过订阅 Rosetta 的 [LocaleChanged](https://molingyu.github.io/RosettaDocs/api/Rosetta.Runtime.Rosetta.html#events) 事件，从而在切换语言后自动读取并替换当前 `GameObject` 的 `Image` 组件内 Source Image(`Sprite`) 字段为当前语言对应的 I18N 图像资源。

![I18NImage](../../res/i18nImage.png)

## 属性
|Name|Description|
|---|:---|
|I18N Comment|多语言资源注释。该注释会随着当前的 I18N 音频资源的模板文件一同交给多语言工作者。|
|I18N Space|该 I18N Image 资源所属的域，关于域的说明请参阅 [使用 Collector 和 Creator: 域](https://molingyu.github.io/RosettaDocs/guides/useCollectorAndCreator.html#space--%E5%9F%9F)|
|Res Name|多语言资源的资源名。在运行时，Rosetta 会通过该值在对应的多语言文件夹内读取同名的文件。|