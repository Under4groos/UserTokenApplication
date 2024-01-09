using System.Text.Json.Serialization;
using UserToken.Modules;

var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();


new ModulUser(app);


app.Run();


