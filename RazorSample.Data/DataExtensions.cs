using System;
using System.Linq.Expressions;

namespace RazorSample.Data
{
  public static class DataExtensions
  {
    public static ChangeEntry<TEntity> Property<TEntity, TProperty>(
      this ChangeEntry<TEntity> source, Expression<Func<TEntity, TProperty>> property, TProperty value)
      where TEntity : class, new()
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (property == null)
      {
        throw new ArgumentNullException(nameof(property));
      }

      source.Property(source.Entity.Property(property, value));

      return source;
    }

    public static ChangeEntry<TEntity> Key<TEntity, TProperty>(
      this ChangeEntry<TEntity> source, Expression<Func<TEntity, TProperty>> property, TProperty value)
      where TEntity : class, new()
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (property == null)
      {
        throw new ArgumentNullException(nameof(property));
      }

      source.Entity.Property(property, value);

      return source;
    }

    private static string Property<TEntity, TProperty>(this TEntity entity, Expression<Func<TEntity, TProperty>> property, TProperty value)
    {
      var propertyName = ((MemberExpression)property.Body).Member.Name;

      typeof(TEntity).GetProperty(propertyName).SetValue(entity, value);

      return propertyName;
    }
  }
}
