using System;
using Microsoft.AspNetCore.Mvc;

namespace Mpower.Rail.Api
{
    [Route("Mpower/Rail/History/Cancellation")]
    public class CancellationController:Controller
    {
        public CancellationController()
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