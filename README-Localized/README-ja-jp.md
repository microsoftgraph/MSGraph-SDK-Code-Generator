[vipr-source-repo]: https://github.com/microsoft/vipr

[![ビルドの状態](https://o365exchange.visualstudio.com/O365%20Sandbox/_apis/build/status/Microsoft%20Graph/msgraph-package-typewriter)](https://o365exchange.visualstudio.com/O365%20Sandbox/_build/latest?definitionId=1728)

# Microsoft Graph SDK Code Generator

T4 テンプレートを利用した [VIPR][vipr-source-repo] のソース コード ライター。GraphODataTemplateWriter は VIPR から OdcmModel を受け取り、この レポジトリ内の T4 テンプレートへの入力に使用します。

現在、このライターは次の対象言語をサポートしています:
- Android
- CSharp
- Java
- Objective-C
- Python
- TypeScript
- PHP

# コンテンツ
- [前提条件](#prerequisites)
- [概要](#getting-started)
- [このライターで Vipr を使用する](#using-vipr-with-this-writer)
- [投稿](#contributing)
- [ライセンス](#license)

## 前提条件
- [Visual Studio SDK](https://msdn.microsoft.com/ja-jp/library/bb166441.aspx)
- [Visual Studio モデリング SDK](https://msdn.microsoft.com/ja-jp/library/bb126259.aspx)

# 概要

このプロジェクトは git サブモジュールを使用して、アップストリームの依存関係、特に [Vipr][vipr-source-repo]を統合します。 特別な修正を含めるために代替ブランチが必要な場合は、サブモジュール内で手動で確認する必要があります。

ソリューションを正常に開くには、Visual Studio で開く前にサブモジュールが更新されていることを確認してください。このリポジトリを最初に複製するときは、`git clone --recursive` を使用してサブモジュールを同時に更新します。後で、`git submodule update` を実行して、サブモジュールを手動で更新します。複製するときに `--recursive` スイッチを使用しない場合は、最初に `git submodule init` を実行してサブモジュールを初期化します。

セットアップが完了したら、通常どおり GraphODataTemplateWriter ソリューションで操作できます。問題が発生した場合は、NuGet パッケージとプロジェクト参照がすべて最新の状態であることを確認してください。

サブモジュールの詳細については、Git ブックから[この章](http://git-scm.com/book/en/v2/Git-Tools-Submodules)を読み、Web を検索してください。

## Typewriter の使用

Typewriter は、GraphODataTemplateWriter と VIPR を使用してコード ファイルを生成するための新しいソリューションです。これは、コード ファイルの生成を簡素化することを目的とした実行可能ファイルです。ソリューションをビルドして、`\MSGraph-SDK-Code-Generator\src\Typewriter\bin\Release` で、Typewrite の実行可能ファイルを検索します。Typewrite の実行オプションは次のとおりです。

* **-l**、**-language**:生成されたコードファイルの対象の言語。値は次のとおりです。`Android`、`Java`、`ObjC`、`CSharp`、`PHP`、`Python`、`TypeScript`、`GraphEndpointList`。既定値は `CSharp` です。`-generationmode メタデータ`オプションで指定された、クリーンアップされた注釈付きのメタデータのみを生成する場合、これは適用されません。
* **-m**、**-metadata**:ターゲット入力メタデータへのローカル ファイル パスまたは URL。既定値は `https://graph.microsoft.com/v1.0/$metadata` です。この値は必須です。
* **-v**、**-verbosity**:ログの詳細レベル。値は次のとおりです。`Minimal`、`Info`、`Debug`、`Trace`。既定値は`Minimal` です。
* **-o**、**-output**:出力フォルダーへのパスを指定します。既定値は、typewriter.exe を含むディレクトリです。出力ディレクトリの構造とコンテンツは、`-generationmode` および `-language` オプションに基づいて異なります。
* **-d**、**-docs**:[microsoft-graph-docs](https://github.com/microsoftgraph/microsoft-graph-docs) リポジトリのローカル ルートへのパスを指定します。既定値は、typewriter.exe を含むディレクトリです。ドキュメントは、生成されたコード ファイルにドキュメント コメントを追加するために使用されるメタデータにドキュメント注釈を提供するために解析されます。`Metadata` または `Full` の `-generationmode` 値を使用する場合、このオプションは必須です。
* **-g**、**-generationmode**:生成モードを指定します。値は次のとおりです。`Full`、`Metadata`、`Files`。`Full` (既定値) 生成モードでは、出力ファイルを生成する前に、入力メタデータのクリーンアップ、ドキュメントの解析、注釈の追加によって出力コード ファイルが生成されます。`Metadata` 生成モードでは、メタデータのクリーンアップ、ドキュメントの解析、ドキュメントの注釈の追加により、出力メタデータ ファイルが生成されます。`Files` 生成モードは、入力メタデータからコード ファイルを生成し、クリーンアップ、ドキュメント解析、およびドキュメント注釈の追加をバイパスします。
* **-f**、**-outputMetadataFileName**:基本出力メタデータ ファイル名。`-generationmode メタデータ`のみに適用されます。既定値は、`cleanMetadataWithDescriptions` で、これは `-endpointVersion` の値とともに使用され、`cleanMetadataWithDescriptionsv1.0.xml` という名前のメタデータ ファイルを生成します。
* **-e**、**-endpointVersion**:メタデータ ファイルに名前を付けるときに使用されるエンドポイント バージョン。期待される値は `v1.0` および `beta` です。`-generationmode メタデータ`のみに適用されます。
* **-p**、**-properties**:T4 テンプレートで生成ロジックをサポートするプロパティを指定します。プロパティは *key-string:value-string* の形式をとる必要があります。プロパティ間にスペースを設定することにより、複数のプロパティを指定できます。現在サポートされているプロパティは、生成されたモデルファイルの名前空間を指定する *php.namespace* プロパティのみです。このプロパティは省略可能です。

### Typewriter の使用例

#### CSDL のクリーンアップや注釈付けを行わずに、CSDL (メタデータ) ファイルから TypeScript タイピングを生成します。

出力は `outputTypeScript` ディレクトリに送られます。

`.\typewriter.exe -v Info -m D:\cleanMetadataWithDescriptions_v10.xml -o outputTypeScript -l TypeScript -g Files`

#### ドキュメント リポジトリから取得したドキュメント注釈を使用して、メタデータファイルをクリーンアップして注釈を付けます。

出力メタデータファイルは、`output2` ディレクトリに送られます。出力メタデータファイルは、既定値に基づいて `cleanMetadataWithDescriptionsv1.0.xml` という名前になります。

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output2 -d D:\repos\microsoft-graph-docs -g Metadata`

#### クリーンアップされ、ドキュメントリポジトリから提供されるドキュメント注釈で注釈付けされるメタデータから C# コード ファイルを生成します。

出力 C# コード ファイルは、`出力`ディレクトリに送られます。

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output -l CSharp -d D:\repos\microsoft-graph-docs -g Full`



## このライターで Vipr を使用する

1. Visual Studio でソリューションをビルドします。
2. `src\GraphODataTemplateWriter\bin\debug` フォルダーに移動して、コンパイルされたすべてのコンポーネントを確認します。
3. このフォルダーで、`.config\TemplateWriterSettings.json` を変更して、テンプレート マッピングを指定します。詳細については、「[テンプレート ライターの設定](##Template-Writer-Settings)」を参照してください。
4. 同じフォルダーで管理者としてコマンド プロンプトを開き、`Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"` を実行します。サンプルのメタデータ ファイルは、このプロジェクトのルートにあります。

既定では、出力ソースコードは Vipr 実行可能ファイルの横の "output" という名前のフォルダーに配置されます。

## テンプレート ライターの設定
### 使用可能な言語

現時点で選択できる言語は 5 つあります。Java、ObjC、CSharp、TypeScript、および Python。`TargetLanguage` 設定で生成する言語を指定します。

### テンプレート
`TemplatesDirectory` 設定でテンプレート ディレクトリを指定する必要があります。ディレクトリは、フルパスでも、実行中のディレクトリからの相対パスでもかまいません。ディレクトリには、コードを生成する各プラットフォームのサブディレクトリが含まれている必要があります。例については、テンプレート ディレクトリを参照してください。

### テンプレート マッピング
生成するプラットフォームごとに、特定のサブプロセッサへの T4 テンプレートのマッピングを指定する必要があります。`TemplateMapping` 設定は、言語の辞書とテンプレートのリストです。各テンプレートは以下を指定する必要があります。

- `Template`: 拡張子のないテンプレートの名前。
- `SubProcessor`: テンプレートのサブプロセッサ。「[サブプロセッサ](#SubProcessors)」を参照してください
- `Type`: テンプレートの種類。
- `Name`: 名前の形式文字列。

そして任意で:

- `Include`: サブプロセッサに含めるオブジェクトのセミコロン区切りリスト。
- `Exclude`: サブプロセッサから除外するオブジェクトのセミコロン区切りリスト。
- `Ignore`:  サブプロセッサから無視するオブジェクトのセミコロン区切りリスト。
- `Matches`: サブプロセッサに含めるオブジェクトのセミコロン区切りリスト。
- `FileCasing`、`UpperCamel`、`LowerCamel`、`Snake` は、作成される特定のファイルのファイル ケーシングに使用します。

**注: これらのオプション パラメーターの多くは、Vipr が注釈を完全にサポートする前に使用されていました。 これらのパラメーターの使用は Vipr に追加されたため、従来のシナリオに限定する必要があります**

例:

` { "Template":"EntityCollectionPage", "SubProcessor":"NavigationCollectionProperty", "Type":"Request", "Name": "<Class><Property>CollectionPage", "Matches" : "includeThisType", "Exclude" :"ExcludedTypeName;OtherExcludedTypeName" }`

サブプロセッサは、**OdcmModel** をクエリし、OData オブジェクトのセットを返すメソッドにマッピングれることを理解することが重要です。このマッピングは [TemplateProcess.InitializeSubprocessor()](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/blob/dev/src/GraphODataTemplateWriter/TemplateProcessor/TemplateProcessor.cs#L54) で維持されます。言語固有のマッピングは [config ディレクトリ](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/dev/src/GraphODataTemplateWriter/.config)に存在します。サブプロセッサによって返される各 OData オブジェクトは、マッピングされるテンプレートに適用され、各 OData オブジェクトごとにコード ファイルが出力されます。

上記の例では、NavigationCollectionProperty サブプロセッサの結果セットのオブジェクトがそれぞれ EntityCollectionPage テンプレートに適用されます。各結果は、NavigationCollectionProperty サブプロセッサによって返される各オブジェクトのコード ファイルになります。 

#### SubProcessors

SubProcessors は、コード ファイルを生成するテンプレートに渡される OData オブジェクトの種類を決定します。

- `CollectionMethod` コレクション型のすべてのメソッド
- `CollectionProperty` コレクション型のプロパティ
- `CollectionReferenceProperty` 非包含コレクションで使用されるコレクション型のすべてのナビゲーション プロパティ
- `ComplexType` すべての複合型
- `EntityContainer` エンティティ コンテナ
- `EntityReferenceType` 非包含コレクションで使用されるすべてのエンティティ型
- `EntityType` すべてのエンティティ型
- `EnumType` すべての列挙型
- `MediaEntityType` すべてのメディア エンティティ型
- `Method` すべてのアクションと機能
- `MethodWithBody` HTTP リクエスト内で本文を送信するすべてのメソッドと関数
- `NavigationCollectionProperty` コレクション型のすべてのナビゲーション プロパティ
- `NonCollectionMethod` コレクションを返さないすべてのメソッドと関数
- `Other` モデル全体。
- `Property` すべてのプロパティ型
- `StreamProperty` ストリームを返すすべてのプロパティ型

#### 型

テンプレート型。

- `Request` 要求を行うテンプレート
- `Model` モデル
- `Shared` コードを出力しないが、他のテンプレートに含まれるテンプレート
- `Client` クライアント オブジェクトの作成に使用されるテンプレート
- `Other` その他の型


#### テンプレート名

`Name` 形式文字列を使用してテンプレートの名前を設定します。`<Class>`、`<Property>`、`<Method>`、`<Container>` を挿入できます。値は対応するオブジェクトの名前に置き換えられます。存在しない項目を挿入すると、空の文字列に置き換えられます。
注:以下により、テンプレート内からテンプレート名を設定することもできます: `host.SetTemplateName("foo");`

#### テンプレート編集

ソリューションには、実際の T4 テンプレートをホストし、ブラウジング/編集を簡単にする非構築プロジェクトが含まれています。このプロジェクトによって、新しいテンプレート ファイルが自動的に検出されます。

#### 包含/除外

SubProcessor から特定のオブジェクトを除外したり、特定のオブジェクトのみを処理したりする特定の時間がある場合があります。ここれを行うには、含めるオブジェクトのセミコロン区切りリストを設定します。`以下を含めます: foo;bar`。この場合、名前が foo または bar のオブジェクトだけが処理されます。これの反対は、SubProcessorに含まれるオブジェクトから除外リストにある名前のすべてのオブジェクトを除外する除外設定です。包含と除外を一緒に使用できません。

#### 無視/一致
オブジェクトの名前を使用して包含または除外できない場合は、任意のオブジェクトで長い説明要素を使用できます。以下のようのなセミコロンで区切られた文字列リストを含む長い説明を挿入します: `foo;bar;baz`。`"Matches" : "foo;baz"` を追加すると、長い説明の foo と baz を含むオブジェクトのみが処理されます。無視はこの逆です。

注:`odcjObject.LongDescriptionContains("foo");` によってテンプレートをチェック インすることもできます。

**注: Vipr が注釈を完全にサポートする前に、包含/除外および無視/一致が使用されました。 これらのパラメーターの使用は Vipr に追加されたため、従来のシナリオに限定する必要があります**

## グラフ メタデータに対する構築

現在、メタデータを期待する形の SDK を正常に生成するメタデータにするために行ういくつかの手順があります。

  - 機能の注釈を削除します ([\#132](https://github.com/Microsoft/Vipr/issues/132) を参照)
  - ナビゲーション注釈をサムネイル
     ```xml
     <Annotation String="navigable" Term="Org.OData.Core.V1.LongDescription"/>
     ```に追加します
  - ```onenotePage``` および ```onenoteEntityBaseModel``` から HasStream プロパティを削除します
  - ```ContainsTarget="true"``` を、対応する EntitySet がないナビゲーションプロパティに追加します。これは現在、plannerBucket、plannerTask、plannerPlan、plannerDelta を含むナビゲーション プロパティに適用されます。
  - [docs](https://developer.microsoft.com/ja-jp/graph/docs/concepts/overview) からのタイプとプロパティに長い説明を追加します

[メタデータ](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/master/metadata)ディレクトリに保存されているメタデータ以外のメタデータに対してビルドするには、このリストの最初の 4 つを実行する必要があります。

## 投稿

プル要求が承諾される前に Microsoft の[投稿者のライセンス契約](https://cla.microsoft.com/)を電子的に完了する必要があります。他の Microsoft プロジェクトでこれを行った場合は、既に説明されています。

このプロジェクトでは、[Microsoft オープン ソース倫理規定](https://opensource.microsoft.com/codeofconduct/)が採用されています。詳細については、「[倫理規定の FAQ](https://opensource.microsoft.com/codeofconduct/faq/)」を参照してください。また、その他の質問やコメントがあれば、[opencode@microsoft.com](mailto:opencode@microsoft.com) までお問い合わせください。

[なぜ CLA なのか?](https://www.gnu.org/licenses/why-assign.html) (FSF から)

## ライセンス

Copyright (c) Microsoft Corporation.All Rights Reserved.Licensed under the [MIT license](LICENSE).
