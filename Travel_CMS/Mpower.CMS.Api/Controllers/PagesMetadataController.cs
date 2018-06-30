using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Models;
using Mpower.Data.Repository;
using Mpower.CMS.Api.Models;
using Mpower.Models.ApplicationViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("PagesMetadata")]
    public class PagesMetadataController : Controller
    {

       private  IPages_MetadataRepository _pages_MetadataRepository;
       private ApplicationDbContext _applicationDbContext;

       public PagesMetadataController (IPages_MetadataRepository pages_MetadataRepository, ApplicationDbContext applicationDbContext)
       {
           _applicationDbContext = applicationDbContext;
           _pages_MetadataRepository = pages_MetadataRepository;
       }

       [HttpPostAttribute]
       [RouteAttribute("Insert/{pageId}")]
       public IActionResult Insert([FromBodyAttribute]Pages_Metadata pages_Metadata, Int64 pageId)
       {
            if (_pages_MetadataRepository.Insert(pages_Metadata,pageId))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = pages_Metadata });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });
       }

       [HttpPostAttribute]
       [RouteAttribute("Update")]
       public IActionResult Update([FromBodyAttribute]Pages_Metadata _pages_Metadata)
       {
           if(!ModelState.IsValid)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "invalid model", Status = "failed" });
           }
           Pages_Metadata pages_Metadata = _pages_MetadataRepository.Update(_pages_Metadata);
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success",ResponseResult=pages_Metadata });
       }

       [HttpGetAttribute]
       [RouteAttribute("FindById/{id}")]
       public IActionResult FindById(long id,long pageId)
       {
           Pages_Metadata pages_Metadata = _pages_MetadataRepository.FindById(id);
           if(pages_Metadata==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=pages_Metadata });
       }

       [HttpGetAttribute]
       [RouteAttribute("DeleteById/{id}")]
       public IActionResult DeleteById (long id,Int64 pageId)
       {
           if(_pages_MetadataRepository.DeleteById(id,pageId))
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });
       }
    }
}