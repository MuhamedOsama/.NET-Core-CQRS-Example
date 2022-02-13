using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace awsss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IHttpClientFactory _clientFactory;


        public HomeController(IAmazonS3 s3Client, IHttpClientFactory clientFactory)
        {
            _s3Client = s3Client;
            _clientFactory = clientFactory;

        }
        // GET: api/<HomeController>
        [HttpGet]
        public async Task<ActionResult> Get(string key)
        {
            ListObjectsV2Response objects = await _s3Client.ListObjectsV2Async(new ListObjectsV2Request { BucketName = "EnsafTestingBucket" });
            var url = _s3Client.GetPreSignedURL(new GetPreSignedUrlRequest { Expires = DateTime.Today.AddDays(5), BucketName = "EnsafTestingBucket", Key = key });
            return Ok(new { url, objects });
        }
        [HttpDelete]
        public async Task<ActionResult> deleteAll()
        {
            ListObjectsV2Response objects = await _s3Client.ListObjectsV2Async(new ListObjectsV2Request { BucketName = "EnsafTestingBucket" });
            foreach (S3Object objectt in objects.S3Objects)
            {
                var properties = new Dictionary<string, object>();
                await _s3Client.DeleteAsync("EnsafTestingBucket", objectt.Key, properties);
            }
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> UploadImage(IFormFile file)
        {
            Uri Address = new Uri($"https://objectstorage.me-jeddah-1.oraclecloud.com/p/v8SGo8pcp44eNcIa2113IqJZ9dT4zUY1doSLVEklmUx8dsxZotp5YLUgiq-oXFXT/n/axrrofos01zp/b/EnsafTestingBucket/o/{file.FileName}");
            var client = _clientFactory.CreateClient();
            var content = new StreamContent(file.OpenReadStream());
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            content.Headers.ContentEncoding.Add("base64");
            HttpResponseMessage result = await client.PutAsync(Address, content);
            var responseContent = await result.Content.ReadAsStringAsync();
            return Ok();
        }

    }
}
