# 使用 Collector 和 Creator

当你通过 I18N 标记完成对 I18N 资源的标记后，你可以通过创建 Collector 和 Creator 生成你的 翻译模板文件了。Collector 会自动扫描指定的 Unity 资源文件，从中收集需要进行 I18N 
的资源。之后这些数据会传入给对应的 Creator 从而生成特定格式的翻译模板文件。

## Collector | 收集器
Collector 是在 Creator 内部使用的类。一个 Creator 可以拥有多个 Collector。以使得他们可以从不同的地方来收集相同类型的I18N 资源。

* **SceneCollector & PrefabCollector** 

通过扫描 `GameObject` 上是否挂载了 I18NComponent 从  `Scene`/`Prefab` 上搜集 I18N 数据。

* **ScriptableObjectCollector**

通过反射获取类型信息，判断其是否存在 I18NAttribute 而收集 `ScriptableObject` 上的 I18N 数据。

![ScriptableObjectCollector](./res/scriptableObjectCollector.png)

`ScriptableObjectCollector` 会扫描指定的文件夹，然后自动载入所有的 `ScriptableObject` 资源。

## Creator | 创建器

* **PotCreator**

![PotCreator](./res/potCreator.png)

`PotCreator` 负责创建 Pot 格式的翻译模板文件。关于 Pot 的更多内容可以访问[这里](https://www.gnu.org/software/gettext/manual/gettext.html#PO-Files)了解。

### 属性
|Name|Description|
|---|:---|
|Name|生成的pot文件的名字，同时该值也意味着该资源的**域**（space）。|
|Output Path|模板文件的输出地址。|
|Info|等同于 Po 文件的[文件头部分](https://www.gnu.org/software/gettext/manual/gettext.html#Header-Entry)|

* **MediaCreator**

![MediaCreator](./res/mediaCreator.png)

### 属性
|Name|Description|
|---|:---|
|Name|生成的pot文件的名字，同时该值也意味着该资源的**域**（space）。|
|Output Path|模板文件的输出地址。|

## Space | 域

在我们使用 Creator 的过程中，会发现每个 Creator 都需要指定一个 `space`。Rosetta 创造这个概念的主要目的是为了方便 I18N 资源的分段加载。对于游戏来说，I18N 
资源量大，而且往往不仅仅只是文本。一次性载入所有的 I18N 资源会占用太多的内存资源。这时候就需要通过 `space` 
来把资源做分段处理。然后我们只需要在用到的时候加载，然后在不需要的时候释放就可以了（关于缓存和释放的问题，可以参考[这个]()）。

Rosetta 默认所有的 UI 部分的 I18N 资源处于一个名为 `ui` 的域中，这个域默认全局常驻。除此之外，你可以为每个关卡创建一个域，这样就可以在加载关卡数据的时候同时加载其对应的 I18N 
资源。而当退出关卡的时候卸载掉其对应的域从而释放这些不需要的 I18N 资源。