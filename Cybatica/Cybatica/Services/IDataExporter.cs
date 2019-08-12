using Cybatica.Empatica;
using Cybatica.Models;

namespace Cybatica.Services
{
    public interface IDataExporter
    {
        void Export(string path, EmpaticaSession empaticaSession, OcsSession ocsSession);
    }
}