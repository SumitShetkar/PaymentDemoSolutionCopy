using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetail>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail =await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            return paymentDetail;
        }


        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PaymentDetailID ,paymentDetail});
        }

        [HttpPut]
        public async Task<ActionResult<PaymentDetail>> PutPaymentDetail(int id ,PaymentDetail paymentDetail)
        {
            if(id != paymentDetail.PaymentDetailID)
            {
                return BadRequest();
            }
            _context.Entry(paymentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!paymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            _context.PaymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool paymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailID == id);
        }

    }
}
