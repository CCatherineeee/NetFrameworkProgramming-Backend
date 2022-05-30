using COSXML;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Model.Bucket;
using COSXML.Model.Object;
using COSXML.Transfer;
using COSXML.Utils;
using HandyShare.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.OssHandler
{
    public class BucketClient : IBucketClient
    {
        private readonly CosXmlServer _cosXml;
        private readonly string _buketName;
        private readonly string _appid;
        public BucketClient(CosXmlServer cosXml, string buketName, string appid)
        {
            _cosXml = cosXml;
            _buketName = buketName;
            _appid = appid;
        }
        public async Task<IResponse> UpFile(string key, byte[] data)
        {
            try
            {
                string bucket = _buketName + "-" + _appid; //存储桶名称 格式：BucketName-APPID
                PutObjectRequest request = new PutObjectRequest(bucket, key, data);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    // Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = await Task.FromResult(_cosXml.PutObject(request));

                return new IResponse(200, null,  result.GetResultInfo() );
            }
            catch (CosClientException clientEx)
            {
                return new IResponse(500, null ,"CosClientException: " + clientEx.Message );
            }
            catch (CosServerException serverEx)
            {
                return new IResponse(500, null, "CosServerException: " + serverEx.GetInfo());

            }
        }
        /// <summary>
        /// 上传大文件、分块上传
        /// </summary>
        /// <param name="key"></param>
        /// <param name="srcPath"></param>
        /// <param name="progressCallback">委托，可用于显示分块信息</param>
        /// <param name="successCallback">委托，当任务成功时回调</param>
        /// <returns></returns>
        public async Task<IResponse> UpBigFile(string key, string srcPath, Action<long, long> progressCallback, Action<CosResult> successCallback)
        {
            IResponse responseModel = new IResponse();
            string bucket = _buketName + "-" + _appid; //存储桶名称 格式：BucketName-APPID

            TransferManager transferManager = new TransferManager(_cosXml, new TransferConfig());
            COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucket, null, key);
            uploadTask.SetSrcPath(srcPath);
            uploadTask.progressCallback = delegate (long completed, long total)
            {
                progressCallback(completed, total);
                //Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };
            uploadTask.successCallback = delegate (CosResult cosResult)
            {
                COSXMLUploadTask.UploadTaskResult result = cosResult as COSXMLUploadTask.UploadTaskResult;
                successCallback(cosResult);
                responseModel.code = 200;
                responseModel.msg = result.GetResultInfo();
            };
            uploadTask.failCallback = delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    responseModel.code = 0;
                    responseModel.msg = clientEx.Message;
                }
                if (serverEx != null)
                {
                    responseModel.code = 0;
                    responseModel.msg = "CosServerException: " + serverEx.GetInfo();
                }
            };
            await Task.Run(() =>
            {
                transferManager.Upload(uploadTask);
            });
            return responseModel;
        }

        public async Task<IResponse> SelectObjectList()
        {
            try
            {
                string bucket = _buketName + "-" + _appid; //存储桶名称 格式：BucketName-APPID
                GetBucketRequest request = new GetBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                GetBucketResult result = await Task.FromResult(_cosXml.GetBucket(request));
                return new IResponse (200, null,result.GetResultInfo() );
            }
            catch (CosClientException clientEx)
            {
                return new IResponse(500, null, "CosClientException: " + clientEx.Message);
            }
            catch (CosServerException serverEx)
            {
                return new IResponse(500, null, "CosServerException: " + serverEx.GetInfo());
            }
        }
        public async Task<IResponse> DownObject(string key, string localDir, string localFileName)
        {
            try
            {
                string bucket = _buketName + "-" + _appid; //存储桶名称 格式：BucketName-APPID
                GetObjectRequest request = new GetObjectRequest(bucket, key, localDir, localFileName);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                GetObjectResult result = await Task.FromResult(_cosXml.GetObject(request));

                return new IResponse(200, null, result.GetResultInfo());
            }
            catch (CosClientException clientEx)
            {
                return new IResponse(500, null, "CosClientException: " + clientEx.Message);
            }
            catch (CosServerException serverEx)
            {
                return new IResponse(500, null, "CosServerException: " + serverEx.GetInfo());
            }
        }
        public async Task<IResponse> DeleteObject(string buketName)
        {
            try
            {
                string bucket = _buketName + "-" + _appid; //存储桶名称 格式：BucketName-APPID
                string key = "exampleobject"; //对象在存储桶中的位置，即称对象键.
                DeleteObjectRequest request = new DeleteObjectRequest(bucket, key);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                DeleteObjectResult result = await Task.FromResult(_cosXml.DeleteObject(request));

                return new IResponse(200, null, result.GetResultInfo());
            }
            catch (CosClientException clientEx)
            {
                return new IResponse(500, null, "CosClientException: " + clientEx.Message);
            }
            catch (CosServerException serverEx)
            {
                return new IResponse(500, null, "CosServerException: " + serverEx.GetInfo());
            }
        }
    }
}
