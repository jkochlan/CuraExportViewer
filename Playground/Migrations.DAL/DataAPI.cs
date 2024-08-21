using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.DAL
{
    public static class DataAPI
    {
        public static List<IDbInterceptor> ReturnDbInterceptors()
        {
            return DbInterception.Dispatch.GetType().GetProperties().Select(p => p.GetValue(DbInterception.Dispatch)).OfType<IDbInterceptor>().ToList();
        }

        public static void AddInterceptor(IDbInterceptor interceptor)
        {
            DbInterception.Add(interceptor);
        }
    }
}
