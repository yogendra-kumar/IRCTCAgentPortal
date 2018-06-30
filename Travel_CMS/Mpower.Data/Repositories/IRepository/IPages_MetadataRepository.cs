using System;
using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IPages_MetadataRepository
    {
        /// <summary>
        /// Delete the Metadata by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Boolean Value</returns>
        bool DeleteById(Int64 Id,Int64 pageId);

        /// <summary>
        /// Insert Metadata
        /// </summary>
        /// <param name="pages_Metadata">Object of Metadata</param>
        /// <returns>Boolean Value</returns>
        bool Insert(Pages_Metadata pages_Metadata,Int64 pageId);


        /// <summary>
        /// Find Page Metadata by Id
        /// </summary>
        /// <param name="Id">Int64 Type</param>
        /// <returns>Metadata</returns>
        Pages_Metadata FindById(Int64 Id);


        /// <summary>
        /// Get List of Page Metadata
        /// </summary>
        /// <returns>IEnumerable Type Data of Pages Metadata Type</returns>
        IEnumerable<Pages_Metadata> GetList();


        /// <summary>
        /// Update the PagesMetadata
        /// </summary>
        /// <param name="pages_Metadata">Object of Pages Metadata</param>
        /// <returns>Application</returns>
        Pages_Metadata Update(Pages_Metadata pages_Metadata);
    }
}