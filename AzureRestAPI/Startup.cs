using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureRestAPI.AzureConnection;
using AzureRestAPI.AzureDTO;
using AzureRestAPI.AzureService;
using AzureRestAPI.Common;
using AzureRestAPI.Models;
using AzureRestAPI.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AzureRestAPI
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;
        private readonly string Endpoint = "https://ss-activateazure.documents.azure.com:443/";
        private readonly string Key = "nXctaHKDZwgabHbLTO5rVZQHQv4LZuy3mv6gNY8AggHTb8jyYTTiuKkDeM5yQD7ZqHZoqRHVcl2mOTloBODIsw==";

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
                .AddSingleton<IDocumentDBRepository<Book>, DocumentDBRepository<Book>>(serviceProvider =>
                    {
                        var documentClient = new DocumentClient(new Uri(Endpoint), Key);
                        return new DocumentDBRepository<Book>(documentClient);
                    }
                )
                .AddSingleton<IDocumentClient, DocumentClient>()
                .AddSingleton<SharedResources>()                       
                .Configure<AppSettings>(_config)
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
