using Amazon.SecretsManager;
using Amazon.SQS;
using AutoMapper;
using config.rabbitMQ;
using config.rabbitMQ.impl;
using EDM.Infohub.BPO.DataAccess;
using EDM.Infohub.BPO.Events;
using EDM.Infohub.BPO.GenericStructureSQS.SQS.Interfaces;
using EDM.Infohub.BPO.DataAccess.Impl;
using EDM.Infohub.BPO.Mappers;
using EDM.Infohub.BPO.Models.SQS;
using EDM.Infohub.BPO.RabbitMQ;
using EDM.Infohub.BPO.Services;
using EDM.Infohub.BPO.Services.impl;
using EDM.Infohub.BPO.SQS;
using EDM.Infohub.BPO.SQS.Interfaces;
using EDM.Infohub.BPO.Swagger;
using EDM.Infohub.Integration.Security;
using EDM.Infohub.Integration.Services;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Collections.Generic;
using System.Reflection;

namespace EDM.Infohub.BPO
{
    public class Startup
    {
        private ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly string BPOCors = "_BPOCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "BPO " + Configuration["ASPNETCORE_ENVIRONMENT"], Version = "v1" });
            //});
            services.SetupSwaggerDoc(Configuration["ASPNETCORE_ENVIRONMENT"]);

            NpgsqlConnection.GlobalTypeMapper.UseJsonNet();
            //AWS
            _logger.Info("Log StartUp");
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddAWSService<IAmazonSecretsManager>();
            services.AddAWSService<IAmazonSQS>();

            //SQS
            var sqsPapelQueue = new SQSQueueConfig();
            var sqsEventoQueue = new SQSQueueConfig();
            Configuration.GetSection("SQSQueuesConfig").GetSection("papel").Bind(sqsPapelQueue);
            Configuration.GetSection("SQSQueuesConfig").GetSection("evento").Bind(sqsEventoQueue);
            var sqsConfig = new SQSConfig();
            sqsConfig.QueuesConfig = new Dictionary<string, ISQSQueueConfiguration>();
            sqsConfig.QueuesConfig.Add("papel", sqsPapelQueue);
            sqsConfig.QueuesConfig.Add("evento", sqsEventoQueue);
            services.AddSingleton<ISQSConfiguration>(sqsConfig);

            //var rastreamentoMessenger = new RastreamentoMessenger();
            //services.AddSingleton(rastreamentoMessenger);
            services.AddTransient<RastreamentoMessenger>();

            //RabbitMQ
            services.AddLogging();
            services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
            //services.AddSingleton<RabbitMQConnection>();
            services.AddSingleton<IReceiver, Receiver>();
            services.AddSingleton<ISender, Sender>();

            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: BPOCors,
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            //API
            services.AddControllers().AddControllersAsServices();
            //Versioning
            services.AddApiVersioning(param =>
            {
                param.ReportApiVersions = true;
                param.AssumeDefaultVersionWhenUnspecified = true;
                param.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddVersionedApiExplorer(param =>
            {
                param.GroupNameFormat = "'v'VVV";
                param.SubstituteApiVersionInUrl = true;
            });

            //Luz Http Client
            services.AddSingleton<ILuzClient, LuzClient>();
            services.AddSingleton<ILuzService, LuzService>();

            //Start Rabbit Service
            services.AddSingleton<RabbitReceiver>();

            //Start SQS Service
            services.AddTransient<ISendSQSQueue, SendSQSPapel>();
            services.AddTransient<ISendSQSQueue, SendSQSEvento>();

            //Filtro 
            services.AddSingleton<MessageProcessor>();
            services.AddSingleton<Filter>();

            //Banco de dados
            //services.AddSingleton<IDataBaseService, DatabaseService>();
            services.AddTransient<ControleMensagemRepository>();
            services.AddTransient<AssinaturaRepository>();
            services.AddTransient<AssinaturaLogRepository>();
            services.AddTransient<RastreamentoRepository>();
            services.AddTransient<AssinaturaFlagRepository>();
            services.AddTransient<RawDataRepository>();
            services.AddTransient<PrecosRepository>();
            services.AddTransient<FluxosRepository>();
            services.AddTransient<DadosCaracteristicosRepository>();
            services.AddTransient<PuDeEventosRepository>();
            services.AddTransient<RastreamentoEventoRepository>();
            services.AddTransient<RastreamentoPapelRepository>();

            services.AddTransient<IFireForgetRepositoryHandler, FireForgetRepositoryHandler>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton<HangfireService>();

            services.AddScoped<ISecureGateway, SecureGatewayClient>();
            services.AddScoped<ICalendar, EDM.Infohub.BPO.Services.impl.CalendarService>();


            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddControllers().AddControllersAsServices();


            //var serviceProvider = services.BuildServiceProvider();

            //Cache
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, ILoggerFactory loggerFactory, RabbitReceiver receiver, HangfireService hangfire)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddLog4Net();
            //_logger = loggerFactory.CreateLogger<Startup>();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(BPOCors);

            app.UseAuthorization();

            app.ConfigureSwaggerUI(provider, Configuration["ASPNETCORE_ENVIRONMENT"]);
            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //s.GetSecret();

            //app.ApplicationServices.GetServices<ISendSQSQueue>();

            receiver.StartRabbitService();
            _ = hangfire.AgendarRecorrencia("RelatorioCadastroComecoDia", Configuration["InfohubAPIUrl"] + "v2/Caracteristica/relatorio-dia", "0 35 07 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioCadastroManha", Configuration["InfohubAPIUrl"] + "v2/Caracteristica/relatorio-dia", "0 10 10 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioCadastroAlmoco", Configuration["InfohubAPIUrl"] + "v2/Caracteristica/relatorio-dia", "0 15 14 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioCadastroTarde", Configuration["InfohubAPIUrl"] + "v2/Caracteristica/relatorio-dia", "0 15 17 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioCadastroFinalDia", Configuration["InfohubAPIUrl"] + "v2/Caracteristica/relatorio-dia", "0 15 19 ? * MON-FRI");

            _ = hangfire.AgendarRecorrencia("RelatorioPrecoMadrugada", Configuration["InfohubAPIUrl"] + "v2/Preco/relatorio-dia", "0 0 01 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioPrecoComecoDia", Configuration["InfohubAPIUrl"] + "v2/Preco/relatorio-dia", "0 25 07 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioPrecoManha", Configuration["InfohubAPIUrl"] + "v2/Preco/relatorio-dia", "0 0 10 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioPrecoAlmoco", Configuration["InfohubAPIUrl"] + "v2/Preco/relatorio-dia", "0 05 14 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioPrecoTarde", Configuration["InfohubAPIUrl"] + "v2/Preco/relatorio-dia", "0 05 17 ? * MON-FRI");
            _ = hangfire.AgendarRecorrencia("RelatorioPrecoFinalDia", Configuration["InfohubAPIUrl"] + "v2/Preco/relatorio-dia", "0 05 19 ? * MON-FRI");

            _ = hangfire.AgendarRecorrencia("RelatorioPuDeEventos", Configuration["InfohubAPIUrl"] + "v1/PuDeEventos/relatorio-dia", "0 0 7-19 ? * MON-FRI");
        }
    }
}
