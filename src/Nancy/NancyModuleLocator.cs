﻿namespace Nancy
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;

    public interface INancyModuleLocator
    {
        IEnumerable<NancyModule> GetModules();
    }

    public class NancyModuleLocator : INancyModuleLocator
    {
        private readonly Assembly assembly;

        public NancyModuleLocator(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("locator", "The locator parameter cannot be null.");
            }

            this.assembly = assembly;
        }

        public IEnumerable<NancyModule> GetModules()
        {
            return LocateNancyModules(this.assembly);
        }

        private static IEnumerable<NancyModule> LocateNancyModules(Assembly assembly)
        {
            var catalog =
                new AssemblyCatalog(assembly);

            var container =
                new CompositionContainer(catalog);

            return container.GetExportedValues<NancyModule>();
        }
    }
}