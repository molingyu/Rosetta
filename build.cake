var target = Argument("target", "Default");

#addin "Cake.DocFx"
#tool "docfx.console"
Task("Docs").Does(() => {
    DocFxBuild("./DocfxConf/docfx.json");
});

Task("Default").Does(() =>
{
    Information("Hello World!");
});

RunTarget(target);