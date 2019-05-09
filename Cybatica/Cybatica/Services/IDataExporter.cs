using Cybatica.Empatica;

namespace Cybatica.Services
{
    public interface IDataExporter
    {
        void Export(string path, EmpaticaSession empaticaSession);

    }
}
