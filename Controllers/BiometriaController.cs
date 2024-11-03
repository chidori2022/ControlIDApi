using Microsoft.AspNetCore.Mvc;
using ControlIDApi.Services;
using ControliD;

namespace ControlIDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiometriaController : ControllerBase
    {
        private readonly BiometriaService _biometriaService;

        public BiometriaController()
        {
            _biometriaService = new BiometriaService();
        }

        // Endpoint para inicializar o leitor biométrico
        [HttpGet("inicializar")]
        public IActionResult InicializarLeitor()
        {
            var resultado = _biometriaService.InicializarLeitor();
            if (resultado == RetCode.SUCCESS)
                return Ok("Leitor inicializado com sucesso.");
            return StatusCode(500, $"Erro ao inicializar o leitor: {CIDBio.GetErrorMessage(resultado)}");
        }

        // Endpoint para finalizar o leitor biométrico
        [HttpGet("finalizar")]
        public IActionResult FinalizarLeitor()
        {
            _biometriaService.FinalizarLeitor();
            return Ok("Leitor finalizado com sucesso.");
        }

        // Endpoint para capturar a imagem de uma digital
        [HttpGet("capturar")]
        public IActionResult CapturarImagem()
        {
            var resultado = _biometriaService.CapturarImagem(out byte[] imageBuf, out uint width, out uint height);

            if (resultado == RetCode.SUCCESS)
            {
                // Converte a imagem para base64 para facilitar o envio na resposta JSON
                string imagemBase64 = Convert.ToBase64String(imageBuf);
                return Ok(new { largura = width, altura = height, imagem = imagemBase64 });
            }
            return StatusCode(500, $"Erro ao capturar a imagem: {CIDBio.GetErrorMessage(resultado)}");
        }

        [HttpGet("identificar")]
        public IActionResult IdentificarDigital()
        {
            var resultado = _biometriaService.IdentificarDigital(out long id, out int score, out int quality);

            if (resultado == RetCode.SUCCESS)
            {
                return Ok(new { id, score, qualidade = quality });
            }
            return StatusCode(500, $"Erro ao identificar a digital: {CIDBio.GetErrorMessage(resultado)}");
        }
    }
}
