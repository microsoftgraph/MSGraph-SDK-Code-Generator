[vipr-source-repo]: https://github.com/microsoft/vipr

[![Estado de la compilación](https://o365exchange.visualstudio.com/O365%20Sandbox/_apis/build/status/Microsoft%20Graph/msgraph-package-typewriter)](https://o365exchange.visualstudio.com/O365%20Sandbox/_build/latest?definitionId=1728)

# Microsoft Graph SDK Code Generator

Escritores de código fuente para [VIPR][vipr-source-repo] que usan plantillas T4. El GraphODataTemplateWriter recibe un OdcmModel de VIPR y lo utiliza para rellenar una plantilla T4 que se encuentra en este repositorio.

Actualmente, el escritor admite los siguientes lenguajes de destino:
- Android
- CSharp
- Java
- Objective-C
- Python
- TypeScript
- PHP

# Contenidos
- [Requisitos previos](#prerequisites)
- [Introducción](#getting-started)
- [Uso de VIPr con este escritor](#using-vipr-with-this-writer)
- [Colaboradores](#contributing)
- [Licencia](#license)

## Requisitos previos
- [Visual Studio SDK](https://msdn.microsoft.com/es-es/library/bb166441.aspx)
- [SDK de modelado de Visual Studio](https://msdn.microsoft.com/es-es/library/bb126259.aspx)

# Introducción

Este proyecto usa submódulos git para integrar las dependencias ascendentes, específicamente [Vipr][vipr-source-repo]. Si necesita una rama alternativa para incluir correcciones especiales, debe buscarlo manualmente en el submódulo.

Para que la solución se abra correctamente, asegúrese de que los submódulos están actualizados antes de abrirla en Visual Studio. Cuando clone este repositorio de forma inicial, use `git clone --recursive` para actualizar los submódulos al mismo tiempo. Después, ejecute `actualización de submódulo git` para actualizar manualmente los submódulos. Si no usa el modificador `--recursive` al clonar, ejecute `git submodule init` primero para inicializar el submódulo.

Una vez completada la configuración, puede trabajar con la solución GraphODataTemplateWriter como siempre. Si se produce algún problema, asegúrese de que los paquetes de NuGet y las referencias de proyecto estén actualizados.

Para obtener más información sobre los submódulos, lea [este capítulo](http://git-scm.com/book/en/v2/Git-Tools-Submodules) de la libreta de Git y consulte la web.

## Uso de Typewriter

Typewriter es una nueva solución para generar archivos de código con GraphODataTemplateWriter y VIPR. Se trata de un ejecutable diseñado para simplificar la creación de archivos de código. Cree la solución para encontrar el ejecutable Typewriter en `\MSGraph-SDK-Code-Generator\src\Typewriter\bin\Release`. Las opciones de ejecución de Typewriter son:

* **-l**, **-language**: Lenguaje de destino para los archivos de código generados. Los valores pueden ser: `Android`, `Java`, `ObjC`, `CSharp`, `PHP`, `Python`, `TypeScript` o `GraphEndpointList`. El valor predeterminado es `CSharp`. Esto no es aplicable cuando solo se generan metadatos limpios y anotados, tal y como se especifica en la opción `-generationmode Metadata`.
* **-m**, **-metadata**: La dirección URL o ruta de acceso del archivo local a los metadatos de entrada objetivo. El valor predeterminado es `https://graph.microsoft.com/v1.0/$metadata`. Este valor es necesario.
* **-v**, **-verbosity**: El nivel de detalle del registro. Los valores pueden ser: `Minimal`, `Info`, `Debug` o `Trace`. El valor predeterminado es `Minimal`.
* **-o**, **-output**: Especifica la ruta de acceso a la carpeta de salida. El valor predeterminado es el directorio que contiene typewriter.exe. La estructura y el contenido del directorio de resultado serán diferentes en función de las opciones `-generationmode` y `-language`.
* **-d**, **-docs**: Especifica la ruta de acceso a la raíz local del repositorio [microsoft-graph-docs](https://github.com/microsoftgraph/microsoft-graph-docs). El valor predeterminado es el directorio que contiene typewriter.exe. La documentación se analiza para proporcionar anotaciones de documentación a los metadatos, que se usan para agregar comentarios a documentos en los archivos de código generados. Esta opción es necesaria cuando se usan valores `-generationmode` de `Metadata` o `Full`.
* **-g**, **-generationmode**: Especifica el modo de generación. Los valores pueden ser: `Full`, `Metadata` o `Files`. El modo de generación `Full` (predeterminado) genera los archivos de código de salida mediante la limpieza de los metadatos de entrada, el análisis de la documentación y la adición de anotaciones antes de generar los archivos de salida. El modo de generación `Metadata` produce un archivo de metadatos de salida al limpiar metadatos, analizar la documentación y agregar anotaciones de documentación. El modo de generación `Files` genera archivos de código desde un metadato de entrada y omite la limpieza, el análisis de documentación y la adición de anotaciones de documentación.
* **-f**, **-outputMetadataFileName**: El nombre de archivo base de los metadatos de salida. Solo se aplica para `-generationmode Metadata`. El valor predeterminado es `cleanMetadataWithDescriptions` que se usa con el valor de `-endpointVersion` para generar un archivo de metadatos denominado `cleanMetadataWithDescriptionsv1.0.xml`.
* **-e**, **-endpointVersion**: La versión del extremo que se usa para asignar un nombre a un archivo de metadatos. Los valores esperados son `v 1.0` y `beta`. Solo se aplica para `-generationmode Metadata`.
* **-p**, **-properties**: Especifique las propiedades para admitir la lógica de generación en las plantillas T4. Las propiedades deben tener el formato de *key-string:value-string*. Se pueden especificar varias propiedades colocando un espacio entre propiedades. La única propiedad compatible actualmente es la propiedad *php.namespace* para especificar el espacio de nombres del archivo de modelo generado. Esta propiedad es opcional.

### Ejemplo de uso de Typewriter

#### Generar términos de TypeScript desde un archivo CSDL (metadatos) sin limpiar o anotar el CSDL.

El resultado se mostrará en el directorio `outputTypeScript`.

`.\typewriter.exe -v Info -m D:\cleanMetadataWithDescriptions_v10.xml -o outputTypeScript -l TypeScript -g Files`

#### Limpiar y realizar anotaciones en un archivo de metadatos con anotaciones de la documentación originales del repositorio de documentación

El archivo de metadatos resultante pasará al directorio `output2`. El archivo de metadatos resultante se denominará `cleanMetadataWithDescriptionsv1.0.xml` en función de los valores predeterminados.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output2 -d D:\repos\microsoft-graph-docs -g Metadata`

#### Generar archivos de código C# a partir de los metadatos que se limpiarán y anotarán con anotaciones de documentación procedentes del repositorio de documentación.

Los archivos C# resultantes se enviarán al directorio `output`.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output -l CSharp -d D:\repos\microsoft-graph-docs -g Full`



## Uso de VIPr con este editor

1. Cree la solución en Visual Studio.
2. Vaya a la carpeta `src\GraphODataTemplateWriter\bin\debug` para buscar todos los componentes compilados.
3. En esa carpeta, modifique `.config\TemplateWriterSettings.json` para especificar la asignación de plantilla, vea [configuración del escritor de plantillas](##Template-Writer-Settings) para obtener más información.
4. Abra un símbolo del sistema como administrador en la misma carpeta y ejecute `Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"`. Puede encontrar un archivo de metadatos de ejemplo en la raíz de este proyecto.

De forma predeterminada, el código fuente de salida se colocará en una carpeta denominada "output" junto al ejecutable Vipr.

## Configuración del escritor de plantillas
### Lenguajes disponibles

Por el momento, hay cinco lenguajes entre los que elegir. Java, ObjC, CSharp, TypeScript y Python. Especifique el lenguaje que desea generar en la opción de configuración `TargetLanguage`.

### Plantillas
Debe especificar un directorio de plantillas en la configuración de `TemplatesDirectory`. El directorio puede ser una ruta de acceso completa o relativa al directorio en ejecución. El directorio tiene que contener un subdirectorio para cada plataforma para la que quiera generar código. Puede ver un ejemplo en el directorio de plantillas.

### Asignación de plantillas
Debe especificar la asignación de las plantillas T4 a subprocesadores específicos para cada plataforma que quiera generar. La configuración `TemplateMapping` es un diccionario de lenguajes y una lista de plantillas. Cada plantilla debe especificar:

- `Template`, el nombre de la plantilla sin las extensiones.
- `SubProcessor` el subprocesador de la plantilla, vea [subprocesadores](#SubProcessors)
- `Type` el tipo de plantilla.
- `Name` la cadena de formato del nombre.

y, opcionalmente:

- `Include`, una lista delimitada por puntos y coma de objetos para incluirlos en el subproceso.
- `Exclude`, una lista delimitada por puntos y coma de objetos para excluirlos en el subproceso.
- `Ignore`, una lista delimitada por puntos y coma de objetos para ignorar en el subproceso.
- `Matches`, una lista delimitada por puntos y coma de objetos para incluirlos en el subproceso.
- `FileCasing`, `UpperCamel`, `LowerCamel` o `Snake` para el uso de mayúsculas y minúsculas para el archivo específico que se está creando.

**Nota: Muchos de estos parámetros opcionales se usaban antes de que Vipr tuviera soporte total para las anotaciones. Ahora que se han agregado anotaciones a Vipr, el uso de estos parámetros debería limitarse a escenarios antiguos.**

Ejemplo :

` { "Template": "EntityCollectionPage", "SubProcessor": "NavigationCollectionProperty", "Type": "Request", "Name": "<Class><Property>CollectionPage", "Matches" : "includeThisType", "Exclude" : "ExcludedTypeName;OtherExcludedTypeName" }`

Es importante tener en cuenta que los subprocesos se asignan a métodos que consultan el **OdcmModel** y devuelven un conjunto de objetos OData. Esta asignación se mantiene en [TemplateProcess.InitializeSubprocessor()](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/blob/dev/src/GraphODataTemplateWriter/TemplateProcessor/TemplateProcessor.cs#L54). Las asignaciones específicas de lenguaje se encuentran en el [directorio de configuración](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/dev/src/GraphODataTemplateWriter/.config). Cada objeto OData devuelto por el subproceso se aplica a la plantilla asignada, lo que resulta en archivo de código por cada objeto OData.

En el ejemplo anterior, los objetos en el conjunto de resultados del subproceso NavigationCollectionProperty se aplicarán en la plantilla EntityCollectionPage. Cada resultado será un archivo de código para cada objeto devuelto por el subproceso NavigationCollectionProperty. 

#### Subprocesos

El subproceso determina qué tipo de objeto OData se pasará a la plantilla que genera el archivo de código.

- `CollectionMethod` todos los métodos de tipo colecciones
- `CollectionProperty` propiedades de tipo colección
- `CollectionReferenceProperty` todas las propiedades de navegación que son de tipo colección que se usan en colecciones de no contención
- `ComplexType` todos los tipos complejos
- `EntityContainer` el EntityContainer
- `EntityReferenceType` todos los tipos de entidad que se usan en colecciones de no contención
- `EntityType` todos los tipos de entidad
- `EnumType` todos los tipos enumerables
- `MediaEntityType` todos los tipos de entidades multimedia
- `Method` todas las funciones y acciones
- `MethodWithBody` todos los métodos y funciones que envían un cuerpo durante la solicitud http
- `NavigationCollectionProperty` todas las propiedades de navegación que son de tipo colección
- `NonCollectionMethod` todos los métodos y funciones que no devuelven una colección
- `Other` todo el modelo.
- `Property` todos los tipos de propiedades
- `StreamProperty` todos los tipos de propiedades que devuelven cadenas

#### Tipos

El tipo de plantilla.

- `Request` una plantilla que realizará una solicitud
- `Model` un modelo
- `Shared` una plantilla que no dará como resultado ningún código pero se incluye en otras plantillas
- `Client` la plantilla usada para crear el objeto de cliente
- `Other` otro tipo


#### Nombre de plantilla

Para establecer el nombre de la plantilla con la cadena de formato `Name`. Puede insertar `<Class>`, `<Property>`, `<Method>` y `<Container>`, los valores se reemplazarán por los nombres del objeto correspondiente. Si inserta un elemento que no existe, se reemplazará por una cadena vacía.
Nota: También puede establecer el nombre de la plantilla en el interior de la plantilla: `host.SetTemplateName("foo");`

#### Edición de plantillas

La solución contiene un proyecto en el que no se generan plantillas, para hospedar las plantillas T4 reales y facilitar su búsqueda o edición. Este proyecto detectará automáticamente los nuevos archivos de plantilla.

#### Incluir/excluir

Puede haber ocasiones específicas en las que quiera excluir o solo procesar ciertos objetos del subproceso. Para ello, puede establecer una lista delimitada por puntos y comas de los objetos que desea incluir: `Include : foo;bar`. Esto solo procesará objetos cuyos nombres sean foo o bar. Lo opuesto de esto es la opción excluir, en la que el subproceso incluirá todos los objetos, excepto aquellos cuyos nombres se encuentren en la lista excluir, no se pueden usar excluir e incluir conjuntamente.

#### Ignorar/coincidir
Cuando no pueda usar el nombre de un objeto para incluir o excluir, puede usar el elemento de descripción larga en cualquier objeto. Inserte una descripción larga con una lista delimitada por puntos y coma de cadenas como: `foo;bar;baz`. Si agrega `"Matches" : "foo;baz"` solo se procesarán los objetos que contienen foo y baz en su descripción larga. Lo opuesto se cumple para ignorar.

Nota: También puede proteger una plantilla si `odcjObject.LongDescriptionContains("foo");`

**Nota: Los parámetros Incluir/excluir o Ignorar/coincidir se usaban antes de que Vipr tuviera soporte total para las anotaciones. Ahora que se han agregado anotaciones a Vipr, el uso de estos parámetros debería limitarse a escenarios antiguos.**

## Creación con metadatos de Graph

En la actualidad, se llevan a cabo varios pasos para formar los metadatos en uno que genere correctamente SDK en la forma que esperamos:

  - Quitar anotaciones de funciones (consulte [\#132](https://github.com/Microsoft/Vipr/issues/132))
  - Agregar una anotación de navegación a la miniatura
     ```xml
     <Annotation String="navigable" Term="Org.OData.Core.V1.LongDescription"/>
     ```
  - Quitar propiedades HasStream de ```onenotePage``` y ```onenoteEntityBaseModel```
  - Agregar ```ContainsTarget="true"``` a propiedades de navegación que no tienen un EntitySet correspondiente. Actualmente, esto se aplica a las propiedades de navegación que contienen plannerBucket, plannerTask, plannerPlan y plannerDelta.
  - Agregar descripciones largas a los tipos y propiedades de [documentos](https://developer.microsoft.com/es-es/graph/docs/concepts/overview)

Para compilar en metadatos que no sean los que se almacenan en el directorio [Metadata](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/master/metadata), tendrá que realizar los cuatro primeros apartados en esta lista.

## Colaboradores

Antes de que podamos aceptar su solicitud de incorporación de cambios, debe completar el [Contrato de licencia de colaborador](https://cla.microsoft.com/) de Microsoft. Si lo ha hecho en otros proyectos de Microsoft, ya no es necesario.

Este proyecto ha adoptado el [Código de conducta de código abierto de Microsoft](https://opensource.microsoft.com/codeofconduct/). Para obtener más información, vea [Preguntas frecuentes sobre el código de conducta](https://opensource.microsoft.com/codeofconduct/faq/) o póngase en contacto con [opencode@microsoft.com](mailto:opencode@microsoft.com) si tiene otras preguntas o comentarios.

[Por qué un CLA?](https://www.gnu.org/licenses/why-assign.html) (desde la FSF)

## Licencia

Copyright (c) Microsoft Corporation. Reservados todos los derechos. Publicado bajo la [licencia MIT](LICENSE).
