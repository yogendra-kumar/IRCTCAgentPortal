using System;
using Microsoft.AspNetCore.Mvc;

namespace Mpower.Rail.Api
{
    [Route("Mpower/Rail/Journey/Plan")]
    public class PlanController:Controller
    {
        public PlanController()
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