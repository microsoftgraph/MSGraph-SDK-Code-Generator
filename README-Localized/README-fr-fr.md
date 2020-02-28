[vipr-source-repo]: https://github.com/microsoft/vipr

[![État de création](https://o365exchange.visualstudio.com/O365%20Sandbox/_apis/build/status/Microsoft%20Graph/msgraph-package-typewriter)](https://o365exchange.visualstudio.com/O365%20Sandbox/_build/latest?definitionId=1728)

# Générateur de code SDK Microsoft Graph

Auteurs de code source pour [VIPR][vipr-source-repo] à l’aide de modèles T4. Le GraphODataTemplateWriter reçoit un OdcmModel de VIPR et l’utilise pour remplir dans un modèle T4 situé au sein de ce référentiel.

Pour l’instant, les langues cibles suivantes sont prises en charge par ce rédacteur :
- Android
- CSharp
- Java
- Objective-C
- Python
- TypeScript
- PHP

# Contenu
- [Conditions préalables](#prerequisites)
- [Prise en main](#getting-started)
- [Utilisation de Vipr avec ce rédacteur](#using-vipr-with-this-writer)
- [Contribution](#contributing)
- [License](#license)

## Conditions préalables
- [Kit de développement logiciel (SDK) Visual Studio](https://msdn.microsoft.com/fr-fr/library/bb166441.aspx)
- [Kit de développement de modèles Visual Studio](https://msdn.microsoft.com/fr-fr/library/bb126259.aspx)

# Prise en main

Ce projet utilise des sous-modules git pour intégrer des dépendances en amont, notamment [VIPR][vipr-source-repo]. Si vous avez besoin d’une autre branche pour inclure des correctifs spéciaux, vous devrez la vérifier manuellement dans le sous-module.

Pour que la solution s’ouvre correctement, assurez-vous que les sous-modules sont mis à jour avant de les ouvrir dans Visual Studio. Lors du clonage initial de ce référentiel, utilisez `git récursif` pour mettre à jour les sous-modules en même temps. Exécutez ensuite la `mise à jour du sous-module git` pour mettre à jour manuellement les sous-modules. Si vous n’utilisez pas le commutateur de `--récursif` lorsque vous procédez au clonage, exécutez d'abord `sous-module git init` pour initialiser le sous-module.

Une fois l’installation terminée, vous pouvez utiliser la solution GraphODataTemplateWriter comme d’habitude. Si vous rencontrez des problèmes, assurez-vous que les packages NuGet et les références de projet sont à jour.

Pour plus d’informations sur les sous-modules, consultez [ce chapitre](http://git-scm.com/book/en/v2/Git-Tools-Submodules) à partir du répertoire git et effectuez une recherche sur le web.

## Utilisation de la machine à écrire

Une machine à écrire est une nouvelle solution de génération de fichiers de code à l’aide de GraphODataTemplateWriter et VIPR. Il s’agit d’un exécutable conçu pour simplifier la génération de fichiers de code. Créez la solution pour trouver le fichier exécutable à écrire dans `\MSGraph-SDK-Code-Generator\src\Typewriter\bin\Release`. Les options d’exécution de la machine à écrire sont les suivantes :

* **-l**, **-langue**: Langue cible pour les fichiers de code générés. Les valeurs possibles sont les suivantes : `Android`, `Java`, `ObjC`, `CSharp`, `PHP`, `Python`, `TypeScript`, ou `GraphEndpointList`. La valeur par défaut est `CSharp`. Cette opération n’est pas applicable lorsque la génération de métadonnées propres et annotées est uniquement spécifiée par l’option `-generationmode Metadata`.
* **-m**, **-métadonnées**: Chemin d’accès du fichier local ou URL vers les métadonnées d’entrée cible. Cette valeur par défaut est `https://graph.microsoft.com/v1.0/$metadata`. Cette valeur est obligatoire.
* **-v**, **-verbosité**: Niveau de verbosité du journal. Les valeurs possibles sont les suivantes : `minimal`, `informations`, `débogage`, ou `suivi`. La valeur par défaut est `Minimal`.
* **-o**, **-sortie**: Spécifie le chemin d'accès au dossier de sortie. La valeur par défaut est le répertoire qui contient typewriter.exe. La structure et le contenu du répertoire de sortie sont différents en fonction des options `-generationmode` et `langue`.
* **-d**, **-documents**: Indique le chemin d’accès à la racine locale du référentiel [microsoft-graph-docs](https://github.com/microsoftgraph/microsoft-graph-docs). La valeur par défaut est le répertoire qui contient typewriter.exe. La documentation est analysée pour fournir des annotations de documentation aux métadonnées qui sont ensuite utilisées pour ajouter des commentaires de document dans les fichiers de code générés. Cette option est requise en cas d'utilisation des valeurs `-generationmode` valeurs de `Metadata` ou `Full`.
* **-g**, **-generationmode**: Spécifie le mode de génération. Les valeurs possibles sont les suivantes : `Full`, `Metadata`, or `Files`. Le mode de génération `Full` (par défaut) génère les fichiers de code de sortie en nettoyant les métadonnées d’entrée, en analysant la documentation et en ajoutant des annotations avant de générer les fichiers de sortie. Le mode de génération `Metadata` génère un fichier de métadonnées de sortie en nettoyant les métadonnées, en analysant la documentation et en ajoutant des annotations de documentation. Le mode de génération `Files` génère des fichiers de code à partir d’une métadonnées d’entrée et ignore le nettoyage, l’analyse de la documentation et l’ajout d’annotations de documentation.
* **-f**, **-outputMetadataFileName**: Nom de fichier des métadonnées de sortie de base. Applicable uniquement pour`-generationmode Metadata`. La valeur par défaut est `cleanMetadataWithDescriptions` qui est utilisée avec la valeur de la `-endpointVersion` pour générer un fichier de métadonnées appelé `cleanMetadataWithDescriptionsv1.0.xml`.
* **-e**, **-endpointVersion**: Version de point de terminaison utilisée lors de la dénomination d’un fichier de métadonnées. Les valeurs attendues sont `v 1.0` et `bêta`. Applicable uniquement pour`-generationmode Metadata`.
* **-p**, **-properties**: Spécifier des propriétés pour prendre en charge la logique de génération dans les modèles T4. Les propriétés doivent prendre la forme de *key-string:value-string*. Vous pouvez spécifier plusieurs propriétés en définissant un espace entre les propriétés. La seule propriété actuellement prise en charge est la propriété *php.namespace* pour spécifier l’espace de noms du fichier de modèle généré. Cette propriété est facultative.

### Exemple d’utilisation de la machine à écrire

#### Générer des saisies au format TypeScript à partir d’un fichier CSDL (métadonnées) sans nettoyer ni annoter le langage CSDL.

La sortie est redirigée vers le répertoire `outputTypeScript`.

`.\typewriter.exe -v Info -m D:\cleanMetadataWithDescriptions_v10.xml -o outputTypeScript -l TypeScript -g Files`

#### Nettoyer et annoter un fichier de métadonnées avec des annotations de documentation provenant du référentiel de documentation

Le fichier de métadonnées de sortie est redirigé vers le répertoire `output2`. Le fichier de métadonnées de sortie s’appelle `cleanMetadataWithDescriptionsv1.0.xml` en fonction des valeurs par défaut.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output2 -d D:\repos\microsoft-graph-docs -g Metadata`

#### Générer des fichiers de code C# à partir des métadonnées qui seront nettoyés et annotés avec des annotations de documentation provenant du référentiel de documentation

Les fichiers de code C# de sortie sont redirigés vers le répertoire `outputTypeScript`.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output -l CSharp -d D:\repos\microsoft-graph-docs -g Full`



## Utilisation de Vipr avec ce rédacteur

1. Créer la solution dans Visual Studio.
2. Accédez au dossier `src\GraphODataTemplateWriter\bin\debug` pour rechercher tous les composants compilés.
3. Dans ce dossier, modifiez `.config\TemplateWriterSettings.json` pour spécifier le mappage de votre modèle, consultez [paramètres du rédacteur de modèle](##Template-Writer-Settings) pour plus d’informations.
4. Ouvrez une invite de commandes en tant qu’administrateur dans le même dossier et exécutez `Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"`. Un exemple de fichier de métadonnées est disponible à la racine de ce projet.

Par défaut, le code source de sortie est placé dans un dossier intitulé « Output » à côté du fichier exécutable Vipr.

## Paramètres du rédacteur de modèle
### Langues disponibles

Vous avez le choix entre cinq langues. Java, ObjC, CSharp, TypeScript et Python. Spécifiez la langue que vous voulez générer dans le paramètre `TargetLanguage`.

### Modèles
Vous devez spécifier un répertoire de modèles sous les paramètres `TemplatesDirectory`. Le répertoire peut être un chemin d’accès complet ou relatif au répertoire en cours d’exécution. Le répertoire doit contenir un sous-répertoire pour chaque plateforme pour laquelle vous voulez générer du code. Consultez le répertoire Modèles pour obtenir un exemple.

### Mappage de modèles
Vous devez spécifier le mappage des modèles T4 aux sous-processus spécifiques de chaque plateforme que vous voulez générer. Le paramètre `TemplateMapping` est un dictionnaire des langues et de la liste des modèles. Chaque modèle doit spécifier :

- `Modèle`, le nom du modèle sans les extensions.
- `Sous-processus` le sous-traitant du modèle consultez [sous-traitant](#SubProcessors)
- `Type` le type de modèle.
- `Nom` la chaîne de format pour le nom.

et vous pouvez également :

- `Inclure`, une liste d’objets séparés par des points-virgules à inclure dans le sous-traitant.
- `Exclure`, une liste d’objets séparés par des points-virgules à exclure du sous-traitant.
- `Ignorer`, une liste d’objets séparés par des points-virgules à ignorer du sous-traitant.
- `Correspondances`, une liste d’objets séparés par des points-virgules à inclure dans le sous-traitant.
- `FileCasing`, `UpperCamel`, `LowerCamel` ou `Snake` pour la casse du fichier en cours de création.

**Remarque : Bon nombre de ces paramètres facultatifs ont été utilisés avant la prise en charge totale des annotations par Vipr. Maintenant que des annotations ont été ajoutées à Vipr, l’utilisation de ces paramètres doit être limitée aux scénarios hérités**

Exemple :

` { "Template": "EntityCollectionPage", "SubProcessor": "NavigationCollectionProperty", "Type": "Request", "Name": "<Class><Property>CollectionPage", "Matches" : "includeThisType", "Exclude" : "ExcludedTypeName;OtherExcludedTypeName" }`

Il est important de comprendre que les sous-traitants sont mappés aux méthodes qui interrogent les **OdcmModel** et retournent un groupe d’objets OData. Ce mappage est conservé dans [TemplateProcess. InitializeSubprocessor ()](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/blob/dev/src/GraphODataTemplateWriter/TemplateProcessor/TemplateProcessor.cs#L54). Les mappages spécifiques à la langue existent dans le [répertoire config](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/dev/src/GraphODataTemplateWriter/.config). Chaque objet OData renvoyé par le sous-traitant est appliqué au modèle mappé, ce qui a pour effet de générer un fichier de code pour chaque objet OData.

Dans l’exemple ci-dessus, les objets dans le jeu de résultats du sous-traitant NavigationCollectionProperty sont chacun appliqués au modèle EntityCollectionPage. Chaque résultat est un fichier de code pour chaque objet renvoyé par le sous-traitant NavigationCollectionProperty. 

#### Sous-traitants

Les sous-traitants déterminent le type d’objet OData qui sera transmis dans le modèle qui génère le fichier de code.

- `CollectionMethod` toutes les méthodes de type collections
- `CollectionProperty` propriétés de type collection
- `CollectionReferenceProperty` toutes les propriétés de navigation de type collection utilisées dans les collections non contenants
- `ComplexType` tous les types complexes
- `EntityContainer` l’EntityContainer
- `EntityReferenceType` tous les types d’entités utilisés dans les collections non contenants
- `EntityType` tous les types d’entités
- `EnumType` tous les types énumérables
- `MediaEntityType` tous les types d’entités multimédias
- `Méthode` toutes les actions et fonctions
- `MethodWithBody` toutes les méthodes et fonctions qui envoient un corps au sein de la demande http
- `NavigationCollectionProperty` toutes les propriétés de navigation de type collection
- `NonCollectionMethod` toutes les méthodes et fonctions qui ne renvoient pas de collection
- `Autre` l’intégralité du modèle.
- `Propriété` tous les types de propriétés
- `StreamProperty` tous les types de propriétés qui retournent des flux

#### Types

Le type de modèle.

- `Demande` un modèle qui fera une demande
- `Modèle` un modèle
- `Partagé` un modèle qui ne génère pas de code, mais qui est inclus dans d’autres modèles
- `Client` le modèle utilisé pour créer l’objet client
- `Autres` tout autre type


#### Nom du modèle

Pour définir le nom du modèle à l'aide de la chaîne de format du `Nom`. Vous pouvez insérer `<Class>`, `<Property>``<Method>` et `<Container>` les valeurs seront remplacées par les noms de l'objet correspondant. Si vous insérez un élément qui n’existe pas, celui-ci est remplacé par une chaîne vide.
Remarque : Vous pouvez également choisir le nom du modèle dans le modèle en procédant comme suit : `host.SetTemplateName("foo");`

#### Modification de modèle

La solution contient un projet hors bâtiment qui héberge les modèles de T4 actuels et facilite leur navigation et leur modification. Les nouveaux fichiers de modèles sont automatiquement découverts par ce projet.

#### Inclut/exclut

Il peut arriver que vous souhaitiez exclure ou uniquement traiter certains objets du sous-traitant. Pour ce faire, vous pouvez créer une liste délimitée par des points-virgules pour les objets que vous voulez inclure : `incluent : foo;bar`. Seules les objets dont les noms sont foo ou bar sont traités. L’inverse est le paramètre Exclure dans lequel le sous-traitant inclut tous les objets, à l’exception de ceux dont les noms figurent dans la liste des exclusions, Exclure et inclure ne peuvent pas être utilisés ensemble.

#### Ignorer/correspondances
Lorsque vous ne pouvez pas utiliser le nom d’un objet à inclure ou exclure, vous pouvez utiliser l’élément de description longue sur un objet. Insérez une description longue avec une liste de chaînes séparées par des points-virgules telle que : `foo;bar;baz`. Si vous ajoutez un `"Matches" : "foo;baz"` seuls les objets qui contiennent foo et baz dans leur description longue sont traités. L’inverse est vrai pour ignorer.

Remarque : Vous pouvez également archiver un modèle en `odcjObject. LongDescriptionContains (« foo »);`

**Remarque : Les mentions/exclusions et ignorer/correspondances ont été utilisés avant la prise en charge totale des annotations par Vipr. Maintenant que des annotations ont été ajoutées à Vipr, l’utilisation de ces paramètres doit être limitée aux scénarios hérités**

## Créez des métadonnées Graph

Plusieurs étapes sont nécessaires pour former les métadonnées afin de générer avec succès les kits de développement de programmes dans la forme attendue :

  - Supprimez des annotations de fonction (consultez [\#132](https://github.com/Microsoft/Vipr/issues/132))
  - Ajoutez une annotation de navigation à une miniature
     ```xml
     <Annotation String="navigable" Term="Org.OData.Core.V1.LongDescription"/>
     ```
  - Supprimez les propriétés HasStream de ```onenotePage``` et ```onenoteEntityBaseModel```
  - Ajoutez ```ContainsTarget="true"``` aux propriétés de navigation qui n’ont pas un EntitySet correspondant. Cela s’applique actuellement aux propriétés de navigation qui contiennent plannerBucket, plannerTask, plannerPlan et plannerDelta.
  - Ajouter des descriptions longues aux types et propriétés de [documentation](https://developer.microsoft.com/fr-fr/graph/docs/concepts/overview)

Pour construire à partir de métadonnées autres que celles stockées dans le répertoire des [métadonnées](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/master/metadata), vous devrez effectuer les quatre premières de cette liste.

## Contribution

Avant de pouvoir accepter votre demande de tirage (pull request), vous devez remplir électroniquement le [contrat de licence de contributeur](https://cla.microsoft.com/) de Microsoft. Si vous avez déjà effectué cette opération pour d’autres projets Microsoft, cela signifie que vous êtes déjà concerné.

Ce projet a adopté le [code de conduite Open Source de Microsoft](https://opensource.microsoft.com/codeofconduct/). Pour en savoir plus, reportez-vous à la [FAQ relative au code de conduite](https://opensource.microsoft.com/codeofconduct/faq/) ou contactez [opencode@microsoft.com](mailto:opencode@microsoft.com) pour toute question ou tout commentaire.

[Pourquoi une CLA ?](https://www.gnu.org/licenses/why-assign.html) (à partir de FSF)

## License

Copyright (c) Microsoft Corporation. Tous droits réservés. Sous [licence MIT](LICENSE).
