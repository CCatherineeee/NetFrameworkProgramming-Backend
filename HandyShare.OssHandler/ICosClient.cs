using COSXML.Model;
using HandyShare.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.OssHandler
{
    public interface ICosClient
    {
        // 建立存储桶
        Task<IResponse> CreateBucket(string buketName);

        // 获取存储桶列表
        Task<IResponse> SelectBucket(int tokenTome = 600);
    }
    
}
