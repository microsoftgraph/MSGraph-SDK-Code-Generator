using System;
namespace Vipr.T4TemplateWriter.TemplateProcessor
{
  public enum SubProcessor
    {
      EntityType,
      ComplexType,
      EnumType,
      Property,
      StreamProperty,
      EntityContainer,
      CollectionProperty,
      Method,
      NonCollectionMethod,
      CollectionMethod,
      MethodWithBody,
      Other
    }
}
