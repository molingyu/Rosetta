#addin nuget:?package=Cake.DocFx&version=0.13.0
#tool nuget:?package=docfx.console&version=2.45.0

var target = Argument("target", "Default");

Task("Docs").Does(() => {
    DocFxBuild("./DocfxConf/docfx.json");
});

Task("Default").Does(() =>
{
    Information("Hello World!");
});

RunTarget(target);