using MoonSharp.Interpreter;
using MoonSharp.VsCodeDebugger;

var debugger = new MoonSharpVsCodeDebugServer();
debugger.Start();

var script = new Script();
script.Options.DebugPrint = s => Console.WriteLine(s);
script.Globals["sleep"] = (Action<int>) Thread.Sleep;

var cwd = AppDomain.CurrentDomain.BaseDirectory;
var friendlyPath = Path.Join(cwd, "test.lua");
var main = script.LoadFile("test.lua", friendlyFilename: friendlyPath);

debugger.AttachToScript(script, "test");
script.Call(main);

debugger.Detach(script);
debugger.Dispose();
