using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Conections;
using System;
using System.Collections.Generic;

namespace Services.Services
{
	public abstract class ServiceWithDbContext : ControllerBase, IDisposable
    {
        protected internal readonly IDbConnectionFactory dbConnectionFactory;
        protected internal DBContext dbContext;

		public ServiceWithDbContext() : base()
		{
            dbConnectionFactory = AppConfig.Instance.DbConnectionFactory;
            dbContext = new DBContext(dbConnectionFactory: dbConnectionFactory);
        }
        public static bool HasAccess(IEnumerable<int> roleIDs, string accessTypes)
        {
            var accesslist = accessTypes.Split(new string[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
            bool accessGranted = false;
            foreach (var accesspair in accesslist)
            {
                var access = accesspair.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (!accessGranted) return false;
            }
            return accessGranted;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

    }
}
