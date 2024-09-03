using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.DAL
{
    public class AllINeedToDo
    {
        public Type GetEntityTypeFromTableName(DbContext context, string tableName)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            var metadataWorkspace = objectContext.MetadataWorkspace;

            var entitySets = metadataWorkspace.GetItems<EntityContainer>(DataSpace.SSpace)
                                              .First().BaseEntitySets.OfType<EntitySet>();

            foreach (var entitySet in entitySets)
            {
                if (entitySet.Table == tableName)
                {
                    var entityType = metadataWorkspace.GetItems<EntityType>(DataSpace.CSpace)
                                                      .First(e => e.Name == entitySet.ElementType.Name);

                    string clrTypeName = entityType.FullName + ", " + typeof(DataContext).Assembly.FullName;
                    var clrType = Type.GetType(clrTypeName);

                    if (clrType != null)
                    {
                        return clrType;
                    }

                    var matchingType = typeof(DataContext).Assembly.GetTypes()
                        .FirstOrDefault(t => t.Name == entityType.Name);

                    if (matchingType != null)
                    {
                        return matchingType;
                    }

                    throw new ArgumentException($"Cannot find CLR type for entity '{entityType.Name}'.");
                }
            }

            throw new ArgumentException($"No entity type found for table '{tableName}'.");
        }
    }
}
