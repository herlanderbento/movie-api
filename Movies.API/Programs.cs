// using Movies.API.Configurations;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddControllers();
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddCors();
// builder.Services.AddAndConfigureControllers();
// builder.Services.AddRouting(options => options.LowercaseUrls = true);
//
// var app = builder.Build();
//
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
//
// app.UseHttpsRedirection();
// app.MapControllers();
// app.UseDocumentation();
//
// app.Run();