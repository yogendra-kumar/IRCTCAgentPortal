using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Models;
using Mpower.Data.Repository;
using Mpower.CMS.Api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("PageBlocks")]
    public class PageBlocksController : Controller
    {
        private IApplication_PagePansRepository _page_BlockRepository;
        public PageBlocksController(IApplication_PagePansRepository pageBlockRepository)
        {
            _page_BlockRepository=pageBlockRepository;
        }

        [HttpGetAttribute]
        [RouteAttribute("GetList")]
        public IActionResult GetList()
        {
            IEnumerable<Application_PagePans> page_Blocks = _page_BlockRepository.GetList();
            if(page_Blocks==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=page_Blocks });
        }

        [HttpGetAttribute]
        [RouteAttribute("GetListByApplicationId/{id}")]
        public IActionResult GetListByApplicationId(long id)
        {
            IEnumerable<Application_PagePans> page_Blocks = _page_BlockRepository.GetListByApplicationId(id);
            if(page_Blocks==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=page_Blocks });
        }

        [HttpPostAttribute]
        [RouteAttribute("Insert")]
        public IActionResult Insert([FromBodyAttribute]Application_PagePans page_Blocks)
        {
            if(!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            if(_page_BlockRepository.Insert(page_Blocks))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success",ResponseResult=page_Blocks });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });            
        }

        [HttpGetAttribute]
        [RouteAttribute("FindById/{id}")]
        public IActionResult FindById(long id)
        {
            Application_PagePans page_Blocks = _page_BlockRepository.FindById(id);
            if(page_Blocks==null)
            {
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=page_Blocks });
        }

        [HttpGetAttribute]
        [RouteAttribute("DeleteById/{id}")]
        public IActionResult DeleteById(long id)
        {
            if (_page_BlockRepository.DeleteById(id))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });
        }

        [HttpPostAttribute]
        [RouteAttribute("Update")]
        public IActionResult Update([FromBodyAttribute]Application_PagePans _page_Blocks)
        {
            if(!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            Application_PagePans page_Blocks=_page_BlockRepository.Update(_page_Blocks);
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "information saved", Status = "success",ResponseResult=page_Blocks });
        }

    }
}
