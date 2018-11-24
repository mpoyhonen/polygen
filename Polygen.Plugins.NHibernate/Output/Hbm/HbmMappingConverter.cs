using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Plugins.Base.Output.Xsd;

namespace Polygen.Plugins.NHibernate.Output.Hbm
{
    /// <summary>
    /// Converts NHibernate mapping output model to HBM file XML structure.
    /// </summary>
    public class HbmMappingConverter
    {
        private static readonly XNamespace HbmNamespace = "urn:nhibernate-mapping-2.2";

        public XsdOutputModel Convert(IProject project, INamespace ns, IEnumerable<Base.Models.Entity.Entity> entities)
        {
            var rootXmlElement = new XElement(HbmNamespace + "mapping",
                new XAttribute("assembly", project.Name), // TODO: Use assembly name
                new XAttribute("namespace", ns.Name),
                new XAttribute("auto-import", "false")
            );

            foreach (var entity in entities)
            {
                rootXmlElement.Add(new XComment($"Generated from entity model '{ns.Name}'"));
                rootXmlElement.Add(Convert(entity));
            }

            return new XsdOutputModel(rootXmlElement);
        }

        private XElement Convert(Base.Models.Entity.Entity entity)
        {
            var entityXmlElement = new XElement("class",
                new XAttribute("name", entity.Name),
                new XAttribute("table", entity.Name) // TODO: Convert to DB name
            );

            foreach (var attribute in entity.Attributes)
            {
                entityXmlElement.Add(Convert(entity, attribute));
            }

            return entityXmlElement;
        }
        
        private XElement Convert(Base.Models.Entity.Entity entity, IClassAttribute attribute)
        {
            return new XElement("property",
                new XAttribute("name", attribute.Name),
                new XAttribute("column", attribute.Name) // TODO: Convert to DB name
            );
        }
    }
}
