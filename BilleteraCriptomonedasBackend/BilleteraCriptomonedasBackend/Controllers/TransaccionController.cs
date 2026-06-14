using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BILLETERACRIPTOMONEDASBACKEND.Data;
using BILLETERACRIPTOMONEDASBACKEND.Models;
using BILLETERACRIPTOMONEDASBACKEND.DTOS;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace BILLETERACRIPTOMONEDASBACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;

        public TransaccionesController(
            AppDbContext context,
            HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransaccionDTO request)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.Action.ToLower() != "purchase" && request.Action.ToLower() != "sale")
            {
                return BadRequest(new { error = "La acción debe ser 'purchase' o 'sale'" });
            }

    
            if (!System.DateTime.TryParseExact(request.DateTime, "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out DateTime transactionDateTime))
            {
                return BadRequest(new { error = "El formato de fecha debe ser 'yyyy-MM-dd HH:mm'" });
            }

            try
            {
                string claveMonedaCryptoYa = "";
                string claveMoneda = request.CryptoCode;
                if(claveMoneda == "bitcoin")
                {
                    claveMonedaCryptoYa = "btc";
                }
                else if(claveMoneda == "ethereum")
                {
                    claveMonedaCryptoYa = "eth";
                }
                else{
                    claveMonedaCryptoYa = "usdt";
                }

                string urlApi = $"https://criptoya.com/api/{claveMonedaCryptoYa}/ars";
                
            
                var response = await _httpClient.GetAsync(urlApi);
                
            
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode(400, new { error = $"No se encontró la criptomoneda: {request.CryptoCode}" });
                }
                
                var contenidoJson = await response.Content.ReadAsStringAsync();
                
                
                dynamic datosCripto = JsonConvert.DeserializeObject(contenidoJson);
                
                
                decimal precioCripto = datosCripto["binance"]["totalAsk"];
                
                
                decimal montoTotal = request.CryptoAmount * precioCripto;
                
                
                var transaccion = new Transaccion
                {
                    CryptoCode = request.CryptoCode.ToLower(),
                    Action = request.Action.ToLower(),
                    ClienteId = request.ClienteId,
                    CryptoAmount = request.CryptoAmount,
                    MoneySpent = montoTotal,
                    TransactionDateTime = transactionDateTime
                };

                _context.Transacciones.Add(transaccion);
                
            
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    id = transaccion.Id,
                    message = "Transacción registrada exitosamente",
                    clienteId = transaccion.ClienteId,
                    cryptoCode = transaccion.CryptoCode,
                    cryptoPrice = precioCripto,
                    cryptoAmount = transaccion.CryptoAmount,
                    totalMoneySpent = transaccion.MoneySpent
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al procesar la transacción: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTransacciones()
        {
            var transacciones = await _context.Transacciones.ToListAsync();
            return Ok(transacciones);
        }
    }
}