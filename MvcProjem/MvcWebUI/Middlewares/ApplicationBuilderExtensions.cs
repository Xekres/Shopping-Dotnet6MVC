using Microsoft.Extensions.FileProviders;

namespace MvcWebUI.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app,string root)
        {
            var path = Path.Combine(root, "node_modules");
            //static dosyaları taşıma nesnesi olusturalım
            var provider = new PhysicalFileProvider(path);
            //Sana bir istek gelirse ;
            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            //bunun dosya sunumu fileprovider tarafından yapılsın.
            options.FileProvider = provider;

            app.UseStaticFiles(options);

            return app;
        }
    }
}
