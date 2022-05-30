using COSXML.Model;
using HandyShare.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.OssHandler
{
    public interface IBucketClient
    {
        // 上传文件
        Task<IResponse> UpFile(string key, byte[] srcPath);

        // 分块上传大文件
        Task<IResponse> UpBigFile(string key, string srcPath, Action<long, long> progressCallback, Action<CosResult> successCallback);

        // 查询存储桶的文件列表
        Task<IResponse> SelectObjectList();

        // 下载文件
        Task<IResponse> DownObject(string key, string localDir, string localFileName);

        // 删除文件
        Task<IResponse> DeleteObject(string buketName);
    }
}
