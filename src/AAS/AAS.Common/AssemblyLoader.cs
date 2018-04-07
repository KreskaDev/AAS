using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AAS.Common
{
    public class AssemblyLoader
    {
        private readonly string _getDirectoryName;

        public AssemblyLoader(string getDirectoryName)
        {
            _getDirectoryName = getDirectoryName;
        }

        public void LoadAssemblies() // Should return result<>
        {
            //Load all assembiles into domain for full generic autofac type registration
            foreach (var filePath in Directory.GetFiles(_getDirectoryName, "*.dll"))
            {
                AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
            }
        }

        public Assembly[] GetAppDomainAssemblyByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith(name))
                .ToArray();
        }

        public IEnumerable<Type> GetAquariumTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("Aquarium"))
                .SelectMany(x => x.GetTypes());
        }
    }
}