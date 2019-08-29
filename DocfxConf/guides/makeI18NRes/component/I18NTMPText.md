# I18NTMPText 组件

I18NTMPText 和 I18NText 类似，不同的是， I18NTMPText 是用来支持 [Text Mesh Pro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@2.0/manual/index.html) 插件的 [Text Mesh Pro UGUI](http://digitalnativestudios.com/textmeshpro/docs/textmeshpro-component/) 组件。

在运行时，I18NTMPText 组件通过订阅 Rosetta 的 [LocaleChanged](https://molingyu.github.io/RosettaDocs/api/Rosetta.Runtime.Rosetta.html#events) 事件，从而在切换语言后自动读取并替换当前 `GameObject` 的 `Text Mesh Pro UGUI` 组件内 `Text` 字段为当前语言对应的 I18N 文本资源。根据设置不同，可能还会替换 
`font`
 字段。

![I18NTMPText](../../res/i18nTMPText.png)

## 属性
|Name|Description|
|---|:---|
|I18N Comment|多语言资源注释。该注释会随着当前的 I18N 音频资源的模板文件一同交给多语言工作者。|
|I18N Space|该 I18N TMPText 资源所属的域，关于域的说明请参阅 [使用 Collector 和 Creator: 域](https://molingyu.github.io/RosettaDocs/guides/useCollectorAndCreator.html#space--%E5%9F%9F)|
|Is Virtual Font|是否启用虚拟字体设定。当启用后，Rosetta 加载时会先去根据 `Virtual Font Name` 读取虚拟字体配置文件，然后获取对应语言的真实字体文件的名称后再加载并应用。|
|Virtual Font Name*|虚拟字体配置文件的名称。|

*注： 只有当 `IsVirtualFont` 为真时，该字段才有用。另外，由于 TMPFont 的特殊性，暂时 Rosetta 只支持通过 Resources 文件夹加载，而无法从外部运行时加载。如果你有特殊需求，可以通过自定义 
Loader 来支持他。*