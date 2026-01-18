using Azure;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivial.servicios
{
    class AzureService
    {
        private const string cadenaConexion = "DefaultEndpointsProtocol=https;AccountName=ainaraserna;AccountKey=1VMAaxrqWVOktpJu2OZK/iAfWpiAwK4jkPg81gMlZsmiOWV5Qm1jZ5hMeDitdn0xr5aZX/xb1bX7+ASt9krx1A==;EndpointSuffix=core.windows.net";
        private const string nombreContenedorBlobs = "2dam";

        public static string GetImagenAzure(string ruta)
        {
            var clienteBlobService = new BlobServiceClient(cadenaConexion);
            var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorBlobs);

            Stream streamImagen = File.OpenRead(ruta);
            string nombreImagen = Path.GetFileName(ruta);
            clienteContenedor.UploadBlob(nombreImagen, streamImagen);

            var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
            return clienteBlobImagen.Uri.AbsoluteUri;
        }

        public static async void DeleteImagenAzure(string url)
        {
            var clienteBlobService = new BlobServiceClient(cadenaConexion);
            var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorBlobs);

            string fichero = Path.GetFileName(url);
            var clienteBlobImagen = clienteContenedor.GetBlobClient(fichero);
            await clienteBlobImagen.DeleteAsync();
        }
    }
}
