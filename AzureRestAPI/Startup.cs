using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureConnection;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using AzureRestAPI.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureRestAPI
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env, IConfiguration config,
        ILoggerFactory loggerFactory)
        {
            _env = env;
            _config = config;
            _loggerFactory = loggerFactory;
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpClient()
                .AddSingleton<IAzureAuthentication, AzureAuthentication>()
                .AddSingleton<IStorageAccountService, StorageAccountService>()
                .AddSingleton<IAccountQueuesService, AccountQueuesService>()
                .AddSingleton<IAzureClient, AzureClient>()
                .AddSingleton<ITableService, TableService>()
                .AddSingleton<IDocumentDBRepository<Item>, DocumentDBRepository<Item>>()
                .AddSingleton<SharedResources>()                       
                .Configure<AppSettings>(_config)
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
