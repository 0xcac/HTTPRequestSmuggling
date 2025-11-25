namespace HTTPRequestSmugglingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapPost("/vuln", async context =>
            {
                Console.WriteLine("====== VULN REQUEST ======");

                // Print request line
                Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Request.Protocol}");

                // Print headers
                Console.WriteLine("---- Headers ----");
                foreach (var header in context.Request.Headers)
                {
                    Console.WriteLine($"{header.Key}: {header.Value}");
                }

                // IMPORTANT: rewind stream if needed
                context.Request.EnableBuffering();

                // Read raw body
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();

                // Reset stream position so app can continue reading if needed
                context.Request.Body.Position = 0;

                Console.WriteLine("---- Body ----");
                Console.WriteLine(body.Length == 0 ? "(empty)" : body);
                Console.WriteLine("====== END REQUEST ======");

                await context.Response.WriteAsync("vuln");
            });
            app.MapGet("/admin", async context =>
            {
                Console.WriteLine("====== ADMIN REQUEST ======");

                // Print request line
                Console.WriteLine($"{context.Request.Method} {context.Request.Path} {context.Request.Protocol}");

                // Print headers
                Console.WriteLine("---- Headers ----");
                foreach (var header in context.Request.Headers)
                {
                    Console.WriteLine($"{header.Key}: {header.Value}");
                }

                // IMPORTANT: rewind stream if needed
                context.Request.EnableBuffering();

                // Read raw body
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();

                // Reset stream position so app can continue reading if needed
                context.Request.Body.Position = 0;

                Console.WriteLine("---- Body ----");
                Console.WriteLine(body.Length == 0 ? "(empty)" : body);
                Console.WriteLine("====== END REQUEST ======");

                await context.Response.WriteAsync("admin");

            });
            app.Run();
        }
    }
}
