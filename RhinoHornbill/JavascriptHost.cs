using NiL.JS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoHornbill
{
    public class JavascriptHost
    {
        private readonly Module module = null;
        public JavascriptHost(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) throw new Exception("filename is not valid");
            if (!File.Exists(filename)) throw new Exception($"no such file named {filename}");
            try
            {
                var code = File.ReadAllText(filename);
                module = new Module(filename, code);

                module.ModuleResolversChain.Add(new ModuleResolver());
            }
            catch (Exception e)
            {
                Console.WriteLine("Construct error: " + e);
            }
        }

        public void Run()
        {
            try
            {
                module.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine("Runtime error: " + e);
            }
        }
    }

    public sealed class ModuleResolver : CachedModuleResolverBase
    {
        public override bool TryGetModule(ModuleRequest moduleRequest, out Module result)
        {
            if (File.Exists(moduleRequest.AbsolutePath))
            {
                var code = File.ReadAllText(moduleRequest.AbsolutePath);
                result = new Module(moduleRequest.AbsolutePath, code);
                return true;
            }
            else
            {
                if (moduleRequest.AbsolutePath.EndsWith("Rhino.js"))
                {
                    var assembly = typeof(ModuleResolver).Assembly;
                    using (var stream = assembly.GetManifestResourceStream("RhinoHornbill.Rhino.js"))
                    using (var reader = new StreamReader(stream))
                    {
                        var code = reader.ReadToEnd();
                        var module = new Module(moduleRequest.AbsolutePath, code);
                        var namespaceProvider = new NamespaceProvider("Rhino");
                        module.Context.DefineVariable("Rhino").Assign(namespaceProvider);
                        result = module;
                        return true;
                    }
                }
            }

            result = null;
            return false;
        }
    }
}
