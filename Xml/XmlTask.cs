using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AzAlternative.Xml
{
    internal class XmlTask : XmlBaseObject, Interfaces.ITask
    {
        private const string ELEMENTNAME = "AzApplication";
        private const string GUID = "Guid";
        private const string NAME = "Name";
        private const string DESCRIPTION = "Description";
        private const string BIZRULEPATH = "";

        public Guid Guid
        {
            get
            {
                string s = GetAttribute(GUID);
                if (s == null)
                    return Guid.Empty;

                return new Guid(s);
            }
            set
            {
                SetAttribute(GUID, value.ToString());
            }
        }

        public string Name
        {
            get
            {
                return GetAttribute(NAME);
            }
            set
            {
                SetAttribute(NAME, value);
            }
        }

        public string Description
        {
            get
            {
                return GetAttribute(DESCRIPTION);
            }
            set
            {
                SetAttribute(DESCRIPTION, value);
            }
        }

        public string BizRuleImportedPath
        {
            get
            {
                return GetAttribute(BIZRULEPATH);
            }
            set
            {
                SetAttribute(BIZRULEPATH, value);
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<Task> Tasks
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<Operation> Operations
        {
            get { throw new NotImplementedException(); }
        }

        public XmlTask(XmlElement node, XmlFactory factory)
            : base(node, factory)
        { }

        public void AddTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void RemoveTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void AddOperation(Operation operation)
        {
            throw new NotImplementedException();
        }

        public void RemoveOperation(Operation operation)
        {
            throw new NotImplementedException();
        }

        public static XmlNodeList GetTasks(XmlElement parent)
        {
            return parent.SelectNodes(ELEMENTNAME);
        }

        public static XmlTask CreateTask(XmlFactory factory, string name, string description)
        {
            XmlTask t = new XmlTask(factory.CreateNew(ELEMENTNAME), factory);
            t.Name = name;
            t.Description = description;

            return t;
        }
    }
}
