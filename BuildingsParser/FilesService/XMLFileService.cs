using System.IO;
using System.Xml.Serialization;

namespace BuildingsParser.FilesService
{
    public class XMLSerialization<TModel> : ISerialization<TModel> where TModel : class
    {
        public TModel _Model;
        public XMLSerialization() { }

        //public bool GetModel(string FilePath)
        //{
        //    return GetModel(FilePath, "");
        //}
        public bool GetModel(string Path)
        {
            using (FileStream fs = new FileStream(Path, FileMode.Open))
            {
                _Model = (TModel)new XmlSerializer(typeof(TModel)).Deserialize(fs);
                return true;
            }
        }

        //public bool CreateModel(string FilePath)
        //{
        //    return CreateModel(FilePath, "");
        //}
        public bool CreateModel(string Path)
        {
            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                new XmlSerializer(typeof(TModel)).Serialize(fs, _Model);
                return true;
            }
        }
    }
}