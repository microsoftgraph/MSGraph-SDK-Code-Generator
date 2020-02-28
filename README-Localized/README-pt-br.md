[vipr-source-repo]: https://github.com/microsoft/vipr

[![Status da compilação](https://o365exchange.visualstudio.com/O365%20Sandbox/_apis/build/status/Microsoft%20Graph/msgraph-package-typewriter)](https://o365exchange.visualstudio.com/O365%20Sandbox/_build/latest?definitionId=1728)

# Gerador de código SDK do Microsoft Graph

Escritores de código-fonte para [VIPR][vipr-source-repo] utilizando modelos T4. O GraphODataTemplateWriter recebe um OdcmModel de VIPR e o utiliza para preencher um modelo T4 localizado dentro deste repositório.

Atualmente, as seguintes linguagens de destino são suportadas por este escritor:
- Android
- CSharp
- Java
- Objective-C
- Python
- TypeScript
- PHP

# Conteúdos
- [Pré-requisitos](#prerequisites)
- [Introdução](#getting-started)
- [Usando o Vipr com este escritor](#using-vipr-with-this-writer)
- [Colaboração](#contributing)
- [Licença](#license)

## Pré-requisitos
- [Visual Studio SDK](https://msdn.microsoft.com/pt-br/library/bb166441.aspx)
- [SDK de modelagem do Visual Studio](https://msdn.microsoft.com/pt-br/library/bb126259.aspx)

# Introdução

Este projeto usa sub-módulos git para integrar dependências upstream, especificamente o [Vipr][vipr-source-repo]. Se você precisar de um branch alternativo para incluir correções especiais, precisará verificar isso manualmente no submódulo.

Para que a solução seja aberta corretamente, certifique-se de que os submódulos sejam atualizados antes de abri-la no Visual Studio. Ao clonar inicialmente esse repositório, use `clone de git --recursivo` para atualizar submódulos ao mesmo tempo. Mais tarde, execute ` atualizar submódulo de git` para atualizar manualmente os submódulos. Se você não usar a opção `--recursivo` durante a clonagem, execute `submódulo git inicial` primeiro para inicializar o submódulo.

Quando a instalação estiver concluída, você poderá trabalhar com a solução GraphODataTemplateWriter como de costume. Se você tiver problemas, verifique se pacotes NuGet e referências do projeto estão todos atualizados.

Para obter mais informações sobre os submódulos, leia [este capítulo](http://git-scm.com/book/en/v2/Git-Tools-Submodules) do livro Git e pesquise na Web.

## Usando a Máquina de Escrever

Máquina de Escrever é uma nova solução para gerar arquivos de código usando a GraphODataTemplateWriter e a VIPR. É um executável que pretende simplificar a geração de arquivos de código. Crie a solução para localizar a máquina de escrever executável em `\MSGraph-SDK-Code-Generator\src\Typewriter\bin\Release`. As opções de execução da máquina de escrever são:

* **-l**, **-linguagem**: O idioma de destino para os arquivos de código gerados. Os valores podem ser: `Android`, `Java`, `ObjC`, `CSharp`, `PHP`, `Python`, `TypeScript` ou `GraphEndpointList`. O valor padrão é `CSharp`. Isso não é aplicável ao gerar apenas metadados limpos e anotados, conforme especificado pela opção `metadados generationmode`.
* **-m**, **-metadados**: O caminho do arquivo local ou a URL para os metadados de entrada de destino. O valor padrão é `https://graph.microsoft.com/v1.0/$metadata`. Esse valor é obrigatório.
* **-v**, **-detalhamento**: O nível de detalhamento do log. Os valores podem ser: `Mínimo`, `Informações`, `Depuração` ou `Rastreamento`. O valor padrão é `Mínimo`.
* **-o**, **-saída**: Especifica o caminho para a pasta de saída. O valor padrão é o diretório que contém typewriter. exe. A estrutura e o conteúdo do diretório de saída serão diferentes com base nas opções `-generationmode` e `-idioma`.
* **-d**, **-documentos**: Especifica o caminho para a raiz local do repositório [microsoft-graph-docs](https://github.com/microsoftgraph/microsoft-graph-docs). O valor padrão é o diretório que contém typewriter. exe. A documentação é analisada para fornecer anotações de documentação aos metadados que são usados ​​para adicionar comentários de documentos nos arquivos de código gerados. Essa opção é necessária ao usar os valores `-generationcmode` de `Metadados` ou `Completo`.
* **-g**, **-generationmode**: Especifica o modo de geração. Os valores podem ser: `Completo`, `Metadados` ou `Arquivos`. O modo de geração `Completo` (padrão) produz os arquivos de código de saída limpando os metadados de entrada, analisando a documentação e adicionando anotações antes de gerar os arquivos de saída. O modo de geração `Metadados` produz um arquivo de metadados de saída limpando metadados, analisando a documentação e adicionando anotações à documentação. O modo de geração `Arquivos` produz arquivos de código a partir de metadados de entrada e ignora a limpeza, a análise de documentação e a adição de anotações de documentação.
* **-f**, **-outputMetadataFileName**: O nome de arquivo de metadados de saída básicos. Aplicável somente a `Metadados -generationmode`. O valor padrão é `cleanMetadataWithDescriptions` que é usado com o valor da `-endpointVersion` para gerar um arquivo de metadados chamado `cleanMetadataWithDescriptionsv1.0.xml`.
* **-e**, **-endpointVersion**: A versão do ponto de extremidade usada ao nomear um arquivo de metadados. Os valores esperados são `v1.0` e `beta`. Aplicável somente a `Metadados -generationmode`.
* **-p**, **-propriedades**: Especifica propriedades para suportar a lógica de geração nos modelos T4. As propriedades devem assumir o formato de *cadeia de chave:cadeia de valor*. Várias propriedades podem ser especificadas, definindo um espaço entre as propriedades. A única propriedade com suporte no momento é a propriedade *php.namespace* para especificar o espaço para nome de arquivo de modelo gerado. Essa propriedade é opcional.

### Exemplo de uso da máquina de escrever

#### Gera digitações de TypeScript de um arquivo CSDL (metadados) sem limpar ou fazer anotações sobre o CSDL.

A saída será direcionada para o diretório `outputTypeScript`.

`.\typewriter.exe -v Info -m D:\cleanMetadataWithDescriptions_v10.xml -o outputTypeScript -l TypeScript -g Files`

#### Limpar e fazer anotações em um arquivo de metadados com anotações de documentação provenientes do repositório de documentos

O arquivo de metadados de saída será direcionado para o diretório `output2`. O arquivo de metadados de saída será denominado `cleanMetadataWithDescriptionsv1.0.xml` com base nos valores padrão.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output2 -d D:\repos\microsoft-graph-docs -g Metadata`

#### Gerar arquivos de código C # a partir dos metadados que serão limpos e anotados com anotações da documentação provenientes do repositório de documentos

Os arquivos de código C # de saída irão para o diretório de `saída`.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output -l CSharp -d D:\repos\microsoft-graph-docs -g Full`



## Usando o Vipr com este Escritor

1. Crie a solução no Visual Studio.
2. Vá para a pasta `src\GraphODataTemplateWriter\bin\debug` para localizar todos os componentes compilados.
3. Nesta pasta, modifique `.config\TemplateWriterSettings.json` para especificar seu mapeamento de modelo, confira [Configurações do Escritor de Modelo](##Template-Writer-Settings) para obter mais detalhes.
4. Abra um aviso de comando como administrador na mesma pasta e execute `Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"`. Um exemplo de arquivo de metadados pode ser encontrado na raiz deste projeto.

Por padrão, o código-fonte de saída será colocado em uma pasta chamada "saída" ao lado do executável do Vipr.

## Configurações do Escritor de Modelos
### Idiomas Disponíveis

Há cinco idiomas que você pode escolher no momento. Java, ObjC, CSharp, TypeScript e Python. Especifique o idioma que você deseja gerar na configuração `TargetLanguage`.

### Modelos
Você deve especificar um diretório de modelos nas Configurações do `TemplatesDirectory`. O diretório pode ser um caminho completo ou relativo ao diretório em execução. O diretório deve conter uma subpasta para cada plataforma para a qual você deseja gerar código. Confira o diretório de Modelos para obter um exemplo.

### Mapeamento de Modelos
Você deve especificar o mapeamento de modelos T4 para Subprocessadores específicos para cada plataforma que deseja gerar. A configuração `TemplateMapping` é um dicionário de idiomas e uma lista de modelos. Cada modelo deve especificar:

- `Modelo`, o nome do modelo sem as extensões.
- `Subprocessador` O Subprocessador para o modelo confira [Subprocessadores](#SubProcessors)
- `Tipo` O tipo de modelo.
- `Nome` A cadeia de caracteres de formato do nome.

e opcionalmente:

- `Incluir`, uma lista de objetos delimitadas por ponto e vírgula a ser incluída no subprocessador.
- `Excluir`, uma lista de objetos delimitadas por ponto e vírgula a ser excluída do subprocessador.
- `Ignorar`, uma lista de objetos delimitadas por ponto e vírgula a ser ignorada do subprocessador.
- `Correspondências`, uma lista de objetos delimitadas por ponto e vírgula a ser incluída no subprocessador.
- `FileCasing`, `UpperCamel`, `LowerCamel` ou `Snake` para o arquivo maíusculas/minúsculas específico que está sendo criado.

**Observação: Muitos desses parâmetros opcionais foram usados ​​antes do Vipr ter suporte total para anotações; agora que as anotações foram adicionadas ao Vipr, o uso desses parâmetros deve ser limitado aos cenários herdados**

Exemplo:

` { "Modelo": "EntityCollectionPage", "Subprocessador": "NavigationCollectionProperty", "Tipo": "Solicitação", "Nome": "<Classe><Propriedade>CollectionPage", "Correspondência" : "includeThisType", "Excluir" : "ExcludedTypeName;OtherExcludedTypeName" }`

É importante compreender que os subprocessadores são mapeados para métodos que consultam o **OdcmModel** e retornam um conjunto de objetos OData. Esse mapeamento é mantido no [TemplateProcess.InitializeSubprocessor()](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/blob/dev/src/GraphODataTemplateWriter/TemplateProcessor/TemplateProcessor.cs#L54). Os mapeamentos específicos do idioma existem na [configuração do diretório](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/dev/src/GraphODataTemplateWriter/.config). Cada objeto OData retornado pelo subprocessador é aplicado ao modelo mapeado que resulta em uma saída do arquivo de código para cada objeto OData.

No exemplo acima, os objetos no conjunto de resultados do subprocessador NavigationCollectionProperty serão aplicados ao modelo EntityCollectionPage. Cada resultado será um arquivo de código para cada objeto retornado pelo subprocessador NavigationCollectionProperty. 

#### Subprocessadores

Os subprocessadores determinam qual tipo de objeto OData será passado para o modelo que gera o arquivo de código.

- `CollectionMethod` Todos os métodos que são do tipo Coleções
- `Collectionproperty` Todos os métodos que são do tipo coleção
- `CollectionReferenceProperty` Todas as Propriedades de Navegação que são do tipo Coleção e são usadas em Coleções Sem Contenção
- `ComplexType` Todos os tipos Complexos
- `EntityContainer` O EntityContainer
- `EntityReferencetype` Todos os tipos de Entidade que são usados em Coleções Sem Contenção
- `EntityType` Todos os tipos de Entidade
- `EnumType` Todos os tipos Enumeráveis
- `MediaEntityType` Todos os tipos de Entidade de Mídia
- `Método` Todas as Ações e Funções
- `MethodWithBody` Todos os Métodos e Funções que enviam um corpo dentro da solicitação http
- `NavigationCollectionProperty` Todas as Propriedades de Navegação do tipo Coleção
- `NonCollectionMethod` Todos os Métodos e Funções que não retornam uma coleção
- `Outros` Todo o modelo.
- `Propriedade` Todos os tipos de propriedades
- `Streamproperty` Todos os tipos de Propriedades que retornam fluxos

#### Tipos

O tipo de modelo.

- `Solicitação` Um modelo que fará uma solicitação
- `Modelo` Um modelo
- `Compartilhado` Um modelo que não produz nenhum código, mas é incluído por outros modelos
- `Cliente` O modelo usado para criar o objeto Cliente
- `Outros` Qualquer outro tipo


#### Nome do Modelo

Para definir o nome do modelo usando a cadeia de caracteres de formato de `Nome`. Você pode inserir `<Classe>`, `<Propriedade>`, `<Método>` e `<Contêiner>` os valores serão substituídos pelos nomes do objeto correspondente. Se você inserir um item que não existe, ele será substituído por uma cadeia de caracteres vazia.
Observação: Você também pode definir o nome do modelo de dentro do modelo por: `host.SetTemplateName("foo");`

#### Edição de Modelos

A solução contém um projeto não criado para hospedar os modelos T4 reais, facilitando a navegação/edição. Os novos arquivos de modelo serão automaticamente detectados por esse projeto.

#### Inclui/Exclui

Pode haver ocasiões específicas em que você deseja excluir ou processar somente certos objetos do subprocessador. Para Fazer isso, você pode definir uma lista delimitada por ponto e vírgula de objetos que você deseja incluir: `Inclui: foo;bar`. Isso só processará objetos cujos nomes sejam foo ou bar. O oposto disso é a configuração de exclusão, na qual o Subprocessador incluirá todos os objetos, exceto aqueles cujos nomes estão na lista de exclusões, excluir e incluir não podem ser usados ​​juntos.

#### Ignorar/Correspondências
Quando não é possível usar o nome de um objeto para incluí-lo ou excluí-lo, você pode usar o elemento descrição longa em qualquer objeto. Insira uma descrição longa com uma lista de cadeias de caracteres delimitadas por ponto e vírgula, como: `foo; bar;baz`. Se você adicionar uma `"Correspondências" : "foo;baz"` apenas objetos que contenham foo e baz na descrição longa serão processados. O oposto é verdadeiro para Ignorar.

Observação: Você também pode fazer o check-in de um modelo por `odcjObject.LongDescriptionContains("foo");`

**Observação: Inclui/Exclui e Ignorar/Correspondências foram usadas antes do Vipr ter suporte total para anotações; agora que as anotações foram adicionadas ao Vipr, o uso desses parâmetros deve estar limitado a cenários herdados**

## Construir em Metadados de Gráfico

Atualmente, há várias etapas que usamos para formar os metadados em um que gerará SDKs com sucesso da forma esperada:

  - Remover anotações de recursos (confira [\#132](https://github.com/Microsoft/Vipr/issues/132))
  - Adicionar anotação de navegação à miniatura
     ```xml
     <Annotation String="navigable" Term="Org.OData.Core.V1.LongDescription"/>
     ```
  - Remover as propriedades do HasStream de ```onenotePage``` e ```onenoteEntityBaseModel```
  - Adicione ```ContainsTarget="true"``` às propriedades de navegação que não têm um EntitySet correspondente. Atualmente, isso se aplica a propriedades de navegação que contêm plannerBucket, plannerTask, plannerPlan e plannerDelta.
  - Adicionar descrições longas aos tipos e propriedades de [documentos](https://developer.microsoft.com/pt-br/graph/docs/concepts/overview)

Para criar metadados diferentes dos armazenados no diretório de [metadados](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/master/metadata), você precisará executar os primeiros quatro nesta lista.

## Colaboração

Antes de aceitar a solicitação pull, você precisará concluir eletronicamente o [Contrato de Licença de Colaborador](https://cla.microsoft.com/) da Microsoft. Caso tenha feito isso para outros projetos da Microsoft, você já estará coberto.

Este projeto adotou o [Código de Conduta de Código Aberto da Microsoft](https://opensource.microsoft.com/codeofconduct/).  Para saber mais, confira as [Perguntas frequentes sobre o Código de Conduta](https://opensource.microsoft.com/codeofconduct/faq/) ou entre em contato pelo [opencode@microsoft.com](mailto:opencode@microsoft.com) se tiver outras dúvidas ou comentários.

[Por que um CLA?](https://www.gnu.org/licenses/why-assign.html) (da FSF)

## Licença

Copyright (c) Microsoft Corporation. Todos os direitos reservados. Licenciada sob a [Licença do MIT](LICENSE).
