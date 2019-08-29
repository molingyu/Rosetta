# I18NAudio 组件

I18NAudio 是用来标记一个 GameObject 上存在需要 I18N 处理的 [AudioSource](https://docs.unity3d.com/Manual/class-AudioSource.html) 的组件。

在运行时，I18NAudio 组件通过订阅 Rosetta 的 [LocaleChanged](https://molingyu.github.io/RosettaDocs/api/Rosetta.Runtime.Rosetta.html#events) 事件，从而在切换语言后自动读取并替换当前 `GameObject` 的 `AudioSource` 组件内 `AudioClip` 字段为当前语言对应的 I18N 音频资源。

![I18NAudio](../../res/i18nAudio.png)


## 属性
|Name|Description|
|---|:---|
|I18N Comment|多语言资源注释。该注释会随着当前的 I18N 音频资源的模板文件一同交给多语言工作者。|
|I18N Space|该 I18N Audio 资源所属的域，关于域的说明请参阅 [使用 Collector 和 Creator: 域](https://molingyu.github.io/RosettaDocs/guides/useCollectorAndCreator.html#space--%E5%9F%9F)|
|Res Name|多语言资源的资源名。在运行时，Rosetta 会通过该值在对应的多语言文件夹内读取同名的文件。|