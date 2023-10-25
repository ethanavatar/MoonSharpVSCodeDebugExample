using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;

var debugger = new MoonSharpVsCodeDebugServer();
debugger.Start();

var script = new Script();
script.Options.DebugPrint = s => Console.WriteLine(s);
script.Globals["sleep"] = (Action<int>) Thread.Sleep;

debugger.AttachToScript(script, "test");

Console.WriteLine("Debug server started on port 41912. Press any key to start the script...");
Console.ReadKey();

var cwd = AppDomain.CurrentDomain.BaseDirectory;
var friendlyPath = Path.Join(cwd, "test.lua");
//script.DoFile("test.lua", codeFriendlyName: friendlyPath);
script.DoString(File.ReadAllText(friendlyPath));

debugger.Detach(script);
debugger.Dispose();
