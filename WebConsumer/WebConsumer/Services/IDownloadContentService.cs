using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebConsumer.Services
{
    public interface IDownloadContentService
    {
        Task<Stream> GetUrlContentStreamAsync(string url, CancellationToken cancellationToken);
    }
}
