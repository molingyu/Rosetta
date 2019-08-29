# 使用 I18N Attribute

Rosetta 总共提供以下几种 I18N Attribute：
* I18NClass
* I18NString
* I18NAudio
* I18NImage

### I18NClass
`I18NClass` 用来标记一个类是否需要进行 I18N 处理。Rosetta 会先从程序集里查找所有所有有 `I18NClass` 标记的类。然后再进一步扫描这些类的字段（field）。

### I18NString

`I18NString` 用来标记一个类的字段是否为一个 I18N 字符串。

### I18NAudio

`I18NAudio` 用来标记一个类的字段是否为一个 I18N 音频。

### I18NImage

`I18NAudio` 用来标记一个类的字段是否为一个 I18N 图片。


需要注意的是 `I18NString`/`I18NAudio`/`I18NAudio` 三者均可以传入一个 comment 参数。其值会在生成 Pot 文档时用作 "#." 注释。
