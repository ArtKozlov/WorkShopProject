
using System.Configuration;

namespace DAL.ElasticSearch
{
    public class ElasticSearchConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("directories")]
        public PathCollection GetPathItems
        {
            get { return (PathCollection)base["directories"]; }
        }
    }

    [ConfigurationCollection(typeof(ElasticSearchHostPath))]
    public class PathCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ElasticSearchHostPath();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ElasticSearchHostPath)element).Path;
        }
        public ElasticSearchHostPath this[int indx]
        {
            get { return (ElasticSearchHostPath)BaseGet(indx); }
        }

    }

    public class ElasticSearchHostPath : ConfigurationElement
    {
        [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }
    }

}
