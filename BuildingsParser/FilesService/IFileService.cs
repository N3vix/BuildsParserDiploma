namespace BuildingsParser.FilesService
{
    public interface ISerialization<TModel> where TModel : class
    {
        //bool GetModel(string FilePath);
        //bool CreateModel(string FilePath);

        bool GetModel(string Path);
        bool CreateModel(string Path);
    }
}