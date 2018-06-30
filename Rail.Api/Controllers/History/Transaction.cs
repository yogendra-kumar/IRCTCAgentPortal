using System;
using Microsoft.AspNetCore.Mvc;

namespace Mpower.Rail.Api
{
    [Route("Mpower/Rail/History/Transaction")]
    public class TransactionController:Controller
    {
        public TransactionController()
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