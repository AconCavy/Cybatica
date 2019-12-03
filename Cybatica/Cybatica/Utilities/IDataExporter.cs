using Cybatica.Empatica;
using Cybatica.Models;

namespace Cybatica.Utilities
{
    public interface IDataExporter
    {
        void Export(string path, EmpaticaSession empaticaSession, OcsSession ocsSession);
    }
}