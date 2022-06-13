using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Polling.Application.Behaviour;
using Polling.Application.Contracts;
using Polling.Infrastructure.Database.MongoDb;
using Polling.Infrastructure.Repositories;
using AutoMapper;

namespace Polling.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {


            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));


            services.AddSingleton<PollingDbConnectionSettings>(serviceProvider =>
            {
                var settings = Configuration.GetSection(nameof(PollingDbConnectionSettings))
                                            .Get<PollingDbConnectionSettings>();
                return settings;
            });

                
            services.AddSingleton<IPollingRepository, PollingRepository>();
            services.AddSingleton<IVoteWeightCalculator, VoteWeightCalculator>();

            services.AddAutoMapper(typeof(Startup));
            
            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Polling.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Polling.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
