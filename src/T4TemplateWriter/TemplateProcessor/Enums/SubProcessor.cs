using System;
namespace Vipr.T4TemplateWriter.TemplateProcessor
{
  public enum SubProcessor
    {
      EntityType,
      ComplexType,
      EnumType,
      MediaEntityType,
      Property,
      StreamProperty,
      EntityContainer,
      CollectionProperty,
      NavigationCollectionProperty,
      CollectionReferenceProperty,
      Method,
      NonCollectionMethod,
      CollectionMethod,
      MethodWithBody,
      Other
    }
}
