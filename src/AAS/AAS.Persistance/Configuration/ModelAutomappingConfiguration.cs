using System;
using System.Diagnostics;
using AAS.Common;
using FluentNHibernate.Automapping;

namespace AAS.Persistance
{
    public class ModelAutomappingConfiguration
        : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            Debug.Print("----ShouldMap----       type:{0} map:{1}", type, type.IsAssignableTo<IEntity>());
            return type.IsAssignableTo<IEntity>();
        }

        public override bool IsDiscriminated(Type type)
        {
            Debug.Print("----IsDiscriminated---- type:{0} map:{1}", type, type.IsAssignableTo<IHierarchyRoot>());
            return type.IsAssignableTo<IHierarchyRoot>();
        }
    }
}