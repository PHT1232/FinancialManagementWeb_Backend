using EntityFramework.DbEntities.ReceiptComponents;
using EntityFramework.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TeamManagementProject_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/receipt")]
    public class ReceiptController : ControllerBase
    {
        private readonly IDataRepository<Receipt> _receiptRepository;

        public ReceiptController(IDataRepository<Receipt> receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Receipt> receipts = await _receiptRepository.GetAll();
            return Ok(receipts);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Receipt receipt)
        {
            if (receipt == null)
            {
                return BadRequest("Employee is null.");
            }
            _receiptRepository.Add(receipt);
            return CreatedAtRoute(
                  "Get",
                  new { receipt.Id },
                  receipt);
        }
    }
}
