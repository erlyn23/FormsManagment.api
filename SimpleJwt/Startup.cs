using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleJwt.DbContexts;
using SimpleJwt.Repositories;
using SimpleJwt.Repositories.Contracts;
using SimpleJwt.Services;
using SimpleJwt.Services.Contracts;
using SimpleJwt.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJwt
{
    public class Startup
    {
        private readonly string _myCors = "MyCors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors(builder =>
            {
                builder.AddPolicy(_myCors, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimpleJwt", Version = "v1" });
            });

            var key = Encoding.ASCII.GetBytes(Configuration["MySecretKey"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddDbContext<FormsManagmentDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));


            //Dependency Injections.

            //Repositories.
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFormRepository, FormRepository>();
            services.AddTransient<IQuestionTypeRepository, QuestionTypeRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IUserFormAnswerRepository, UserFormAnswerRepository>();

            //Services.
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IFormService, FormService>();
            services.AddTransient<IQuestionTypeService, QuestionTypeService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IQuestionOptionService, QuestionOptionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<IUserFormAnswerService, UserFormAnswerService>();

            //Tools.
            services.AddTransient<FileManagment>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimpleJwt v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_myCors);
           
            app.UseAuthorization();
            
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
