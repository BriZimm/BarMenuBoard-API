using System;
using System.Net;
using System.Threading.Tasks;
using BarMenuBoardAPI.Attributes;
using BarMenuBoardAPI.Configuration;
using BarMenuBoardAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Swashbuckle.AspNetCore.Annotations;

namespace BarMenuBoardAPI.Controllers
{
    [Route("api")]
    [ApiController]
    [SwaggerGroup("Files")]
    public class FileController : Controller
    {
        public FileController(IOptions<RecipeImageConfiguration> recipeImageConfiguration,
            IOptions<BoardStyleImageConfiguration> boardStyleimageConfiguration)
        {
            this.recipeImageConfiguration = recipeImageConfiguration;
            this.boardStyleimageConfiguration = boardStyleimageConfiguration;
        }

        private readonly IOptions<RecipeImageConfiguration> recipeImageConfiguration;
        private readonly IOptions<BoardStyleImageConfiguration> boardStyleimageConfiguration;

        [HttpPost("recipe-image", Name = "Upload Recipe Image")]
        [SwaggerOperation(OperationId = "Upload Recipe Image")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UploadRecipeImage([FromForm]UploadFileContainer fileToUpload)
        {
            try
            {
                if (CloudStorageAccount.TryParse(recipeImageConfiguration.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(recipeImageConfiguration.Value.Container);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileToUpload.File.FileName);
                    await blockBlob.UploadFromStreamAsync(fileToUpload.File.OpenReadStream());

                    return Ok();
                }
                else
                {
                    throw new Exception("There was an issue uploading an image");
                }
            }
            catch
            {
                throw new Exception("There was an issue uploading an image");
            }
        }

        [Route("recipe-image/delete/{fileName}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteRecipeImage(string fileName)
        {
            try
            {
                if (CloudStorageAccount.TryParse(recipeImageConfiguration.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = BlobClient.GetContainerReference(recipeImageConfiguration.Value.Container);

                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(fileName);

                        if (await file.ExistsAsync())
                        {
                            await file.DeleteAsync();
                        }
                    }
                }
                return Ok();
            }
            catch
            {
                throw new Exception("There was an issue deleting an image");
            }
        }

        [HttpPost("board-style-image", Name = "Upload Board Style Image")]
        [SwaggerOperation(OperationId = "Upload Board Style Image")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> UploadBoardStyleImage([FromForm]UploadFileContainer fileToUpload)
        {
            try
            {
                if (CloudStorageAccount.TryParse(boardStyleimageConfiguration.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(boardStyleimageConfiguration.Value.Container);

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileToUpload.File.FileName);
                    await blockBlob.UploadFromStreamAsync(fileToUpload.File.OpenReadStream());

                    return Ok();
                }
                else
                {
                    throw new Exception("There was an issue uploading an image");
                }
            }
            catch
            {
                throw new Exception("There was an issue uploading an image");
            }
        }

        [Route("board-style-image/delete/{fileName}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBoardStyleImage(string fileName)
        {
            try
            {
                if (CloudStorageAccount.TryParse(boardStyleimageConfiguration.Value.StorageConnection, out CloudStorageAccount storageAccount))
                {
                    CloudBlobClient BlobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = BlobClient.GetContainerReference(boardStyleimageConfiguration.Value.Container);

                    if (await container.ExistsAsync())
                    {
                        CloudBlob file = container.GetBlobReference(fileName);

                        if (await file.ExistsAsync())
                        {
                            await file.DeleteAsync();
                        }
                    }
                }
                return Ok();
            }
            catch
            {
                throw new Exception("There was an issue deleting an image");
            }
        }
    }
}