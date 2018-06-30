using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Mpower.Data.Models;
using Mpower.CMS.Api.Models;
using Mpower.Data.Repository;
using Mpower.Data;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using System.Text;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("ApplicationFiles")]
    public class ApplicationFilesController : Controller
    {
        private IApplication_FilesRepository _applicationFilesRepository;
        private ApiSetting _apiSetting;
        private IHostingEnvironment _hostingEnviroment;
        private string ImageFileLocation { get { return _apiSetting.FileServerFolder + "/" + _apiSetting.ImageFilePath; } }
        private string DocFileLocation { get { return _apiSetting.FileServerFolder + "/" + _apiSetting.DocFilePath; } }

        public ApplicationFilesController(IApplication_FilesRepository applicationFilesRepository, IOptions<ApiSetting> setting, IHostingEnvironment hostingEnvironement)
        {
            _applicationFilesRepository = applicationFilesRepository;
            _apiSetting = setting.Value;
            _hostingEnviroment = hostingEnvironement;
        }

        [HttpGetAttribute]
        [RouteAttribute("FindById/{id}")]
        public IActionResult FindById(long id)
        {
            Application_Files application_Files = _applicationFilesRepository.FindById(id);
            if (application_Files == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = application_Files });
        }

        [HttpGetAttribute]
        [RouteAttribute("GetList")]
        public IActionResult GetList()
        {
            IEnumerable<Application_Files> application_Files = _applicationFilesRepository.GetList();
            if (application_Files == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = application_Files });
        }

        [HttpGetAttribute]
        [RouteAttribute("GetListByApplicationId/{id}")]
        public IActionResult GetListByApplicationId(long id)
        {
            IEnumerable<Application_Files> application_Files = _applicationFilesRepository.GetListByApplicationId(id);
            if (application_Files == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = application_Files });
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult SaveFile(IFormFile file, long applicationID)
        {
            if (file == null || file.Length == 0)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1003", ResponseMessage = "Invalid file", Status = "failed" });
            }
            try
            {
                CheckFileDirectory();
                StringBuilder directoryPath = new StringBuilder();
                directoryPath.Append((file.ContentType.Contains("image/")?ImageFileLocation:DocFileLocation));
               
                string systemFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "_" + Path.GetExtension(file.FileName);
                
                directoryPath.Append("/" + systemFileName);
                using (FileStream fs = System.IO.File.Create(directoryPath.ToString()))
                {
                    //file.CopyToAsync(fs);
                    file.CopyTo(fs);
                    fs.Flush();
                }

                Application_Files application_Files = new Application_Files { applicationID = applicationID, actualFileName = file.FileName, systemFileName = systemFileName, attachmentUrl = directoryPath.ToString(), createdDate = DateTime.Now };
                application_Files = _applicationFilesRepository.Insert(application_Files);

                if (application_Files != null)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = application_Files });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = ex.ToString(), Status = "failed" });
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateFile(IFormFile file, long id)
        {
            if (file == null || file.Length == 0)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1003", ResponseMessage = "Invalid file.", Status = "failed" });
            }
            try
            {
                //--get existing file detail by file id.
                Application_Files _file = _applicationFilesRepository.FindById(id);
                if (_file == null)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1006", ResponseMessage = "File not found.", Status = "failed" });
                }
                                
                System.IO.File.Delete(_hostingEnviroment.ContentRootPath + "/" + _file.attachmentUrl);
                
                if (!System.IO.File.Exists(_hostingEnviroment.ContentRootPath + "/" + _file.attachmentUrl))
                {
                    CheckFileDirectory();
                    //----------------------------------------
                    StringBuilder directoryPath = new StringBuilder();
                    directoryPath.Append((file.ContentType.Contains("image/")?ImageFileLocation:DocFileLocation));

                    string systemFileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + "_" + Path.GetExtension(file.FileName);                    
                    directoryPath.Append("/" + systemFileName);
                    using (FileStream fs = System.IO.File.Create(directoryPath.ToString()))
                    {
                        //file.CopyToAsync(fs);
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    _file.actualFileName = file.FileName;
                    _file.systemFileName = systemFileName;
                    _file.attachmentUrl = directoryPath.ToString();
                    _file.updatedDate = DateTime.Now;

                    _applicationFilesRepository.Update(_file);

                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = _file });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1007", ResponseMessage = "File not updated", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = ex.ToString(), Status = "failed" });
            }
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteFile(long id)
        {
            if (id == 0)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1003", ResponseMessage = "Invalid file.", Status = "failed" });
            }
            try
            {
                string directoryPath = DocFileLocation;
                //--get existing file detail by file id.
                Application_Files _file = _applicationFilesRepository.FindById(id);
                if (_file == null)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1006", ResponseMessage = "File not found.", Status = "failed" });
                }

                System.IO.File.Delete(_hostingEnviroment.ContentRootPath + "/" + directoryPath + "/" + _file.systemFileName);
                if (!System.IO.File.Exists(_hostingEnviroment.ContentRootPath + "/" + directoryPath + "/" + _file.systemFileName))
                {
                    _applicationFilesRepository.DeleteById(id);
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "File deleted", Status = "success", ResponseResult = _file });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1008", ResponseMessage = "File not deleted", Status = "failed" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = ex.ToString(), Status = "failed" });
            }
        }

        private void CheckFileDirectory()
        {
            //string imageDirectoryPath = ImageFileLocation;
            DirectoryInfo imageDirectory = new DirectoryInfo(_hostingEnviroment.ContentRootPath + "/" + ImageFileLocation);
            if (!imageDirectory.Exists)
            {
                Directory.CreateDirectory(_hostingEnviroment.ContentRootPath + "/" + ImageFileLocation);
            }

            //string fileDirectoryPath = DocFileLocation;
            DirectoryInfo fileDirectory = new DirectoryInfo(_hostingEnviroment.ContentRootPath + "/" + DocFileLocation);
            if (!fileDirectory.Exists)
            {
                Directory.CreateDirectory(_hostingEnviroment.ContentRootPath + "/" + DocFileLocation);
            }
        }
    }
}
