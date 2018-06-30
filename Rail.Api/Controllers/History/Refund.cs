using System;
using Microsoft.AspNetCore.Mvc;

namespace Mpower.Rail.Api
{
    [Route("Mpower/Rail/History/Refund")]
    public class RefundController:Controller
    {
        public RefundController()
        {
        }
        
        [HttpGet]
        //public IActionResult Get([FromBody] OperatorData operatorData)
        public IActionResult Get()
        {
            if(ModelState.IsValid)
            {

            }

            return Ok("Success");
        }        
    }    
}