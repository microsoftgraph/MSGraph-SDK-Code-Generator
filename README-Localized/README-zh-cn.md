[vipr-source-repo]: https://github.com/microsoft/vipr

[![生成状态](https://o365exchange.visualstudio.com/O365%20Sandbox/_apis/build/status/Microsoft%20Graph/msgraph-package-typewriter)](https://o365exchange.visualstudio.com/O365%20Sandbox/_build/latest?definitionId=1728)

# Microsoft Graph SDK 代码生成器

适用于使用 T4 模板的 [VIPR][vipr-source-repo] 的源代码编写器。GraphODataTemplateWriter 从 VIPR 接收 OdcmModel，并将其用于填充此存储库内的 T4 模板。

此编写器当前支持以下目标语言：
- Android
- CSharp
- Java
- Objective-C
- Python
- TypeScript
- PHP

# 内容
- [先决条件](#prerequisites)
- [入门](#getting-started)
- [将 Vipr 与此编写器结合使用](#using-vipr-with-this-writer)
- [参与](#contributing)
- [许可证](#license)

## 先决条件
- [Visual Studio SDK](https://msdn.microsoft.com/zh-cn/library/bb166441.aspx)
- [Visual Studio 建模 SDK](https://msdn.microsoft.com/zh-cn/library/bb126259.aspx)

# 入门

此项目使用 git 子模块来集成上游依赖项，尤其是 [Vipr][vipr-source-repo]。如果需要备用分支以包含特殊修补程序，则需要在子模块中手动将该分支签出。

为了正确打开解决方案，请确保在 Visual Studio 中打开解决方案之前已更新子模块。最初克隆此存储库时，请使用 `git clone --recursive` 在同一时间更新子模块。稍后，请运行 `git submodule update` 以手动更新子模块。如果在进行克隆时不使用 `--recursive` 开关，请先运行`git submodule init` 以初始化子模块。

设置完成后，可照常使用 GraphODataTemplateWriter 解决方案。如果遇到问题，请确保 NuGet 包和项目引用均为最新版本。

有关子模块的详细信息，请参阅 Git 手册中的[本章](http://git-scm.com/book/en/v2/Git-Tools-Submodules)并在网上搜索。

## 使用 Typewriter

Typewriter 是使用 GraphODataTemplateWriter 和 VIPR 来生成代码文件的新解决方案。这是一种旨在简化代码文件生成过程的可执行文件。请生成该解决方案以便在 `\MSGraph-SDK-Code-Generator\src\Typewriter\bin\Release` 中查找 Typewriter 可执行文件。Typewriter 运行选项包括：

* **-l**，**-language**：所生成的代码文件的目标语言。值可以是：`Android`、`Java`、`ObjC`、`CSharp`、`PHP`、`Python`、`TypeScript` 或 `GraphEndpointList`。默认值为 `CSharp`。仅生成由 `-generationmode Metadata` 选项指定的经过清理和批注的元数据时，此选项不适用。
* **-m**，**-metadata**：目标输入元数据的本地文件路径或 URL。默认值为 `https://graph.microsoft.com/v1.0/$metadata`。此值是必需的。
* **-v**，**-verbosity**：日志详细级别。值可以是：`Minimal`、`Info`、`Debug` 或 `Trace`。默认值为 `Minimal`。
* **-o**，**-output**：指定输出文件夹的路径。默认值为包含 typewriter.exe 的目录。输出目录的结构和内容将因 `-generationmode` 和 `-language` 选项而异。
* **-d**，**-docs**：指定 [microsoft-graph-docs](https://github.com/microsoftgraph/microsoft-graph-docs) 存储库的本地根目录的路径。默认值为包含 typewriter.exe 的目录。系统会分析文档以将文档批注提供给元数据，随后用于在生成的代码文件中添加文档注释。当 `-generationmode` 的值为 `Metadata` 或 `Full` 时，此选项是必需的。
* **-g**，**-generationmode**：指定生成模式。值可以是：`Full`、`Metadata` 或 `Files`。`Full`（默认值）生成模式将生成输出代码文件，方法是清理输入元数据、分析文档以及在生成输出文件前添加批注。`Metadata` 生成模式将生成输出元数据文件，方法是清理元数据、分析文档以及添加文档批注。`Files` 生成模式将从输入元数据生成代码文件，并绕过清理、文档分析和添加文档批注的过程。
* **-f**，**-outputMetadataFileName**：基本输出元数据文件名。仅适用于 `-generationmode Metadata`。默认值为 `cleanMetadataWithDescriptions`，与 `-endpointVersion` 值一起使用，以生成名为 `cleanMetadataWithDescriptionsv1.0.xml` 的元数据文件。
* **-e**，**-endpointVersion**：命名元数据文件时使用的终结点版本。期望的值是 `v1.0` 和 `beta`。仅适用于 `-generationmode Metadata`。
* **-p**，**-properties**：指定属性以支持 T4 模板中的生成逻辑。属性必须采用 *key-string:value-string* 的形式。可通过在属性之间设置空格来指定多个属性。目前唯一支持的属性是 *php.namespace* 属性，用于指定生成的模型文件命名空间。此属性是可选的。

### Typewriter 使用示例

#### 在不清理或添加 CSDL 批注的情况下从 CSDL（元数据）文件生成 TypeScript 打字。

输出将进入 `outputTypeScript` 目录。

`.\typewriter.exe -v Info -m D:\cleanMetadataWithDescriptions_v10.xml -o outputTypeScript -l TypeScript -g Files`

#### 清理元数据文件以及使用源于文档存储库的文档批注为元数据文件添加批注

输出元数据文件将进入 `output2` 目录。输出元数据文件将根据默认值命名为 `cleanMetadataWithDescriptionsv1.0.xml`。

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output2 -d D:\repos\microsoft-graph-docs -g Metadata`

#### 从将要进行清理以及使用源于文档存储库的文档批注进行批注的元数据生成 C# 代码文件

输出 C# 代码文件将进入 `output` 目录。

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output -l CSharp -d D:\repos\microsoft-graph-docs -g Full`



## 将 Vipr 与此编写器结合使用

1. 在 Visual Studio 中生成解决方案。
2. 转到 `src\GraphODataTemplateWriter\bin\debug` 文件夹，查找所有已编译的组件。
3. 在该文件夹中，修改 `.config\TemplateWriterSettings.json` 以指定模板映射。有关更多详细信息，请参阅[模板编写器设置](##Template-Writer-Settings)。
4. 在同一文件夹中以管理员身份打开命令提示窗口，然后运行 `Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"`。可在此项目的根目录中找到一个示例元数据文件。

默认情况下，输出源代码将放入到名为“output”的文件夹（在 Vipr 可执行文件旁边）。

## 模板编写器设置
### 可用语言

目前有五种语言可供选择。Java、ObjC、CSharp、TypeScript 和 Python。在 `TargetLanguage` 设置中指定要生成的语言。

### 模板
必须在 `TemplatesDirectory` 设置下指定模板目录。该目录可以是完整路径或相对于运行目录的路径。该目录必须包含要生成代码的每个平台的子目录。请查看 Templates 目录中的示例。

### 模板映射
必须为想要生成的每个平台指定 T4 模板到特定子处理器的映射。`TemplateMapping` 设置是语言和模板列表的字典。每个模板都必须指定：

- `Template`：不含扩展名的模板名称。
- `SubProcessor`：模板的子处理器，请参阅[子处理器](#SubProcessors)。
- `Type`：模板类型。
- `Name`：名称的格式字符串。

并可指定（可选）：

- `Include`：要包含在子处理器中的以分号分隔的对象列表。
- `Exclude`：要从子处理器中排除的以分号分隔的对象列表。
- `Ignore`：要从子处理器中忽略的以分号分隔的对象列表。
- `Matches`：要包含在子处理器中的以分号分隔的对象列表。
- `FileCasing`、`UpperCamel`、`LowerCamel` 或 `Snake`：所要创建的特定文件的文件大小写。

**注意：许多这些可选参数都是在 Vipr 完全支持批注前使用的；现在，由于已向 Vipr 添加了批注，因此这些参数的使用应仅限于旧方案**

示例：

` { "Template":"EntityCollectionPage", "SubProcessor":"NavigationCollectionProperty", "Type":"Request", "Name": "<Class><Property>CollectionPage", "Matches" : "includeThisType", "Exclude" :"ExcludedTypeName;OtherExcludedTypeName" }`

请务必了解子处理器会映射到用于查询 **OdcmModel** 并返回一组 OData 对象的方法。此映射保留在 [TemplateProcess.InitializeSubprocessor()](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/blob/dev/src/GraphODataTemplateWriter/TemplateProcessor/TemplateProcessor.cs#L54) 中。特定于语言的映射存在于[配置目录](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/dev/src/GraphODataTemplateWriter/.config)中。子处理器返回的每个 OData 对象会应用于映射的模板，这会为每个 OData 对象产生代码文件输出。

在上面的示例中，NavigationCollectionProperty 子处理器的结果集中的对象将分别应用于 EntityCollectionPage 模板。每个结果将是 NavigationCollectionProperty 子处理器返回的每个对象的代码文件。 

#### 子处理器

子处理器决定了要将什么类型的 OData 对象传递给模板以生成代码文件。

- `CollectionMethod`：属于集合类型的所有方法
- `CollectionProperty`：集合类型的属性
- `CollectionReferenceProperty`：在非包容集合中使用的集合类型的所有导航属性
- `ComplexType`：所有复杂类型
- `EntityContainer`：EntityContainer
- `EntityReferenceType`：在非包容集合中使用的所有实体类型
- `EntityType`：所有实体类型
- `EnumType`：所有枚举类型
- `MediaEntityType`：所有媒体实体类型
- `Method`：所有操作和函数
- `MethodWithBody`：在 http 请求中发送主体的所有方法和函数
- `NavigationCollectionProperty`：属于集合类型的所有导航属性
- `NonCollectionMethod`：不返回集合的所有方法和函数
- `Other`：整个模型
- `Property`：所有属性类型
- `StreamProperty`：返回 Streams 的所有属性类型

#### 类型

模板的类型。

- `Request`：将发出请求的模板
- `Model`：模型
- `Shared`：不会输出任何代码但由其他模板包含的模板
- `Client`：用于创建客户端对象的模板
- `Other`：任何其他类型


#### 模板名称

使用 `Name` 格式字符串设置模板的名称。你可以插入 `<Class>`、`<Property>`、`<Method>` 和 `<Container>`，值将替换为相应对象的名称。如果插入的某一项不存在，它将被替换为空字符串。
注意：还可以通过 `host.SetTemplateName("foo");` 从模板内设置模板名称。

#### 模板编辑

该解决方案包含一个非生成项目，用于托管实际的 T4 模板并简化这些模板的浏览/编辑操作。此项目将自动发现新模板文件。

#### 包含/排除

在特定情况中可能会希望排除或仅处理子处理器的某些对象。若要执行此操作，可以设置一个要包含的对象列表（以分号分隔各个对象）：`Include : foo;bar`。这种情况将仅处理名称为 foo 或 bar 的对象。与此相反的是排除设置，即子处理器将包含除名称位于排除列表中的对象之外的所有对象，包含和排除不能一起使用。

#### 忽略/匹配
无法使用对象名称来包含或排除时，可对任何对象使用详细描述元素。使用以分号分隔的字符串列表（如：`foo;bar;baz`）插入详细描述。如果添加 `"Matches" : "foo;baz"`，则仅会处理在详细描述中包含 foo 和 baz 的对象。反之对于忽略 (Ignore) 也是如此。

注意：还可以通过 `odcjObject.LongDescriptionContains("foo");` 签入模板

**注意：包含/排除和忽略/匹配是在 Vipr 完全支持批注前使用的；现在，由于已向 Vipr 添加了批注，因此这些参数的使用应仅限于旧方案**

## 根据图形元数据生成

当前，我们采取以下几个步骤将元数据形成一个可以成功生成期望格式的 SDK 的元数据：

  - 删除功能批注（请参阅 [\#132](https://github.com/Microsoft/Vipr/issues/132)）
  - 向缩略图 
     ```xml
     <Annotation String="navigable" Term="Org.OData.Core.V1.LongDescription"/>
     ``` 添加导航批注
  - 从 ```onenotePage``` 和 ```onenoteEntityBaseModel``` 删除 HasStream 属性
  - 向不具有相应 EntitySet 的导航属性添加 ```ContainsTarget="true"```。这目前适用于包含 plannerBucket、plannerTask、plannerPlan 和 plannerDelta 的导航属性
  - 向[文档](https://developer.microsoft.com/zh-cn/graph/docs/concepts/overview)中的类型和属性添加详细描述

为了根据未存储在 [metadata](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/master/metadata) 目录中的其他元数据进行生成，需执行此列表中的前四个步骤。

## 参与

为了能够接受你的拉取请求，你需要以电子方式填写 Microsoft 的[参与者许可协议](https://cla.microsoft.com/)。如果你已对其他 Microsoft 项目执行此操作，则表示你已经符合条件。

此项目遵循 [Microsoft 开放源代码行为准则](https://opensource.microsoft.com/codeofconduct/)。有关详细信息，请参阅[行为准则常见问题解答](https://opensource.microsoft.com/codeofconduct/faq/)。如有其他任何问题或意见，也可联系 [opencode@microsoft.com](mailto:opencode@microsoft.com)。

[为何需要 CLA？](https://www.gnu.org/licenses/why-assign.html)（来自 FSF）

## 许可证

版权所有 (c) Microsoft Corporation。保留所有权利。根据 MIT [许可证](LICENSE)获得许可。
