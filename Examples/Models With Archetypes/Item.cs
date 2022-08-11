using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Meep.Tech.XBam.EFCore.Tests.Examples.ModelsWithArchetypes {

  /// <summary>
  /// The Base Model for all Items
  /// </summary>
  public class Item : Model<Item, Item.Type>, IUnique, IModel.IUseDefaultUniverse {
    string IUnique.Id { get; set; }

    [AutoBuild, Required, NotNull]
    public string Name {
      get;
      private set;
    }

    [AutoBuild]
    public string BaseColor {
      get;
      private set;
    }

    /// <summary>
    /// The Base Archetype for Items
    /// </summary>
    public abstract class Type : Archetype<Item, Item.Type>.WithDefaultParamBasedModelBuilders {

      /// <summary>
      /// Used to make new Child Archetypes for Item.Type 
      /// </summary>
      /// <param name="id">The unique identity of the Child Archetype</param>
      protected Type(Identity id)
        : base(id) { }
    }
  }

  public class Book : Item.Type {
    Book() 
      : base(null) {}
  }

  public class CombShell : Shell.Type {
    public override Shell.ShapeType DefaultShape
      => Shell.ShapeType.Comb;
    CombShell()
      : base(null) { }

  }

  public class Shell : Item {

    public enum ShapeType {
      Round,
      Triangle,
      Comb
    }

    [AutoBuild]
    public ShapeType Shape {
      get;
      private set;
    }

    public abstract new class Type : Item.Type {
      public abstract ShapeType DefaultShape {
        get;
      }

      protected Type(Identity id)
        : base(id ?? null) { }
    }
  }
}
