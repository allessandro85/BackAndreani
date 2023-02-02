using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Conections;
using Services.Services;

namespace Services.Controllers
{
	[Authorize]
	public abstract class PYController : ControllerBase, IDisposable
	{
		protected internal readonly IDbConnectionFactory dbConnectionFactory;
		protected internal DBContext dbContext;
		protected string LoggedUserAlias => User?.Identity.Name;
		protected ClaimsIdentity Identity => User?.Identity as ClaimsIdentity;

		protected string lang { get; set; }

		public PYController() : base()
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