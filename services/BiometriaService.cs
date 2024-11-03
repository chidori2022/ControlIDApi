using ControliD;

namespace ControlIDApi.Services
{
    public class BiometriaService
    {
        private CIDBio idbio;

        public BiometriaService()
        {
            idbio = new CIDBio();
        }

        public RetCode InicializarLeitor()
        {
            return CIDBio.Init();  // Chamada direta para método estático
        }

        public void FinalizarLeitor()
        {
            CIDBio.Terminate();  // Chamada direta para método estático
        }

        public RetCode CapturarImagem(out byte[] imageBuf, out uint width, out uint height)
        {
            return idbio.CaptureImage(out imageBuf, out width, out height);  // Chamada via instância 'idbio'
        }

        public RetCode IdentificarDigital(out long id, out int score, out int quality)
        {
            return idbio.CaptureAndIdentify(out id, out score, out quality);  // Chamada via instância 'idbio'
        }

    }
}
