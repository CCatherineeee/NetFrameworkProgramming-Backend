using COSXML;
using COSXML.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.OssHandler
{
    /// <summary>
    /// 生成Cos客户端工具类
    /// </summary>
    public class CosBuilder
    {
        private CosXmlServer cosXml;
        private string _appid;
        private string _region;
        private CosXmlConfig cosXmlConfig;
        private QCloudCredentialProvider cosCredentialProvider;
        public CosBuilder()
        {

        }


        public CosBuilder SetAccount(string appid, string region)
        {
            _appid = appid;
            _region = region;
            return this;
        }
        public CosBuilder SetCosXmlServer(int ConnectionTimeoutMs = 60000, int ReadWriteTimeoutMs = 40000, bool IsHttps = true, bool SetDebugLog = true)
        {
            cosXmlConfig = new CosXmlConfig.Builder()
                .SetConnectionTimeoutMs(ConnectionTimeoutMs)
                .SetReadWriteTimeoutMs(ReadWriteTimeoutMs)
                .IsHttps(true)
                .SetAppid(_appid)
                .SetRegion(_region)
                .SetDebugLog(true)
                .Build();
            return this;
        }
        public CosBuilder SetSecret(string secretId, string secretKey, long durationSecond = 600)
        {

            cosCredentialProvider = new DefaultQCloudCredentialProvider(secretId, secretKey, durationSecond);
            return this;
        }
        public CosXmlServer Builder()
        {
            //初始化 CosXmlServer
            cosXml = new CosXmlServer(cosXmlConfig, cosCredentialProvider);
            return cosXml;
        }
    }
}
