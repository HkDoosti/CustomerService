 
var builder = WebApplication.CreateBuilder(args);

 
builder.Services
    .AddCrudTestInfrastructure(builder.Configuration)
    .AddCrudTestApplication();


builder.Services.AddControllers();
//builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();



//if (app.Environment.IsDevelopment())
//{
//    app.UseWebAssemblyDebugging();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for Customerion scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseHttpsRedirection();

//app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


//app.MapRazorPages();
app.MapControllers();
//app.MapFallbackToFile("index.html");

app.Run();
public partial class Program { }