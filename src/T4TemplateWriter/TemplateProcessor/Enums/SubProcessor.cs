namespace Vipr.T4TemplateWriter.TemplateProcessor
{
    using System;
    
    public enum SubProcessor
    {
      EntityType,
      ComplexType,
      EnumType,
      MediaEntityType,
      Property,
      StreamProperty,
      MediaEntity,
      EntityContainer,
      CollectionProperty,
      NavigationCollectionProperty,
      CollectionReferenceProperty,
      EntityReferenceType,
      Method,
      NonCollectionMethod,
      CollectionMethod,
      MethodWithBody,
      Other
    }
}
