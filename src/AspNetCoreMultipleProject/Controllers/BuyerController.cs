using AspNetCoreMultipleProject.Commands;
using AspNetCoreMultipleProject.Queries;
using AspNetCoreMultipleProject.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Controllers
{
    [Route("e-auction/api/v1/buyer/[controller]")]
    public class BuyerController : Controller
    {
        private readonly IMediator _mediator;
      

        public BuyerController(IMediator mediator)
        {
           
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/getAllBuyer")]
        public async Task<IActionResult> GetAllBuyer()
        {
            
            try
            {
                return Ok(await _mediator.Send(new GetAllBuyersQuery()));
            }
            catch(Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        [Route("/place-bid")]
        public async Task<IActionResult> PlaceBid([FromBody] BuyerInfoVM value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                
                var result = await _mediator.Send(new AddBuyerCommand(value));
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(
             detail: ex.StackTrace,
             title: ex.Message);
            }
        }


        [HttpPost("/update-bid")]
     
        public async Task<IActionResult> UpdateBid([FromBody] BuyerInfoVM value)
        {
            try
            {
                if (value.ProductId == 0)
                {
                    return BadRequest();
                }
                
                await _mediator.Send(new UpdateBidCommand(value.ProductId, value.Email, value.BidAmount));
                return Ok();
            }
            catch (Exception ex)
            {

                return Problem(
             detail: ex.StackTrace,
             title: ex.Message);
            }
           
        }

       
    }
}
