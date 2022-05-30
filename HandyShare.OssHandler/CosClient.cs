using COSXML;
using COSXML.CosException;
using COSXML.Model.Bucket;
using COSXML.Model.Service;
using COSXML.Utils;
using HandyShare.Response;
using System;
using System.Threading.Tasks;

namespace HandyShare.OssHandler
{
    public class CosClient : ICosClient
    {
        CosXmlServer _cosXml;
        private readonly string _appid;
        private readonly string _region;
        public CosClient(CosXmlServer cosXml,string appid)
        {
            _cosXml = cosXml;
            _appid = appid;
        }

        public async Task<IResponse> CreateBucket(string buketName)
        {
            try
            {
                string bucket = buketName + "-" + _appid;
                PutBucketRequest request = new PutBucketRequest(bucket);
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), 600);
                //执行请求
                PutBucketResult result = await Task.FromResult(_cosXml.PutBucket(request));

                return new IResponse ( 200,null, result.GetResultInfo() );
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine();
                return new IResponse(500, null, "CosClientException: " + clientEx.Message);

            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                return new IResponse(500, null, "CosClientException: " + "CosServerException: " + serverEx.GetInfo());
            }
        }

        public async Task<IResponse> SelectBucket(int tokenTome = 600)
        {
            try
            {
                GetServiceRequest request = new GetServiceRequest();
                //设置签名有效时长
                request.SetSign(TimeUtils.GetCurrentTime(TimeUnit.SECONDS), tokenTome);
                //执行请求
                GetServiceResult result = await Task.FromResult(_cosXml.GetService(request));
                return new IResponse(200, null, result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                return new IResponse(500, null, "CosClientException: " + clientEx.Message);
            }
            catch (CosServerException serverEx)
            {
                return new IResponse(500, null, "CosClientException: " + "CosServerException: " + serverEx.GetInfo());
            }
        }
    }
}
