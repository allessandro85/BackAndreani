using Microsoft.Extensions.Configuration;
using Services.Conections;
using System.Data;


namespace Services
{
    public sealed class AppConfig 
    {
		private static volatile AppConfig dis;
		private static readonly object syncRoot = new object();

		private readonly IConfiguration Configuration;
		public AppConfig(IConfiguration config)
		{
			this.Configuration = config;
		}
		public Dictionary<string, string> ExtraConnectionStrings { get; } = new Dictionary<string, string>();
        public IDbConnectionFactory DbConnectionFactory { get; private set; }
		public IDbConnectionFactory AuthorizationdbConnectionFactory { get; private set; }


        private AppConfig()
		{

		}

		public static AppConfig Instance
		{
			get
			{
				if (dis == null)
				{
					lock (syncRoot)
					{
						if (dis == null)
						{
							dis = new AppConfig();
						}
					}
				}
				return dis;
			}
		}

		public void Set(IDbConnectionFactory dbConnectionFactory, string appID, string appName, IDbConnectionFactory authorizationdbConnectionFactory = null)
		{
			DbConnectionFactory = dbConnectionFactory;

			//IDbConnectionFactory dbConnectionFactory2 = new SqlConnectionFactory(Configuration.GetConnectionString("ProyectDB"));
			//AppConfig.Instance.Set(dbConnectionFactory: dbConnectionFactory2, appID: "", appName: "ProuectoDB");

			//AuthorizationdbConnectionFactory = authorizationdbConnectionFactory;
			//AppID = appID;
			//AppName = appName;
			//WebfeelSettings = webfeelSettings ?? throw new ArgumentNullException(nameof(webfeelSettings));
			//Mappers = new Mappers();
			// Configuramos el ExternalIO si está especificado o le asignamos la clase NoExternalIO
			//ExternalIOFactory = null;
			//string assembly = webfeelSettings.ExternalIOAssembly, tipo = webfeelSettings.ExternalIOFactoryType;

			//if (!string.IsNullOrWhiteSpace(assembly) && !string.IsNullOrWhiteSpace(tipo))
			//{
			//var extIO = Activator.CreateInstance(Type.GetType($"{tipo}, {assembly}", throwOnError: true, ignoreCase: true));
			//ExternalIOFactory = extIO as ExternalIOFactory;

			//log4net.LogManager.GetLogger(type: typeof(AppConfig)).Info("Creada instancia de ExternalIO");
			//HayExternalIO = true;
			//}
			//else
			//{
			// Si no hay sistema externo creamos la instancia NoExternalIO
			//ExternalIOFactory = new NoExternalIO();
			//HayExternalIO = false;
			//}
		}

		//public Persona Persona
		//{
		//	get
		//	{
		//		return new Persona();
		//	}
		//}

		public IDbConnection GetConnection()
		{
			return DbConnectionFactory.CreateConnection();
		}

	}
}
